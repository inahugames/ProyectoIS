using MPP;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_54CS
{
    public class BLLDigitoVerificador_54CS
    {
        MPPDigitoVerificador_54CS MPPdv = new MPPDigitoVerificador_54CS();

        // REVISIÓN: se ejecuta antes de cada login.
        // 1. Genera el OBJETO DV igual que en la GENERACIÓN, pero sin persistirlo.
        // 2. Consulta la tabla DV de la BD a través de un SELECT.
        // 3. Compara el DVH y el DVV del objeto generado con los consultados.
        // Devuelve true si los datos son consistentes. Si no lo son, "detalle"
        // describe qué valores no coinciden y en qué tablas.
        public bool VerificarConsistencia(out string detalle)
        {
            detalle = string.Empty;

            DigitoVerificadorBD_54CS generado = MPPdv.GenerarObjetoDV();
            DigitoVerificadorBD_54CS persistido = MPPdv.LeerDVPersistido();

            if (persistido == null)
            {
                // Primera ejecución del sistema: la tabla DV está vacía, se
                // inicializa con los valores actuales y se continúa normalmente.
                MPPdv.RecalcularDV();
                return true;
            }

            bool consistente = generado.DVHBaseDatos_54CS == persistido.DVHBaseDatos_54CS
                            && generado.DVVBaseDatos_54CS == persistido.DVVBaseDatos_54CS;
            if (!consistente)
            {
                detalle = ArmarDetalleInconsistencia(generado, persistido);
            }
            return consistente;
        }

        // REPARACIÓN - RECALCULAR EL DV: fuerza la GENERACIÓN del DV tal cual
        // ocurre en cada persistencia. No resuelve la inconsistencia: la acepta
        // como nuevo estado válido de los datos.
        public void RecalcularDV()
        {
            MPPdv.RecalcularDV();
        }

        // Estado actual (recalculado en memoria) y estado almacenado, para las
        // pantallas de administración.
        public DigitoVerificadorBD_54CS ObtenerDVGenerado()
        {
            return MPPdv.GenerarObjetoDV();
        }

        public DigitoVerificadorBD_54CS ObtenerDVPersistido()
        {
            return MPPdv.LeerDVPersistido();
        }

        public bool RealizarBackup(string rutaArchivo, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                MPPdv.RealizarBackup(rutaArchivo);
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        // REPARACIÓN - RESTORE BD
        public bool RestaurarBackup(string rutaArchivo, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                MPPdv.RestaurarBackup(rutaArchivo);
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        private string ArmarDetalleInconsistencia(DigitoVerificadorBD_54CS generado, DigitoVerificadorBD_54CS persistido)
        {
            StringBuilder detalle = new StringBuilder();
            detalle.AppendLine($"DVH de la BD -> Almacenado: {persistido.DVHBaseDatos_54CS} | Calculado: {generado.DVHBaseDatos_54CS}");
            detalle.AppendLine($"DVV de la BD -> Almacenado: {persistido.DVVBaseDatos_54CS} | Calculado: {generado.DVVBaseDatos_54CS}");
            detalle.AppendLine();

            foreach (DVTabla_54CS tabla in generado.Tablas_54CS)
            {
                DVTabla_54CS almacenada = persistido.Tablas_54CS.FirstOrDefault(t => t.NombreTabla_54CS == tabla.NombreTabla_54CS);
                if (almacenada == null)
                {
                    detalle.AppendLine($"Tabla {tabla.NombreTabla_54CS}: sin DV almacenado (tabla nueva o fila DV eliminada).");
                }
                else if (almacenada.DVH_54CS != tabla.DVH_54CS || almacenada.DVV_54CS != tabla.DVV_54CS)
                {
                    detalle.AppendLine($"Tabla {tabla.NombreTabla_54CS}: DVH Almacenado {almacenada.DVH_54CS} / Calculado {tabla.DVH_54CS} - DVV Almacenado {almacenada.DVV_54CS} / Calculado {tabla.DVV_54CS}");
                }
            }
            foreach (DVTabla_54CS almacenada in persistido.Tablas_54CS)
            {
                if (!generado.Tablas_54CS.Any(t => t.NombreTabla_54CS == almacenada.NombreTabla_54CS))
                {
                    detalle.AppendLine($"Tabla {almacenada.NombreTabla_54CS}: tiene DV almacenado pero ya no existe en la BD.");
                }
            }
            return detalle.ToString();
        }
    }
}
