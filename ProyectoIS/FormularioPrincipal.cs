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
using MPP;

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
            MPPEventos_54CS mppev = new MPPEventos_54CS();
            Eventos_54CS evento = new Eventos_54CS()
            {
                Criticidad_54CS = "1",
                Evento_54CS = "Logout",
                Modulo_54CS = "Login",
                Fecha_54CS = DateTime.Now,
                Login_54CS = SessionManager_54CS.Instancia.Login_54CS
            };
            mppev.GuardarEvento(evento);
            SessionManager_54CS.Logout();
            Application.Exit();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MPPEventos_54CS mppev = new MPPEventos_54CS();
            Eventos_54CS evento = new Eventos_54CS()
            {
                Criticidad_54CS = "1",
                Evento_54CS = "Logout",
                Modulo_54CS = "Login",
                Fecha_54CS = DateTime.Now,
                Login_54CS = SessionManager_54CS.Instancia.Login_54CS
            };
            SessionManager_54CS.Logout();
            mppev.GuardarEvento(evento);
            LogIn nuevo = new LogIn();
            this.Hide();
            nuevo.Show();
        }
    }
}
