using MPP;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_54CS
{
    public class BLLUsuarios_54CS
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
        public bool EliminarUsuario(int id, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.EliminarUsuario(id);
                if (!resultado)
                {
                    mensaje = "No se elimino correctamente";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error";
                return false;
            }
        }
        public bool BloquearUsuario(string login,out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.BloquearUsuario(login); // No se si agregar la variable block
                if(!resultado)
                {
                    mensaje = "No se bloqueo correctamente";
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error";
                return false;
            }
        }
        public bool ActualizarUsuario(List<Usuario_54CS> lista, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.ActualizarUsuarios(lista); 
                if (!resultado)
                {
                    mensaje = "No se actualizo el usuario correctamente";
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error";
                return false;
            }
        }
        public bool GuardarUsuario(Usuario_54CS usuario, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.GuardarUsuario(usuario);
                if (!resultado)
                {
                    mensaje = "No se guardo correctamente";
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error";
                return false;
            }
        }
        public bool ActualizarContraseña(string usuario,string password, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.ActualizarContraseña(usuario,password);
                if (!resultado)
                {
                    mensaje = "No se actualizo la contraseña correctamente";
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error";
                return false;
            }
        }
    } 
}
