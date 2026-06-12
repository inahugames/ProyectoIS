using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPermisos
    {
        Conexion_54CS conexionSQL = new Conexion_54CS();
        public DataTable ObtenerPermisos()
        {
            return conexionSQL.Leer("SELECT * FROM Permisos_54CS");
        }
    }
}
