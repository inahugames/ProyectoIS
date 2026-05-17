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
                    DNI_54cs = int.Parse(row["DNI_54CS"].ToString()),
                    Nombre_54CS = row["Nombre_54CS"].ToString(),
                    Apellido_54CS = row["Apellido_54CS"].ToString(),
                    Login_54CS = row["Login_54CS"].ToString(),
                    Password_54CS = row["Password_54CS"].ToString(),
                    Rol_54CS = row["Rol_54CS"].ToString(),
                    Email_54CS = row["Email_54CS"].ToString(),
                    Block_54CS = Convert.ToBoolean(row["Block_54CS"].ToString()),
                    Activo_54CS = Convert.ToBoolean(row["Activo_54CS"].ToString())
                };
                usuarios.Add(usuario);
            }
            return usuarios;
        }

        public bool EliminarUsuario(int id)
        {
            return usuariossql.EliminarUsuario(id)>0;
        }
        public bool BloquearUsuario(string login)
        {
            bool block = true;
            return usuariossql.BloquearUsuario(login, block) > 0;
        }

        public bool ActualizarUsuarios(List<Usuario_54CS> lista)
        {
            if (lista != null)
            {
                foreach (Usuario_54CS user in lista)
                {
                    usuariossql.ActualizarUsuarios(user);
                }
            }
            return true;
        }

        public bool GuardarUsuario(Usuario_54CS user)
        {
            return usuariossql.GuardarUsuario(user) > 0;
        }
    }
}
