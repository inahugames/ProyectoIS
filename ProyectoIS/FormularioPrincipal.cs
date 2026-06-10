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
    public partial class FormularioPrincipal : Form
    {
        private int childFormNumber = 0;

        public FormularioPrincipal()
        {
            InitializeComponent();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionUsuario frm = new GestionUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void bitácoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitacoraEventos formulario = new BitacoraEventos();
            formulario.MdiParent = this;
            formulario.Show();
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

        private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionarFamilias formulario = new GestionarFamilias();
            formulario.MdiParent = this;
            formulario.Show();
        }
    }
}
