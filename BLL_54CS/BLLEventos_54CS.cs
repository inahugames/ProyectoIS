using MPP;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL_54CS
{
    public class BLLEventos_54CS
    {
        MPPEventos_54CS MPPeventos = new MPPEventos_54CS();

        public List<Eventos_54CS> ObtenerTodos() => MPPeventos.ObtenerEventos();

        public bool GuardarEvento(Eventos_54CS evento, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPeventos.GuardarEvento(evento);
                if (!resultado)
                {
                    mensaje = "No se guardo correctamente";
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error";
                return false;
            }
        }

        public bool EliminarEvento(Eventos_54CS evento,out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                bool resultado = MPPeventos.EliminarEvento(evento);
                if (!resultado)
                {
                    mensaje = "No se elimino correctamente";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error";
                return false;
            }
        }
    }
}
