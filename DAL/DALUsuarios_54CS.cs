using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUsuarios_54CS
    {
        private Conexion_54CS conexionSQL = new Conexion_54CS();
        private readonly string _connectionString = "Server=.;DataBase=BDProyecto;Integrated Security=True";
        public DataTable ObtenerUsuarios()
        {
            string query = "SELECT * FROM Usuarios_54CS";
            return conexionSQL.Leer(query);
        }
        public int CrearUsuario(int dni, string Apellido, string Nombre, string Login, string Password, string Rol, string Email, bool block, bool activo)
        {
            string query = $"INSERT INTO Usuarios_54CS (DNI_54CS,Apellido_54CS,Nombre_54CS,Login_54CS,Password_54CS,Rol_54CS,Email_54CS, Block_54CS, Activo_54CS) Values ('{dni}','{Apellido}','{Nombre}','{Login}','{Password}','{Rol}','{Email}','{block}','{activo}')";
            return conexionSQL.Escribir(query);
        }

        public int EliminarUsuario(int dni)
        {
            string query = $"DELETE FROM Usuarios_54CS WHERE DNI_54CS = @DNI";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@DNI",dni }
            };
            return conexionSQL.Escribir(query, parametros);
        }
        public int BloquearUsuario(string usuario,bool bloqueo)
        {
            string query = $"UPDATE Usuarios_54CS SET Block_54CS=@Block WHERE Login_54CS = @Login";

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { "@Block", bloqueo},
                {"@Login",usuario }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int DesbloquearUsuario(string usuario,bool bloqueo)
        {
            string query = $"UPDATE Usuarios_54CS SET Block_54CS=@Block WHERE Login_54CS = @Login";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { "@Block",bloqueo},
                { "@Login", usuario }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int ActivarUsuario(string usuario)
        {
            string query = $"UPDATE Usuarios_54CS SET Activo_54CS=@Activo WHERE Login_54CS = @Login";
            bool activar = true;
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@Login",usuario },
                {"@Activo", activar }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int DesactivarUsuario(string usuario)
        {
            string query = $"UPDATE Usuarios_54CS SET Activo_54CS=@Activo WHERE Login_54CS = @Login";
            bool activar = false;
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {"@Login",usuario },
                {"@Activo", activar }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int ActualizarUsuarios(Usuario_54CS user)
        {
                string query = $"UPDATE Usuarios_54CS SET Rol_54CS=@Rol,Activo_54CS=@Activo,Email_54CS=@Email,Block_54CS=@Block WHERE DNI_54CS = @DNI";

                Dictionary<string, object> parametros = new Dictionary<string, object>()
                {
                    { "@Rol", user.Rol_54CS},
                    { "@Activo", user.Activo_54CS },
                    { "@Email", user.Email_54CS },
                    { "@Block", user.Block_54CS },
                    { "@DNI", user.DNI_54cs }
                };
                return conexionSQL.Escribir(query, parametros);
        }

        public int GuardarUsuario(Usuario_54CS user)
        {
            string query = $"INSERT INTO Usuarios_54CS VALUES (@DNI,@Apellido,@Nombre,@Login,@Password,@Rol,@Email,@Block,@Activo)";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
                {
                    { "@Rol", user.Rol_54CS},
                    { "@Activo", user.Activo_54CS },
                    { "@Email", user.Email_54CS },
                    { "@Block", user.Block_54CS },
                    { "@DNI", user.DNI_54cs },
                    { "@Login",user.Login_54CS },
                    { "@Password",user.Password_54CS },
                    { "@Nombre", user.Nombre_54CS },
                    { "@Apellido", user.Apellido_54CS }
                };
            return conexionSQL.Escribir(query, parametros);
        }

        public int ActualizarContraseña(string usuario, string contraseña)
        {
            string query = $"UPDATE Usuarios_54CS SET Password_54CS=@Password WHERE Login_54CS=@Usuario";
            Dictionary<string, object> parametros = new Dictionary<string, object>()
                {
                    { "@Password", contraseña },
                    { "@Usuario", usuario }
                };
            return conexionSQL.Escribir(query, parametros);
        }

        public List<int> ObtenerIdsRolesPorUsuario(int idUsuario)
        {
            List<int> idsRoles = new List<int>();
            string query = "SELECT IdRol FROM Usuario_Rol WHERE IdUsuario = @idUsu";

            using (SqlConnection cx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, cx))
                {
                    cmd.Parameters.AddWithValue("@idUsu", idUsuario);
                    cx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            idsRoles.Add(Convert.ToInt32(dr["IdRol"]));
                        }
                    }
                }
            }
            return idsRoles;
        }
    }
}
