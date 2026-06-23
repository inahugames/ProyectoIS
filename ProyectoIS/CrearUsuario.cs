using BLL_54CS;
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
    public partial class CrearUsuario : Form, IIdiomaObservador_54CS
    {
        public CrearUsuario()
        {
            InitializeComponent();
            IdiomaManager_54CS.Suscribir(this);
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("CrearUsuario"))
            {
                if (txtApellido.Text != "" && txtDNI.Text != "" && txtEmail.Text != "" && txtNombre.Text != "" && txtRol.Text != "")
                {
                    try
                    {
                        string apellidoIngresado = txtApellido.Text.Trim();
                        string emailIngresado = txtEmail.Text.ToLower().Trim();
                        string nombreIngresado = txtNombre.Text.Trim();
                        string rolIngresado = txtRol.Text.Trim();
                        BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                        List<Usuario_54CS> lista = bll.ObtenerTodos();
                        Encriptador_54CS encripta = new Encriptador_54CS();
                        string dni = txtDNI.Text.Replace(".", "").Replace(" ", "").Trim(); //limpia el dni por si se ingresa con puntos o espacios
                        int Mitad = dni.Length / 2;
                        string primeramitad = dni.Substring(0, Mitad);
                        string segundamitad = dni.Substring(Mitad);
                        Usuario_54CS nuevo = new Usuario_54CS();
                        bll.CrearUsuario(Convert.ToInt32(txtDNI.Text), apellidoIngresado, nombreIngresado, nombreIngresado + primeramitad, encripta.EncriptarContraseña(apellidoIngresado + segundamitad), rolIngresado, emailIngresado, false, true, out string msj);
                        if (string.IsNullOrEmpty(msj) == true)
                        {
                            MessageBox.Show("Usuario creado exitosamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                            {
                                Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                Fecha_54CS = System.DateTime.Now,
                                Modulo_54CS = "Gestión de Usuario",
                                Evento_54CS = "Usuario Creado",
                                Criticidad_54CS = "3"
                            };
                            BLLEventos_54CS blle = new BLLEventos_54CS();
                            blle.GuardarEvento(Evento, out string mens);
                        }
                        else
                        {
                            MessageBox.Show(msj, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("El usuario ya se encuentra registrado, no es necesario que se vuelva a registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else
                {
                    MessageBox.Show("No deje ningún campo sin llenar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos suficientes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
