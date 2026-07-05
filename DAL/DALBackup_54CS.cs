using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBackup_54CS
    {
        private readonly string _masterConnectionString = "Server=.;DataBase=master;Integrated Security=True";
        private const string NombreBD = "BDProyecto";
        private const string CarpetaIntercambio = @"C:\Backup_BDProyecto";
        private const int ErrorNoSePuedeAbrirDispositivo = 3201; // "Cannot open backup device"

        // genera un backup completo (.bak) en la ruta indicada
        public void RealizarBackup(string rutaArchivo)
        {
            try
            {
                EjecutarBackup(rutaArchivo); // intento directo a la ruta elegida
            }
            catch (SqlException ex) when (ex.Number == ErrorNoSePuedeAbrirDispositivo)
            {
                // el servicio de SQL no puede escribir en la carpeta elegida:
                // se genera el backup en la carpeta de intercambio y la
                // aplicacion lo copia al destino.
                string rutaIntermedia = PrepararRutaIntermedia(rutaArchivo);
                EjecutarBackup(rutaIntermedia);
                if (!string.Equals(rutaIntermedia, rutaArchivo, StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(rutaIntermedia, rutaArchivo, true);
                    IntentarBorrar(rutaIntermedia);
                }
            }
        }
        public void RestaurarBackup(string rutaArchivo)
        {
            SqlConnection.ClearAllPools();

            try
            {
                EjecutarRestore(rutaArchivo); // intento directo desde la ruta elegida
            }
            catch (SqlException ex) when (ex.Number == ErrorNoSePuedeAbrirDispositivo)
            {
                // el servicio de SQL no puede leer el archivo desde la carpeta
                // elegida se copia a la carpeta de intercambio y se restaura
                // desde ahi
                string rutaIntermedia = PrepararRutaIntermedia(rutaArchivo);
                if (!string.Equals(rutaIntermedia, rutaArchivo, StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(rutaArchivo, rutaIntermedia, true);
                }
                try
                {
                    EjecutarRestore(rutaIntermedia);
                }
                finally
                {
                    IntentarBorrar(rutaIntermedia);
                }
            }

            // despues del restore se regenera el DV para dejar la BD en un
            // estado consistente aunque el backup fuera anterior a la
            // implementacion del Digito Verificador.
            DALDigitoVerificador_54CS.RecalcularYPersistir();
        }

        private void EjecutarBackup(string rutaArchivo)
        {
            using (SqlConnection conexion = new SqlConnection(_masterConnectionString))
            using (SqlCommand comando = new SqlCommand($"BACKUP DATABASE [{NombreBD}] TO DISK = @Ruta WITH INIT", conexion))
            {
                comando.Parameters.AddWithValue("@Ruta", rutaArchivo);
                comando.CommandTimeout = 300;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private void EjecutarRestore(string rutaArchivo)
        {
            using (SqlConnection conexion = new SqlConnection(_masterConnectionString))
            {
                conexion.Open();
                using (SqlCommand unUsuario = new SqlCommand($"ALTER DATABASE [{NombreBD}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", conexion))
                {
                    unUsuario.CommandTimeout = 300;
                    unUsuario.ExecuteNonQuery();
                }
                try
                {
                    using (SqlCommand restaurar = new SqlCommand($"RESTORE DATABASE [{NombreBD}] FROM DISK = @Ruta WITH REPLACE", conexion))
                    {
                        restaurar.Parameters.AddWithValue("@Ruta", rutaArchivo);
                        restaurar.CommandTimeout = 600;
                        restaurar.ExecuteNonQuery();
                    }
                }
                finally
                {
                    try
                    {
                        using (SqlCommand multiUsuario = new SqlCommand($"ALTER DATABASE [{NombreBD}] SET MULTI_USER", conexion))
                        {
                            multiUsuario.CommandTimeout = 300;
                            multiUsuario.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException)
                    {
                        // se ignora
                    }
                }
            }
        }

        private static string PrepararRutaIntermedia(string rutaArchivo)
        {
            Directory.CreateDirectory(CarpetaIntercambio);
            return Path.Combine(CarpetaIntercambio, Path.GetFileName(rutaArchivo));
        }

        private static void IntentarBorrar(string rutaArchivo)
        {
            try
            {
                File.Delete(rutaArchivo);
            }
            catch
            {
                // si no se puede borrar el archivo intermedio no es un error
                // queda como una copia extra del backup
            }
        }
    }
}
