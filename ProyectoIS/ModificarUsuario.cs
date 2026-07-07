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
using System.Diagnostics.Eventing.Reader;

namespace ProyectoIS
{
    public partial class ModificarUsuario : Form, IIdiomaObservador_54CS
    {
        Usuario_54CS seleccion = new Usuario_54CS();
        public ModificarUsuario(Usuario_54CS seleccionado)
        {
            InitializeComponent();
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            txtApellido.Text = seleccionado.Apellido_54CS.Trim();
            txtNombre.Text = seleccionado.Nombre_54CS.Trim();
            txtDNI.Text = Convert.ToString(seleccionado.DNI_54cs).Trim();
            txtEmail.Text = seleccionado.Email_54CS.Trim();
            CargarRoles(seleccionado.Rol_54CS?.Trim());
            seleccion = seleccionado;
        }
        private void CargarRoles(string rolActual)
        {
            cmbRol.DisplayMember = "Nombre";
            cmbRol.Items.Clear();

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

            cmbRol.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(rolActual) &&
                !string.Equals(rolActual, "Sin Asignar", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 1; i < cmbRol.Items.Count; i++)
                {
                    var rol = cmbRol.Items[i] as Rol_54CS;
                    if (rol != null && string.Equals(rol.Nombre, rolActual, StringComparison.OrdinalIgnoreCase))
                    {
                        cmbRol.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarUsuario"))
            {
                BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                List<Usuario_54CS> lista = bll.ObtenerTodos();
                foreach (Usuario_54CS user in lista)
                {
                    if (user.DNI_54cs == seleccion.DNI_54cs)
                    {
                        if (txtEmail.Text != user.Email_54CS)
                        {
                            user.Email_54CS = txtEmail.Text;
                        }

                        Rol_54CS rolSeleccionado = cmbRol.SelectedItem as Rol_54CS;
                        bool deseaAsignarRol = rolSeleccionado != null;
                        if (deseaAsignarRol && !SessionManager_54CS.Instancia.TienePermiso("AsignarRoles"))
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene el permiso \"AsignarRoles\". Puede guardar el usuario sin asignarle un rol."), IdiomaManager_54CS.TraducirMensaje("Acción Denegada"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string nuevoNombreRol = deseaAsignarRol ? rolSeleccionado.Nombre : "Sin Asignar";
                        if (nuevoNombreRol != user.Rol_54CS)
                        {
                            user.Rol_54CS = nuevoNombreRol;
                            user.RolesAsignados = deseaAsignarRol
                                ? new List<Rol_54CS> { rolSeleccionado }
                                : new List<Rol_54CS>();
                            try
                            {
                                BLLRoles_54CS rolesBLL = new BLLRoles_54CS();
                                rolesBLL.ActualizarRolesDeUsuario(user.DNI_54cs, user.RolesAsignados);
                            }
                            catch (Exception exRol)
                            {
                                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No se pudo asignar el rol: ") + exRol.Message, IdiomaManager_54CS.TraducirMensaje("Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        bll.ActualizarUsuario(lista, out string m);
                        Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                        {
                            Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                            Fecha_54CS = System.DateTime.Now,
                            Modulo_54CS = "Gestión de Usuario",
                            Evento_54CS = "Usuario Modificado",
                            Criticidad_54CS = "3"
                        };
                        BLLEventos_54CS bllev = new BLLEventos_54CS();
                        bllev.GuardarEvento(Evento, out string msj);
                        MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Usuario con DNI {0} modificado exitosamente."), user.DNI_54cs), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
