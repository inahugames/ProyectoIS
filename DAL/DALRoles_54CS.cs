using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALRoles_54CS
    {

        private readonly string _connectionString = "Server=.;DataBase=BDProyecto;Integrated Security=True";
        private Conexion_54CS conexionSQL = new Conexion_54CS();
        public DataTable ObtenerRoles()
        {
            string query = "SELECT * FROM Roles_54CS";
            return conexionSQL.Leer(query);
        }

        public DataTable ObtenerRelacionesRolFamilia()
        {
            string query = "SELECT IdRol, IdFamilia FROM Rol_Familia";
            return conexionSQL.Leer(query);
        }

        public DataTable ObtenerRelacionesRolPermiso()
        {
            string query = "SELECT IdRol, IdPermiso FROM Rol_Permiso";
            return conexionSQL.Leer(query);
        }

        public int InsertarRol(string descripcion)
        {
            string query = $"INSERT INTO Roles_54CS (Descripcion_54CS) Values (@Descripcion)";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@Descripcion",descripcion }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int InsertarRelacionRolFamilia(int idRol, int idFamilia)
        {
            string query = "INSERT INTO Rol_Familia (IdRol, IdFamilia) VALUES (@idRol, @idFamilia)";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idRol",idRol},
                { "@idFamilia",idFamilia }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int InsertarRelacionRolPermiso(int idRol, int idPermiso)
        {
            string query = "INSERT INTO Rol_Permiso (IdRol, IdPermiso) VALUES (@idRol, @idPermiso)";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idRol",idRol},
                { "@idPermiso",idPermiso }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int AsignarRolAUsuario(int idUsuario, int idRol)
        {
            string query = "INSERT INTO Usuario_Rol (IdUsuario, IdRol) VALUES (@idUsu, @idRol)";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idRol",idRol},
                { "@idUsu",idUsuario}
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int EliminarRolesDeUsuario(int idUsuario)
        {
            string query = "DELETE FROM Usuario_Rol WHERE IdUsuario = @idUsu";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { "@idUsu",idUsuario}
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int EliminarRelacionesDeRol(int idRol)
        {
            // Borra los hijos antes de borrar el padre para evitar errores de Foreign Key
            string query = @"
                DELETE FROM Rol_Familia WHERE IdRol = @idRol;
                DELETE FROM Rol_Permiso WHERE IdRol = @idRol;";

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idRol",idRol}
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int EliminarRol(int idRol)
        {
            string query = "DELETE FROM Rol WHERE IdRol = @idRol";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@idRol",idRol}
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public bool ExisteRolEnUso(int idRol)
        {
            string query = "SELECT COUNT(1) FROM Usuario_Rol WHERE IdRol = @idRol";
            using (SqlConnection cx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@idRol", idRol);
                    cx.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
