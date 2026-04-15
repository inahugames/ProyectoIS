using BE;
using MPP;
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
    public partial class Form1 : Form
    {
        public int Login = 3;
        public Form1()
        {
            InitializeComponent();
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            MPPUsuarios DBUsuarios = new MPPUsuarios();
            List<Usuario> ListUsuarios = DBUsuarios.ObtenerUsuarios();
            if (txtUser.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                string User = txtUser.Text;
                string Password = txtPassword.Text;
                foreach (Usuario user in ListUsuarios)
                {
                    if (User == user.Login.Trim() && Password == user.Password.Trim())
                    {
                        if (user.Block == false && user.Activo == true)
                        { MessageBox.Show("Inicio de Sesión Exitoso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        else
                        { MessageBox.Show("Usuario Bloqueado, Contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else if (User == user.Login.Trim() && Password != user.Password.Trim())
                    {
                        Login = Login - 1;
                        MessageBox.Show("Contraseña Incorrecta, Intentos Restantes:" + Login);
                    }
                }
                if (Login == 0)
                {
                    MessageBox.Show("Usuario Bloqueado, contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Añadir lógica de bloqueo en BD más adelante
                }
            }
            else
            {
                MessageBox.Show("Complete los campos de Usuario y Contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }
    }
}
