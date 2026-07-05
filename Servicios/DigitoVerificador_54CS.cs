using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    // Dígitos verificadores calculados para una tabla de la Base de Datos.
    public class DVTabla_54CS
    {
        public string NombreTabla_54CS { get; set; }
        public long DVH_54CS { get; set; }
        public long DVV_54CS { get; set; }
    }

    // "OBJETO DV": representa el Dígito Verificador de toda la Base de Datos.
    // Contiene el detalle por tabla y los totales (DVH y DVV de la BD) que
    // son los valores que se persisten en la tabla especial DV (DV_54CS).
    public class DigitoVerificadorBD_54CS
    {
        public List<DVTabla_54CS> Tablas_54CS { get; set; } = new List<DVTabla_54CS>();
        public long DVHBaseDatos_54CS { get; set; }
        public long DVVBaseDatos_54CS { get; set; }
    }
}
