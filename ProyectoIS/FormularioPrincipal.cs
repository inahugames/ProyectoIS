using System.Drawing.Drawing2D;
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

namespace ProyectoIS
{
    public partial class FormularioPrincipal : Form, IIdiomaObservador_54CS
    {
        private int childFormNumber = 0;

        public FormularioPrincipal()
        {
            InitializeComponent();

        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosPrincipal();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("VerUsuarios"))
            {
                GestionUsuario frm = new GestionUsuario();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bitácoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("VerBitacora"))
            {
                BitacoraEventos formulario = new BitacoraEventos();
                formulario.MdiParent = this;
                formulario.Show();
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void idiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("CambiarIdioma"))
            {
                CambiarIdioma formulario = new CambiarIdioma();
                formulario.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormularioPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Solo se registra el logout si todavía hay una sesión activa (el
            // formulario puede quedar oculto tras volver al login y cerrarse
            // recién cuando la aplicación termina). Si la salida se debe a una
            // inconsistencia del Dígito Verificador tampoco se escribe: esa
            // escritura recalcularía el DV y ocultaría la inconsistencia.
            if (SessionManager_54CS.HaySesion && !SessionManager_54CS.IntegridadComprometida)
            {
                BLLEventos_54CS bllev = new BLLEventos_54CS();
                Eventos_54CS evento = new Eventos_54CS()
                {
                    Criticidad_54CS = "1",
                    Evento_54CS = "Logout",
                    Modulo_54CS = "Login",
                    Fecha_54CS = DateTime.Now,
                    Login_54CS = SessionManager_54CS.Instancia.Login_54CS
                };
                bllev.GuardarEvento(evento, out string msj);
                SessionManager_54CS.Logout();
            }
            Application.Exit();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("Logout"))
            {
                BLLEventos_54CS bllev = new BLLEventos_54CS();
                Eventos_54CS evento = new Eventos_54CS()
                {
                    Criticidad_54CS = "1",
                    Evento_54CS = "Logout",
                    Modulo_54CS = "Login",
                    Fecha_54CS = DateTime.Now,
                    Login_54CS = SessionManager_54CS.Instancia.Login_54CS
                };
                SessionManager_54CS.Logout();
                bllev.GuardarEvento(evento, out string msj);
                LogIn nuevo = new LogIn();
                this.Hide();
                nuevo.Show();
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("VerFamilias"))
            {
                GestionarFamilias formulario = new GestionarFamilias();
                formulario.MdiParent = this;
                formulario.Show();
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("VerRoles"))
            {
                GestionarRoles formulario = new GestionarRoles();
                formulario.MdiParent = this;
                formulario.Show();
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogIn relogin = new LogIn();
            relogin.ShowDialog(this);
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            ConfigurarVistaPrincipal();
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            ActualizarAccesos();
            Activated += (s, args) => ActualizarAccesos();
        }

        private void ActualizarAccesos()
        {
            if (!SessionManager_54CS.HaySesion) return;
            var sesion = SessionManager_54CS.Instancia;
            usuariosToolStripMenuItem.Visible = sesion.TienePermiso("VerUsuarios");
            bitácoraToolStripMenuItem.Visible = sesion.TienePermiso("VerBitacora");
            idiomaToolStripMenuItem.Visible = sesion.TienePermiso("CambiarIdioma");
            cerrarSesiónToolStripMenuItem.Visible = sesion.TienePermiso("Logout");
            perfilesToolStripMenuItem.Visible = sesion.TienePermiso("VerFamilias");
            rolesToolStripMenuItem.Visible = sesion.TienePermiso("VerRoles");
            backupToolStripMenuItem.Visible = restoreToolStripMenuItem.Visible =
                digVerToolStripMenuItem.Visible = EsAdministradorDelSistema();
            SincronizarNavegacionPrincipal();
        }

        // Las acciones de recuperación requieren un permiso hoja explícito.
        private bool EsAdministradorDelSistema()
        {
            return SessionManager_54CS.HaySesion &&
                SessionManager_54CS.Instancia.TienePermiso("DigitoVerificador");
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!EsAdministradorDelSistema())
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Backup de SQL Server (*.bak)|*.bak";
                dialogo.FileName = $"BDProyecto_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                Cursor = Cursors.WaitCursor;
                try
                {
                    BLLDigitoVerificador_54CS bllDV = new BLLDigitoVerificador_54CS();
                    if (bllDV.RealizarBackup(dialogo.FileName, out string mensaje))
                    {
                        RegistrarEventoAdministracion("Backup BD");
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Backup realizado correctamente en:") + "\n" + dialogo.FileName, IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("No se pudo realizar el backup: {0}"), mensaje), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!EsAdministradorDelSistema())
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (RestaurarBD formulario = new RestaurarBD())
            {
                if (formulario.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
            }
            // Tras el restore la sesión actual queda obsoleta: se registra el
            // evento, se cierra la sesión y se vuelve a la pantalla del Login.
            RegistrarEventoAdministracion("Restore BD");
            VolverAlLogin();
        }

        private void digVerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!EsAdministradorDelSistema())
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                BLLDigitoVerificador_54CS bllDV = new BLLDigitoVerificador_54CS();
                if (bllDV.VerificarConsistencia(out string detalle))
                {
                    var dv = bllDV.ObtenerDVPersistido();
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Los datos son consistentes.\nDVH de la BD: {0}\nDVV de la BD: {1}"), dv.DVHBaseDatos_54CS, dv.DVVBaseDatos_54CS), IdiomaManager_54CS.TraducirMensaje("Dígito Verificador"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool reparado;
                    using (RecuperacionDV formulario = new RecuperacionDV(detalle))
                    {
                        reparado = formulario.ShowDialog(this) == DialogResult.OK;
                    }
                    if (!reparado)
                    {
                        SessionManager_54CS.IntegridadComprometida = true;
                        Application.Exit();
                        return;
                    }
                    if (reparado)
                    {
                        // Tras la reparación se vuelve al Login para un nuevo acceso.
                        VolverAlLogin();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Error: {0}"), ex.Message), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void temaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Alterna entre el tema claro y el oscuro, lo persiste y lo
            // re-aplica en vivo a todas las ventanas abiertas.
            Tema_54CS.AlternarModo();
        }

        private void RegistrarEventoAdministracion(string nombreEvento)
        {
            try
            {
                BLLEventos_54CS bllev = new BLLEventos_54CS();
                Eventos_54CS evento = new Eventos_54CS()
                {
                    Criticidad_54CS = "2",
                    Evento_54CS = nombreEvento,
                    Modulo_54CS = "Admin",
                    Fecha_54CS = DateTime.Now,
                    Login_54CS = SessionManager_54CS.Instancia.Login_54CS
                };
                bllev.GuardarEvento(evento, out string msj);
            }
            catch
            {
                // el registro en bitácora no debe impedir la operación
            }
        }

        private void VolverAlLogin()
        {
            SessionManager_54CS.Logout();
            LogIn nuevo = new LogIn();
            this.Hide();
            nuevo.Show();
        }

        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("Logout") && SessionManager_54CS.Instancia.TienePermiso("CambiarClave"))
            {
                var confirmacion = MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("¿Desea cerrar sesión para cambiar su contraseña?"), IdiomaManager_54CS.TraducirMensaje("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion == DialogResult.Yes)
                {
                    SessionManager_54CS.Logout();
                    LogIn nuevo = new LogIn();
                    this.Hide();
                    nuevo.Show();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private MdiClient escritorioPrincipal;
        private readonly Dictionary<PrincipalButton, ToolStripMenuItem> accionesPrincipal = new Dictionary<PrincipalButton, ToolStripMenuItem>();
        private readonly Dictionary<PrincipalButton, string> textosPrincipal = new Dictionary<PrincipalButton, string>();
        private readonly List<GrupoPrincipal> gruposPrincipal = new List<GrupoPrincipal>();
        private GrupoPrincipal grupoCuentaPrincipal;
        private bool actualizandoPrincipal;

        private sealed class GrupoPrincipal
        {
            public PrincipalButton Boton;
            public FlowLayoutPanel Contenido;
            public ToolStripMenuItem[] Items;
            public bool Abierto;
        }

        private void ConfigurarVistaPrincipal()
        {
            escritorioPrincipal = Controls.OfType<MdiClient>().Single();
            textosPrincipal.Add(botonInicioPrincipal, "inicio");
            RegistrarGrupoPrincipal(nav_adminToolStripMenuItem, grupo_adminToolStripMenuItem, new[] { usuariosToolStripMenuItem, rolesToolStripMenuItem, backupToolStripMenuItem, restoreToolStripMenuItem, bitácoraToolStripMenuItem, digVerToolStripMenuItem, perfilesToolStripMenuItem });
            RegistrarGrupoPrincipal(nav_maestrosToolStripMenuItem, grupo_maestrosToolStripMenuItem, new[] { productosToolStripMenuItem, clientesToolStripMenuItem, proveedoresToolStripMenuItem, bitacoraCToolStripMenuItem });
            grupoCuentaPrincipal = RegistrarGrupoPrincipal(nav_usuarioToolStripMenuItem, grupo_usuarioToolStripMenuItem, new[] { loginToolStripMenuItem, cambiarClaveToolStripMenuItem, cerrarSesiónToolStripMenuItem, idiomaToolStripMenuItem, temaToolStripMenuItem });
            RegistrarGrupoPrincipal(nav_ventasToolStripMenuItem, grupo_ventasToolStripMenuItem, new[] { carritoToolStripMenuItem, facturarToolStripMenuItem, despacharToolStripMenuItem });
            RegistrarGrupoPrincipal(nav_reportesToolStripMenuItem, grupo_reportesToolStripMenuItem, new[] { rep1ToolStripMenuItem, rep2ToolStripMenuItem, rep3ToolStripMenuItem });
            RegistrarAccionPrincipal(nav_comprasToolStripMenuItem, comprasToolStripMenuItem);
            RegistrarAccionPrincipal(nav_ayudaToolStripMenuItem, ayudaToolStripMenuItem);
            RegistrarAccionPrincipal(nav_usuariosToolStripMenuItem, usuariosToolStripMenuItem);
            RegistrarAccionPrincipal(nav_rolesToolStripMenuItem, rolesToolStripMenuItem);
            RegistrarAccionPrincipal(nav_backupToolStripMenuItem, backupToolStripMenuItem);
            RegistrarAccionPrincipal(nav_restoreToolStripMenuItem, restoreToolStripMenuItem);
            RegistrarAccionPrincipal(nav_bitácoraToolStripMenuItem, bitácoraToolStripMenuItem);
            RegistrarAccionPrincipal(nav_digVerToolStripMenuItem, digVerToolStripMenuItem);
            RegistrarAccionPrincipal(nav_perfilesToolStripMenuItem, perfilesToolStripMenuItem);
            RegistrarAccionPrincipal(nav_productosToolStripMenuItem, productosToolStripMenuItem);
            RegistrarAccionPrincipal(nav_clientesToolStripMenuItem, clientesToolStripMenuItem);
            RegistrarAccionPrincipal(nav_proveedoresToolStripMenuItem, proveedoresToolStripMenuItem);
            RegistrarAccionPrincipal(nav_bitacoraCToolStripMenuItem, bitacoraCToolStripMenuItem);
            RegistrarAccionPrincipal(nav_loginToolStripMenuItem, loginToolStripMenuItem);
            RegistrarAccionPrincipal(nav_cambiarClaveToolStripMenuItem, cambiarClaveToolStripMenuItem);
            RegistrarAccionPrincipal(nav_cerrarSesiónToolStripMenuItem, cerrarSesiónToolStripMenuItem);
            RegistrarAccionPrincipal(nav_idiomaToolStripMenuItem, idiomaToolStripMenuItem);
            RegistrarAccionPrincipal(nav_temaToolStripMenuItem, temaToolStripMenuItem);
            RegistrarAccionPrincipal(nav_carritoToolStripMenuItem, carritoToolStripMenuItem);
            RegistrarAccionPrincipal(nav_facturarToolStripMenuItem, facturarToolStripMenuItem);
            RegistrarAccionPrincipal(nav_despacharToolStripMenuItem, despacharToolStripMenuItem);
            RegistrarAccionPrincipal(nav_rep1ToolStripMenuItem, rep1ToolStripMenuItem);
            RegistrarAccionPrincipal(nav_rep2ToolStripMenuItem, rep2ToolStripMenuItem);
            RegistrarAccionPrincipal(nav_rep3ToolStripMenuItem, rep3ToolStripMenuItem);
            RegistrarAccionPrincipal(acceso_usuariosToolStripMenuItem, usuariosToolStripMenuItem);
            RegistrarAccionPrincipal(acceso_rolesToolStripMenuItem, rolesToolStripMenuItem);
            RegistrarAccionPrincipal(acceso_perfilesToolStripMenuItem, perfilesToolStripMenuItem);
            RegistrarAccionPrincipal(acceso_bitácoraToolStripMenuItem, bitácoraToolStripMenuItem);
            Layout += (s, e) => { if (inicioPrincipal.Bounds != escritorioPrincipal.Bounds) inicioPrincipal.Bounds = escritorioPrincipal.Bounds; };
            MdiChildActivate += (s, e) =>
            {
                if (ActiveMdiChild != null)
                {
                    OcultarTemaIntegrado(ActiveMdiChild);
                    inicioPrincipal.Visible = false;
                    tituloPrincipal.Text = ActiveMdiChild.Text;
                }
                else MostrarInicioPrincipal();
                AplicarTemaPrincipal();
            };
            MostrarInicioPrincipal();
        }

        private void botonInicioPrincipal_Click(object sender, EventArgs e)
        {
            MostrarInicioPrincipal();
        }

        private void cerrarVistaPrincipal_Click(object sender, EventArgs e)
        {
            ActiveMdiChild?.Close();
        }

        private void temaPrincipal_Click(object sender, EventArgs e)
        {
            temaToolStripMenuItem.PerformClick();
        }

        private void cuentaPrincipal_Click(object sender, EventArgs e)
        {
            grupoCuentaPrincipal.Abierto = true;
            SincronizarNavegacionPrincipal();
            navegacionPrincipal.ScrollControlIntoView(grupoCuentaPrincipal.Contenido);
        }

        private void cabeceraPrincipal_Resize(object sender, EventArgs e)
        {
            cuentaPrincipal.Location = new Point(cabeceraPrincipal.Width - 220, 20);
            temaPrincipal.Location = new Point(cuentaPrincipal.Left - 112, 21);
            cerrarVistaPrincipal.Location = new Point(temaPrincipal.Left - 42, 22);
            tituloPrincipal.Width = Math.Max(150, cerrarVistaPrincipal.Left - 46);
            subtituloPrincipal.Width = Math.Max(150, temaPrincipal.Left - 46);
        }

        private void bienvenidaPrincipal_Resize(object sender, EventArgs e)
        {
            bienvenidaTituloPrincipal.Width = Math.Max(100, bienvenidaPrincipal.Width - 128);
            bienvenidaTextoPrincipal.Width = Math.Max(100, bienvenidaPrincipal.Width - 130);
        }

        private void iconoBienvenidaPrincipal_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(Color.White, 2.3F))
                for (int x = 19; x <= 37; x += 18) for (int y = 19; y <= 37; y += 18) e.Graphics.DrawRectangle(pen, x, y, 11, 11);
        }

        private void OcultarTemaIntegrado(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control is UsuariosThemeSwitch)
                    control.Visible = false;
                else
                    OcultarTemaIntegrado(control);
            }
        }

        private void RegistrarAccionPrincipal(PrincipalButton button, ToolStripMenuItem item)
        {
            textosPrincipal.Add(button, item.Name);
            accionesPrincipal.Add(button, item);
            button.Click += (s, e) =>
            {
                ActualizarAccesos();
                if (!item.Available || !item.Enabled) return;
                Type tipo = TipoVentanaPrincipal(item);
                Form existente = tipo == null ? null : MdiChildren.FirstOrDefault(form => form.GetType() == tipo);
                if (existente != null) existente.Activate();
                else item.PerformClick();
                if (tipo != null && ActiveMdiChild != null)
                {
                    inicioPrincipal.Visible = false;
                    ActiveMdiChild.WindowState = FormWindowState.Maximized;
                    ActiveMdiChild.Activate();
                    tituloPrincipal.Text = ActiveMdiChild.Text;
                }
                AplicarTemaPrincipal();
            };
        }

        private Type TipoVentanaPrincipal(ToolStripMenuItem item)
        {
            if (item == usuariosToolStripMenuItem) return typeof(GestionUsuario);
            if (item == rolesToolStripMenuItem) return typeof(GestionarRoles);
            if (item == perfilesToolStripMenuItem) return typeof(GestionarFamilias);
            if (item == bitácoraToolStripMenuItem) return typeof(BitacoraEventos);
            return null;
        }

        private GrupoPrincipal RegistrarGrupoPrincipal(PrincipalButton boton, FlowLayoutPanel contenido, ToolStripMenuItem[] items)
        {
            var group = new GrupoPrincipal { Boton = boton, Items = items, Contenido = contenido };
            textosPrincipal.Add(boton, boton.Name.Substring(4));
            group.Boton.Click += (s, e) => { group.Abierto = !group.Abierto; SincronizarNavegacionPrincipal(); };
            gruposPrincipal.Add(group);
            return group;
        }

        private string TextoPrincipal(string key, string fallback) => IdiomaManager_54CS.ObtenerTexto("FormularioPrincipal", key, fallback);

        private void MostrarInicioPrincipal()
        {
            if (inicioPrincipal == null) return;
            inicioPrincipal.Bounds = escritorioPrincipal.Bounds;
            inicioPrincipal.Visible = true;
            inicioPrincipal.BringToFront();
            tituloPrincipal.Text = TextoPrincipal("inicio", "Inicio");
            AplicarTemaPrincipal();
        }

        private void ActualizarTextosPrincipal()
        {
            if (inicioPrincipal == null) return;
            marcaPrincipal.Text = TextoPrincipal("marcaPrincipal", "Gestión");
            subtituloPrincipal.Text = TextoPrincipal("subtituloPrincipal", "Elegí una sección para comenzar");
            bienvenidaTituloPrincipal.Text = TextoPrincipal("bienvenidaTituloPrincipal", "Tu espacio de trabajo");
            bienvenidaTextoPrincipal.Text = TextoPrincipal("bienvenidaTextoPrincipal", "Accedé a las herramientas de gestión desde un solo lugar.");
            accesosTituloPrincipal.Text = TextoPrincipal("accesosTituloPrincipal", "Accesos rápidos");
            piePrincipal.Text = TextoPrincipal("piePrincipal", "Las opciones disponibles dependen de tus permisos.");
            foreach (var entry in textosPrincipal)
            {
                string fallback = accionesPrincipal.ContainsKey(entry.Key) ? accionesPrincipal[entry.Key].Text : entry.Value;
                entry.Key.Caption = TextoPrincipal(entry.Value, fallback);
                if (entry.Key.Large) entry.Key.Subtitle = TextoPrincipal("descripcion_" + entry.Value, string.Empty);
            }
            if (inicioPrincipal.Visible) tituloPrincipal.Text = TextoPrincipal("inicio", "Inicio");
            temaPrincipal.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro", Tema_54CS.EsOscuro ? "Claro" : "Oscuro");
            cuentaPrincipal.Caption = SessionManager_54CS.HaySesion ? SessionManager_54CS.Instancia.Nombre_54CS : TextoPrincipal("miCuentaPrincipal", "Mi cuenta");
            cerrarVistaPrincipal.AccessibleName = TextoPrincipal("cerrarVistaAccesible", "Cerrar vista");
            SincronizarNavegacionPrincipal();
        }

        private void SincronizarNavegacionPrincipal()
        {
            if (inicioPrincipal == null || actualizandoPrincipal) return;
            actualizandoPrincipal = true;
            try
            {
                navegacionPrincipal.SuspendLayout();
                foreach (var entry in accionesPrincipal)
                {
                    entry.Key.Visible = SessionManager_54CS.HaySesion && entry.Value.Available;
                    entry.Key.Enabled = entry.Value.Enabled;
                }
                foreach (var group in gruposPrincipal)
                {
                    group.Boton.Visible = SessionManager_54CS.HaySesion && group.Items.Any(item => item.Available);
                    if (textosPrincipal[group.Boton] == adminToolStripMenuItem.Name) group.Boton.Visible = group.Boton.Visible && EsAdministradorDelSistema();
                    group.Contenido.Visible = group.Boton.Visible && group.Abierto;
                    group.Boton.Expanded = group.Abierto;
                }
                accesosTituloPrincipal.Visible = accionesPrincipal.Any(entry => entry.Key.Large && entry.Value.Available);
                navegacionPrincipal.ResumeLayout(true);
            }
            finally { actualizandoPrincipal = false; }
            AplicarTemaPrincipal();
        }

        public void AplicarTemaPrincipal()
        {
            if (inicioPrincipal == null) return;
            bool dark = Tema_54CS.EsOscuro;
            Color background = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            Color surface = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color accent = dark ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);
            Color foreground = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            Color muted = dark ? Color.Silver : Color.FromArgb(103, 110, 124);
            Color border = dark ? Color.FromArgb(66, 66, 62) : Color.FromArgb(220, 225, 234);
            BackColor = inicioPrincipal.BackColor = cabeceraPrincipal.BackColor = background;
            lateralPrincipal.BackColor = dark ? Color.FromArgb(36, 36, 34) : Color.White;
            navegacionPrincipal.BackColor = lateralPrincipal.BackColor;
            foreach (var group in gruposPrincipal) group.Contenido.BackColor = lateralPrincipal.BackColor;
            foreach (PrincipalButton button in textosPrincipal.Keys.Concat(new[] { cuentaPrincipal }))
            {
                bool selected = button == botonInicioPrincipal && inicioPrincipal.Visible;
                if (!inicioPrincipal.Visible && ActiveMdiChild != null && accionesPrincipal.ContainsKey(button))
                    selected = TipoVentanaPrincipal(accionesPrincipal[button]) == ActiveMdiChild.GetType();
                button.BackColor = selected ? accent : button.Large ? surface : button == cuentaPrincipal ? background : lateralPrincipal.BackColor;
                button.ForeColor = selected ? (dark ? Color.FromArgb(35, 30, 27) : Color.White) : foreground;
                button.Accent = selected ? button.ForeColor : accent;
                button.Muted = muted;
                button.HoverBackColor = dark ? Color.FromArgb(62, 58, 53) : Color.FromArgb(228, 235, 248);
                button.FlatAppearance.BorderSize = button.Large ? 1 : 0;
                button.FlatAppearance.BorderColor = border;
                button.Invalidate();
            }
            bienvenidaPrincipal.BackColor = surface;
            bienvenidaPrincipal.BorderColor = border;
            iconoBienvenidaPrincipal.BackColor = iconoBienvenidaPrincipal.BorderColor = accent;
            cerrarVistaPrincipal.Visible = !inicioPrincipal.Visible && ActiveMdiChild != null;
            cerrarVistaPrincipal.BackColor = background;
            cerrarVistaPrincipal.ForeColor = foreground;
            cerrarVistaPrincipal.FlatAppearance.BorderSize = 0;
            cerrarVistaPrincipal.HoverBackColor = dark ? Color.FromArgb(80, 50, 45) : Color.FromArgb(255, 227, 221);
            foreach (Label label in new[] { marcaPrincipal, tituloPrincipal, bienvenidaTituloPrincipal, accesosTituloPrincipal }) label.ForeColor = foreground;
            foreach (Label label in new[] { subtituloPrincipal, bienvenidaTextoPrincipal, piePrincipal }) label.ForeColor = muted;
            temaPrincipal.DarkMode = dark;
            temaPrincipal.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", dark ? "TemaClaro" : "TemaOscuro", dark ? "Claro" : "Oscuro");
            Invalidate(true);
        }
    }

    public enum PrincipalGlyph { None, Home, Users, Shield, Layers, Events, Database, Grid, Account, Help }

}
