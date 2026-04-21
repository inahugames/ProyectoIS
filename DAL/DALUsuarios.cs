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
        public int EditarUsuario(string usuario,bool bloqueo)
        {
            string query = $"UPDATE Usuarios_0724 SET Block_0724=@Block WHERE Login_0724 = @Login";

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { "@Block", bloqueo},
                {"@Login",usuario }
            };
            return conexionSQL.Escribir(query, parametros);
        }
    }
}
