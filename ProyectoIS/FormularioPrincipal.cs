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
        }
    }
}
