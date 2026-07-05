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
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
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
            if (SessionManager_54CS.Instancia.TienePermiso("VerUsuarios"))
            {
                usuariosToolStripMenuItem.Visible = true;
            }
            if (SessionManager_54CS.Instancia.TienePermiso("VerBitacora"))
            {
                bitácoraToolStripMenuItem.Visible = true;
            }
            if (SessionManager_54CS.Instancia.TienePermiso("CambiarIdioma"))
            {
                idiomaToolStripMenuItem.Visible = true;
            }
            if (SessionManager_54CS.Instancia.TienePermiso("Logout"))
            {
                cerrarSesiónToolStripMenuItem.Visible = true;
            }
            if (SessionManager_54CS.Instancia.TienePermiso("VerFamilias"))
            {
                perfilesToolStripMenuItem.Visible = true;
            }
            if (SessionManager_54CS.Instancia.TienePermiso("VerRoles"))
            {
                rolesToolStripMenuItem.Visible = true;
            }
            if (EsAdministradorDelSistema())
            {
                backupToolStripMenuItem.Visible = true;
                restoreToolStripMenuItem.Visible = true;
                digVerToolStripMenuItem.Visible = true;
            }
        }

        // El Administrador del Sistema se reconoce por su rol o por tener el
        // permiso "DigitoVerificador". Es quien puede realizar backups,
        // restores y controlar el Dígito Verificador.
        private bool EsAdministradorDelSistema()
        {
            try
            {
                SessionManager_54CS sesion = SessionManager_54CS.Instancia;
                bool rolAdministrador = sesion.Rol_54CS != null && sesion.Rol_54CS.Trim().ToLower().Contains("admin");
                bool permisoDV = false;
                try
                {
                    permisoDV = sesion.TienePermiso("DigitoVerificador");
                }
                catch
                {
                    // si no se pueden evaluar los permisos, alcanza con el rol
                }
                return rolAdministrador || permisoDV;
            }
            catch
            {
                return false;
            }
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
    }
}
