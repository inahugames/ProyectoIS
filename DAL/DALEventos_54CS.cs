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
            string query = $"INSERT INTO Eventos_54CS(Login_54CS,Fecha_54CS,Hora_54CS,Modulo_54CS,Evento_54CS,Criticidad_54CS) VALUES ('{eventito.Login_74CS}','{eventito.Fecha_74CS}','{eventito.Hora_74CS}','{eventito.Modulo_74CS}','{eventito.Evento_74CS}','{eventito.Criticidad_74CS}')";
            return conexionSQL.Escribir(query);
        }
    }
}
