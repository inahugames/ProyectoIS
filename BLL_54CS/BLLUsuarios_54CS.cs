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
        // Política de bloqueo por intentos fallidos de contraseña.
        // Centralizada aquí para que todos los puntos de autenticación
        // (login, cambio de contraseña, etc.) compartan el mismo criterio.
        private const int MaximosIntentosFallidos = 3;
        private const int VentanaIntentosHoras = 3;
        private const string EventoContrasenaErronea = "Contraseña Errónea";
        private const string EventoUsuarioBloqueado = "Usuario Bloqueado";

        MPPUsuarios_54CS MPPusuario = new MPPUsuarios_54CS();
        BLLEventos_54CS BLLeventos = new BLLEventos_54CS();
        public List<Usuario_54CS> ObtenerTodos() => MPPusuario.ObtenerUsuarios();

        // Cuenta los intentos fallidos de contraseña del login dentro de la
        // ventana configurable (evita arrastrar intentos antiguos de por vida).
        public int ContarIntentosFallidosRecientes(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return 0;
            }
            string loginNormalizado = login.Trim();
            DateTime desde = DateTime.Now.AddHours(-VentanaIntentosHoras);
            int cantidad = 0;
            foreach (Eventos_54CS ev in BLLeventos.ObtenerTodos())
            {
                if (ev.Login_54CS != null
                    && ev.Login_54CS.Trim() == loginNormalizado
                    && ev.Evento_54CS == EventoContrasenaErronea
                    && ev.Fecha_54CS >= desde)
                {
                    cantidad++;
                }
            }
            return cantidad;
        }

        // Registra un intento fallido para el login y, si se alcanza el umbral,
        // bloquea al usuario. Devuelve true cuando el usuario quedó bloqueado.
        public bool RegistrarIntentoFallido(string login, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(login))
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje("Login inválido");
                return false;
            }
            try
            {
                Eventos_54CS intento = new Eventos_54CS()
                {
                    Login_54CS = login.Trim(),
                    Fecha_54CS = DateTime.Now,
                    Modulo_54CS = "Login",
                    Evento_54CS = EventoContrasenaErronea,
                    Criticidad_54CS = "1"
                };
                if (!BLLeventos.GuardarEvento(intento, out string msjEvento))
                {
                    mensaje = msjEvento;
                    return false;
                }

                // Se cuenta DESPUÉS de guardar el intento actual, de modo que
                // éste forme parte del contador. Umbral exacto y consistente.
                int intentos = ContarIntentosFallidosRecientes(login);
                if (intentos >= MaximosIntentosFallidos)
                {
                    if (!BloquearUsuario(login, out string msjBloqueo))
                    {
                        mensaje = msjBloqueo;
                        return false;
                    }
                    Eventos_54CS bloqueo = new Eventos_54CS()
                    {
                        Login_54CS = login.Trim(),
                        Fecha_54CS = DateTime.Now,
                        Modulo_54CS = "Login",
                        Evento_54CS = EventoUsuarioBloqueado,
                        Criticidad_54CS = "2"
                    };
                    BLLeventos.GuardarEvento(bloqueo, out string msjEvBloqueo);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                mensaje = $"Ocurrió un error: {ex.Message}";
                return false;
            }
        }

        // Borra únicamente los intentos fallidos del login indicado, dejándolo
        // con el contador en cero. Se usa al desbloquear a un usuario.
        public void LimpiarIntentosFallidos(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return;
            }
            string loginNormalizado = login.Trim();
            foreach (Eventos_54CS ev in BLLeventos.ObtenerTodos())
            {
                if (ev.Login_54CS != null
                    && ev.Login_54CS.Trim() == loginNormalizado
                    && ev.Evento_54CS == EventoContrasenaErronea)
                {
                    BLLeventos.EliminarEvento(ev, out string msj);
                }
            }
        }
    
        public bool CrearUsuario(int dni,string Apellido, string Nombre, string Login, string Password, string Rol, string Email, bool Block, bool Activo, out string mensaje, Rol_54CS rolSeleccionado = null)
        {
            mensaje = string.Empty;
            Usuario_54CS usuario = new Usuario_54CS();
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
                    Idioma_54CS = "es", // idioma por defecto para usuarios nuevos
                };
                ValidarUsuario(usuario);
                ExigirPermiso("CrearUsuarios");
                if (rolSeleccionado != null) ExigirPermiso("AsignarRoles");
                if (!string.Equals(usuario.Rol_54CS, rolSeleccionado?.Nombre ?? "", StringComparison.Ordinal))
                    throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El rol seleccionado no coincide con el usuario."));
                bool resultado = MPPusuario.CrearUsuarios(usuario, rolSeleccionado);
                if (!resultado)
                {
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se creo correctamente");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
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
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se elimino correctamente");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
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
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se bloqueo correctamente");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
                return false;
            }
        }

        public bool DesbloquearUsuario(string login, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.DesbloquearUsuario(login); // No se si agregar la variable block
                if (!resultado)
                {
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se desbloqueo correctamente");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
                return false;
            }
        }

        public bool ActivarUsuario(string login, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.ActivarUsuario(login); // No se si agregar la variable block
                if (!resultado)
                {
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se activo correctamente");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
                return false;
            }
        }

        public bool DesactivarUsuario(string login, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.DesactivarUsuario(login); // No se si agregar la variable block
                if (!resultado)
                {
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se desactivo correctamente");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
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
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se actualizo el usuario correctamente");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
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
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se guardo correctamente");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
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
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se actualizo la contraseña correctamente");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
                return false;
            }
        }

        public bool GuardarIdiomaUsuario(string login, string idioma, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPusuario.ActualizarIdioma(login, idioma);
                if (!resultado)
                {
                    mensaje = IdiomaManager_54CS.TraducirMensaje("No se guardó la preferencia de idioma correctamente");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message);
                return false;
            }
        }

        private static readonly Dictionary<string, List<DateTime>> intentosRecuperacion =
            new Dictionary<string, List<DateTime>>(StringComparer.OrdinalIgnoreCase);

        public bool AutenticarRecuperacion(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)) return false;
            login = login.Trim();
            lock (intentosRecuperacion)
            {
                if (!intentosRecuperacion.TryGetValue(login, out var intentos))
                    intentosRecuperacion[login] = intentos = new List<DateTime>();
                intentos.RemoveAll(t => t < DateTime.Now.AddHours(-VentanaIntentosHoras));
                if (intentos.Count >= MaximosIntentosFallidos) return false;
                // Una autenticación de recuperación nunca escribe en una BD inconsistente.
                intentos.Add(DateTime.Now);
                var candidatos = ObtenerTodos().Where(u =>
                    string.Equals(u.Login_54CS.Trim(), login, StringComparison.OrdinalIgnoreCase)).ToList();
                if (candidatos.Count != 1) return false;
                var usuario = candidatos[0];
                if (usuario.Block_54CS || !usuario.Activo_54CS ||
                    ContarIntentosFallidosRecientes(login) >= MaximosIntentosFallidos ||
                    !new Encriptador_54CS().VerificarContraseña(password, usuario.Password_54CS)) return false;
                CargarPermisosDelUsuarioEnSesion(usuario);
                if (!usuario.TienePermiso("DigitoVerificador")) return false;
                intentos.Clear();
                return true;
            }
        }

        private static void ExigirPermiso(string permiso)
        {
            if (!SessionManager_54CS.HaySesion || !SessionManager_54CS.Instancia.TienePermiso(permiso))
                throw new UnauthorizedAccessException(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."));
        }

        public static void ValidarUsuario(Usuario_54CS usuario)
        {
            if (usuario == null || usuario.DNI_54cs <= 0 || usuario.DNI_54cs > 99999999)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Ingrese un DNI válido."));
            if (string.IsNullOrWhiteSpace(usuario.Nombre_54CS) || usuario.Nombre_54CS.Trim().Length > 20)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El nombre es obligatorio y admite hasta 20 caracteres."));
            if (string.IsNullOrWhiteSpace(usuario.Apellido_54CS) || usuario.Apellido_54CS.Trim().Length > 50)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El apellido es obligatorio y admite hasta 50 caracteres."));
            if (string.IsNullOrWhiteSpace(usuario.Login_54CS) || usuario.Login_54CS.Length > 50)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El login es obligatorio y admite hasta 50 caracteres."));
            ValidarEmail(usuario.Email_54CS);
            if ((usuario.Rol_54CS ?? "").Length > 20)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El nombre del rol admite hasta 20 caracteres."));
        }

        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Ingrese un correo electrónico válido."));
            var direccion = new System.Net.Mail.MailAddress(email.Trim());
            if (direccion.Address != email.Trim())
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Ingrese un correo electrónico válido."));
            if (new Encriptador_54CS().EncriptarReversible(email.Trim()).Length > 200)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El correo electrónico es demasiado largo."));
        }

        public bool ModificarPerfil(int dni, string email, Rol_54CS rol, bool cambiarRol, out string mensaje)
        {
            mensaje = "";
            try
            {
                ExigirPermiso("ModificarUsuario");
                if (cambiarRol) ExigirPermiso("AsignarRoles");
                ValidarEmail(email);
                if (rol != null && rol.Nombre.Length > 20)
                    throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El nombre del rol admite hasta 20 caracteres."));
                return MPPusuario.ModificarPerfil(new Usuario_54CS
                {
                    DNI_54cs = dni, Email_54CS = email.Trim(), Rol_54CS = rol?.Nombre ?? ""
                }, rol, cambiarRol);
            }
            catch (Exception ex) { mensaje = IdiomaManager_54CS.TraducirMensaje(ex.Message); return false; }
        }

        public void CargarPermisosDelUsuarioEnSesion(Usuario_54CS user)
        {
            MPPusuario.CargarPermisosDelUsuarioEnSesion(user);
        }
    } 
}
