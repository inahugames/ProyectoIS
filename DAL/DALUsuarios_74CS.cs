using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios;

namespace DAL
{
    public class DALUsuarios_74CS
    {
        private Conexion_74CS conexionSQL = new Conexion_74CS();
        public DataTable ObtenerUsuarios()
        {
            string query = "SELECT * FROM Usuarios_0724";
            return conexionSQL.Leer(query);
        }
        public int BloquearUsuario(string usuario,bool bloqueo)
        {
            string query = $"UPDATE Usuarios_0724 SET Block_0724=@Block WHERE Login_0724 = @Login";

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { "@Block", bloqueo},
                {"@Login",usuario }
            };
            return conexionSQL.Escribir(query, parametros);
        }

        public int ActualizarUsuarios(List<Usuario_74CS> lista)
        {
            foreach ( Usuario_74CS user in lista )
            {
                string query = $"UPDATE Usuarios_0724 SET Rol_0724=@Rol,Activo_0724=@Activo,Email_0724=@Email,Block_0724=@Block WHERE DNI_0724 = @DNI";

                Dictionary<string, object> parametros = new Dictionary<string, object>()
                {
                    { "@Rol", user.Rol_74CS},
                    { "@Activo", user.Activo_74CS },
                    { "@Email", user.Email_74CS },
                    { "@Block", user.Block_74CS },
                    { "@DNI", user.DNI_74cs }
                };
                return conexionSQL.Escribir(query, parametros);
            }
            return 1;
        }
    }
}
