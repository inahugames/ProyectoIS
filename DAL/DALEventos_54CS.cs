using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALEventos_54CS
    {
        private Conexion_54CS conexionSQL = new Conexion_54CS();

        public DataTable ObtenerEventos()
        {
            string query = "SELECT * FROM Eventos_54CS";
            return conexionSQL.Leer(query);
        }

        public int GuardarEvento( Eventos_54CS eventito )
        {
            // el dateformat es para evitar errores con 
            const string query = "SET DATEFORMAT dmy; INSERT INTO Eventos_54CS(Login_54CS,Fecha_54CS,Modulo_54CS,Evento_54CS,Criticidad_54CS) VALUES (@Login,@Fecha,@Modulo,@Evento,@Criticidad)";
            return conexionSQL.Escribir(query, new Dictionary<string, object>
            {
                { "@Login", eventito.Login_54CS }, { "@Fecha", eventito.Fecha_54CS },
                { "@Modulo", eventito.Modulo_54CS }, { "@Evento", eventito.Evento_54CS },
                { "@Criticidad", eventito.Criticidad_54CS }
            });
        }

        public int EliminarEvento ( Eventos_54CS evento)
        {
            const string query = "DELETE FROM Eventos_54CS WHERE Login_54CS=@Login AND Evento_54CS=@Evento";
            return conexionSQL.Escribir(query, new Dictionary<string, object>
            {
                { "@Login", evento.Login_54CS }, { "@Evento", "Contraseña Errónea" }
            });
        }
    }
}
