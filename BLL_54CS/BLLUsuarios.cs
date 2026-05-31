using MPP;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_54CS
{
    public class BLLUsuarios
    {
        MPPUsuarios_54CS MPPusuario = new MPPUsuarios_54CS();
        public List<Usuario_54CS> ObtenerTodos() => MPPusuario.ObtenerUsuarios();
    
        public bool CrearUsuario(int dni,string Apellido, string Nombre, string Login, string Password, string Rol, string Email, bool Block, bool Activo, out string mensaje, out Usuario_54CS usuario)
        {
            mensaje = string.Empty;
            usuario = null;
            try
            {
                usuario = new Usuario_54CS()
                {
                    DNI_54cs = dni,
                    Apellido_54CS = Apellido,
                    Nombre_54CS = Nombre,
                    Login_54CS = Login,
                    Password_54CS = Password,
                    Rol_54CS = Rol,
                    Email_54CS = Email,
                    Block_54CS = Block,
                    Activo_54CS = Activo,
                };
                bool resultado = MPPusuario.CrearUsuarios(usuario);
                if (!resultado)
                {
                    mensaje = "No se creo correctamente";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = $"Error inesperado:{ex.Message}";
                usuario = null;
                return false;
            }
        }

    } 
}
