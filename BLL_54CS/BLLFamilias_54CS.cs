using MPP;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_54CS
{
    public class BLLFamilias_54CS
    {
        private readonly MPPPermisos_54CS _mpp;

        public BLLFamilias_54CS()
        {
            _mpp = new MPPPermisos_54CS();
        }

        public List<Rol_54CS> ObtenerFamilias()
        {
            return _mpp.ObtenerFamiliasEnsambladas();
        }

        public void CrearFamilia(Familia_54CS nuevaFamilia, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nuevaFamilia.Nombre))
                throw new ArgumentException("El nombre de la Familia no puede estar vacío.");

            var hijos = nuevaFamilia.ObtenerHijos();
            if (hijos.Count == 0)
                throw new InvalidOperationException("Una Familia debe contener al menos un permiso o una familia.");

            int idFamiliaGenerada = _mpp.InsertarFamilia(nuevaFamilia.Nombre, descripcion);

            foreach (var hijo in hijos)
            {
                if (hijo is Permiso_54CS)
                {
                    _mpp.InsertarRelacionFamiliaPermiso(idFamiliaGenerada, hijo.ID);
                }
                else if (hijo is Familia_54CS)
                {
                    _mpp.InsertarRelacionFamiliaFamilia(idFamiliaGenerada, hijo.ID);
                }
                else
                {
                    throw new InvalidOperationException("Una Familia solo puede contener Permisos u otras Familias.");
                }
            }
        }

        public void AgregarPermisoAFamilia(Familia_54CS familia, Permiso_54CS permiso)
        {
            if (familia == null) { throw new ArgumentException("Debe seleccionar una Familia."); }
            if (permiso == null) { throw new ArgumentException("Debe seleccionar un Permiso."); }
            if (familia.TienePermiso(permiso.Nombre)) { throw new InvalidOperationException($"El permiso '{permiso.Nombre}' ya pertenece a la familia '{familia.Nombre}'."); }

            _mpp.InsertarRelacionFamiliaPermiso(familia.ID, permiso.ID);
            familia.Agregar(permiso);
        }

        public void QuitarPermisoDeFamilia(Familia_54CS familia, Permiso_54CS permiso)
        {
            if (familia == null) { throw new ArgumentException("Debe seleccionar una Familia."); }
            if (permiso == null) { throw new ArgumentException("Debe seleccionar un Permiso."); }
            if (!familia.ObtenerHijos().Any(h => h.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase))) { throw new InvalidOperationException($"El permiso '{permiso.Nombre}' no pertenece directamente a la familia '{familia.Nombre}'."); }
            if (familia.ObtenerHijos().Count <= 1) { throw new InvalidOperationException("No se puede eliminar el último permiso de una Familia"); }
            
            _mpp.EliminarRelacionFamiliaPermiso(familia.ID, permiso.ID);
            familia.Remover(permiso);
        }

        public void EliminarFamilia(int idFamilia)
        {
            if (idFamilia <= 0) { throw new ArgumentException("Identificador de Familia inválido."); }

            // no se puede borrar una familia que algún rol u otra familia este usando
            if (_mpp.ExisteFamiliaEnUso(idFamilia))
            {
                throw new InvalidOperationException( "No se puede eliminar la Familia porque está siendo utilizada por un rol u otra familia. " + "Eliminela primero de donde se esté usando.");
            }
            _mpp.EliminarRelacionesDeFamilia(idFamilia);
            _mpp.EliminarFamilia(idFamilia);
        }
    }
}
