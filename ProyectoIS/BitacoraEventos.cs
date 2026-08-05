using BLL_54CS;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIS
{
    public partial class BitacoraEventos : Form, IIdiomaObservador_54CS
    {
        private readonly BLLUsuarios_54CS _bllUsuarios = new BLLUsuarios_54CS();
        private List<Usuario_54CS> _usuarios = new List<Usuario_54CS>();
        private bool valido = true;

        public BitacoraEventos()
        {
            InitializeComponent();
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            try { _usuarios = _bllUsuarios.ObtenerTodos(); }
            catch { _usuarios = new List<Usuario_54CS>(); }

            Actualizar();
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFiltraCrit_Click(object sender, EventArgs e)
        {
        }

        private void btnFiltraFecha_Click(object sender, EventArgs e)
        {
        }

        private void btnFiltraLogin_Click(object sender, EventArgs e)
        {
            if ( valido == false )
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Ingrese un rango de fechas válido."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            if ( comboCriticidad.Text != "" || txtLogin.Text != "" || fechaPickerInicio.Text != "" || comboMódulo.Text != "")
            {
                string filtroCriticidad = comboCriticidad.Text.ToLower();
                string filtroLogin = txtLogin.Text.ToLower();
                DateTime filtroFechaInicio = fechaPickerInicio.Value.Date;
                DateTime filtroFechaFin = fechaPickerFin.Value.Date.AddDays(1).AddTicks(-1); // añado un dia y le resto un segundo para cubrir hasta las 23:59:59 del dia seleccionado
                string filtroModulo = comboMódulo.Text.ToLower();
                BLLEventos_54CS blle = new BLLEventos_54CS();
                List<Eventos_54CS> lista = blle.ObtenerTodos();
                IEnumerable<Eventos_54CS> consulta = lista;
                
                if (string.IsNullOrEmpty(filtroCriticidad) == false)
                {
                    consulta = consulta.Where(ev => ev.Criticidad_54CS.ToLower().Contains(filtroCriticidad.ToLower()));
                }
                if (string.IsNullOrEmpty(filtroLogin) == false)
                {
                    consulta = consulta.Where(ev => ev.Login_54CS.ToLower().Contains(filtroLogin.ToLower()));
                }
                if (fechaPickerInicio.Checked && fechaPickerFin.Checked)
                {
                    if (string.IsNullOrEmpty(filtroFechaInicio.ToString()) == false && string.IsNullOrEmpty(filtroFechaFin.ToString()) == false)
                    {
                        consulta = consulta.Where(evento => evento.Fecha_54CS >= filtroFechaInicio && evento.Fecha_54CS <= filtroFechaFin);
                    }
                }
                if (string.IsNullOrEmpty(filtroModulo) == false)
                {
                    consulta = consulta.Where(ev => ev.Modulo_54CS.ToLower().Contains(filtroModulo.ToLower()));
                }
                List<Eventos_54CS> filtrados = consulta.ToList();
                dgvEventos.Rows.Clear();
                foreach (Eventos_54CS evento in filtrados)
                {
                    dgvEventos.Rows.Add(evento.Login_54CS, evento.Fecha_54CS, evento.Modulo_54CS, evento.Evento_54CS, evento.Criticidad_54CS);
                }
            }
        }

        private void btnCancelarFiltros_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            dgvEventos.Rows.Clear();
            BLLEventos_54CS blle = new BLLEventos_54CS();
            List<Eventos_54CS> lista = blle.ObtenerTodos();
            foreach (Eventos_54CS v in lista)
            {
                dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS, v.Modulo_54CS, v.Evento_54CS, v.Criticidad_54CS);
            }
        }

        private void dgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            MostrarResponsableSeleccionado();
        }

        private void MostrarResponsableSeleccionado()
        {
            txtNombreResponsable.Text = string.Empty;
            txtApellidoResponsable.Text = string.Empty;

            if (_usuarios == null) { return; }

            DataGridViewRow fila = dgvEventos.CurrentRow;
            if (fila == null || fila.IsNewRow)
                return;

            object valorLogin = fila.Cells["Login"].Value;
            string login = valorLogin == null ? null : valorLogin.ToString();
            if (string.IsNullOrWhiteSpace(login))
                return;

            Usuario_54CS us;

            foreach (Usuario_54CS user in _usuarios)
            {
                try
                {
                    if (user.Login_54CS.Trim() == login)
                    {
                        txtNombreResponsable.Text = user.Nombre_54CS.Trim();
                        txtApellidoResponsable.Text = user.Apellido_54CS.Trim();
                        return;
                    }
                    else
                    {
                        txtNombreResponsable.Text = "Usuario no encontrado";
                        txtApellidoResponsable.Text = "Usuario no encontrado";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Error: {0}"), ex.Message), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            if (valido == false)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Ingrese un rango de fechas válido."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var encabezados = new List<string>();
            foreach (DataGridViewColumn col in dgvEventos.Columns)
                encabezados.Add(col.HeaderText);

            var filas = new List<string[]>();
            foreach (DataGridViewRow fila in dgvEventos.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                var celdas = new string[dgvEventos.Columns.Count];
                for (int c = 0; c < dgvEventos.Columns.Count; c++)
                {
                    object valor = fila.Cells[c].Value;
                    celdas[c] = valor == null ? string.Empty : valor.ToString();
                }
                filas.Add(celdas);
            }

            if (filas.Count == 0)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No hay eventos para exportar."), IdiomaManager_54CS.TraducirMensaje("Exportar a PDF"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // encabezado del pdf
            var info = new List<string>();
            info.Add("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            List<string> filtros = DescribirFiltrosActivos();
            info.Add(filtros.Count > 0
                ? "Filtros aplicados: " + string.Join("   |   ", filtros)
                : "Filtros aplicados: ninguno (todos los eventos)");
            info.Add("Total de eventos: " + filas.Count);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                sfd.FileName = "Bitacora_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                sfd.Title = "Exportar bitácora a PDF";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var anchos = new List<float> { 1.4f, 1.6f, 1.6f, 2.6f, 1.0f };
                    while (anchos.Count < encabezados.Count) anchos.Add(1f);
                    if (anchos.Count > encabezados.Count) anchos = anchos.GetRange(0, encabezados.Count);

                    PdfBitacoraExporter_54CS.Exportar(
                        sfd.FileName,
                        "Bitácora de Eventos",
                        info,
                        encabezados,
                        anchos,
                        filas);

                    var abrir = MessageBox.Show(
                        IdiomaManager_54CS.TraducirMensaje("PDF exportado correctamente en:") + Environment.NewLine + sfd.FileName +
                        Environment.NewLine + Environment.NewLine + IdiomaManager_54CS.TraducirMensaje("¿Desea abrirlo ahora?"),
                        IdiomaManager_54CS.TraducirMensaje("Exportación exitosa"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (abrir == DialogResult.Yes)
                    {
                        try { System.Diagnostics.Process.Start(sfd.FileName); }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No se pudo exportar el PDF: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<string> DescribirFiltrosActivos()
        {
            var filtros = new List<string>();

            if (!string.IsNullOrWhiteSpace(comboCriticidad.Text))
                filtros.Add("Criticidad = " + comboCriticidad.Text);

            if (!string.IsNullOrWhiteSpace(txtLogin.Text))
                filtros.Add("Login contiene \"" + txtLogin.Text + "\"");

            if (!string.IsNullOrWhiteSpace(comboMódulo.Text))
                filtros.Add("Módulo = " + comboMódulo.Text);

            if (fechaPickerInicio.Checked && fechaPickerFin.Checked)
                filtros.Add("Fecha entre " + fechaPickerInicio.Value.ToString("dd/MM/yyyy") +
                            " y " + fechaPickerFin.Value.ToString("dd/MM/yyyy"));

            return filtros;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fechaPickerFin_ValueChanged(object sender, EventArgs e)
        {
            if (fechaPickerFin.Value < fechaPickerInicio.Value)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Ingrese un rango de fechas válido."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
                fechaPickerFin.Value = DateTime.Today;
            }
            else
            {
                valido = true;
            }
        }

        private void fechaPickerInicio_ValueChanged(object sender, EventArgs e)
        {
            if (fechaPickerFin.Value < fechaPickerInicio.Value)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Ingrese un rango de fechas válido."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
                fechaPickerInicio.Value = DateTime.Today;
            }
            else
            {
                valido = true;
            }
        }
    }
}
