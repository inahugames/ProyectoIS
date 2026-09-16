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
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El nombre del Rol no puede estar vacío."));

            nuevoRol.Nombre = nuevoRol.Nombre.Trim();
            if (nuevoRol.Nombre.Length > 20)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("El nombre del rol admite hasta 20 caracteres."));
            var hijos = nuevoRol.ObtenerHijos();
            if (hijos.Count == 0)
                throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("No se puede crear un Rol vacío. Debe contener al menos una familia o permiso."));

            _mpp.EnTransaccion(() =>
            {
                if (_mpp.ObtenerArbolDeRolesCompleto().Any(r => string.Equals(r.Nombre.Trim(), nuevoRol.Nombre, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("Ya existe un rol con ese nombre."));
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
            });
        }

        public void AgregarFamiliaARol(Familia_54CS rol, Familia_54CS familia)
        {
            if (rol == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar un Rol."));
            if (familia == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar una Familia."));
            if (rol.Nombre.Equals(familia.Nombre, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("No se puede agregar un Rol a sí mismo."));
            if (rol.ObtenerHijos().Any(h => h is Familia_54CS && h.Nombre.Equals(familia.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(string.Format(IdiomaManager_54CS.TraducirMensaje("La familia '{0}' ya pertenece al rol '{1}'."), familia.Nombre, rol.Nombre));

            foreach (var permiso in familia.ObtenerListaPermisos())
            {
                if (rol.TienePermiso(permiso))
                    throw new InvalidOperationException(string.Format(IdiomaManager_54CS.TraducirMensaje("Conflicto: el permiso '{0}' ya existe en el rol '{1}'."), permiso, rol.Nombre));
            }

            _mpp.InsertarRelacionRolFamilia(rol.ID, familia.ID);
            rol.Agregar(familia);
        }

        public void QuitarFamiliaDeRol(Familia_54CS rol, Familia_54CS familia)
        {
            if (rol == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar un Rol."));
            if (familia == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar una Familia."));

            if (!rol.ObtenerHijos().Any(h => h is Familia_54CS && h.Nombre.Equals(familia.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(string.Format(IdiomaManager_54CS.TraducirMensaje("La familia '{0}' no pertenece directamente al rol '{1}'."), familia.Nombre, rol.Nombre));

            if (rol.ObtenerHijos().Count <= 1)
                throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("No se puede eliminar el único elemento del Rol"));
           
            _mpp.EliminarRelacionRolFamilia(rol.ID, familia.ID);
            rol.Remover(familia);
        }

        public void AgregarPermisoARol(Familia_54CS rol, Permiso_54CS permiso)
        {
            if (rol == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar un Rol."));
            if (permiso == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar un Permiso."));

            if (rol.TienePermiso(permiso.Nombre))
                throw new InvalidOperationException(string.Format(IdiomaManager_54CS.TraducirMensaje("El permiso '{0}' ya existe en el rol '{1}'."), permiso.Nombre, rol.Nombre));

            _mpp.InsertarRelacionRolPermiso(rol.ID, permiso.ID);
            rol.Agregar(permiso);
        }

        public void QuitarPermisoDeRol(Familia_54CS rol, Permiso_54CS permiso)
        {
            if (rol == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar un Rol."));
            if (permiso == null)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Debe seleccionar un Permiso."));

            if (!rol.ObtenerHijos().Any(h => h is Permiso_54CS && h.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(string.Format(IdiomaManager_54CS.TraducirMensaje("El permiso '{0}' no es un permiso suelto directo del rol '{1}'."), permiso.Nombre, rol.Nombre));

            if (rol.ObtenerHijos().Count <= 1)
                throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("No se puede eliminar el único elemento del Rol"));

            _mpp.EliminarRelacionRolPermiso(rol.ID, permiso.ID);
            rol.Remover(permiso);
        }

        public void EliminarRol(int idRol)
        {
            if (_mpp.ExisteRolEnUso(idRol))
            {
                throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("No se puede eliminar el Rol porque actualmente hay usuarios que lo tienen asignado"));
            }

            _mpp.EnTransaccion(() =>
            {
                if (_mpp.ExisteRolEnUso(idRol)) throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("El rol está en uso."));
                _mpp.EliminarRelacionesDeRol(idRol);
                if (!_mpp.EliminarRol(idRol)) throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("El rol ya no existe."));
            });
        }

        public void ActualizarRolesDeUsuario(int idUsuario, List<Rol_54CS> rolesNuevos)
        {
            if (idUsuario <= 0)
                throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Identificador de usuario inválido"));

            _mpp.EnTransaccion(() =>
            {
                _mpp.EliminarRolesDeUsuario(idUsuario);

                // si la lista no está vacía, insertamos los nuevos
                if (rolesNuevos != null && rolesNuevos.Any())
                {
                    foreach (var rol in rolesNuevos)
                    {
                        _mpp.AsignarRolAUsuario(idUsuario, rol.ID);
                    }
                }
            });
        }
    }
}
