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
        /// Obtiene solo los permisos individuales.
        /// Ideal para llenar el CheckedListBox al momento de crear una NUEVA FAMILIA.
        /// </summary>
        public List<Rol_54CS> ObtenerPermisosParaCrearFamilia()
        {
            return _mpp.ObtenerPermisosSueltos();
        }
    }
}
