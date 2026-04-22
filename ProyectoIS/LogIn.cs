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
using Servicios;

namespace ProyectoIS
{
    public partial class LogIn : Form
    {
        public int Login = 3;
        public LogIn()
        {
            InitializeComponent();
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            MPPUsuarios_54CS DBUsuarios = new MPPUsuarios_54CS();
            List<Usuario_54CS> ListUsuarios = DBUsuarios.ObtenerUsuarios();
            bool Success = false; // Se utiliza solamente para determinar si el usuario existe en la Base de Datos
            if (txtUser.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                string User = txtUser.Text;
                string Password = txtPassword.Text;
                foreach (Usuario_54CS user in ListUsuarios)
                {
                    if (User == user.Login_74CS.Trim())
                    {
                        Success = true;
                        if (user.Block_74CS == true)
                        {
                            MessageBox.Show("Usuario Bloqueado, contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        else
                        {
                            if (Password != user.Password_74CS.Trim())
                            {
                                Login = Login - 1;
                                MessageBox.Show("Contraseña Incorrecta, Intentos Restantes: " + Login, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (Password == user.Password_74CS.Trim())
                            {
                                { MessageBox.Show("Inicio de Sesión Exitoso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); Success = true; }
                                SessionManager_54CS.Nombre_74CS = user.Nombre_74CS;
                                SessionManager_54CS.Rol_74CS = user.Rol_74CS;
                                SessionManager_54CS.Login_74CS = user.Login_74CS;
                                SessionManager_54CS.Logged_74CS = true;
                                Eventos_54CS Evento = new Eventos_54CS()
                                {
                                    Login_74CS = user.Login_74CS,
                                    Fecha_74CS = System.DateTime.Today.ToString(),
                                    Hora_74CS = System.DateTime.Now.ToString(),
                                    Modulo_74CS = "Login",
                                    Evento_74CS = "Login",
                                    Criticidad_74CS = "1"
                                };
                                MPPEventos_54CS mppe = new MPPEventos_54CS();
                                mppe.GuardarEvento(Evento);
                                break;
                            }
                        }

                    }
                    /*if (User == user.Login.Trim() && Password == user.Password.Trim())
                    {
                        if (user.Block == false && user.Activo == true)
                        { MessageBox.Show("Inicio de Sesión Exitoso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); Success = true; }
                        else
                        { MessageBox.Show("Usuario Bloqueado, Contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Success = true; }
                    }
                    else if (User == user.Login.Trim() && Password != user.Password.Trim())
                    {
                        Login = Login - 1;
                        MessageBox.Show("Contraseña Incorrecta, Intentos Restantes: " + Login, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Success = true;
                    }
                    
                }*/
                if ( Success == false )
                { MessageBox.Show("Usuario no encontrado en la Base de Datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); break; }
                    if (Login == 0)
                    {
                        MessageBox.Show("Usuario Bloqueado, contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                        mpp.BloquearUsuario(User);
                    }

                    else
                    {
                        MessageBox.Show("Complete los campos de Usuario y Contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }
    }
}
