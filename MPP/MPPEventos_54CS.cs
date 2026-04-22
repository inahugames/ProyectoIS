using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPEventos_54CS
    {
        private DALEventos_54CS DAL = new DALEventos_54CS();
        public List<Eventos_54CS> ObtenerEventos()
        {
            List<Eventos_54CS> ListaEventos = new List<Eventos_54CS>();
            DataTable dt = DAL.ObtenerEventos();
            foreach ( DataRow row in dt.Rows)
            {
                Eventos_54CS evento = new Eventos_54CS()
                {
                    Login_54CS = row["Login_54CS"].ToString(),
                    Fecha_54CS = row["Fecha_54CS"].ToString(),
                    Hora_54CS = row["Hora_54CS"].ToString(),
                    Modulo_54CS = row["Modulo_54CS"].ToString(),
                    Evento_54CS = row["Evento_54CS"].ToString(),
                    Criticidad_54CS = row["Criticidad_54CS"].ToString()
                };
            }
            return ListaEventos;
        }

        public bool GuardarEvento(Eventos_54CS eventito)
        {
            return DAL.GuardarEvento(eventito) > 0;
        }

    }
}
