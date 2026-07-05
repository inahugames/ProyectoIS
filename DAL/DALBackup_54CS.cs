using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Backup y Restore de la Base de Datos. Las operaciones se ejecutan
    // conectados a master porque no se puede restaurar una base mientras se
    // está conectado a ella.
    public class DALBackup_54CS
    {
        private readonly string _masterConnectionString = "Server=.;DataBase=master;Integrated Security=True";
        private const string NombreBD = "BDProyecto";

        // Genera un backup completo (.bak) en la ruta indicada. La ruta debe
        // ser accesible para la cuenta de servicio de SQL Server.
        public void RealizarBackup(string rutaArchivo)
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

        // REPARACIÓN - RESTORE BD: reemplaza la BD actual con el contenido del
        // backup elegido para normalizar la situación ante una inconsistencia.
        public void RestaurarBackup(string rutaArchivo)
        {
            // Se liberan las conexiones del pool de esta aplicación para que el
            // RESTORE pueda tomar acceso exclusivo de la base.
            SqlConnection.ClearAllPools();

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
                    // No enmascarar una excepción del RESTORE: si la BD quedó
                    // en estado RESTORING este ALTER falla, y debe conservarse
                    // el error original para que el Administrador vea la causa
                    // real (y pueda reintentar con un backup válido).
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
                        // se ignora: prevalece la excepción original del RESTORE
                    }
                }
            }

            // Después del restore se regenera el DV para dejar la BD en un
            // estado consistente aunque el backup fuera anterior a la
            // implementación del Dígito Verificador.
            DALDigitoVerificador_54CS.RecalcularYPersistir();
        }
    }
}
