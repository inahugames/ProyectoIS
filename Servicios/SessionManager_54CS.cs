using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class SessionManager_54CS
    {
        public static string Login_54CS;
        public static string Nombre_54CS;
        public static string Rol_54CS;
        public static bool Logged_54CS;

        public static void Logout()
        {
            Login_54CS = null; Nombre_54CS = null; Rol_54CS = null;
            Logged_54CS = false;
        }
    }
}
