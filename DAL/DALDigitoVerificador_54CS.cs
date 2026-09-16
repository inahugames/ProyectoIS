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
    public class DALDigitoVerificador_54CS
    {
        private static readonly string _connectionString = Conexion_54CS.Cadena;

        // Nombre de la tabla especial DV y de la fila que guarda el total de la BD
        public const string TablaDV = "DV_54CS";
        public const string FilaTotalBD = "TOTAL_BD";

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

        // devuelve null si la tabla DV todavia no fue inicializada
        public static DigitoVerificadorBD_54CS LeerDVPersistido()
        {
            DigitoVerificadorBD_54CS objetoDV = new DigitoVerificadorBD_54CS { TieneTotal_54CS = false };
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
                            objetoDV.TieneTotal_54CS = true;
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
                objetoDV.DVHBaseDatos_54CS = objetoDV.Tablas_54CS.Sum(t => t.DVH_54CS);
                objetoDV.DVVBaseDatos_54CS = objetoDV.Tablas_54CS.Sum(t => t.DVV_54CS);
            }
            return objetoDV;
        }

        // Sumas posicionales: el orden físico de SQL no importa, pero sí la
        // asociación de valores dentro de cada fila, sus tipos y sus posiciones.
        public static DVTabla_54CS CalcularDVDeTabla(string nombreTabla, DataTable datos)
        {
            var filas = datos.Rows.Cast<DataRow>()
                .Select(r => r.ItemArray.Select(SerializarCelda).ToArray())
                .OrderBy(r => string.Concat(r), StringComparer.Ordinal).ToList();
            string esquema = SerializarCelda(nombreTabla) +
                string.Concat(datos.Columns.Cast<DataColumn>().Select(c =>
                    SerializarCelda(c.ColumnName) + SerializarCelda(c.DataType.FullName))) +
                SerializarCelda(filas.Count);
            var horizontal = new StringBuilder(esquema);
            foreach (var fila in filas)
                foreach (string celda in fila) horizontal.Append(celda);
            var vertical = new StringBuilder(esquema);
            for (int columna = 0; columna < datos.Columns.Count; columna++)
                foreach (var fila in filas) vertical.Append(fila[columna]);
            return new DVTabla_54CS
            {
                NombreTabla_54CS = nombreTabla,
                DVH_54CS = SumarPosiciones(horizontal.ToString(), 1000000007),
                DVV_54CS = SumarPosiciones(vertical.ToString(), 1000000009)
            };
        }

        private static string SerializarCelda(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "N;";
            string texto = ConvertirATextoCanonico(valor);
            string tipo = valor.GetType().FullName;
            return "V" + tipo.Length.ToString(CultureInfo.InvariantCulture) + ":" + tipo +
                texto.Length.ToString(CultureInfo.InvariantCulture) + ":" + texto;
        }

        private static long SumarPosiciones(string texto, long modulo)
        {
            long suma = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                long posicion = (i + 1L) % modulo;
                long peso = posicion * posicion % modulo;
                suma = (suma + texto[i] * peso) % modulo;
            }
            return suma;
        }

        private static string ConvertirATextoCanonico(object valor)
        {
            if (valor is DateTime fecha)
            {
                return fecha.ToString("yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture);
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
