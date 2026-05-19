using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using BCrypt.Net;

namespace Servicios
{
    public class Encriptador_54CS
    {
        public string EncriptarContraseña(string contra)
        {
            return BCrypt.Net.BCrypt.HashPassword(contra);
        }

        public bool VerificarContraseña(string ingresada, string almacenada)
        {
            return BCrypt.Net.BCrypt.Verify(ingresada, almacenada);
        }
    }
}
