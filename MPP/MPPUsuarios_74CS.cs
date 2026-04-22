using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
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
                Usuario_74CS usuario = new Usuario_74CS()
                {
                    DNI_74cs = int.Parse(row["DNI_0724"].ToString()),
                    Nombre_74CS = row["Nombre_0724"].ToString(),
                    Apellido_74CS = row["Apellido_0724"].ToString(),
                    Login_74CS = row["Login_0724"].ToString(),
                    Password_74CS = row["Password_0724"].ToString(),
                    Rol_74CS = row["Rol_0724"].ToString(),
                    Email_74CS = row["Email_0724"].ToString(),
                    Block_74CS = Convert.ToBoolean(row["Block_0724"].ToString()),
                    Activo_74CS = Convert.ToBoolean(row["Activo_0724"].ToString())
                };
                usuarios.Add(usuario);
            }
            return usuarios;
        }

        public bool BloquearUsuario(string login)
        {
            bool block = true;
            return usuariossql.BloquearUsuario(login, block) > 0;
        }

        public bool ActualizarUsuarios(List<Usuario_74CS> lista)
        {
            return usuariossql.ActualizarUsuarios(lista) > 0;
        }
    }
}
