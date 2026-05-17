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
            Application.Exit();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_54CS.Logout();
            LogIn nuevo = new LogIn();
            this.Hide();
            nuevo.Show();
        }
    }
}
