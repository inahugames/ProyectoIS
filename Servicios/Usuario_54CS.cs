using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Usuario_54CS
    {
        // Atributos iniciales del usuario
        private int _dni_54CS;
        public int DNI_54cs // Encapsulamiento
        {
            get { return _dni_54CS; }
            set { _dni_54CS = value; }
        }
        public string Apellido_54CS;
        public string Nombre_54CS;
        public string Login_54CS;
        public string Password_54CS;
        public string Rol_54CS;
        public string Email_54CS;
        public bool Block_54CS;
        public bool Activo_54CS;
    }
}
