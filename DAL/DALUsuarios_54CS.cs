using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios;

namespace DAL
{
    public class DALUsuarios_54CS
    {
        private Conexion_54CS conexionSQL = new Conexion_54CS();
        public DataTable ObtenerUsuarios()
        {
            string query = "SELECT * FROM Usuarios_54CS";
            return conexionSQL.Leer(query);
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

        public int ActualizarUsuarios(List<Usuario_54CS> lista)
        {
            foreach ( Usuario_54CS user in lista )
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
            return 1;
        }
    }
}
