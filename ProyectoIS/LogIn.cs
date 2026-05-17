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
        public int Login = 4;
        public LogIn()
        {
            InitializeComponent();
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            MPPUsuarios_54CS DBUsuarios = new MPPUsuarios_54CS();
            List<Usuario_54CS> ListUsuarios = DBUsuarios.ObtenerUsuarios();
            bool Existe = false; // se usa para determinar si existe el usuario en la bd
            if (txtUser.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                string User = txtUser.Text;
                string Password = txtPassword.Text;
                foreach (Usuario_54CS user in ListUsuarios)
                {
                    if (User == user.Login_54CS.Trim())
                    {
                        if (user.Block_54CS == true)
                        {
                            MessageBox.Show("Usuario Bloqueado, contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        if (user.Activo_54CS == false)
                        {
                            MessageBox.Show("Usuario desactivado, contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        else
                        {
                            MPPEventos_54CS mppev = new MPPEventos_54CS();
                            List<Eventos_54CS> listaeventos = mppev.ObtenerEventos();
                            foreach (Eventos_54CS ev in listaeventos)
                            {
                                if (ev.Login_54CS == User && ev.Evento_54CS == "Contraseña Errónea" && DateTime.Now < ev.Fecha_54CS.AddHours(3))
                                {
                                    Login = Login - 1;
                                }
                            }
                            Seguridad_54CS seg = new Seguridad_54CS();
                            try
                            {
                                bool login = seg.VerificarContraseña(Password, user.Password_54CS);
                                if (login == false)
                                {
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                                    {
                                        Login_54CS = User, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Login",
                                        Evento_54CS = "Contraseña Errónea",
                                        Criticidad_54CS = "1"
                                    };
                                    mppev.GuardarEvento(Evento);
                                    listaeventos = mppev.ObtenerEventos();  
                                    if ( Login <= 0 )
                                    {
                                                user.Block_54CS = true;
                                                MessageBox.Show("Usuario Bloqueado, contacte a un Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                Existe = true;
                                                Eventos_54CS Eventito = new Eventos_54CS() //Crear un evento
                                                {
                                                    Login_54CS = User, // mismo login que el usuario que se logeo
                                                    Fecha_54CS = System.DateTime.Now,
                                                    Modulo_54CS = "Login",
                                                    Evento_54CS = "Usuario Bloqueado",
                                                    Criticidad_54CS = "2"
                                                };
                                                mppev.GuardarEvento(Eventito);
                                                MPPUsuarios_54CS mppuser = new MPPUsuarios_54CS();
                                                mppuser.BloquearUsuario(user.Login_54CS);
                                                break;
                                     }
                                    else if (Login >= 1) { MessageBox.Show("Contraseña Incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                    Existe = true;
                                }
                                else if (login == true)
                                {
                                    { MessageBox.Show("Inicio de Sesión Exitoso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); Existe = true; }
                                    SessionManager_54CS.Login(user.Login_54CS,user.Nombre_54CS,user.Rol_54CS);
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento de tipo login
                                    {
                                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Login",
                                        Evento_54CS = "Login",
                                        Criticidad_54CS = "1"
                                    };
                                    MPPEventos_54CS mppe = new MPPEventos_54CS();
                                    mppe.GuardarEvento(Evento);
                                    FormularioPrincipal frm = new FormularioPrincipal();
                                    frm.Show();
                                    this.Hide();
                                    break;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Error al autenticar.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Complete los campos de usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Existe == false)
            {
                MessageBox.Show("Usuario no encontrado en la Base de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
