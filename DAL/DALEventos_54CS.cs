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
            string query = $"SET DATEFORMAT dmy; INSERT INTO Eventos_54CS(Login_54CS,Fecha_54CS,Hora_54CS,Modulo_54CS,Evento_54CS,Criticidad_54CS) VALUES ('{eventito.Login_54CS}','{Convert.ToString(eventito.Fecha_54CS)}','{eventito.Hora_54CS}','{eventito.Modulo_54CS}','{eventito.Evento_54CS}','{eventito.Criticidad_54CS}')";
            return conexionSQL.Escribir(query);
        }
    }
}
