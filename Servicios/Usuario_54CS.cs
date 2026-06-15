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
        public List<Rol_54CS> RolesAsignados;

        public bool TienePermiso(string permisoBuscado)
        {
            // Retorna 'true' si alguno de los roles asignados (o sus hijos) tiene el permiso
            return RolesAsignados.Any(rol => rol.TienePermiso(permisoBuscado));
        }

        public List<string> ObtenerTodosLosPermisosPlanos()
        {
            List<string> listaPlana = new List<string>();

            // Recorremos los roles de nivel superior que tiene asignados el usuario
            foreach (var rol in RolesAsignados)
            {
                // El Composite se encarga de extraer todas sus familias y permisos internos
                listaPlana.AddRange(rol.ObtenerListaPermisos());
            }

            // Usamos Distinct() para eliminar duplicados si dos roles comparten el mismo permiso
            // Usamos OrderBy() para que te los devuelva ordenados alfabéticamente
            return listaPlana.Distinct().OrderBy(p => p).ToList();
        }
    }
}
