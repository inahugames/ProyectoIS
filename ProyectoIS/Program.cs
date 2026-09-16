using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios;

namespace ProyectoIS
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializa el sistema de idiomas: le indica dónde están los archivos
            // de idioma (Idiomas\*.json, levantados desde la aplicación) y carga el
            // idioma por defecto antes de mostrar cualquier formulario. La preferencia
            // guardada de cada usuario se aplica más adelante, al iniciar sesión.
            string carpetaIdiomas = Path.Combine(Application.StartupPath, "Idiomas");
            IdiomaManager_54CS.Inicializar(carpetaIdiomas);
            IdiomaManager_54CS.CargarIdioma(IdiomaManager_54CS.IdiomaPorDefecto);

            SessionManager_54CS.ResolverUsuarioActual = login =>
            {
                var bll = new BLL_54CS.BLLUsuarios_54CS();
                var usuario = bll.ObtenerTodos().SingleOrDefault(u =>
                    string.Equals(u.Login_54CS.Trim(), login.Trim(), StringComparison.OrdinalIgnoreCase));
                if (usuario != null) bll.CargarPermisosDelUsuarioEnSesion(usuario);
                return usuario;
            };

            Application.Run(new LogIn());
        }
    }
}
