using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFamilia_54CS
    {
        private readonly string _connectionString = "Server=.;DataBase=BDProyecto;Integrated Security=True";
        private Conexion_54CS conexionSQL = new Conexion_54CS();

        private int EjecutarScalarParaId(string query, SqlParameter[] parametros)
        {
            using (SqlConnection cx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddRange(parametros);
                    cx.Open();
                    // executescalar guarda el resultado del id
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public DataTable ObtenerFamilias()
        {
            string query = "SELECT * FROM Familias_54CS";
            return conexionSQL.Leer(query);
        }

        public DataTable ObtenerRelacionesFamiliaPermiso()
        {
            string query = "SELECT IdFamilia, IdPermiso FROM Familia_Permiso";
            return conexionSQL.Leer(query);
        }

        public int InsertarFamilia(string nombre, string descripcion)
        {
            string query = "INSERT INTO Familias_54CS (Nombre_54CS, Descripcion_54CS) VALUES (@nombre, @desc); SELECT SCOPE_IDENTITY();";
            SqlParameter[] parametros = {
                new SqlParameter("@nombre", nombre),
                new SqlParameter("@desc", (object)descripcion ?? DBNull.Value)
            };
            return EjecutarScalarParaId(query, parametros);
        }

        public void InsertarRelacionFamiliaPermiso(int idFamilia, int idPermiso)
        {
            string query = "INSERT INTO Familia_Permiso (IdFamilia, IdPermiso) VALUES (@idFam, @idPerm)";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idFam",idFamilia},
                { "@idPerm",idPermiso}
            };
            conexionSQL.Escribir(query, parametros);
        }

        /// <summary>
        /// Quita la relación entre una Familia y uno de sus Permisos (no borra ninguna de las dos entidades).
        /// </summary>
        public int EliminarRelacionFamiliaPermiso(int idFamilia, int idPermiso)
        {
            string query = "DELETE FROM Familia_Permiso WHERE IdFamilia = @idFam AND IdPermiso = @idPerm";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idFam",idFamilia},
                { "@idPerm",idPermiso}
            };
            return conexionSQL.Escribir(query, parametros);
        }
    }
}
