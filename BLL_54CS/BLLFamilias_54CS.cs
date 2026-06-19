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
    }
}
