using BLL_54CS;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProyectoIS
{
    public partial class CambiarIdioma : Form, IIdiomaObservador_54CS
    {
        // Clase auxiliar para poder mostrar un nombre amigable en el combo
        // (ej: "English") mientras guardamos por dentro el código real (ej: "en").
        private class ItemIdioma
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
        }

        public CambiarIdioma()
        {
            InitializeComponent();
            IdiomaManager_54CS.Suscribir(this); // 2.1 - Observer: nos suscribimos para traducirnos en caliente
            CargarIdiomasDisponibles();
        }

        // 2.1 - Observer: este formulario también se traduce solo si el idioma
        // se llega a cambiar desde otra ventana mientras esta está abierta.
        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void CargarIdiomasDisponibles()
        {
            // 2.2 - GUI: ventana sencilla con lista desplegable armada a partir de
            // los archivos de idioma disponibles en la carpeta Idiomas (es.json, en.json, etc.)
            var codigos = IdiomaManager_54CS.ObtenerIdiomasDisponibles();
            var items = codigos
                .Select(c => new ItemIdioma { Codigo = c, Nombre = IdiomaManager_54CS.ObtenerNombreAmigable(c) })
                .ToList();

            cmbIdiomas.DisplayMember = "Nombre";
            cmbIdiomas.ValueMember = "Codigo";
            cmbIdiomas.DataSource = items;

            string actual = IdiomaManager_54CS.IdiomaActual;
            var seleccionado = items.FirstOrDefault(i => i.Codigo == actual);
            if (seleccionado != null)
            {
                cmbIdiomas.SelectedItem = seleccionado;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbIdiomas.SelectedItem == null)
            {
                MessageBox.Show(
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "MsjSeleccione", "Debe seleccionar un idioma."),
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "TituloAviso", "Aviso"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idiomaAnterior = IdiomaManager_54CS.IdiomaActual;
            string idiomaSeleccionado = ((ItemIdioma)cmbIdiomas.SelectedItem).Codigo;

            if (idiomaSeleccionado == idiomaAnterior)
            {
                this.Close();
                return;
            }

            // 2.3 - Cambiar el idioma: aplica el cambio en caliente y notifica (Observer)
            // a todos los formularios abiertos en ese momento, sin reiniciar el programa.
            IdiomaManager_54CS.CambiarIdioma(idiomaSeleccionado);

            // Guarda la preferencia en la base de datos asociada al usuario logeado, para
            // que la próxima vez que inicie sesión la aplicación recuerde su idioma.
            BLLUsuarios_54CS bllUsuarios = new BLLUsuarios_54CS();
            bool guardadoOk = bllUsuarios.GuardarIdiomaUsuario(SessionManager_54CS.Instancia.Login_54CS, idiomaSeleccionado, out string mensajeError);

            // 2.4 - Ojo: reportar el cambio a la Bitácora
            Eventos_54CS evento = new Eventos_54CS()
            {
                Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                Fecha_54CS = DateTime.Now,
                Modulo_54CS = "Idioma",
                Evento_54CS = $"Cambio de idioma de '{idiomaAnterior}' a '{idiomaSeleccionado}'",
                Criticidad_54CS = "1"
            };
            BLLEventos_54CS bllEventos = new BLLEventos_54CS();
            bllEventos.GuardarEvento(evento, out string mensajeBitacora);

            // 2.5 - Mensajes requeridos
            if (guardadoOk)
            {
                MessageBox.Show(
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "MsjExito", "Idioma cambiado correctamente."),
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "TituloAviso", "Aviso"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "MsjErrorGuardado", "El idioma se aplicó, pero no se pudo guardar la preferencia para la próxima sesión.") + " " + mensajeError,
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "TituloError", "Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
