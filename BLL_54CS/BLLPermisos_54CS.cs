using MPP;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_54CS
{
    public class BLLPermisos_54CS
    {
        private readonly MPPPermisos_54CS _mpp;

        public BLLPermisos_54CS()
        {
            _mpp = new MPPPermisos_54CS();
        }

        // ==========================================
        // MÉTODOS DE LECTURA (Consultas para la UI)
        // ==========================================

        /// <summary>
        /// Obtiene todos los Roles principales completamente ensamblados.
        /// Ideal para la pestaña de "Asignación a Usuarios".
        /// </summary>
        public List<Rol_54CS> ObtenerRolesDelSistema()
        {
            return _mpp.ObtenerArbolDeRolesCompleto();
        }

        /// <summary>
        /// Obtiene las Familias ensambladas y los Permisos sueltos combinados en una sola lista.
        /// Ideal para llenar el CheckedListBox al momento de crear un NUEVO ROL.
        /// </summary>
        public List<Rol_54CS> ObtenerElementosParaCrearRol()
        {
            var elementosDisponibles = new List<Rol_54CS>();
            elementosDisponibles.AddRange(_mpp.ObtenerFamiliasEnsambladas());
            elementosDisponibles.AddRange(_mpp.ObtenerPermisosSueltos());

            return elementosDisponibles;
        }

        /// <summary>
        /// Obtiene solo los permisos individuales.
        /// Ideal para llenar el CheckedListBox al momento de crear una NUEVA FAMILIA.
        /// </summary>
        public List<Rol_54CS> ObtenerPermisosParaCrearFamilia()
        {
            return _mpp.ObtenerPermisosSueltos();
        }

        // ==========================================
        // MÉTODOS DE ESCRITURA (Creación de entidades)
        // ==========================================

        /// <summary>
        /// Valida y guarda un nuevo Rol en la base de datos junto con sus jerarquías.
        /// </summary>
        public void CrearRol(Familia_54CS nuevoRol)
        {
            // 1. Validaciones de Negocio
            if (string.IsNullOrWhiteSpace(nuevoRol.Nombre))
                throw new ArgumentException("El nombre del Rol no puede estar vacío.");

            var hijos = nuevoRol.ObtenerHijos();
            if (hijos.Count == 0)
                throw new InvalidOperationException("No se puede crear un Rol vacío. Debe contener al menos una familia o permiso.");

            // Opcional: Validar que el nombre no exista ya en la BD (requeriría un método extra en la DAL)

            // 2. Insertar el registro principal en la tabla Rol y obtener su nuevo ID
            int idRolGenerado = _mpp.InsertarRol(nuevoRol.Nombre);

            // 3. Iterar sobre los hijos y guardar las relaciones en las tablas intermedias
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

        /// <summary>
        /// Valida y guarda una nueva Familia en la base de datos junto con sus permisos internos.
        /// </summary>
        /// <param name="nuevaFamilia">Objeto familia instanciado</param>
        /// <param name="descripcion">Texto descriptivo de la familia</param>
        public void CrearFamilia(Familia_54CS nuevaFamilia, string descripcion)
        {
            // 1. Validaciones de Negocio
            if (string.IsNullOrWhiteSpace(nuevaFamilia.Nombre))
                throw new ArgumentException("El nombre de la Familia no puede estar vacío.");

            var hijos = nuevaFamilia.ObtenerHijos();
            if (hijos.Count == 0)
                throw new InvalidOperationException("Una Familia debe contener al menos un permiso.");

            // 2. Insertar la Familia principal
            int idFamiliaGenerada = _mpp.InsertarFamilia(nuevaFamilia.Nombre, descripcion);

            // 3. Insertar las relaciones (Una familia solo contiene permisos en este esquema de BD)
            foreach (var hijo in hijos)
            {
                if (hijo is Permiso_54CS)
                {
                    _mpp.InsertarRelacionFamiliaPermiso(idFamiliaGenerada, hijo.ID);
                }
                else
                {
                    // Regla de seguridad estricta para evitar inconsistencias de esquema
                    throw new InvalidOperationException("El esquema actual define que una Familia solo puede contener permisos directos.");
                }
            }
        }

        // ==========================================
        // MÉTODOS DE ELIMINACIÓN
        // ==========================================

        /// <summary>
        /// Intenta eliminar un Rol del sistema. Si está asignado a un usuario, aborta la operación.
        /// </summary>
        public void EliminarRol(int idRol)
        {
            // 1. Regla de Negocio: No se puede borrar un rol si algún usuario lo está usando
            if (_mpp.ExisteRolEnUso(idRol))
            {
                throw new InvalidOperationException("No se puede eliminar el Rol porque actualmente hay usuarios que lo tienen asignado.");
            }

            // 2. Primero eliminamos los registros hijos (las relaciones en las tablas intermedias)
            // Si borramos el padre primero, SQL Server lanzará un error de Foreign Key.
            _mpp.EliminarRelacionesDeRol(idRol);

            // 3. Finalmente, eliminamos el Rol principal
            _mpp.EliminarRol(idRol);
        }

        // ==========================================
        // MÉTODOS DE ASIGNACIÓN A USUARIOS
        // ==========================================

        /// <summary>
        /// Actualiza los roles de un usuario en la base de datos.
        /// </summary>
        public void ActualizarRolesDeUsuario(int idUsuario, List<Rol_54CS> rolesNuevos)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("Identificador de usuario inválido.");

            // 1. Limpiamos los roles anteriores (Es más seguro borrar y recrear que buscar diferencias)
            _mpp.EliminarRolesDeUsuario(idUsuario);

            // 2. Si la lista no está vacía, insertamos los nuevos
            if (rolesNuevos != null && rolesNuevos.Any())
            {
                foreach (var rol in rolesNuevos)
                {
                    // Validamos que estemos asignando Roles principales y no permisos sueltos
                    _mpp.AsignarRolAUsuario(idUsuario, rol.ID);
                }
            }
        }
    }
}
