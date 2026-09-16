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
    public partial class CambiarContraseña : Form, IIdiomaObservador_54CS
    {
        string usuario;
        string contraseña;
        public CambiarContraseña()
        {
            InitializeComponent();
        }

        public CambiarContraseña(Usuario_54CS user) : this()
        {
            usuario = user.Login_54CS.Trim();
            txtUser.Text = usuario;
            contraseña = user.Password_54CS.Trim();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            ActiveControl = txtPass;
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosClave();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var bllUsuario = new BLLUsuarios_54CS();
            var actual = bllUsuario.ObtenerTodos().SingleOrDefault(u => u.Login_54CS.Trim() == usuario);
            if (actual == null || actual.Block_54CS || !actual.Activo_54CS)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("El usuario no está habilitado."), IdiomaManager_54CS.TraducirMensaje("Error"));
                return;
            }
            bllUsuario.CargarPermisosDelUsuarioEnSesion(actual);
            if (!actual.TienePermiso("CambiarClave"))
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."), IdiomaManager_54CS.TraducirMensaje("Error"));
                return;
            }
            if (txtPass.Text.Trim() != "" && txtPassConf.Text.Trim() != "")
            {
                if (txtPass.Text.Trim() != txtPassConf.Text.Trim())
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Las contraseñas ingresadas son distintas entre sí."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string password = txtPass.Text.Trim();
                Encriptador_54CS seg = new Encriptador_54CS();
                bool misma = seg.VerificarContraseña(password, contraseña);
                if (misma == false)
                {
                    password = seg.EncriptarContraseña(password);
                    BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                    if (!bll.ActualizarContraseña(usuario, password, out string msj))
                    {
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje(msj), IdiomaManager_54CS.TraducirMensaje("Error"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    BLLEventos_54CS blle = new BLLEventos_54CS();
                    Eventos_54CS nuevo = new Eventos_54CS()
                    {
                        Criticidad_54CS = "2",
                        Modulo_54CS = "Login",
                        Login_54CS = usuario,
                        Evento_54CS = "Cambio de Contraseña",
                        Fecha_54CS = DateTime.Now
                    };
                    blle.GuardarEvento(nuevo, out string mens);
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Contraseña cambiada exitosamente, inicie sesión con su nueva contraseña"),IdiomaManager_54CS.TraducirMensaje("Aviso"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else if (misma == true)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Debe ingresar una contraseña diferente a la actual."), IdiomaManager_54CS.TraducirMensaje("Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private UsuariosRoundedPanel[] camposClave => new[] { campoUsuarioClave, campoNuevaClave, campoConfirmacionClave };

        private void temaClave_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void cancelarClave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void campoClave_Enter(object sender, EventArgs e)
        {
            var campo = (UsuariosRoundedPanel)((Control)sender).Parent;
            campo.BorderColor = AcentoClave;
            campo.Invalidate();
        }

        private void campoClave_Leave(object sender, EventArgs e)
        {
            var campo = (UsuariosRoundedPanel)((Control)sender).Parent;
            campo.BorderColor = BordeClave;
            campo.Invalidate();
        }

        private void ojoClave_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadClave(txtPass, ojoClave);
        }

        private void ojoConfirmacion_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadClave(txtPassConf, ojoConfirmacion);
        }

        private void AlternarVisibilidadClave(TextBox input, LoginEyeButton eye)
        {
            input.UseSystemPasswordChar = !input.UseSystemPasswordChar;
            eye.Revealed = !input.UseSystemPasswordChar;
            ActualizarTextosClave();
            eye.Invalidate();
        }

        private Color AcentoClave => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);
        private Color BordeClave => Tema_54CS.EsOscuro ? Color.FromArgb(79, 79, 75) : Color.FromArgb(208, 214, 225);

        private void ActualizarTextosClave()
        {
            if (subtituloClave == null) return;
            subtituloClave.Text = IdiomaManager_54CS.ObtenerTexto("CambiarContraseña", "subtituloClave", "Elegí una nueva contraseña para tu cuenta");
            ayudaClave.Text = IdiomaManager_54CS.ObtenerTexto("CambiarContraseña", "ayudaClave", "Debe ser diferente de tu contraseña actual.");
            cancelarClave.Text = IdiomaManager_54CS.ObtenerTexto("CambiarContraseña", "cancelarClave", "Cancelar");
            temaClave.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro", Tema_54CS.EsOscuro ? "Claro" : "Oscuro");
            txtUser.AccessibleName = label1.Text;
            txtPass.AccessibleName = label2.Text;
            txtPassConf.AccessibleName = label4.Text;
            ojoClave.AccessibleName = TextoOjoClave(txtPass);
            ojoConfirmacion.AccessibleName = TextoOjoClave(txtPassConf);
        }

        private string TextoOjoClave(TextBox input)
        {
            return IdiomaManager_54CS.ObtenerTexto("LogIn", input.UseSystemPasswordChar ? "MostrarClave" : "OcultarClave", input.UseSystemPasswordChar ? "Mostrar contraseña" : "Ocultar contraseña") + ": " + input.AccessibleName;
        }

        public void AplicarTemaClave()
        {
            if (tarjetaClave == null) return;
            bool dark = Tema_54CS.EsOscuro;
            Color surface = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color inputColor = dark ? Color.FromArgb(35, 35, 34) : Color.FromArgb(249, 250, 252);
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            tarjetaClave.BackColor = surface;
            tarjetaClave.BorderColor = BordeClave;
            insigniaClave.BackColor = insigniaClave.BorderColor = AcentoClave;
            foreach (UsuariosRoundedPanel field in camposClave)
            {
                field.BackColor = inputColor;
                field.BorderColor = field.ContainsFocus ? AcentoClave : BordeClave;
                field.ForeColor = dark ? Color.Silver : Color.FromArgb(102, 112, 130);
            }
            foreach (TextBox input in new[] { txtUser, txtPass, txtPassConf })
            {
                input.BackColor = inputColor;
                input.ForeColor = ForeColor;
                input.BorderStyle = BorderStyle.None;
            }
            foreach (Label label in new[] { label1, label2, label3, label4 }) label.ForeColor = ForeColor;
            subtituloClave.ForeColor = ayudaClave.ForeColor = dark ? Color.Silver : Color.FromArgb(103, 110, 124);
            btnModificar.BackColor = AcentoClave;
            btnModificar.ForeColor = dark ? Color.FromArgb(35, 30, 27) : Color.White;
            cancelarClave.BackColor = surface;
            cancelarClave.ForeColor = cancelarClave.FlatAppearance.BorderColor = AcentoClave;
            cancelarClave.FlatAppearance.BorderSize = 1;
            foreach (LoginEyeButton eye in new[] { ojoClave, ojoConfirmacion })
            {
                eye.BackColor = eye.HoverBackColor = inputColor;
                eye.ForeColor = camposClave[1].ForeColor;
                eye.FlatAppearance.BorderSize = 0;
            }
            temaClave.DarkMode = dark;
            ActualizarTextosClave();
            Invalidate(true);
        }
    }
}
