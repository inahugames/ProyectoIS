using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Usuario_74CS
    {
        // Atributos iniciales del usuario
        private int _dni_74CS;
        public int DNI_74cs // Encapsulamiento
        {
            get { return _dni_74CS; }
            set { _dni_74CS = value; }
        }
        public string Apellido_74CS;
        public string Nombre_74CS;
        public string Login_74CS;
        public string Password_74CS;
        public string Rol_74CS;
        public string Email_74CS;
        public bool Block_74CS;
        public bool Activo_74CS;
    }
}
