using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        private int _dni;
        public int DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }
        public string Apellido;
        public string Nombre;
        public string Login;
        public string Password;
        public string Rol;
        public string Email;
        public bool Block;
        public bool Activo;
    }
}
