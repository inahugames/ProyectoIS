using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Conexion_74CS
    {
        private readonly string _connectionString = "Server=.;DataBase=BDProyecto;Integrated Security=True";
    
        public DataTable Leer(string query, Dictionary<string, object> parametros = null, bool StoredProcedure = false)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cm = new SqlCommand();
            using (cm = new SqlCommand(query, connection))
            {
                if (parametros != null)
                {
                    foreach (var p in parametros)
                    {
                        cm.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }
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
            SqlConnection conexion = new SqlConnection(_connectionString);
            conexion.Open();
            SqlCommand cm = new SqlCommand();
            using (cm = new SqlCommand(query, conexion))
            {
                if (parametros != null)
                {
                    foreach (var p in parametros)
                    {
                        cm.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }
            }
            return cm.ExecuteNonQuery();
        }
    }
}