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
            txtRol.Text = seleccionado.Rol_54CS.Trim();
            seleccion = seleccionado;
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
                        if (txtRol.Text != user.Rol_54CS)
                        {
                            user.Rol_54CS = txtRol.Text;
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
