using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // GENERACIÓN del Dígito Verificador (DVH / DVV).
    //
    // DVH (Dígito Verificador Horizontal): cada valor de cada columna de un
    // registro se convierte a su equivalente hexadecimal y se suma con los
    // demás valores del mismo registro (cálculo columna a columna). La suma de
    // los DVH de todos los registros da el DVH de la tabla, y la suma de los
    // DVH de todas las tablas da el DVH de la BD.
    //
    // DVV (Dígito Verificador Vertical): cada valor de cada columna se suma
    // con los valores de todos los registros de la misma columna (cálculo
    // registro a registro). La suma de los DVV de todas las columnas da el DVV
    // de la tabla, y la suma de los DVV de todas las tablas da el DVV de la BD.
    //
    // Ambos totales se persisten en la tabla especial DV (DV_54CS), junto con
    // el detalle por tabla para poder informarle al Administrador cuál tabla
    // presenta la inconsistencia.
    public class DALDigitoVerificador_54CS
    {
        private static readonly string _connectionString = "Server=.;DataBase=BDProyecto;Integrated Security=True";

        // Nombre de la tabla especial DV y de la fila que guarda el total de la BD.
        public const string TablaDV = "DV_54CS";
        public const string FilaTotalBD = "TOTAL_BD";

        // Genera el OBJETO DV en memoria (sin persistirlo). Se usa tanto en la
        // GENERACIÓN (antes de guardar) como en la REVISIÓN del login.
        public static DigitoVerificadorBD_54CS GenerarObjetoDV()
        {
            DigitoVerificadorBD_54CS objetoDV = new DigitoVerificadorBD_54CS();
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                AsegurarTablaDV(conexion);
                foreach (string tabla in ObtenerTablasVerificables(conexion))
                {
                    DataTable datos = LeerTablaCompleta(conexion, tabla);
                    DVTabla_54CS dvTabla = CalcularDVDeTabla(tabla, datos);
                    objetoDV.Tablas_54CS.Add(dvTabla);
                    objetoDV.DVHBaseDatos_54CS += dvTabla.DVH_54CS; // suma de los DVH de cada tabla
                    objetoDV.DVVBaseDatos_54CS += dvTabla.DVV_54CS; // suma de los DVV de cada tabla
                }
            }
            return objetoDV;
        }

        // GENERACIÓN completa: calcula el OBJETO DV y lo persiste en la tabla DV.
        // Se invoca automáticamente después de cada persistencia sobre la BD
        // (ver Conexion_54CS.Escribir) y desde la opción RECALCULAR EL DV.
        public static DigitoVerificadorBD_54CS RecalcularYPersistir()
        {
            DigitoVerificadorBD_54CS objetoDV = GenerarObjetoDV();
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    using (SqlCommand limpiar = new SqlCommand($"DELETE FROM {TablaDV}", conexion, transaccion))
                    {
                        limpiar.ExecuteNonQuery();
                    }
                    foreach (DVTabla_54CS dvTabla in objetoDV.Tablas_54CS)
                    {
                        InsertarFilaDV(conexion, transaccion, dvTabla.NombreTabla_54CS, dvTabla.DVH_54CS, dvTabla.DVV_54CS);
                    }
                    InsertarFilaDV(conexion, transaccion, FilaTotalBD, objetoDV.DVHBaseDatos_54CS, objetoDV.DVVBaseDatos_54CS);
                    transaccion.Commit();
                }
            }
            return objetoDV;
        }

        // REVISIÓN: lee los DV almacenados en la tabla DV mediante un SELECT.
        // Devuelve null si la tabla DV todavía no fue inicializada.
        public static DigitoVerificadorBD_54CS LeerDVPersistido()
        {
            DigitoVerificadorBD_54CS objetoDV = new DigitoVerificadorBD_54CS();
            bool tieneFilas = false;
            bool tieneTotal = false;
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                AsegurarTablaDV(conexion);
                string consulta = $"SELECT Tabla_54CS, DVH_54CS, DVV_54CS FROM {TablaDV}";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        tieneFilas = true;
                        string nombre = Convert.ToString(lector["Tabla_54CS"]).Trim();
                        long dvh = Convert.ToInt64(lector["DVH_54CS"]);
                        long dvv = Convert.ToInt64(lector["DVV_54CS"]);
                        if (nombre == FilaTotalBD)
                        {
                            objetoDV.DVHBaseDatos_54CS = dvh;
                            objetoDV.DVVBaseDatos_54CS = dvv;
                            tieneTotal = true;
                        }
                        else
                        {
                            objetoDV.Tablas_54CS.Add(new DVTabla_54CS
                            {
                                NombreTabla_54CS = nombre,
                                DVH_54CS = dvh,
                                DVV_54CS = dvv
                            });
                        }
                    }
                }
            }
            if (!tieneFilas)
            {
                return null; // la tabla DV nunca fue generada
            }
            if (!tieneTotal)
            {
                // fila de total ausente: se reconstruye a partir del detalle por tabla
                objetoDV.DVHBaseDatos_54CS = objetoDV.Tablas_54CS.Sum(t => t.DVH_54CS);
                objetoDV.DVVBaseDatos_54CS = objetoDV.Tablas_54CS.Sum(t => t.DVV_54CS);
            }
            return objetoDV;
        }

        // ------------------------- Cálculo -------------------------

        private static DVTabla_54CS CalcularDVDeTabla(string nombreTabla, DataTable datos)
        {
            // DVH: cálculo horizontal, columna a columna dentro de cada registro.
            // Cada valor se pondera por la posición de su columna dentro del
            // registro; así el DVH detecta también el intercambio de valores
            // entre columnas de un mismo registro (cosa que una suma simple no
            // detectaría) y se diferencia del cálculo vertical del DVV.
            long dvhTabla = 0;
            foreach (DataRow registro in datos.Rows)
            {
                long dvhRegistro = 0;
                for (int columna = 0; columna < datos.Columns.Count; columna++)
                {
                    dvhRegistro += CalcularValorCelda(registro[columna]) * (columna + 1);
                }
                dvhTabla += dvhRegistro; // suma de los DVH de todos los registros
            }

            // DVV: cálculo vertical, registro a registro dentro de cada columna.
            long dvvTabla = 0;
            for (int columna = 0; columna < datos.Columns.Count; columna++)
            {
                long dvvColumna = 0;
                foreach (DataRow registro in datos.Rows)
                {
                    dvvColumna += CalcularValorCelda(registro[columna]);
                }
                dvvTabla += dvvColumna; // suma de los DVV de todas las columnas
            }

            return new DVTabla_54CS
            {
                NombreTabla_54CS = nombreTabla,
                DVH_54CS = dvhTabla,
                DVV_54CS = dvvTabla
            };
        }

        // Convierte el valor de una celda a su equivalente hexadecimal y lo
        // reduce a un número: cada carácter del valor se convierte a su código
        // expresado en hexadecimal y se suma el valor numérico de ese
        // hexadecimal. Los valores nulos aportan 0.
        private static long CalcularValorCelda(object valor)
        {
            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            string texto = ConvertirATextoCanonico(valor);
            long suma = 0;
            foreach (char caracter in texto)
            {
                string hexadecimal = ((int)caracter).ToString("X"); // equivalente hexadecimal del carácter
                suma += Convert.ToInt64(hexadecimal, 16);            // valor numérico del hexadecimal
            }
            return suma;
        }

        // Representación de texto estable e independiente de la cultura del
        // equipo, para que el mismo dato produzca siempre el mismo DV.
        private static string ConvertirATextoCanonico(object valor)
        {
            if (valor is DateTime fecha)
            {
                return fecha.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            }
            if (valor is bool logico)
            {
                return logico ? "1" : "0";
            }
            if (valor is byte[] bytes)
            {
                return BitConverter.ToString(bytes);
            }
            if (valor is IFormattable formateable)
            {
                return formateable.ToString(null, CultureInfo.InvariantCulture);
            }
            return Convert.ToString(valor, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        // ------------------------- Acceso a datos -------------------------

        // Tablas de usuario de la BD sobre las que se calcula el DV. Se excluye
        // la propia tabla DV (si se incluyera, cada recálculo la modificaría y
        // el valor nunca podría coincidir) y las tablas de sistema.
        private static List<string> ObtenerTablasVerificables(SqlConnection conexion)
        {
            List<string> tablas = new List<string>();
            string consulta = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
                                WHERE TABLE_TYPE = 'BASE TABLE'
                                  AND TABLE_NAME NOT IN (@TablaDV, 'sysdiagrams')
                                ORDER BY TABLE_NAME";
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@TablaDV", TablaDV);
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        tablas.Add(lector.GetString(0));
                    }
                }
            }
            return tablas;
        }

        private static DataTable LeerTablaCompleta(SqlConnection conexion, string tabla)
        {
            using (SqlCommand comando = new SqlCommand($"SELECT * FROM [{tabla}]", conexion))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                DataTable datos = new DataTable();
                adaptador.Fill(datos);
                return datos;
            }
        }

        private static void AsegurarTablaDV(SqlConnection conexion)
        {
            string consulta = $@"IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{TablaDV}')
                                 CREATE TABLE {TablaDV} (
                                     Tabla_54CS varchar(128) NOT NULL PRIMARY KEY,
                                     DVH_54CS bigint NOT NULL,
                                     DVV_54CS bigint NOT NULL
                                 )";
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.ExecuteNonQuery();
            }
        }

        private static void InsertarFilaDV(SqlConnection conexion, SqlTransaction transaccion, string tabla, long dvh, long dvv)
        {
            string consulta = $"INSERT INTO {TablaDV} (Tabla_54CS, DVH_54CS, DVV_54CS) VALUES (@Tabla, @DVH, @DVV)";
            using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@Tabla", tabla);
                comando.Parameters.AddWithValue("@DVH", dvh);
                comando.Parameters.AddWithValue("@DVV", dvv);
                comando.ExecuteNonQuery();
            }
        }
    }
}
