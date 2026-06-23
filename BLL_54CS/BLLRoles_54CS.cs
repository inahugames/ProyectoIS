using MPP;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_54CS
{
    public class BLLRoles_54CS
    {
        private readonly MPPPermisos_54CS _mpp;

        public BLLRoles_54CS()
        {
            _mpp = new MPPPermisos_54CS();
        }

        public List<Rol_54CS> ObtenerRolesDelSistema()
        {
            return _mpp.ObtenerArbolDeRolesCompleto();
        }

        public List<Rol_54CS> ObtenerElementosParaCrearRol()
        {
            var elementosDisponibles = new List<Rol_54CS>();
            elementosDisponibles.AddRange(_mpp.ObtenerFamiliasEnsambladas());
            elementosDisponibles.AddRange(_mpp.ObtenerPermisosSueltos());

            return elementosDisponibles;
        }

        public void CrearRol(Familia_54CS nuevoRol)
        {
            if (string.IsNullOrWhiteSpace(nuevoRol.Nombre))
                throw new ArgumentException("El nombre del Rol no puede estar vacío.");

            var hijos = nuevoRol.ObtenerHijos();
            if (hijos.Count == 0)
                throw new InvalidOperationException("No se puede crear un Rol vacío. Debe contener al menos una familia o permiso.");

            int idRolGenerado = _mpp.InsertarRol(nuevoRol.Nombre);

            foreach (var hijo in hijos)
            {
                if (hijo is Familia_54CS)
                {
                    _mpp.InsertarRelacionRolFamilia(idRolGenerado, hijo.ID);
                }
                else if (hijo is Permiso_54CS)
                {
                    _mpp.InsertarRelacionRolPermiso(idRolGenerado, hijo.ID);
                }
            }
        }

        public void AgregarFamiliaARol(Familia_54CS rol, Familia_54CS familia)
        {
            if (rol == null)
                throw new ArgumentException("Debe seleccionar un Rol.");
            if (familia == null)
                throw new ArgumentException("Debe seleccionar una Familia.");
            if (rol.Nombre.Equals(familia.Nombre, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede agregar un Rol a sí mismo.");
            if (rol.ObtenerHijos().Any(h => h is Familia_54CS && h.Nombre.Equals(familia.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"La familia '{familia.Nombre}' ya pertenece al rol '{rol.Nombre}'.");

            foreach (var permiso in familia.ObtenerListaPermisos())
            {
                if (rol.TienePermiso(permiso))
                    throw new InvalidOperationException($"Conflicto: el permiso '{permiso}' ya existe en el rol '{rol.Nombre}'.");
            }

            _mpp.InsertarRelacionRolFamilia(rol.ID, familia.ID);
            rol.Agregar(familia);
        }

        public void QuitarFamiliaDeRol(Familia_54CS rol, Familia_54CS familia)
        {
            if (rol == null)
                throw new ArgumentException("Debe seleccionar un Rol.");
            if (familia == null)
                throw new ArgumentException("Debe seleccionar una Familia.");

            if (!rol.ObtenerHijos().Any(h => h is Familia_54CS && h.Nombre.Equals(familia.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"La familia '{familia.Nombre}' no pertenece directamente al rol '{rol.Nombre}'.");

            if (rol.ObtenerHijos().Count <= 1)
                throw new InvalidOperationException("No se puede eliminar el único elemento del Rol");
           
            _mpp.EliminarRelacionRolFamilia(rol.ID, familia.ID);
            rol.Remover(familia);
        }

        public void AgregarPermisoARol(Familia_54CS rol, Permiso_54CS permiso)
        {
            if (rol == null)
                throw new ArgumentException("Debe seleccionar un Rol.");
            if (permiso == null)
                throw new ArgumentException("Debe seleccionar un Permiso.");

            if (rol.TienePermiso(permiso.Nombre))
                throw new InvalidOperationException($"El permiso '{permiso.Nombre}' ya existe en el rol '{rol.Nombre}'.");

            _mpp.InsertarRelacionRolPermiso(rol.ID, permiso.ID);
            rol.Agregar(permiso);
        }

        public void QuitarPermisoDeRol(Familia_54CS rol, Permiso_54CS permiso)
        {
            if (rol == null)
                throw new ArgumentException("Debe seleccionar un Rol.");
            if (permiso == null)
                throw new ArgumentException("Debe seleccionar un Permiso.");

            if (!rol.ObtenerHijos().Any(h => h is Permiso_54CS && h.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"El permiso '{permiso.Nombre}' no es un permiso suelto directo del rol '{rol.Nombre}'.");

            if (rol.ObtenerHijos().Count <= 1)
                throw new InvalidOperationException("No se puede eliminar el único elemento del Rol");

            _mpp.EliminarRelacionRolPermiso(rol.ID, permiso.ID);
            rol.Remover(permiso);
        }

        public void EliminarRol(int idRol)
        {
            if (_mpp.ExisteRolEnUso(idRol))
            {
                throw new InvalidOperationException("No se puede eliminar el Rol porque actualmente hay usuarios que lo tienen asignado");
            }

            _mpp.EliminarRelacionesDeRol(idRol);
            _mpp.EliminarRol(idRol);
        }

        public void ActualizarRolesDeUsuario(int idUsuario, List<Rol_54CS> rolesNuevos)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("Identificador de usuario inválido");

            _mpp.EliminarRolesDeUsuario(idUsuario);

            // si la lista no está vacía, insertamos los nuevos
            if (rolesNuevos != null && rolesNuevos.Any())
            {
                foreach (var rol in rolesNuevos)
                {
                    _mpp.AsignarRolAUsuario(idUsuario, rol.ID);
                }
            }
        }
    }
}
