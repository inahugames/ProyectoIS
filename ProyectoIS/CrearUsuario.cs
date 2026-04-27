using MPP;
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
    public partial class CrearUsuario : Form
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtApellido.Text != null && txtDNI.Text != null && txtEmail.Text != null && txtNombre.Text != null && txtRol.Text != null)
            {
                Seguridad_54CS encripta = new Seguridad_54CS();
                string dni = txtDNI.Text.Replace(".", "").Replace(" ", "").Trim(); //limpia el dni por si se ingresa con puntos o espacios
                int Mitad = dni.Length / 2;
                string primeramitad = dni.Substring(0, Mitad);
                string segundamitad = dni.Substring(Mitad);
                Usuario_54CS nuevo = new Usuario_54CS();
                {
                    nuevo.Apellido_54CS = txtApellido.Text;
                    nuevo.Nombre_54CS = txtNombre.Text;
                    nuevo.DNI_54cs = Convert.ToInt32(txtDNI.Text);
                    nuevo.Email_54CS = txtEmail.Text;
                    nuevo.Rol_54CS = txtRol.Text;
                    nuevo.Login_54CS = nuevo.Nombre_54CS + primeramitad;
                    nuevo.Password_54CS = encripta.EncriptarContraseña(nuevo.Apellido_54CS + segundamitad);
                    nuevo.Activo_54CS = true;
                    nuevo.Block_54CS = false;
                }
                MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                mpp.GuardarUsuario(nuevo);
                MessageBox.Show("Usuario creado exitosamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No deje ningún campo sin llenar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
