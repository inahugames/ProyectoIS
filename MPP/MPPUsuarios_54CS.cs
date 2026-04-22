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
    public class MPPUsuarios_54CS
    {
        private DALUsuarios_54CS usuariossql = new DALUsuarios_54CS();
        public List<Usuario_54CS> ObtenerUsuarios()
        {
            DataTable tabla = usuariossql.ObtenerUsuarios();
            List<Usuario_54CS> usuarios = new List<Usuario_54CS>();
            foreach (DataRow row in tabla.Rows)
            {
                Usuario_54CS usuario = new Usuario_54CS()
                {
                    DNI_74cs = int.Parse(row["DNI_74CS"].ToString()),
                    Nombre_74CS = row["Nombre_74CS"].ToString(),
                    Apellido_74CS = row["Apellido_74CS"].ToString(),
                    Login_74CS = row["Login_74CS"].ToString(),
                    Password_74CS = row["Password_74CS"].ToString(),
                    Rol_74CS = row["Rol_74CS"].ToString(),
                    Email_74CS = row["Email_74CS"].ToString(),
                    Block_74CS = Convert.ToBoolean(row["Block_74CS"].ToString()),
                    Activo_74CS = Convert.ToBoolean(row["Activo_74CS"].ToString())
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

        public bool ActualizarUsuarios(List<Usuario_54CS> lista)
        {
            return usuariossql.ActualizarUsuarios(lista) > 0;
        }
    }
}
