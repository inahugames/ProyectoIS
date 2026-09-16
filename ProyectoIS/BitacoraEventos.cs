using System.Drawing.Drawing2D;
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
        private string criticidadAplicada = "", loginAplicado = "", moduloAplicado = "";
        private DateTime? inicioAplicado, finAplicado;

        public BitacoraEventos()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            DistribuirVistaBitacora();
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            try { _usuarios = _bllUsuarios.ObtenerTodos(); }
            catch { _usuarios = new List<Usuario_54CS>(); }

            Actualizar();
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosBitacora();
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
                criticidadAplicada = comboCriticidad.Text;
                loginAplicado = txtLogin.Text;
                moduloAplicado = comboMódulo.Text;
                inicioAplicado = fechaPickerInicio.Checked && fechaPickerFin.Checked ? (DateTime?)filtroFechaInicio : null;
                finAplicado = inicioAplicado.HasValue ? (DateTime?)fechaPickerFin.Value.Date : null;
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
            txtLogin.Text = string.Empty;
            fechaPickerFin.Value = DateTime.Today;
            fechaPickerInicio.Value = DateTime.Today;
            comboCriticidad.Text = string.Empty;
            comboMódulo.Text = string.Empty;
        }

        private void Actualizar()
        {
            criticidadAplicada = loginAplicado = moduloAplicado = "";
            inicioAplicado = finAplicado = null;
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
                        txtNombreResponsable.Text = IdiomaManager_54CS.TraducirMensaje("Usuario no encontrado");
                        txtApellidoResponsable.Text = IdiomaManager_54CS.TraducirMensaje("Usuario no encontrado");
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
            if (!SessionManager_54CS.Instancia.TienePermiso("VerBitacora"))
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."), IdiomaManager_54CS.TraducirMensaje("Error"));
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
            info.Add(IdiomaManager_54CS.TraducirMensaje("Generado: ") + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            List<string> filtros = DescribirFiltrosActivos();
            info.Add(filtros.Count > 0
                ? IdiomaManager_54CS.TraducirMensaje("Filtros aplicados: ") + string.Join("   |   ", filtros)
                : IdiomaManager_54CS.TraducirMensaje("Filtros aplicados: ninguno (todos los eventos)"));
            info.Add(IdiomaManager_54CS.TraducirMensaje("Total de eventos: ") + filas.Count);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                sfd.FileName = "Bitacora_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                sfd.Title = IdiomaManager_54CS.TraducirMensaje("Exportar bitácora a PDF");

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var anchos = new List<float> { 1.4f, 1.6f, 1.6f, 2.6f, 1.0f };
                    while (anchos.Count < encabezados.Count) anchos.Add(1f);
                    if (anchos.Count > encabezados.Count) anchos = anchos.GetRange(0, encabezados.Count);

                    PdfBitacoraExporter_54CS.Exportar(
                        sfd.FileName,
                        IdiomaManager_54CS.TraducirMensaje("Bitácora de Eventos"),
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
            if (!string.IsNullOrWhiteSpace(criticidadAplicada))
                filtros.Add(IdiomaManager_54CS.TraducirMensaje("Criticidad = ") + criticidadAplicada);
            if (!string.IsNullOrWhiteSpace(loginAplicado))
                filtros.Add(IdiomaManager_54CS.TraducirMensaje("Login contiene: ") + loginAplicado);
            if (!string.IsNullOrWhiteSpace(moduloAplicado))
                filtros.Add(IdiomaManager_54CS.TraducirMensaje("Módulo = ") + moduloAplicado);
            if (inicioAplicado.HasValue)
                filtros.Add(string.Format(IdiomaManager_54CS.TraducirMensaje("Fecha entre {0} y {1}"),
                    inicioAplicado.Value.ToString("dd/MM/yyyy"), finAplicado.Value.ToString("dd/MM/yyyy")));
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

        private UsuariosRoundedPanel[] camposBitacora => new[] { campoLoginBitacora, campoModuloBitacora, campoCriticidadBitacora, campoInicioBitacora, campoFinBitacora, campoNombreBitacora, campoApellidoBitacora };
        private readonly Font fuenteTablaBitacora = new Font("Segoe UI", 10F);
        private readonly Font fuenteEncabezadoBitacora = new Font("Segoe UI", 10F, FontStyle.Bold);
        private Color AcentoBitacora => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);

        private void temaBitacora_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void campoBitacora_Enter(object sender, EventArgs e)
        {
            var campo = (UsuariosRoundedPanel)((Control)sender).Parent;
            campo.BorderColor = AcentoBitacora;
            campo.Invalidate();
        }

        private void campoBitacora_Leave(object sender, EventArgs e)
        {
            AplicarTemaBitacora();
        }

        private void comboBitacora_DrawItem(object sender, DrawItemEventArgs e)
        {
            var combo = (ComboBox)sender;
            if (e.Index < 0 || e.Index >= combo.Items.Count) return;
            bool selected = (e.State & DrawItemState.Selected) != 0;
            using (var brush = new SolidBrush(selected ? AcentoBitacora : combo.BackColor)) e.Graphics.FillRectangle(brush, e.Bounds);
            TextRenderer.DrawText(e.Graphics, combo.GetItemText(combo.Items[e.Index]), combo.Font, e.Bounds, selected ? Color.Black : ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        private void BitacoraEventos_Resize(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) DistribuirVistaBitacora();
        }

        private void DistribuirVistaBitacora()
        {
            if (filtrosBitacora == null) return;
            int ancho = Math.Max(860, ClientSize.Width - 48);
            btnExportarPDF.SetBounds(ClientSize.Width - 224, 26, 200, 42);
            temaBitacora.Location = new Point(ClientSize.Width - 340, 28);
            tituloBitacora.Width = Math.Max(300, ClientSize.Width - 450);
            filtrosBitacora.SetBounds(24, 108, ancho, 166);
            Control[] inputs = { txtLogin, comboMódulo, comboCriticidad, fechaPickerInicio, fechaPickerFin };
            Label[] labels = { label3, label4, label1, label2, label5 };
            int campoAncho = (ancho - 84) / 5;
            for (int i = 0; i < 5; i++)
            {
                int x = 18 + i * (campoAncho + 12);
                labels[i].SetBounds(x, 43, campoAncho, 24);
                camposBitacora[i].SetBounds(x, 70, campoAncho, 38);
                inputs[i].SetBounds(9, 7, campoAncho - 18, 25);
            }
            btnAplicarFiltros.SetBounds(ancho - 334, 118, 182, 36);
            btnCancelarFiltros.SetBounds(ancho - 140, 118, 122, 36);
            int yResponsable = Math.Max(474, ClientSize.Height - 128);
            tablaBitacora.SetBounds(24, 288, ancho, yResponsable - 302);
            dgvEventos.SetBounds(12, 10, ancho - 24, tablaBitacora.Height - 22);
            responsableBitacora.SetBounds(24, yResponsable, Math.Min(790, ancho - 170), 108);
            lblResponsable.SetBounds(18, 12, responsableBitacora.Width - 36, 24);
            int mitad = (responsableBitacora.Width - 54) / 2;
            lblNombreResp.SetBounds(18, 40, mitad, 23);
            lblApellidoResp.SetBounds(36 + mitad, 40, mitad, 23);
            camposBitacora[5].SetBounds(18, 65, mitad, 32);
            camposBitacora[6].SetBounds(36 + mitad, 65, mitad, 32);
            txtNombreResponsable.SetBounds(9, 6, mitad - 18, 24);
            txtApellidoResponsable.SetBounds(9, 6, mitad - 18, 24);
            btnSalir.SetBounds(ClientSize.Width - 174, yResponsable + 54, 150, 42);
        }

        private void PintarCriticidadBitacora(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvEventos.Columns["Criticidad"].Index) return;
            e.Paint(e.ClipBounds, e.PaintParts & ~DataGridViewPaintParts.ContentForeground);
            string valor = Convert.ToString(e.Value);
            if (string.IsNullOrWhiteSpace(valor)) { e.Handled = true; return; }
            Color color = valor == "1" ? Color.FromArgb(85, 139, 220) : valor == "2" ? Color.FromArgb(231, 180, 72) : valor == "3" ? Color.FromArgb(233, 143, 115) : Color.Silver;
            var rect = new Rectangle(e.CellBounds.X + (e.CellBounds.Width - 48) / 2, e.CellBounds.Y + (e.CellBounds.Height - 24) / 2, 48, 24);
            var estado = e.Graphics.Save();
            e.Graphics.SetClip(Rectangle.Intersect(e.ClipBounds, e.CellBounds));
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = new GraphicsPath())
            using (var brush = new SolidBrush(color))
            {
                path.AddArc(rect.Left, rect.Top, 12, 12, 180, 90);
                path.AddArc(rect.Right - 12, rect.Top, 12, 12, 270, 90);
                path.AddArc(rect.Right - 12, rect.Bottom - 12, 12, 12, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - 12, 12, 12, 90, 90);
                path.CloseFigure();
                e.Graphics.FillPath(brush, path);
                TextRenderer.DrawText(e.Graphics, valor, e.CellStyle.Font, rect, Color.FromArgb(25, 28, 32), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
            e.Graphics.Restore(estado);
            e.Handled = true;
        }

        private void ActualizarTextosBitacora()
        {
            if (temaBitacora == null) return;
            foreach (Label label in new[] { tituloBitacora, subtituloBitacora, tituloFiltrosBitacora }) label.Text = IdiomaManager_54CS.ObtenerTexto("BitacoraEventos", label.Name, label.Name);
            btnCancelarFiltros.Text = IdiomaManager_54CS.ObtenerTexto("BitacoraEventos", "limpiarBitacora", "Limpiar");
            temaBitacora.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro");
            Control[] inputs = { txtLogin, comboMódulo, comboCriticidad, fechaPickerInicio, fechaPickerFin, txtNombreResponsable, txtApellidoResponsable };
            Label[] labels = { label3, label4, label1, label2, label5, lblNombreResp, lblApellidoResp };
            for (int i = 0; i < inputs.Length; i++) inputs[i].AccessibleName = labels[i].Text;
        }

        public void AplicarTemaBitacora()
        {
            if (temaBitacora == null) return;
            bool dark = Tema_54CS.EsOscuro;
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            Color superficie = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color campo = dark ? Color.FromArgb(35, 35, 34) : Color.FromArgb(249, 250, 252);
            Color borde = dark ? Color.FromArgb(74, 74, 70) : Color.FromArgb(219, 223, 231);
            foreach (var tarjeta in new[] { filtrosBitacora, tablaBitacora, responsableBitacora }) { tarjeta.BackColor = superficie; tarjeta.BorderColor = borde; }
            foreach (var tarjeta in camposBitacora)
            {
                tarjeta.BackColor = campo;
                tarjeta.BorderColor = tarjeta.ContainsFocus ? AcentoBitacora : borde;
                foreach (Control input in tarjeta.Controls) { input.BackColor = campo; input.ForeColor = ForeColor; if (input is TextBox txt) txt.BorderStyle = BorderStyle.None; }
            }
            foreach (Label label in new[] { tituloBitacora, subtituloBitacora, tituloFiltrosBitacora, label1, label2, label3, label4, label5, lblResponsable, lblNombreResp, lblApellidoResp }) { label.ForeColor = ForeColor; label.BackColor = Color.Transparent; }
            tituloFiltrosBitacora.ForeColor = AcentoBitacora;
            subtituloBitacora.ForeColor = dark ? Color.Silver : Color.DimGray;
            dgvEventos.BackgroundColor = superficie;
            dgvEventos.GridColor = borde;
            dgvEventos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEventos.DefaultCellStyle.BackColor = superficie;
            dgvEventos.DefaultCellStyle.Font = fuenteTablaBitacora;
            dgvEventos.ColumnHeadersDefaultCellStyle.Font = fuenteEncabezadoBitacora;
            dgvEventos.DefaultCellStyle.ForeColor = ForeColor;
            dgvEventos.AlternatingRowsDefaultCellStyle.BackColor = campo;
            dgvEventos.DefaultCellStyle.SelectionBackColor = dark ? Color.FromArgb(78, 57, 47) : Color.FromArgb(229, 237, 253);
            dgvEventos.DefaultCellStyle.SelectionForeColor = ForeColor;
            dgvEventos.ColumnHeadersDefaultCellStyle.BackColor = superficie;
            dgvEventos.ColumnHeadersDefaultCellStyle.ForeColor = ForeColor;
            dgvEventos.ColumnHeadersDefaultCellStyle.SelectionBackColor = superficie;
            dgvEventos.ColumnHeadersDefaultCellStyle.SelectionForeColor = ForeColor;
            foreach (Control control in Controls) if (control is BitacoraGlyph) control.ForeColor = AcentoBitacora;
            foreach (Button boton in new[] { btnAplicarFiltros, btnCancelarFiltros, btnExportarPDF, btnSalir })
            {
                bool primario = boton == btnAplicarFiltros;
                boton.BackColor = primario ? AcentoBitacora : superficie;
                boton.ForeColor = primario ? (dark ? Color.FromArgb(35, 30, 27) : Color.White) : AcentoBitacora;
                boton.FlatAppearance.BorderColor = borde;
                boton.FlatAppearance.BorderSize = primario ? 0 : 1;
                ((UsuariosRoundedButton)boton).HoverBackColor = primario ? (dark ? Color.FromArgb(246, 183, 152) : Color.FromArgb(35, 78, 170)) : (dark ? Color.FromArgb(94, 72, 58) : Color.FromArgb(216, 230, 254));
            }
            temaBitacora.DarkMode = dark;
            ActualizarTextosBitacora();
            Invalidate(true);
        }
    }

}
