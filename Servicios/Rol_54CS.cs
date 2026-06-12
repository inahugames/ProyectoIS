using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public abstract class Rol_54CS
    {
        public string Nombre { get; set; }
        public int ID { get; set; }

        public Rol_54CS(string nombre)
        {
            Nombre = nombre;
        }

        // Métodos para el manejo del árbol (Composite)
        public abstract void Agregar(Rol_54CS rol);
        public abstract void Remover(Rol_54CS rol);
        public abstract IList<Rol_54CS> ObtenerHijos();
        public abstract bool TienePermiso(string permisoBuscado);
        public abstract IList<string> ObtenerListaPermisos();
    }
}
