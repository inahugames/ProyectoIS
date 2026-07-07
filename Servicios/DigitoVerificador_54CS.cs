using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class DVTabla_54CS
    {
        public string NombreTabla_54CS { get; set; }
        public long DVH_54CS { get; set; }
        public long DVV_54CS { get; set; }
    }

    public class DigitoVerificadorBD_54CS
    {
        public List<DVTabla_54CS> Tablas_54CS { get; set; } = new List<DVTabla_54CS>();
        public long DVHBaseDatos_54CS { get; set; }
        public long DVVBaseDatos_54CS { get; set; }
    }
}
