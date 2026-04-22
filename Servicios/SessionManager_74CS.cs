using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class SessionManager_74CS
    {
        public static string Login_74CS;
        public static string Nombre_74CS;
        public static string Rol_74CS;
        public static bool Logged_74CS;

        public static void Logout()
        {
            Login_74CS = null; Nombre_74CS = null; Rol_74CS = null;
            Logged_74CS = false;
        }
    }
}
