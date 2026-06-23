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

        public DataTable ObtenerRelacionesFamiliaFamilia()
        {
            string query = "SELECT IdFamiliaPadre, IdFamiliaHijo FROM Familia_Familia";
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

        public void InsertarRelacionFamiliaFamilia(int idFamiliaPadre, int idFamiliaHijo)
        {
            string query = "INSERT INTO Familia_Familia (IdFamiliaPadre, IdFamiliaHijo) VALUES (@idPadre, @idHijo)";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idPadre",idFamiliaPadre},
                { "@idHijo",idFamiliaHijo}
            };
            conexionSQL.Escribir(query, parametros);
        }

        public int EliminarRelacionFamiliaFamilia(int idFamiliaPadre, int idFamiliaHijo)
        {
            string query = "DELETE FROM Familia_Familia WHERE IdFamiliaPadre = @idPadre AND IdFamiliaHijo = @idHijo";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idPadre",idFamiliaPadre},
                { "@idHijo",idFamiliaHijo}
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public bool ExisteFamiliaEnUso(int idFamilia)
        {
            string query = @"
                SELECT
                    (SELECT COUNT(1) FROM Rol_Familia      WHERE IdFamilia     = @id) +
                    (SELECT COUNT(1) FROM Familia_Familia  WHERE IdFamiliaHijo = @id)";
            using (SqlConnection cx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@id", idFamilia);
                    cx.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public int EliminarRelacionesDeFamilia(int idFamilia)
        {
            string query = @"
                DELETE FROM Familia_Permiso  WHERE IdFamilia       = @id;
                DELETE FROM Familia_Familia  WHERE IdFamiliaPadre  = @id;";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@id",idFamilia}
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int EliminarFamilia(int idFamilia)
        {
            string query = "DELETE FROM Familias_54CS WHERE IdFamilia_54CS = @id";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@id",idFamilia}
            };
            return conexionSQL.Escribir(query, parametros);
        }

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
