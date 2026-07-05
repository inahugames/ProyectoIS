using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Conexion_54CS
    {
        private readonly string _connectionString = "Server=.;DataBase=BDProyecto;Integrated Security=True";

        public DataTable Leer(string query, Dictionary<string, object> parametros = null, bool StoredProcedure = false)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cm = new SqlCommand(query, connection))
            {
                if (parametros != null)
                {
                    foreach (var p in parametros)
                    {
                        cm.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }
                connection.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cm))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public int Escribir(string query, Dictionary<string, object> parametros = null)
        {
            if (Servicios.SessionManager_54CS.IntegridadComprometida)
            {
                throw new InvalidOperationException("Escritura bloqueada: se detectó una inconsistencia de datos pendiente de reparación.");
            }

            int filasAfectadas;
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            using (SqlCommand cm = new SqlCommand(query, conexion))
            {
                if (parametros != null)
                {
                    foreach (var p in parametros)
                    {
                        cm.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }
                conexion.Open();
                filasAfectadas = cm.ExecuteNonQuery();
            }

            DALDigitoVerificador_54CS.RecalcularYPersistir();

            return filasAfectadas;
        }
    }
}
