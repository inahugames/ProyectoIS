using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_54CS;
using Servicios;

namespace ProyectoIS
{
    public partial class GestionUsuario : Form, IIdiomaObservador_54CS
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, string lParam);
        
        public Usuario_54CS seleccionado => dgvUsuarios.CurrentRow?.Tag as Usuario_54CS;
        public List<Usuario_54CS> lista = new List<Usuario_54CS>();
        public Color ColorFondoContenido => pnlContenido == null ? Color.Empty : pnlContenido.BackColor;
        public GestionUsuario()
        {
            InitializeComponent();
        }

        private void EncabezadoUsuarios_Resize(object sender, EventArgs e)
        {
            btnCrear.Left = Math.Max(300, pnlEncabezado.ClientSize.Width - btnCrear.Width - 4);
            btnTema.Left = Math.Max(280, btnCrear.Left - btnTema.Width - 10);
            cbUsuario.Left = Math.Max(170, btnTema.Left - cbUsuario.Width - 16);
        }

        private void EstadisticasUsuarios_Resize(object sender, EventArgs e)
        {
            int ancho = Math.Max(180, (estadisticasUsuarios.ClientSize.Width - 28) / 3);
            pnlTotal.Width = pnlActivos.Width = pnlBloqueados.Width = ancho;
        }

        private void dgvUsuarios_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.IsCurrentCellDirty) dgvUsuarios.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvUsuarios_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) ActualizarResumen();
        }

        private void txtBuscar_HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                SendMessage(txtBuscar.Handle, 0x1501, new IntPtr(1), "⌕  Buscar por nombre, usuario o correo");
        }

        private void entradaAnimacion_Tick(object sender, EventArgs e)
        {
                entradaProgreso = Math.Min(100, entradaProgreso + 8);
                entradaTicks++;
                int pad = 9 + (100 - entradaProgreso) / 5;
                pnlTotal.Padding = new Padding(18, pad, 10, 6);
                pnlActivos.Padding = new Padding(18, pad, 10, 6);
                pnlBloqueados.Padding = new Padding(18, pad, 10, 6);
                UsuariosRoundedPanel[] tarjetas = { pnlTotal, pnlActivos, pnlBloqueados };
                for (int i = 0; i < tarjetas.Length; i++)
                {
                    int desplazamiento = Math.Max(0, 12 - Math.Max(0, entradaTicks - i * 5) * 2);
                    UsuariosStatIcon icono = tarjetas[i].Controls.OfType<UsuariosStatIcon>().FirstOrDefault();
                    Label caption = tarjetas[i].Controls.OfType<Label>().FirstOrDefault(control => control.Name.StartsWith("lbl", StringComparison.Ordinal));
                    Label valor = tarjetas[i].Controls.OfType<Label>().FirstOrDefault(control => control.Name.StartsWith("valor", StringComparison.Ordinal));
                    if (icono != null) icono.Top = 15 + desplazamiento;
                    if (caption != null) caption.Top = 47 + desplazamiento;
                    if (valor != null) valor.Top = 8 + desplazamiento;
                }
                if (entradaProgreso == 100 && entradaTicks >= 18) entradaAnimacion.Stop();
        }

        private void temaAnimacion_Tick(object sender, EventArgs e)
        {
                temaProgreso = Math.Min(100, temaProgreso + 8);
                pnlContenido.BackColor = Interpolar(temaOrigen, temaDestino, temaProgreso);
                Invalidate(true);
                if (temaProgreso == 100) temaAnimacion.Stop();
        }

        public void AplicarTemaUsuarios(Color colorAnterior)
        {
            if (btnTema != null) btnTema.DarkMode = Tema_54CS.EsOscuro;
            if (btnTema != null) btnTema.Text = Tema_54CS.EsOscuro ? "☀  " + IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "TemaClaro", "Claro") : "☾  " + IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "TemaOscuro", "Oscuro");
            temaDestino = Tema_54CS.EsOscuro ? Color.FromArgb(31, 31, 30) : Color.FromArgb(248, 249, 252);
            AplicarEstiloPremium();
            if (!IsHandleCreated || colorAnterior == temaDestino) return;
            temaOrigen = colorAnterior;
            pnlContenido.BackColor = colorAnterior;
            temaProgreso = 0;
            temaAnimacion.Start();
        }

        private static Color Interpolar(Color origen, Color destino, int progreso)
        {
            return Color.FromArgb(origen.R + (destino.R - origen.R) * progreso / 100, origen.G + (destino.G - origen.G) * progreso / 100, origen.B + (destino.B - origen.B) * progreso / 100);
        }

        private void btnTema_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void AplicarEstiloPremium()
        {
            Color fondo = Tema_54CS.EsOscuro ? Color.FromArgb(31, 31, 30) : Color.FromArgb(248, 249, 252);
            Color superficie = Tema_54CS.EsOscuro ? Color.FromArgb(43, 43, 41) : Color.White;
            Color texto = Tema_54CS.EsOscuro ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            pnlContenido.BackColor = fondo;
            foreach (UsuariosRoundedPanel panel in new[] { pnlFiltros, pnlTabla, pnlTotal, pnlActivos, pnlBloqueados })
            {
                panel.BackColor = superficie;
                panel.ForeColor = texto;
                panel.BorderColor = Tema_54CS.EsOscuro ? Color.FromArgb(69, 69, 66) : Color.FromArgb(223, 228, 236);
            }
            lblCategoria.ForeColor = Tema_54CS.EsOscuro ? Color.FromArgb(220, 140, 110) : Color.FromArgb(52, 103, 190);
            lblTitulo.ForeColor = texto;
            lblSubtitulo.ForeColor = Tema_54CS.EsOscuro ? Color.FromArgb(190, 188, 185) : Color.FromArgb(90, 96, 105);
            foreach (Control control in pnlContenido.Controls) control.ForeColor = texto;
            dgvUsuarios.BackgroundColor = superficie;
            dgvUsuarios.DefaultCellStyle.BackColor = superficie;
            dgvUsuarios.DefaultCellStyle.ForeColor = texto;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = superficie;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = texto;
            dgvUsuarios.GridColor = Tema_54CS.EsOscuro ? Color.FromArgb(65, 65, 62) : Color.FromArgb(228, 231, 236);
            Invalidate(true);
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            filtrosExpandidos = !filtrosExpandidos;
            filtrosAnimacion.Tag = filtrosExpandidos ? 250 : 96;
            if (filtrosExpandidos) tableFiltros.Visible = true;
            filtrosAnimacion.Start();
        }

        private void filtrosAnimacion_Tick(object sender, EventArgs e)
        {
            int target = Convert.ToInt32(filtrosAnimacion.Tag);
            int delta = target > pnlFiltros.Height ? 18 : -18;
            int next = pnlFiltros.Height + delta;
            if ((delta > 0 && next >= target) || (delta < 0 && next <= target))
            {
                next = target;
                filtrosAnimacion.Stop();
                tableFiltros.Visible = filtrosExpandidos;
                btnAplicar.Visible = filtrosExpandidos;
                btnCancelar.Visible = true;
                flpFiltrosAcciones.Height = filtrosExpandidos ? 38 : 0;
            }
            pnlFiltros.Height = next;
            pnlFiltros.PerformLayout();
        }

        private void GestionUsuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            filtrosAnimacion?.Stop();
            filtrosAnimacion?.Dispose();
            entradaAnimacion?.Stop();
            entradaAnimacion?.Dispose();
            temaAnimacion?.Stop();
            temaAnimacion?.Dispose();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltrosEnMemoria();
        }

        private void AplicarFiltrosEnMemoria()
        {
            string term = txtBuscar.Text.Trim();
            IEnumerable<Usuario_54CS> consulta = lista;
            if (!string.IsNullOrEmpty(term)) consulta = consulta.Where(u => (u.Nombre_54CS + " " + u.Apellido_54CS + " " + u.Login_54CS + " " + u.Email_54CS).IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(filtroDniAplicado)) consulta = consulta.Where(u => u.DNI_54cs.ToString().Contains(filtroDniAplicado));
            if (!string.IsNullOrWhiteSpace(filtroNombreAplicado)) consulta = consulta.Where(u => u.Nombre_54CS.IndexOf(filtroNombreAplicado, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(filtroApellidoAplicado)) consulta = consulta.Where(u => u.Apellido_54CS.IndexOf(filtroApellidoAplicado, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(filtroEmailAplicado)) consulta = consulta.Where(u => u.Email_54CS.IndexOf(filtroEmailAplicado, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(filtroLoginAplicado)) consulta = consulta.Where(u => u.Login_54CS.IndexOf(filtroLoginAplicado, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(filtroRolAplicado)) consulta = consulta.Where(u => u.Rol_54CS.IndexOf(filtroRolAplicado, StringComparison.OrdinalIgnoreCase) >= 0);
            CargarFiltrados(consulta.ToList());
        }

        private void CargarFiltrados(IEnumerable<Usuario_54CS> usuarios)
        {
            dgvUsuarios.Rows.Clear();
            foreach (Usuario_54CS v in usuarios)
            {
                int indice = dgvUsuarios.Rows.Add(false, v.DNI_54cs, v.Login_54CS, v.Nombre_54CS, v.Apellido_54CS, v.Email_54CS, v.Rol_54CS, v.Block_54CS, v.Activo_54CS);
                dgvUsuarios.Rows[indice].Tag = v;
            }
            ActualizarResumen();
            ActualizarContadores(lista);
        }

        private void ActualizarResumen()
        {
            if (lblResumen == null) return;
            int seleccionados = dgvUsuarios.Rows.Cast<DataGridViewRow>().Count(r => r.Cells.Count > 0 && Convert.ToBoolean(r.Cells[0].Value));
            string plantilla = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "ResumenUsuarios", "{0} de {1} usuarios · {2} seleccionados");
            lblResumen.Text = string.Format(plantilla, dgvUsuarios.Rows.Count, lista.Count, seleccionados);
        }

        private void ActualizarContadores(IEnumerable<Usuario_54CS> usuarios)
        {
            List<Usuario_54CS> datos = usuarios.ToList();
            if (lblTotal == null) return;
            lblTotal.Text = datos.Count.ToString();
            lblActivos.Text = datos.Count(u => u.Activo_54CS).ToString();
            lblBloqueados.Text = datos.Count(u => u.Block_54CS).ToString();
        }

        private void dgvUsuarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "Nombre")
            {
                Usuario_54CS usuario = dgvUsuarios.Rows[e.RowIndex].Tag as Usuario_54CS;
                if (usuario != null)
                {
                    e.PaintBackground(e.CellBounds, true);
                    Rectangle avatar = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 9, 30, 30);
                    Color[] colores = { Color.FromArgb(110, 150, 220), Color.FromArgb(164, 112, 205), Color.FromArgb(94, 178, 177), Color.FromArgb(224, 135, 105) };
                    using (SolidBrush brush = new SolidBrush(colores[e.RowIndex % colores.Length])) e.Graphics.FillEllipse(brush, avatar);
                    string iniciales = (usuario.Nombre_54CS.Length > 0 ? usuario.Nombre_54CS.Substring(0, 1) : string.Empty) + (usuario.Apellido_54CS.Length > 0 ? usuario.Apellido_54CS.Substring(0, 1) : string.Empty);
                    TextRenderer.DrawText(e.Graphics, iniciales.ToUpperInvariant(), e.CellStyle.Font, avatar, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    TextRenderer.DrawText(e.Graphics, usuario.Nombre_54CS, e.CellStyle.Font, new Rectangle(e.CellBounds.X + 46, e.CellBounds.Y, e.CellBounds.Width - 48, e.CellBounds.Height), e.CellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                    e.Handled = true;
                }
            }
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "Block" || dgvUsuarios.Columns[e.ColumnIndex].Name == "Activo")
            {
                bool state = Convert.ToBoolean(dgvUsuarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                e.PaintBackground(e.CellBounds, true);
                Rectangle pill = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 11, Math.Max(50, e.CellBounds.Width - 16), 25);
                Color fondoPill = Tema_54CS.EsOscuro
                    ? (state ? (dgvUsuarios.Columns[e.ColumnIndex].Name == "Block" ? Color.FromArgb(86, 46, 43) : Color.FromArgb(43, 75, 53)) : Color.FromArgb(65, 65, 62))
                    : (state ? (dgvUsuarios.Columns[e.ColumnIndex].Name == "Block" ? Color.FromArgb(255, 225, 222) : Color.FromArgb(220, 242, 226)) : Color.FromArgb(235, 237, 240));
                using (SolidBrush brush = new SolidBrush(fondoPill))
                using (System.Drawing.Drawing2D.GraphicsPath path = RoundedPath(pill, 10))
                    e.Graphics.FillPath(brush, path);
                Color colorTexto = state && dgvUsuarios.Columns[e.ColumnIndex].Name == "Block" ? Color.FromArgb(235, 120, 106) : (Tema_54CS.EsOscuro ? Color.FromArgb(150, 220, 165) : Color.FromArgb(38, 120, 75));
                TextRenderer.DrawText(e.Graphics, Convert.ToString(e.FormattedValue), e.CellStyle.Font, pill, colorTexto, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            CSeleccionar.ToolTipText = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "CSeleccionarTooltip", "Seleccionar para activar o desactivar");
            lblTitulo.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "lblTitulo", "Usuarios");
            lblSubtitulo.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "lblSubtitulo", "Gestioná las cuentas y los permisos de acceso");
            cbUsuario.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "cbUsuario", "Modo modificación");
            btnTema.Text = Tema_54CS.EsOscuro ? "☀  " + IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "TemaClaro", "Claro") : "☾  " + IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "TemaOscuro", "Oscuro");
            dgvUsuarios.Invalidate();
            if (cbUsuario.Checked)
                TxtModoModificar();
            else
                TxtModoConsulta();
        }


        private bool Autorizar(string permiso, bool requiereSeleccion = false)
        {
            if (!SessionManager_54CS.Instancia.TienePermiso(permiso))
            {
                MostrarError("No tiene permisos suficientes.");
                return false;
            }
            if (requiereSeleccion && seleccionado == null)
            {
                MostrarError("Seleccione un usuario.");
                return false;
            }
            return true;
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje(mensaje), IdiomaManager_54CS.TraducirMensaje("Error"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (!Autorizar("DesbloquearUsuario", true)) return;
            foreach (Usuario_54CS user in lista)
            {
                if (user.Login_54CS == seleccionado.Login_54CS && user.Block_54CS == true)
                {
                    BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                    if (!bll.DesbloquearUsuario(user.Login_54CS, out string msj)) { MostrarError(msj); return; }
                    // Se limpian ÚNICAMENTE los intentos fallidos del usuario
                    // desbloqueado (antes se recorrían todos los eventos y se
                    // reseteaban los contadores de cualquier usuario reciente).
                    bll.LimpiarIntentosFallidos(user.Login_54CS);
                    lista = bll.ObtenerTodos();
                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                    {
                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                        Fecha_54CS = System.DateTime.Now,
                        Modulo_54CS = "Gestión de Usuario",
                        Evento_54CS = "Usuario Desbloqueado",
                        Criticidad_54CS = "2"
                    };
                    BLLEventos_54CS bllev = new BLLEventos_54CS();
                    bllev.GuardarEvento(Evento, out string mens);
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Usuario con DNI {0} desbloqueado exitosamente"), user.DNI_54cs), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else if (user.Login_54CS == seleccionado.Login_54CS && user.Block_54CS == false)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("El usuario seleccionado no se encuentra bloqueado."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Actualizar();
        }

        private void cbUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("CrearUsuarios") ||
                SessionManager_54CS.Instancia.TienePermiso("DesbloquearUsuario") ||
                SessionManager_54CS.Instancia.TienePermiso("ModificarUsuario") ||
                SessionManager_54CS.Instancia.TienePermiso("ActivarDesactivarUsuario") ||
                SessionManager_54CS.Instancia.TienePermiso("EliminarUsuario"))
            {
                if (cbUsuario.Checked)
                {
                    dgvUsuarios.Columns[0].Visible = true;
                    if (SessionManager_54CS.Instancia.TienePermiso("ActivarDesactivarUsuario"))
                    {
                        btnAct.Visible = true;
                    }
                    if (SessionManager_54CS.Instancia.TienePermiso("DesbloquearUsuario"))
                    {
                        btnDesbloquear.Visible = true;
                    }
                    if (SessionManager_54CS.Instancia.TienePermiso("ModificarUsuario"))
                    {
                        btnModificar.Visible = true;
                    }
                    if (SessionManager_54CS.Instancia.TienePermiso("EliminarUsuario"))
                    {
                        btnEliminar.Visible = true;
                    }
                    if (SessionManager_54CS.Instancia.TienePermiso("CrearUsuarios"))
                    {
                        btnCrear.Visible = true;
                    }
                    TxtModoModificar();
                }
                else
                {
                    dgvUsuarios.Columns[0].Visible = false;
                    btnAct.Visible = false;
                    btnModificar.Visible = false;
                    btnDesbloquear.Visible = false;
                    btnEliminar.Visible = false;
                    btnCrear.Visible = false;
                    TxtModoConsulta();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dgvUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "Block" || dgvUsuarios.Columns[e.ColumnIndex].Name == "Activo")
            {
                bool estado;
                if (!bool.TryParse(Convert.ToString(e.Value), out estado)) return;
                bool bloqueado = dgvUsuarios.Columns[e.ColumnIndex].Name == "Block";
                string clave = bloqueado ? (estado ? "Bloqueado" : "Desbloqueado") : (estado ? "Activo" : "Inactivo");
                e.Value = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", clave, clave);
                e.FormattingApplied = true;
                e.CellStyle.ForeColor = estado == bloqueado
                    ? Tema_54CS.ColorAlerta
                    : (Tema_54CS.EsOscuro ? Color.FromArgb(115, 205, 145) : Color.FromArgb(35, 125, 82));
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (!Autorizar("CrearUsuarios", false)) return;
            CrearUsuario nuevo = new CrearUsuario();
            nuevo.ShowDialog();
            Actualizar();
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            if (!Autorizar("ActivarDesactivarUsuario", false)) return;
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    DialogResult opcion;
                    opcion = MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Realmente quiere activar/desactivar el usuario?"), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (opcion == DialogResult.OK)
                    {
                        BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                        List<Usuario_54CS> lista = bll.ObtenerTodos();
                        foreach (Usuario_54CS user in lista)
                        {
                            if (user.DNI_54cs == Convert.ToInt32(row.Cells[1].Value))
                            {
                                if (user.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                                {
                                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No puede modificar el usuario que se encuentra logeado actualmente."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                                if (user.Activo_54CS == true)
                                {
                                    if (!bll.DesactivarUsuario(user.Login_54CS, out string msj)) { MostrarError(msj); return; }
                                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Se desactivó el usuario con DNI: {0}"), user.DNI_54cs), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                                    {
                                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Gestión de Usuario",
                                        Evento_54CS = "Desactivación de usuario",
                                        Criticidad_54CS = "2"
                                    };
                                    BLLEventos_54CS bllev = new BLLEventos_54CS();
                                    bllev.GuardarEvento(Evento, out string mens);
                                    lista = bll.ObtenerTodos();
                                    break;
                                }
                                else
                                {
                                    if (!bll.ActivarUsuario(user.Login_54CS, out string msj)) { MostrarError(msj); return; }
                                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Se activó el usuario con DNI: {0}"), user.DNI_54cs), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                                    {
                                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Gestión de Usuario",
                                        Evento_54CS = "Activación de usuario",
                                        Criticidad_54CS = "2"
                                    };
                                    BLLEventos_54CS bllev = new BLLEventos_54CS();
                                    bllev.GuardarEvento(Evento, out string mens);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            Actualizar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!Autorizar("ModificarUsuario", true)) return;
            if (seleccionado.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No puede modificar el usuario que se encuentra logeado actualmente."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ModificarUsuario nuevo = new ModificarUsuario(seleccionado);
                nuevo.ShowDialog();
                Actualizar();
            }
        }

        public void Actualizar()
        {
            dgvUsuarios.Rows.Clear();
            BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
            lista = bll.ObtenerTodos();
            foreach (Usuario_54CS v in lista)
            {
                int indice = dgvUsuarios.Rows.Add(false, v.DNI_54cs,v.Login_54CS, v.Nombre_54CS, v.Apellido_54CS, v.Email_54CS, v.Rol_54CS, v.Block_54CS, v.Activo_54CS);
                dgvUsuarios.Rows[indice].Tag = v;
            }
            ActualizarContadores(lista);
            ActualizarResumen();
            dgvUsuarios.Columns[0].Visible = false;
            cbUsuario.Checked = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!Autorizar("EliminarUsuario", true)) return;
            BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
            foreach ( Usuario_54CS user in lista )
            {
                if (user.DNI_54cs == seleccionado.DNI_54cs)
                {
                    if (seleccionado.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                    {
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No puede modificar el usuario que se encuentra logeado actualmente."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    DialogResult opcion;
                    opcion = MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Realmente quiere eliminar al usuario con DNI: {0}?"), user.DNI_54cs), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if ( opcion == DialogResult.OK)
                    {
                        if (!bll.EliminarUsuario(user.DNI_54cs, out string msj)) { MostrarError(msj); return; }
                        Actualizar();
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario eliminado."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                        {
                            Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                            Fecha_54CS = System.DateTime.Now,
                            Modulo_54CS = "Gestión de Usuario",
                            Evento_54CS = "Eliminación de Usuario",
                            Criticidad_54CS = "3"
                        };
                        BLLEventos_54CS bllev = new BLLEventos_54CS();
                        bllev.GuardarEvento(Evento, out string mens);
                        break;
                    }
                    else
                    {
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Operación cancelada."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtModoConsulta()
        {
            txtMsj.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "AyudaConsulta", "Completá los filtros y elegí Aplicar para buscar usuarios.");
        }

        private void TxtModoModificar()
        {
            txtMsj.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "AyudaModificar", "Seleccioná un usuario para modificarlo, eliminarlo, desbloquearlo o cambiar su estado.");
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            filtroDniAplicado = txtDNI.Text.Trim();
            filtroNombreAplicado = txtNombre.Text.Trim();
            filtroApellidoAplicado = txtApellido.Text.Trim();
            filtroEmailAplicado = txtEmail.Text.Trim();
            filtroLoginAplicado = txtLogin.Text.Trim();
            filtroRolAplicado = txtRol.Text.Trim();
            lista = new BLLUsuarios_54CS().ObtenerTodos();
            AplicarFiltrosEnMemoria();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtLogin.Clear();
            txtRol.Clear();
            txtBuscar.Clear();
            filtroDniAplicado = filtroNombreAplicado = filtroApellidoAplicado = filtroEmailAplicado = filtroLoginAplicado = filtroRolAplicado = "";
            Actualizar();
        }

        private void GestionUsuario_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            pnlFiltros.Height = 96;
            tableFiltros.Visible = false;
            btnAplicar.Visible = false;
            flpFiltrosAcciones.Height = 0;
            Tema_54CS.Aplicar(this);
            AplicarEstiloPremium();
            CSeleccionar.ToolTipText = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", "CSeleccionarTooltip", "Seleccionar para activar o desactivar");
            IdiomaManager_54CS.Suscribir(this);
            Actualizar();
            TxtModoConsulta();

            EncabezadoUsuarios_Resize(this, EventArgs.Empty);
            EstadisticasUsuarios_Resize(this, EventArgs.Empty);
            entradaAnimacion.Start();
            if (SessionManager_54CS.Instancia.TienePermiso("CrearUsuarios") == false &&
                SessionManager_54CS.Instancia.TienePermiso("DesbloquearUsuario") == false &&
                SessionManager_54CS.Instancia.TienePermiso("ModificarUsuario") == false &&
                SessionManager_54CS.Instancia.TienePermiso("ActivarDesactivarUsuario") == false &&
                SessionManager_54CS.Instancia.TienePermiso("EliminarUsuario") == false)
            {
                cbUsuario.Visible = false;
            }
        }
    }
}
