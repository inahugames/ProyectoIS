using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BE;
using DAL;
using Servicios;

namespace MPP
{
    public class MPPUsuarios_74CS
    {
        private DALUsuarios_74CS usuariossql = new DALUsuarios_74CS();
        public List<Usuario_74CS> ObtenerUsuarios()
        {
            DataTable tabla = usuariossql.ObtenerUsuarios();
            List<Usuario_74CS> usuarios = new List<Usuario_74CS>();
            foreach (DataRow row in tabla.Rows)
            {
                Usuario_74CS usuario= new Usuario_74CS()
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

        public bool BloquearUsuario(string login)
        {
            bool block = true;
            return usuariossql.EditarUsuario(login, block) > 0;
        }
    }
}
