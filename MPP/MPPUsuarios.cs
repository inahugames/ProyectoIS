using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BE;
using DAL;

namespace MPP
{
    public class MPPUsuarios
    {
        private DALUsuarios usuariossql = new DALUsuarios();
        public List<Usuario> ObtenerUsuarios()
        {
            DataTable tabla = usuariossql.ObtenerUsuarios();
            List<Usuario> usuarios = new List<Usuario>();
            foreach (DataRow row in tabla.Rows)
            {
                Usuario usuario= new Usuario()
                {
                    DNI = int.Parse(row["DNI_0724"].ToString()),
                    Nombre = row["Nombre_0724"].ToString(),
                    Apellido = row["Apellido_0724"].ToString(),
                    Login = row["Login_0724"].ToString(),
                    Password = row["Password_0724"].ToString(),
                    Rol = row["Rol_0724"].ToString(),
                    Email = row["Email_0724"].ToString(),
                    Block = Convert.ToBoolean(row["Block_0724"].ToString()),
                    Activo = Convert.ToBoolean(row["Activo_0724"].ToString())
                };
                usuarios.Add(usuario);
            }
            return usuarios;
        }
    }
}
