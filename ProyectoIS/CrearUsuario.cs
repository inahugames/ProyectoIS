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
            if (txtApellido.Text != "" && txtDNI.Text != "" && txtEmail.Text != "" && txtNombre.Text != "" && txtRol.Text != "")
            {
                try
                {
                    string apellidoIngresado = txtApellido.Text.Trim();
                    string emailIngresado = txtEmail.Text.ToLower().Trim();
                    string nombreIngresado = txtNombre.Text.Trim();
                    string rolIngresado = txtRol.Text.Trim();
                    MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                    List<Usuario_54CS> lista = mpp.ObtenerUsuarios();
                    /*foreach (Usuario_54CS user in lista)
                    {
                        if (Convert.ToString(user.DNI_54cs) == txtDNI.Text.Replace(".", "").Trim() || user.Email_54CS.ToLower() == emailIngresado)
                        {
                            MessageBox.Show("El usuario ya se encuentra registrado. No es necesario que se vuelva a registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }*/
                    Seguridad_54CS encripta = new Seguridad_54CS();
                    string dni = txtDNI.Text.Replace(".", "").Replace(" ", "").Trim(); //limpia el dni por si se ingresa con puntos o espacios
                    int Mitad = dni.Length / 2;
                    string primeramitad = dni.Substring(0, Mitad);
                    string segundamitad = dni.Substring(Mitad);
                    Usuario_54CS nuevo = new Usuario_54CS();
                    {
                        nuevo.Apellido_54CS = apellidoIngresado;
                        nuevo.Nombre_54CS = nombreIngresado;
                        nuevo.DNI_54cs = Convert.ToInt32(txtDNI.Text);
                        nuevo.Email_54CS = emailIngresado;
                        nuevo.Rol_54CS = rolIngresado;
                        nuevo.Login_54CS = nuevo.Nombre_54CS + primeramitad;
                        nuevo.Password_54CS = encripta.EncriptarContraseña(nuevo.Apellido_54CS + segundamitad);
                        nuevo.Activo_54CS = true;
                        nuevo.Block_54CS = false;
                    }
                    mpp.GuardarUsuario(nuevo);
                    MessageBox.Show("Usuario creado exitosamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                    {
                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                        Fecha_54CS = System.DateTime.Now,
                        Modulo_54CS = "Gestión de Usuario",
                        Evento_54CS = "Usuario Creado",
                        Criticidad_54CS = "3"
                    };
                    MPPEventos_54CS mppe = new MPPEventos_54CS();
                    mppe.GuardarEvento(Evento);
                }
                catch
                {
                    MessageBox.Show("El usuario ya se encuentra registrado, no es necesario que se vuelva a registrar.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }

            else
            {
                MessageBox.Show("No deje ningún campo sin llenar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Nombre_Click(object sender, EventArgs e)
        {

        }

        private void txtRol_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
