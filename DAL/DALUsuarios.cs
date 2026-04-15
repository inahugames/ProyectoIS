using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUsuarios
    {
        private Conexion conexionSQL = new Conexion();
        public DataTable ObtenerUsuarios()
        {
            string query = "SELECT * FROM Usuarios_0724";
            return conexionSQL.Leer(query);
        }
    }
}
