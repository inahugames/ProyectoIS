using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class SessionManager_54CS
    {
        public static SessionManager_54CS _session;
        private static readonly object _lock = new object();
        
        public string Login_54CS;
        public string Nombre_54CS;
        public string Rol_54CS;
        public string Idioma_54CS;
        public List<Rol_54CS> listperm;
        public static bool HaySesion
        {
            get
            {
                lock (_lock)
                {
                    return _session != null;
                }
            }
        }

        public static bool IntegridadComprometida { get; set; }
        public static Func<string, Usuario_54CS> ResolverUsuarioActual { get; set; }

        public static SessionManager_54CS Instancia
        {
            get
            {
                lock (_lock)
                {
                    if (_session == null)
                    {
                        throw new Exception ("Sesión no iniciada");
                    }
                    return _session;
                }
            }
        }

        public static void Login(string Login, string Nombre, string Rol, List<Rol_54CS> perm, string Idioma = null)
        {
            lock (_lock)
            {
                if (_session != null)
                {
                    throw new Exception(IdiomaManager_54CS.TraducirMensaje("Sesión ya iniciada"));
                }
                _session = new SessionManager_54CS();
                _session.Login_54CS = Login;
                _session.Nombre_54CS = Nombre;
                _session.Rol_54CS = Rol;
                _session.listperm = perm;
                _session.Idioma_54CS = Idioma;
            }
        }

        public static void Logout()
        {
            if (_session != null)
            {
                _session = null;
            }
            else
            {
                throw new Exception(IdiomaManager_54CS.TraducirMensaje("Sesión no iniciada"));
            }
        }

        public bool TienePermiso(string permisoBuscado)
        {
            if (IntegridadComprometida) return false;
            if (ResolverUsuarioActual != null)
            {
                try
                {
                    var usuario = ResolverUsuarioActual(Login_54CS);
                    if (usuario == null || usuario.Block_54CS || !usuario.Activo_54CS)
                    {
                        listperm = new List<Rol_54CS>();
                        return permisoBuscado == "Logout";
                    }
                    listperm = usuario.RolesAsignados;
                    Rol_54CS = usuario.Rol_54CS;
                    Idioma_54CS = usuario.Idioma_54CS;
                }
                catch
                {
                    listperm = new List<Rol_54CS>();
                    return permisoBuscado == "Logout";
                }
            }
            return listperm != null && listperm.Any(rol => rol.TienePermiso(permisoBuscado));
        }
    }
}
