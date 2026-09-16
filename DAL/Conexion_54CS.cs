using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Conexion_54CS
    {
        internal static string Cadena => System.Configuration.ConfigurationManager.ConnectionStrings["BDProyecto"]?.ConnectionString
            ?? "Server=.;DataBase=BDProyecto;Integrated Security=True";
        [ThreadStatic] private static SqlConnection conexionActual;
        [ThreadStatic] private static SqlTransaction transaccionActual;

        public static void EnTransaccion(Action accion)
        {
            if (transaccionActual != null) { accion(); return; }
            if (SessionManager_54CS.IntegridadComprometida)
                throw new InvalidOperationException(IdiomaManager_54CS.TraducirMensaje("Escritura bloqueada: se detectó una inconsistencia de datos pendiente de reparación."));
            using (var conexion = new SqlConnection(Cadena))
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                {
                    conexionActual = conexion;
                    transaccionActual = transaccion;
                    try { accion(); transaccion.Commit(); }
                    finally { transaccionActual = null; conexionActual = null; }
                }
            }
            DALDigitoVerificador_54CS.RecalcularYPersistir();
        }

        private static SqlCommand Comando(string query, SqlConnection conexion, Dictionary<string, object> parametros)
        {
            var comando = new SqlCommand(query, conexion, transaccionActual);
            if (parametros != null)
                foreach (var p in parametros)
                    comando.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
            return comando;
        }

        public DataTable Leer(string query, Dictionary<string, object> parametros = null, bool StoredProcedure = false)
        {
            using (var propia = conexionActual == null ? new SqlConnection(Cadena) : null)
            using (var comando = Comando(query, conexionActual ?? propia, parametros))
            {
                if (StoredProcedure) comando.CommandType = CommandType.StoredProcedure;
                if (propia != null) propia.Open();
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    var tabla = new DataTable();
                    adaptador.Fill(tabla);
                    return tabla;
                }
            }
        }

        public int Escribir(string query, Dictionary<string, object> parametros = null)
        {
            int filas = 0;
            EnTransaccion(() =>
            {
                using (var comando = Comando(query, conexionActual, parametros))
                    filas = comando.ExecuteNonQuery();
            });
            return filas;
        }

        public int InsertarId(string query, Dictionary<string, object> parametros)
        {
            int id = 0;
            EnTransaccion(() =>
            {
                using (var comando = Comando(query, conexionActual, parametros))
                    id = Convert.ToInt32(comando.ExecuteScalar());
            });
            return id;
        }
    }
}
