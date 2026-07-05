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
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            CargarRoles();
        }

        private void CargarRoles()
        {
            cmbRol.DisplayMember = "Nombre";
            cmbRol.Items.Clear();

            // Primera opción: no asignar ningún rol. Permite crear usuarios a quienes
            // no poseen el permiso "AsignarRoles". Es una cadena simple, por lo que al
            // seleccionarla "SelectedItem as Rol_54CS" devuelve null.
            cmbRol.Items.Add(IdiomaManager_54CS.ObtenerTexto("CrearUsuario", "cmbRolSinRol", "(Sin rol)"));

            try
            {
                BLLRoles_54CS rolesBLL = new BLLRoles_54CS();
                foreach (var rol in rolesBLL.ObtenerRolesDelSistema())
                {
                    cmbRol.Items.Add(rol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al cargar los datos: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbRol.SelectedIndex = 0; // Por defecto queda seleccionada la opción "sin rol".
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("CrearUsuarios"))
            {
                Rol_54CS rolSeleccionado = cmbRol.SelectedItem as Rol_54CS;
                bool deseaAsignarRol = rolSeleccionado != null;
                if (deseaAsignarRol && !SessionManager_54CS.Instancia.TienePermiso("AsignarRoles"))
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene el permiso \"AsignarRoles\". Puede crear el usuario sin asignarle un rol."), IdiomaManager_54CS.TraducirMensaje("Acción Denegada"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtApellido.Text != "" && txtDNI.Text != "" && txtEmail.Text != "" && txtNombre.Text != "")
                {
                    try
                    {
                        string apellidoIngresado = txtApellido.Text.Trim();
                        string emailIngresado = txtEmail.Text.ToLower().Trim();
                        string nombreIngresado = txtNombre.Text.Trim();
                        string rolIngresado = deseaAsignarRol ? rolSeleccionado.Nombre : ""; // vacío = sin rol
                        BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                        List<Usuario_54CS> lista = bll.ObtenerTodos();
                        Encriptador_54CS encripta = new Encriptador_54CS();
                        string dni = txtDNI.Text.Replace(".", "").Replace(" ", "").Trim(); //limpia el dni por si se ingresa con puntos o espacios
                        int Mitad = dni.Length / 2;
                        string primeramitad = dni.Substring(0, Mitad);
                        string segundamitad = dni.Substring(Mitad);
                        Usuario_54CS nuevo = new Usuario_54CS();
                        int dniUsuario = Convert.ToInt32(txtDNI.Text);
                        bll.CrearUsuario(dniUsuario, apellidoIngresado, nombreIngresado, nombreIngresado + primeramitad, encripta.EncriptarContraseña(apellidoIngresado + segundamitad), rolIngresado, emailIngresado, false, true, out string msj);
                        if (string.IsNullOrEmpty(msj) == true)
                        {
                            if (deseaAsignarRol)
                            {
                                try
                                {
                                    BLLRoles_54CS rolesBLL = new BLLRoles_54CS();
                                    rolesBLL.ActualizarRolesDeUsuario(dniUsuario, new List<Rol_54CS> { rolSeleccionado });
                                }
                                catch (Exception exRol)
                                {
                                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("El usuario se creó, pero no se pudo asignar el rol: ") + exRol.Message, IdiomaManager_54CS.TraducirMensaje("Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }

                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario creado exitosamente."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show(msj, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("El usuario ya se encuentra registrado, no es necesario que se vuelva a registrar."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No deje ningún campo sin llenar."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
