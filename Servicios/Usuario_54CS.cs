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
        public string Apellido_54CS { get; set; }
        public string Nombre_54CS { get; set;  }
        public string Login_54CS { get; set;  }
        public string Password_54CS { get; set; }
        public string Rol_54CS { get; set; }
        public string Email_54CS { get; set; }
        public bool Block_54CS { get; set; }
        public bool Activo_54CS { get; set; }
        public string Idioma_54CS { get; set; }
        public List<Rol_54CS> RolesAsignados;

        public bool TienePermiso(string permisoBuscado)
        {
            // devuelve true si alguno de los roles asignados (o sus hijos) tiene el permiso
            return RolesAsignados.Any(rol => rol.TienePermiso(permisoBuscado));
        }

        public List<string> ObtenerTodosLosPermisosPlanos()
        {
            List<string> listaPlana = new List<string>();
            foreach (var rol in RolesAsignados)
            {
                listaPlana.AddRange(rol.ObtenerListaPermisos());
            }
            return listaPlana.Distinct().OrderBy(p => p).ToList();
        }
    }
}
