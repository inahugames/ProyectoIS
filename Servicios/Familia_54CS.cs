using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Familia_54CS : Rol_54CS
    {
        private List<Rol_54CS> _hijos;

        public Familia_54CS(string nombre) : base(nombre)
        {
            _hijos = new List<Rol_54CS>();
        }

        public override IList<string> ObtenerListaPermisos()
        {
            var lista = new List<string>();
            foreach (var hijo in _hijos)
            {
                lista.AddRange(hijo.ObtenerListaPermisos());
            }
            return lista; // no devolver duplicados
        }

        public override void Agregar(Rol_54CS rol)
        {
            if (this.Nombre.Equals(rol.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("No se puede agregar una familia a sí misma.");
            }
            var permisosNuevos = rol.ObtenerListaPermisos();
            // Verificar si la familia ya tienen alguno de esos permisos
            foreach (var permiso in permisosNuevos)
            {
                if (this.TienePermiso(permiso))
                {
                    throw new Exception($"Conflicto: El permiso '{permiso}' ya existe en la familia '{this.Nombre}' o en alguna de sus sub-familias.");
                }
            }
            _hijos.Add(rol);
        }

        public override void Remover(Rol_54CS rol)
        {
            if (_hijos.Contains(rol))
            {
                _hijos.Remove(rol);
            }
        }

        public override IList<Rol_54CS> ObtenerHijos()
        {
            return _hijos.AsReadOnly(); // Previene modificaciones directas a la lista
        }

        public override bool TienePermiso(string permisoBuscado)
        {
            // Si el nombre de la familia es exactamente el permiso que buscamos
            if (this.Nombre.Equals(permisoBuscado, System.StringComparison.OrdinalIgnoreCase))
                return true;

            // Si no, delegamos la pregunta recursivamente a sus hijos
            return _hijos.Any(hijo => hijo.TienePermiso(permisoBuscado));
        }
    }
}