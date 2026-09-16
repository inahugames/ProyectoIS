using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using BLL_54CS;
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

namespace ProyectoIS
{
    public partial class CrearUsuario : Form, IIdiomaObservador_54CS
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            CargarRoles();
            ActiveControl = txtNombre;
        }

        private void CargarRoles()
        {
            cmbRol.DisplayMember = "Nombre";
            cmbRol.Items.Clear();

            // Primera opción: no asignar ningún rol. Permite crear usuarios a quienes
            // no poseen el permiso "AsignarRoles". Es una cadena simple, por lo que al
            // seleccionarla "SelectedItem as Rol_54CS" devuelve null.
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

            cmbRol.SelectedIndex = 0; // Por defecto queda seleccionada la opción "sin rol".
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosAlta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("CrearUsuarios"))
            {
                Rol_54CS rolSeleccionado = cmbRol.SelectedItem as Rol_54CS;
                bool deseaAsignarRol = rolSeleccionado != null;
                if (deseaAsignarRol && !SessionManager_54CS.Instancia.TienePermiso("AsignarRoles"))
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene el permiso \"AsignarRoles\". Puede crear el usuario sin asignarle un rol."), IdiomaManager_54CS.TraducirMensaje("Acción Denegada"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtApellido.Text) && !string.IsNullOrWhiteSpace(txtDNI.Text) && !string.IsNullOrWhiteSpace(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    try
                    {
                        string apellidoIngresado = txtApellido.Text.Trim();
                        string emailIngresado = txtEmail.Text.ToLower().Trim();
                        string nombreIngresado = txtNombre.Text.Trim();
                        string rolIngresado = deseaAsignarRol ? rolSeleccionado.Nombre : ""; // vacío = sin rol
                        BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                        List<Usuario_54CS> lista = bll.ObtenerTodos();
                        Encriptador_54CS encripta = new Encriptador_54CS();
                        string dni = txtDNI.Text.Replace(".", "").Replace(" ", "").Trim(); //limpia el dni por si se ingresa con puntos o espacios
                        int Mitad = dni.Length / 2;
                        string primeramitad = dni.Substring(0, Mitad);
                        string segundamitad = dni.Substring(Mitad);
                        Usuario_54CS nuevo = new Usuario_54CS();
                        if (!int.TryParse(dni, out int dniUsuario) || dniUsuario <= 0 || dniUsuario > 99999999)
                            throw new ArgumentException(IdiomaManager_54CS.TraducirMensaje("Ingrese un DNI válido."));
                        bool creado = bll.CrearUsuario(dniUsuario, apellidoIngresado, nombreIngresado, nombreIngresado + primeramitad, encripta.EncriptarContraseña(apellidoIngresado + segundamitad), rolIngresado, emailIngresado, false, true, out string msj, rolSeleccionado);
                        if (creado)
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario creado exitosamente."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                            {
                                Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                Fecha_54CS = System.DateTime.Now,
                                Modulo_54CS = "Gestión de Usuario",
                                Evento_54CS = "Usuario Creado",
                                Criticidad_54CS = "3"
                            };
                            BLLEventos_54CS blle = new BLLEventos_54CS();
                            blle.GuardarEvento(Evento, out string mens);
                        }
                        else
                        {
                            MessageBox.Show(msj, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje(ex.Message), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No deje ningún campo sin llenar."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Nombre_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, string text);
        private UsuariosRoundedPanel[] camposAlta => new[] { campoNombreAlta, campoApellidoAlta, campoDniAlta, campoEmailAlta };

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
            field.BorderColor = AcentoAlta;
            field.Invalidate();
        }

        private void campoAlta_Leave(object sender, EventArgs e)
        {
            var field = (UsuariosRoundedPanel)((Control)sender).Parent;
            field.BorderColor = BordeAlta;
            field.Invalidate();
        }

        private void campoAlta_HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) ActualizarTextosAlta();
        }

        private void cmbRol_DrawItem(object sender, DrawItemEventArgs e)
        {
                if (e.Index < 0 || e.Index >= cmbRol.Items.Count) return;
                bool selected = (e.State & DrawItemState.Selected) != 0 && (e.State & DrawItemState.ComboBoxEdit) == 0;
                Color background = selected ? AcentoAlta : cmbRol.BackColor;
                Color foreground = selected ? (Tema_54CS.EsOscuro ? Color.FromArgb(35, 30, 27) : Color.White) : cmbRol.ForeColor;
                using (SolidBrush brush = new SolidBrush(background)) e.Graphics.FillRectangle(brush, e.Bounds);
                TextRenderer.DrawText(e.Graphics, cmbRol.GetItemText(cmbRol.Items[e.Index]), e.Font, e.Bounds, foreground, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private Color AcentoAlta => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);
        private Color BordeAlta => Tema_54CS.EsOscuro ? Color.FromArgb(79, 79, 75) : Color.FromArgb(208, 214, 225);

        private void ActualizarTextosAlta()
        {
            if (cancelarAlta == null) return;
            tituloAlta.Text = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "Text", "Crear usuario");
            btnCrear.Text = tituloAlta.Text;
            subtituloAlta.Text = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "subtituloAlta", "Completá los datos de la nueva cuenta");
            tituloDatosAlta.Text = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "tituloDatosAlta", "Datos personales");
            tituloRolesAlta.Text = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "tituloRolesAlta", "Rol y acceso");
            ayudaRolAlta.Text = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "ayudaRolAlta", "Podés crear la cuenta sin asignar un rol.");
            cancelarAlta.Text = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "cancelarAlta", "Cancelar");
            temaAlta.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro", Tema_54CS.EsOscuro ? "Claro" : "Oscuro");
            if (cmbRol.Items.Count > 0 && cmbRol.Items[0] is string)
                cmbRol.Items[0] = IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "cmbRolSinRol", "(Sin rol)");
            TextBox[] inputs = { txtNombre, txtApellido, txtDNI, txtEmail };
            Label[] labels = { Nombre, label1, label2, label3 };
            string[] cues = { Nombre.Text, label1.Text, IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "ejemploDniAlta", "Sin puntos ni espacios"), "nombre@ejemplo.com" };
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].AccessibleName = labels[i].Text;
                if (inputs[i].IsHandleCreated) SendMessage(inputs[i].Handle, 0x1501, new IntPtr(1), cues[i]);
            }
            cmbRol.AccessibleName = label4.Text;
        }

        public void AplicarTemaAlta()
        {
            if (cancelarAlta == null) return;
            bool dark = Tema_54CS.EsOscuro;
            Color surface = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color inputColor = dark ? Color.FromArgb(35, 35, 34) : Color.FromArgb(249, 250, 252);
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            foreach (UsuariosRoundedPanel panel in new[] { datosAlta, rolesAlta })
            {
                panel.BackColor = surface;
                panel.BorderColor = BordeAlta;
                panel.ForeColor = AcentoAlta;
            }
            insigniaAlta.BackColor = insigniaAlta.BorderColor = AcentoAlta;
            foreach (UsuariosRoundedPanel field in camposAlta)
            {
                field.BackColor = inputColor;
                field.BorderColor = field.ContainsFocus ? AcentoAlta : BordeAlta;
                field.ForeColor = AcentoAlta;
            }
            foreach (TextBox input in new[] { txtNombre, txtApellido, txtDNI, txtEmail })
            {
                input.BackColor = inputColor;
                input.ForeColor = ForeColor;
                input.BorderStyle = BorderStyle.None;
            }
            foreach (Label label in new[] { Nombre, label1, label2, label3, label4, tituloAlta, tituloDatosAlta, tituloRolesAlta }) label.ForeColor = ForeColor;
            subtituloAlta.ForeColor = ayudaRolAlta.ForeColor = dark ? Color.Silver : Color.FromArgb(103, 110, 124);
            campoRolAlta.BackColor = cmbRol.BackColor = inputColor;
            campoRolAlta.BorderColor = BordeAlta;
            cmbRol.ForeColor = ForeColor;
            cmbRol.FlatStyle = FlatStyle.Flat;
            btnCrear.BackColor = AcentoAlta;
            btnCrear.ForeColor = dark ? Color.FromArgb(35, 30, 27) : Color.White;
            cancelarAlta.BackColor = surface;
            cancelarAlta.ForeColor = cancelarAlta.FlatAppearance.BorderColor = AcentoAlta;
            cancelarAlta.FlatAppearance.BorderSize = 1;
            temaAlta.DarkMode = dark;
            ActualizarTextosAlta();
            Invalidate(true);
        }
    }

}
