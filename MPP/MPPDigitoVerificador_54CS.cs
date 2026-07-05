using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPDigitoVerificador_54CS
    {
        // GENERACIÓN sin persistir: OBJETO DV en memoria.
        public DigitoVerificadorBD_54CS GenerarObjetoDV()
        {
            return DALDigitoVerificador_54CS.GenerarObjetoDV();
        }

        // GENERACIÓN completa: recalcula y guarda el DV en la tabla DV.
        public DigitoVerificadorBD_54CS RecalcularDV()
        {
            return DALDigitoVerificador_54CS.RecalcularYPersistir();
        }

        // REVISIÓN: SELECT sobre la tabla DV (null si nunca se generó).
        public DigitoVerificadorBD_54CS LeerDVPersistido()
        {
            return DALDigitoVerificador_54CS.LeerDVPersistido();
        }

        public void RealizarBackup(string rutaArchivo)
        {
            new DALBackup_54CS().RealizarBackup(rutaArchivo);
        }

        public void RestaurarBackup(string rutaArchivo)
        {
            new DALBackup_54CS().RestaurarBackup(rutaArchivo);
        }
    }
}
