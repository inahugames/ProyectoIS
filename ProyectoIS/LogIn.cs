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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIS
{
    public partial class LogIn : Form, IIdiomaObservador_54CS
    {
        public LogIn()
        {
            InitializeComponent();

        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            // REVISIÓN: lo primero que hace el sistema antes del login es
            // verificar la consistencia de los datos mediante el Dígito
            // Verificador (DVH / DVV).
            if (!VerificarIntegridadDeDatos())
            {
                return;
            }
            BLLUsuarios_54CS DBUsuarios = new BLLUsuarios_54CS();
            List<Usuario_54CS> ListUsuarios = DBUsuarios.ObtenerTodos();
            bool Existe = false; // se usa para determinar si existe el usuario en la bd
            if (txtUser.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                string User = txtUser.Text;
                string Password = txtPassword.Text;
                foreach (Usuario_54CS user in ListUsuarios)
                {
                    if (User == user.Login_54CS.Trim())
                    {
                        if (user.Block_54CS == true)
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario Bloqueado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        if (user.Activo_54CS == false)
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario desactivado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        else
                        {
                            BLLEventos_54CS bllev = new BLLEventos_54CS();
                            BLLUsuarios_54CS bllUsu = new BLLUsuarios_54CS();
                            Encriptador_54CS seg = new Encriptador_54CS();
                            try
                            {
                                bool login = seg.VerificarContraseña(Password, user.Password_54CS);
                                if (login == false)
                                {
                                    // Política de bloqueo centralizada en la BLL:
                                    // registra el intento fallido y bloquea al
                                    // alcanzar el umbral (3 intentos en 3 h).
                                    bool bloqueado = bllUsu.RegistrarIntentoFallido(User, out string msjBloqueo);
                                    Existe = true;
                                    if (bloqueado)
                                    {
                                        user.Block_54CS = true;
                                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario Bloqueado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Contraseña Incorrecta."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (login == true)
                                {
                                    BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                                    bll.CargarPermisosDelUsuarioEnSesion(user);
                                    if (user.TienePermiso("Login") == false)
                                    {
                                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No posee el permiso para iniciar sesión."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    try
                                    {
                                        string idiomaPreferido = string.IsNullOrWhiteSpace(user.Idioma_54CS) ? IdiomaManager_54CS.IdiomaPorDefecto : user.Idioma_54CS;
                                        IdiomaManager_54CS.CambiarIdioma(idiomaPreferido);
                                        SessionManager_54CS.Login(user.Login_54CS, user.Nombre_54CS, user.Rol_54CS, user.RolesAsignados, idiomaPreferido);
                                        Eventos_54CS Evento = new Eventos_54CS() //Crear un evento de tipo login
                                        {
                                            Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                            Fecha_54CS = System.DateTime.Now,
                                            Modulo_54CS = "Login",
                                            Evento_54CS = "Login",
                                            Criticidad_54CS = "1"
                                        };
                                        bllev.GuardarEvento(Evento, out string msj);
                                        { MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Inicio de Sesión Exitoso"), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information); Existe = true; }
                                        FormularioPrincipal frm = new FormularioPrincipal();
                                        //para mostrar los permisos del usuario en login:
                                        /*List<string> permisos = user.ObtenerTodosLosPermisosPlanos();
                                        MessageBox.Show(string.Join(",", user.ObtenerTodosLosPermisosPlanos()));*/
                                        frm.Show();
                                        this.Hide();
                                        break;
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Error: {0}"), ex.Message),IdiomaManager_54CS.TraducirMensaje("Error"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Error al autenticar. {0}"), ex),IdiomaManager_54CS.TraducirMensaje("Error"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Complete los campos de usuario y contraseña."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Existe == false)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario no encontrado en la Base de Datos."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosLogin();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Tema_54CS.Aplicar(this);
            ActiveControl = txtUser;
            IdiomaManager_54CS.Suscribir(this);
        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            // REVISIÓN: también se verifica la consistencia de los datos antes
            // de permitir el cambio de contraseña, ya que implica autenticarse.
            if (!VerificarIntegridadDeDatos())
            {
                return;
            }
            BLLUsuarios_54CS DBUsuarios = new BLLUsuarios_54CS();
            List<Usuario_54CS> ListUsuarios = DBUsuarios.ObtenerTodos();
            bool Existe = false; // se usa para determinar si existe el usuario en la bd
            if (txtUser.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                string User = txtUser.Text;
                string Password = txtPassword.Text;
                foreach (Usuario_54CS user in ListUsuarios)
                {
                    if (User == user.Login_54CS.Trim())
                    {
                        if (user.Block_54CS == true)
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario Bloqueado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        if (user.Activo_54CS == false)
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario desactivado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        else
                        {
                            BLLUsuarios_54CS bllUsu = new BLLUsuarios_54CS();
                            Encriptador_54CS seg = new Encriptador_54CS();
                            try
                            {
                                bool contracorrecta = seg.VerificarContraseña(Password, user.Password_54CS);
                                if (contracorrecta == false)
                                {
                                    // Política de bloqueo centralizada en la BLL:
                                    // registra el intento fallido y bloquea al
                                    // alcanzar el umbral (3 intentos en 3 h),
                                    // con el mismo criterio que el login normal.
                                    bool bloqueado = bllUsu.RegistrarIntentoFallido(User, out string msjBloqueo);
                                    Existe = true;
                                    if (bloqueado)
                                    {
                                        user.Block_54CS = true;
                                        ListUsuarios = bllUsu.ObtenerTodos();
                                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario bloqueado, contacte a un administrador."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Contraseña incorrecta."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    break;
                                }
                                else if (contracorrecta == true)
                                {
                                    BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                                    bll.CargarPermisosDelUsuarioEnSesion(user);
                                    if (user.TienePermiso("CambiarClave"))
                                    {
                                        CambiarContraseña cambia = new CambiarContraseña(user);
                                        cambia.ShowDialog();
                                        ListUsuarios = DBUsuarios.ObtenerTodos();
                                        Existe = true;
                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene los permisos suficientes."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al autenticar."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Complete los campos de usuario y contraseña."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Existe == false)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario no encontrado en la Base de Datos."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // REVISIÓN del Dígito Verificador:
        // 1. Se genera el OBJETO DV igual que en la GENERACIÓN, sin persistirlo.
        // 2. Se consulta la tabla DV de la BD a través de un SELECT.
        // 3. Se comparan el DVH y el DVV generados con los consultados.
        // 4. Si no se corresponden: si quien intenta ingresar es el Administrador
        //    del Sistema, se le presenta la pantalla de REPARACIÓN; en caso
        //    contrario se presenta el mensaje de inconsistencia y se sale del
        //    sistema. Devuelve true cuando el login puede continuar normalmente.
        private bool VerificarIntegridadDeDatos()
        {
            try
            {
                BLLDigitoVerificador_54CS bllDV = new BLLDigitoVerificador_54CS();
                if (bllDV.VerificarConsistencia(out string detalle))
                {
                    return true;
                }

                if (EsAdministradorDelSistema(txtUser.Text.Trim(), txtPassword.Text))
                {
                    // El Administrador es reconocido por el sistema y se le
                    // presenta el GUI de REPARACIÓN.
                    using (RecuperacionDV formulario = new RecuperacionDV(detalle))
                    {
                        formulario.ShowDialog(this);
                    }
                    // Se limpia la pantalla y se vuelve al Login para hacer un
                    // nuevo acceso.
                    txtUser.Clear();
                    txtPassword.Clear();
                    return false;
                }

                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Se detectó una inconsistencia en los datos de la Base de Datos. El sistema se cerrará. Contacte al Administrador del Sistema."), IdiomaManager_54CS.TraducirMensaje("Error de Integridad"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se marca la integridad como comprometida para que ninguna
                // escritura durante el cierre recalcule (y oculte) el DV.
                SessionManager_54CS.IntegridadComprometida = true;
                Application.Exit();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No se pudo verificar la integridad de los datos: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Recuperación: credenciales, estado de cuenta, límite de intentos y permiso explícito.
        private bool EsAdministradorDelSistema(string login, string password)
        {
            try { return new BLLUsuarios_54CS().AutenticarRecuperacion(login, password); }
            catch { return false; }
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, string text);
        private void temaLogin_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void mostrarClave_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            mostrarClave.Revealed = !txtPassword.UseSystemPasswordChar;
            ActualizarTextosLogin();
            mostrarClave.Invalidate();
        }

        private void campoLogin_Enter(object sender, EventArgs e)
        {
            var panel = (UsuariosRoundedPanel)((Control)sender).Parent;
            panel.BorderColor = Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(52, 103, 190);
            panel.Invalidate();
        }

        private void campoLogin_Leave(object sender, EventArgs e)
        {
            var panel = (UsuariosRoundedPanel)((Control)sender).Parent;
            panel.BorderColor = Tema_54CS.EsOscuro ? Color.FromArgb(79, 79, 75) : Color.FromArgb(208, 214, 225);
            panel.Invalidate();
        }

        private void txtUser_HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) ActualizarTextosLogin();
        }

        private void ActualizarTextosLogin()
        {
            if (subtituloLogin == null) return;
            Text = IdiomaManager_54CS.ObtenerTexto("LogIn", "Text", "Acceso");
            label3.Text = IdiomaManager_54CS.ObtenerTexto("LogIn", "label3", "¡Bienvenido!");
            subtituloLogin.Text = IdiomaManager_54CS.ObtenerTexto("LogIn", "subtituloLogin", "Ingresá tus datos para continuar");
            temaLogin.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro", Tema_54CS.EsOscuro ? "Claro" : "Oscuro");
            mostrarClave.AccessibleName = IdiomaManager_54CS.ObtenerTexto("LogIn", txtPassword.UseSystemPasswordChar ? "MostrarClave" : "OcultarClave", txtPassword.UseSystemPasswordChar ? "Mostrar contraseña" : "Ocultar contraseña");
            txtUser.AccessibleName = label1.Text;
            txtPassword.AccessibleName = label2.Text;
            if (txtUser.IsHandleCreated)
                SendMessage(txtUser.Handle, 0x1501, new IntPtr(1), IdiomaManager_54CS.ObtenerTexto("LogIn", "UsuarioEjemplo", "Tu nombre de usuario"));
        }

        public void AplicarTemaLogin()
        {
            if (tarjetaLogin == null) return;
            bool dark = Tema_54CS.EsOscuro;
            Color accent = dark ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);
            Color surface = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color input = dark ? Color.FromArgb(35, 35, 34) : Color.FromArgb(249, 250, 252);
            Color border = dark ? Color.FromArgb(79, 79, 75) : Color.FromArgb(208, 214, 225);
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            tarjetaLogin.BackColor = surface;
            tarjetaLogin.BorderColor = border;
            insigniaLogin.BackColor = accent;
            insigniaLogin.BorderColor = accent;
            foreach (UsuariosRoundedPanel field in new[] { campoUsuario, campoClave })
            {
                field.BackColor = input;
                field.BorderColor = field.ContainsFocus ? accent : border;
                field.ForeColor = dark ? Color.Silver : Color.FromArgb(102, 112, 130);
            }
            txtUser.BackColor = txtPassword.BackColor = input;
            txtUser.BorderStyle = txtPassword.BorderStyle = BorderStyle.None;
            txtUser.ForeColor = txtPassword.ForeColor = ForeColor;
            label1.ForeColor = label2.ForeColor = label3.ForeColor = ForeColor;
            subtituloLogin.ForeColor = dark ? Color.Silver : Color.FromArgb(103, 110, 124);
            btnLogIn.BackColor = accent;
            btnLogIn.ForeColor = dark ? Color.FromArgb(35, 30, 27) : Color.White;
            btnCambiar.BackColor = surface;
            btnCambiar.ForeColor = accent;
            btnCambiar.FlatAppearance.BorderColor = accent;
            btnCambiar.FlatAppearance.BorderSize = 1;
            mostrarClave.BackColor = input;
            mostrarClave.ForeColor = campoClave.ForeColor;
            mostrarClave.FlatAppearance.BorderSize = 0;
            mostrarClave.HoverBackColor = input;
            temaLogin.DarkMode = dark;
            ActualizarTextosLogin();
            Invalidate(true);
        }
    }

}
