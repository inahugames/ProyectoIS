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
                    Login_54CS = row["Login_54CS"].ToString().Trim(),
                    Fecha_54CS = Convert.ToDateTime(row["Fecha_54CS"]),
                    //Hora_54CS = (row["Hora_54CS"]),
                    Modulo_54CS = row["Modulo_54CS"].ToString().Trim(),
                    Evento_54CS = row["Evento_54CS"].ToString().Trim(),
                    Criticidad_54CS = row["Criticidad_54CS"].ToString().Trim()
                    
                };
                ListaEventos.Add(evento);
            }
            return ListaEventos;
        }

        public bool GuardarEvento(Eventos_54CS eventito)
        {
            return DAL.GuardarEvento(eventito) > 0;
        }

    }
}
