using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Permiso_54CS : Rol_54CS
    {
        public Permiso_54CS(string nombre) : base(nombre) { }

        public override void Agregar(Rol_54CS rol)
        {
            throw new Exception("No se puede agregar a un permiso.");
        }

        public override void Remover(Rol_54CS rol)
        {
            throw new Exception("No se puede eliminar un permiso");
        }

        public override IList<Rol_54CS> ObtenerHijos()
        {
            return new List<Rol_54CS>();
        }

        public override bool TienePermiso(string permisoBuscado)
        {
            // Solo devuelve true si su nombre coincide
            return this.Nombre.Equals(permisoBuscado, System.StringComparison.OrdinalIgnoreCase);
        }

        public override IList<string> ObtenerListaPermisos()
        {
            // Un permiso solo aporta su propio nombre a la lista
            return new List<string> { this.Nombre };
        }
    }
}
