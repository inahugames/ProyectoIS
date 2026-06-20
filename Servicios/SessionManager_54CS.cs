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
        //public bool Logged_54CS;

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
                    throw new Exception("Sesión ya iniciada");
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
                throw new Exception("Sesión no iniciada");
            }
        }

        public bool TienePermiso(string permisoBuscado)
        {
            // Retorna 'true' si alguno de los roles asignados (o sus hijos) tiene el permiso
            return listperm.Any(rol => rol.TienePermiso(permisoBuscado));
        }
    }
}
