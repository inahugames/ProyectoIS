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
        MPPPermisos_54CS perm = new MPPPermisos_54CS();
        public List<Usuario_54CS> ObtenerUsuarios()
        {
            DataTable tabla = usuariossql.ObtenerUsuarios();
            List<Usuario_54CS> usuarios = new List<Usuario_54CS>();
            foreach (DataRow row in tabla.Rows)
            {
                Usuario_54CS usuario = new Usuario_54CS()
                {
                    DNI_54cs = int.Parse(row["DNI_54CS"].ToString()), // Si no corre el codigo revisar el orden xq en la DAL va primero el apellido
                    Nombre_54CS = row["Nombre_54CS"].ToString(),
                    Apellido_54CS = row["Apellido_54CS"].ToString(),
                    Login_54CS = row["Login_54CS"].ToString(),
                    Password_54CS = row["Password_54CS"].ToString(),
                    Rol_54CS = row["Rol_54CS"].ToString(),
                    Email_54CS = row["Email_54CS"].ToString(),
                    Block_54CS = Convert.ToBoolean(row["Block_54CS"].ToString()),
                    Activo_54CS = Convert.ToBoolean(row["Activo_54CS"].ToString()),
                    Idioma_54CS = (tabla.Columns.Contains("Idioma_54CS") && row["Idioma_54CS"] != DBNull.Value)
                        ? row["Idioma_54CS"].ToString()
                        : "es", // si todavía no se corrió la migración de BD, usamos español por defecto
                    RolesAsignados = new List<Rol_54CS>()
                };
                usuarios.Add(usuario);
            }
            return usuarios;
        }

        public bool CrearUsuarios(Usuario_54CS usuario)
        {
            return usuariossql.GuardarUsuario(usuario) > 0;
        }

        public bool ActivarUsuario(string login)
        {
            return usuariossql.ActivarUsuario(login) > 0;
        }
        
        public bool DesactivarUsuario(string login)
        {
            return usuariossql.DesactivarUsuario(login) > 0;
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

        public bool DesbloquearUsuario(string login)
        {
            bool block = false;
            return usuariossql.DesbloquearUsuario(login, block) > 0;
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

        public bool ActualizarContraseña(string usuario, string password)
        {
            return usuariossql.ActualizarContraseña(usuario, password) > 0;
        }

        public bool ActualizarIdioma(string usuario, string idioma)
        {
            return usuariossql.ActualizarIdioma(usuario, idioma) > 0;
        }

        public void CargarPermisosDelUsuarioEnSesion(Usuario_54CS usuario)
        {
            List<int> idsRolesAsignados = usuariossql.ObtenerIdsRolesPorUsuario(usuario.DNI_54cs);
            List<Rol_54CS> arbolCompletoDelSistema = perm.ObtenerArbolDeRolesCompleto();
            usuario.RolesAsignados = new List<Rol_54CS>();

            foreach (var rol in arbolCompletoDelSistema)
            {
                if (idsRolesAsignados.Contains(rol.ID))
                {
                    usuario.RolesAsignados.Add(rol);
                }
            }
        }
    }
}
