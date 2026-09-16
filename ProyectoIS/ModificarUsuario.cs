using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_54CS;
using System.Diagnostics.Eventing.Reader;

namespace ProyectoIS
{
    public partial class ModificarUsuario : Form, IIdiomaObservador_54CS
    {
        Usuario_54CS seleccion;
        private bool rolEditado;
        public ModificarUsuario()
        {
            InitializeComponent();
        }

        public ModificarUsuario(Usuario_54CS seleccionado) : this()
        {
            if (seleccionado == null) throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Seleccione un usuario."));
            seleccion = seleccionado;
            txtApellido.Text = seleccionado.Apellido_54CS.Trim();
            txtNombre.Text = seleccionado.Nombre_54CS.Trim();
            txtDNI.Text = Convert.ToString(seleccionado.DNI_54cs).Trim();
            txtEmail.Text = seleccionado.Email_54CS.Trim();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            btnModificar.Enabled = seleccion != null;
            if (seleccion == null) return;
            CargarRoles(seleccion.Rol_54CS?.Trim());
            cmbRol.Enabled = SessionManager_54CS.Instancia.TienePermiso("AsignarRoles");
            ActiveControl = txtEmail;
        }

        private void cmbRol_SelectionChangeCommitted(object sender, EventArgs e)
        {
            rolEditado = true;
        }

        private void CargarRoles(string rolActual)
        {
            cmbRol.DisplayMember = "Nombre";
            cmbRol.Items.Clear();

            cmbRol.Items.Add(IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "cmbRolSinRol", "(Sin rol)"));

            try
            {
                BLLRoles_54CS rolesBLL = new BLLRoles_54CS();
                foreach (var rol in rolesBLL.ObtenerRolesDelSistema())
                {
                    cmbRol.Items.Add(rol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al cargar los datos: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbRol.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(rolActual) &&
                !string.Equals(rolActual, "Sin Asignar", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 1; i < cmbRol.Items.Count; i++)
                {
                    var rol = cmbRol.Items[i] as Rol_54CS;
                    if (rol != null && string.Equals(rol.Nombre, rolActual, StringComparison.OrdinalIgnoreCase))
                    {
                        cmbRol.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosEdicion();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var rol = cmbRol.SelectedItem as Rol_54CS;
            var bll = new BLLUsuarios_54CS();
            if (!bll.ModificarPerfil(seleccion.DNI_54cs, txtEmail.Text, rol, rolEditado, out string mensaje))
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje(mensaje), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var evento = new Eventos_54CS
            {
                Login_54CS = SessionManager_54CS.Instancia.Login_54CS,
                Fecha_54CS = DateTime.Now, Modulo_54CS = "Gestión de Usuario",
                Evento_54CS = "Usuario Modificado", Criticidad_54CS = "3"
            };
            new BLLEventos_54CS().GuardarEvento(evento, out string aviso);
            MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Usuario con DNI {0} modificado exitosamente."), seleccion.DNI_54cs),
                IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, string text);
        private UsuariosRoundedPanel[] camposEdicion => new[] { campoNombreEdicion, campoApellidoEdicion, campoDniEdicion, campoEmailEdicion };

        private void temaAlta_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void cancelarAlta_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void campoAlta_Enter(object sender, EventArgs e)
        {
            var field = (UsuariosRoundedPanel)((Control)sender).Parent;
            field.BorderColor = AcentoEdicion;
            field.Invalidate();
        }

        private void campoAlta_Leave(object sender, EventArgs e)
        {
            var field = (UsuariosRoundedPanel)((Control)sender).Parent;
            field.BorderColor = BordeEdicion;
            field.Invalidate();
        }

        private void campoAlta_HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) ActualizarTextosEdicion();
        }

        private void cmbRol_DrawItem(object sender, DrawItemEventArgs e)
        {
                if (e.Index < 0 || e.Index >= cmbRol.Items.Count) return;
                bool selected = (e.State & DrawItemState.Selected) != 0 && (e.State & DrawItemState.ComboBoxEdit) == 0;
                Color background = selected ? AcentoEdicion : cmbRol.BackColor;
                Color foreground = selected ? (Tema_54CS.EsOscuro ? Color.FromArgb(35, 30, 27) : Color.White) : cmbRol.ForeColor;
                if (!cmbRol.Enabled) foreground = SystemColors.GrayText;
                using (SolidBrush brush = new SolidBrush(background)) e.Graphics.FillRectangle(brush, e.Bounds);
                TextRenderer.DrawText(e.Graphics, cmbRol.GetItemText(cmbRol.Items[e.Index]), e.Font, e.Bounds, foreground, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private Color AcentoEdicion => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);
        private Color BordeEdicion => Tema_54CS.EsOscuro ? Color.FromArgb(79, 79, 75) : Color.FromArgb(208, 214, 225);

        private void ActualizarTextosEdicion()
        {
            if (cancelarEdicion == null) return;
            tituloEdicion.Text = IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "Text", "Modificar usuario");
            btnModificar.Text = IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "btnModificar", "Guardar cambios");
            subtituloEdicion.Text = IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "subtituloEdicion", "Actualizá el email y el rol de la cuenta");
            tituloDatosEdicion.Text = IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "tituloDatosEdicion", "Datos personales");
            tituloRolesEdicion.Text = IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "tituloRolesEdicion", "Rol y acceso");
            ayudaRolEdicion.Text = IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "ayudaRolEdicion", "La asignación de roles requiere el permiso correspondiente.");
            cancelarEdicion.Text = IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "cancelarEdicion", "Cancelar");
            temaEdicion.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro", Tema_54CS.EsOscuro ? "Claro" : "Oscuro");
            if (cmbRol.Items.Count > 0 && cmbRol.Items[0] is string)
                cmbRol.Items[0] = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "cmbRolSinRol", "(Sin rol)");
            TextBox[] inputs = { txtNombre, txtApellido, txtDNI, txtEmail };
            Label[] labels = { Nombre, label1, label2, label3 };
            string[] cues = { Nombre.Text, label1.Text, IdiomaManager_54CS.ObtenerTexto("ModificarUsuario", "ejemploDniEdicion", "Sin puntos ni espacios"), "nombre@ejemplo.com" };
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].AccessibleName = labels[i].Text;
                if (inputs[i].IsHandleCreated) SendMessage(inputs[i].Handle, 0x1501, new IntPtr(1), cues[i]);
            }
            cmbRol.AccessibleName = label4.Text;
        }

        public void AplicarTemaEdicion()
        {
            if (cancelarEdicion == null) return;
            bool dark = Tema_54CS.EsOscuro;
            Color surface = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color inputColor = dark ? Color.FromArgb(35, 35, 34) : Color.FromArgb(249, 250, 252);
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            foreach (UsuariosRoundedPanel panel in new[] { datosEdicion, rolesEdicion })
            {
                panel.BackColor = surface;
                panel.BorderColor = BordeEdicion;
                panel.ForeColor = AcentoEdicion;
            }
            insigniaEdicion.BackColor = insigniaEdicion.BorderColor = AcentoEdicion;
            foreach (UsuariosRoundedPanel field in camposEdicion)
            {
                field.BackColor = inputColor;
                field.BorderColor = field.ContainsFocus ? AcentoEdicion : BordeEdicion;
                field.ForeColor = AcentoEdicion;
            }
            foreach (TextBox input in new[] { txtNombre, txtApellido, txtDNI, txtEmail })
            {
                input.BackColor = inputColor;
                input.ForeColor = ForeColor;
                input.BorderStyle = BorderStyle.None;
            }
            foreach (Label label in new[] { Nombre, label1, label2, label3, label4, tituloEdicion, tituloDatosEdicion, tituloRolesEdicion }) label.ForeColor = ForeColor;
            subtituloEdicion.ForeColor = ayudaRolEdicion.ForeColor = dark ? Color.Silver : Color.FromArgb(103, 110, 124);
            campoRolEdicion.BackColor = cmbRol.BackColor = inputColor;
            campoRolEdicion.BorderColor = BordeEdicion;
            cmbRol.ForeColor = ForeColor;
            cmbRol.FlatStyle = FlatStyle.Flat;
            btnModificar.BackColor = AcentoEdicion;
            btnModificar.ForeColor = dark ? Color.FromArgb(35, 30, 27) : Color.White;
            cancelarEdicion.BackColor = surface;
            cancelarEdicion.ForeColor = cancelarEdicion.FlatAppearance.BorderColor = AcentoEdicion;
            cancelarEdicion.FlatAppearance.BorderSize = 1;
            temaEdicion.DarkMode = dark;
            ActualizarTextosEdicion();
            Invalidate(true);
        }
    }
}
