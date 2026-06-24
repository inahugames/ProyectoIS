using BLL_54CS;
using Servicios;
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
    public partial class LogIn : Form, IIdiomaObservador_54CS
    {
        public LogIn()
        {
            InitializeComponent();
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            BLLUsuarios_54CS DBUsuarios = new BLLUsuarios_54CS();
            List<Usuario_54CS> ListUsuarios = DBUsuarios.ObtenerTodos();
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
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario Bloqueado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        if (user.Activo_54CS == false)
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario desactivado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        else
                        {
                            BLLEventos_54CS bllev = new BLLEventos_54CS();
                            List<Eventos_54CS> listaeventos = bllev.ObtenerTodos();
                            int Login = 4;
                            foreach (Eventos_54CS ev in listaeventos)
                            {
                                if (ev.Login_54CS == User && ev.Evento_54CS == "Contraseña Errónea" && DateTime.Now < ev.Fecha_54CS.AddHours(3))
                                {
                                    Login = Login - 1;
                                }
                            }
                            Encriptador_54CS seg = new Encriptador_54CS();
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
                                    bllev.GuardarEvento(Evento, out string msj);
                                    listaeventos = bllev.ObtenerTodos();  
                                    if ( Login <= 0 )
                                    {
                                                user.Block_54CS = true;
                                                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario Bloqueado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                Existe = true;
                                                Eventos_54CS Eventito = new Eventos_54CS() //Crear un evento
                                                {
                                                    Login_54CS = User, // mismo login que el usuario que se logeo
                                                    Fecha_54CS = System.DateTime.Now,
                                                    Modulo_54CS = "Login",
                                                    Evento_54CS = "Usuario Bloqueado",
                                                    Criticidad_54CS = "2"
                                                };
                                                bllev.GuardarEvento(Eventito, out string mens);
                                                BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                                                bll.BloquearUsuario(user.Login_54CS, out string mensj);
                                                break;
                                     }
                                    else if (Login >= 1) { MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Contraseña Incorrecta."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                    Existe = true;
                                }
                                else if (login == true)
                                {
                                    BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                                    bll.CargarPermisosDelUsuarioEnSesion(user);
                                    if (user.TienePermiso("Login") == false)
                                    {
                                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No posee el permiso para iniciar sesión."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    { MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Inicio de Sesión Exitoso"), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information); Existe = true; }
                                    string idiomaPreferido = string.IsNullOrWhiteSpace(user.Idioma_54CS) ? IdiomaManager_54CS.IdiomaPorDefecto : user.Idioma_54CS;
                                    IdiomaManager_54CS.CambiarIdioma(idiomaPreferido);

                                    SessionManager_54CS.Login(user.Login_54CS,user.Nombre_54CS,user.Rol_54CS, user.RolesAsignados, idiomaPreferido);
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento de tipo login
                                    {
                                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Login",
                                        Evento_54CS = "Login",
                                        Criticidad_54CS = "1"
                                    };
                                    bllev.GuardarEvento(Evento, out string msj);
                                    FormularioPrincipal frm = new FormularioPrincipal();
                                    List<string> permisos = user.ObtenerTodosLosPermisosPlanos();
                                    MessageBox.Show(string.Join(",",user.ObtenerTodosLosPermisosPlanos()));
                                    frm.Show();
                                    this.Hide();
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Error al autenticar. {0}"), ex),IdiomaManager_54CS.TraducirMensaje("Error"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Complete los campos de usuario y contraseña."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Existe == false)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario no encontrado en la Base de Datos."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            IdiomaManager_54CS.Suscribir(this);
        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            BLLUsuarios_54CS DBUsuarios = new BLLUsuarios_54CS();
            List<Usuario_54CS> ListUsuarios = DBUsuarios.ObtenerTodos();
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
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario Bloqueado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        if (user.Activo_54CS == false)
                        {
                            MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario desactivado, contacte a un Administrador"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Existe = true;
                            break;
                        }
                        else
                        {
                            BLLEventos_54CS bllev = new BLLEventos_54CS();
                            Encriptador_54CS seg = new Encriptador_54CS();
                            try
                            {
                                bool contracorrecta = seg.VerificarContraseña(Password, user.Password_54CS);
                                if (contracorrecta == false)
                                {
                                    int intentos = 3;
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                                    {
                                        Login_54CS = User, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Login",
                                        Evento_54CS = "Contraseña Errónea",
                                        Criticidad_54CS = "2"
                                    };
                                    bllev.GuardarEvento(Evento, out string msj);
                                    List<Eventos_54CS> listev = bllev.ObtenerTodos();
                                    foreach (Eventos_54CS ev in listev)
                                    {
                                        if (ev.Login_54CS == User && ev.Evento_54CS == "Contraseña Errónea")
                                        {
                                            intentos = intentos - 1;
                                        }
                                    }
                                    Existe = true;
                                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Contraseña incorrecta."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    if ( intentos <= 0)
                                    {
                                        user.Block_54CS = true;
                                        Eventos_54CS Eventito = new Eventos_54CS() //Crear un evento
                                        {
                                            Login_54CS = User, // mismo login que el usuario que se logeo
                                            Fecha_54CS = System.DateTime.Now,
                                            Modulo_54CS = "Login",
                                            Evento_54CS = "Usuario Bloqueado",
                                            Criticidad_54CS = "2"
                                        };
                                        bllev.GuardarEvento(Eventito, out string mens);
                                        BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                                        bll.BloquearUsuario(user.Login_54CS, out string m);
                                        ListUsuarios = bll.ObtenerTodos();
                                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario bloqueado, contacte a un administrador."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    break;
                                }
                                else if (contracorrecta == true)
                                {
                                    BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                                    bll.CargarPermisosDelUsuarioEnSesion(user);
                                    if (user.TienePermiso("CambiarClave"))
                                    {
                                        CambiarContraseña cambia = new CambiarContraseña(user);
                                        cambia.ShowDialog();
                                        ListUsuarios = DBUsuarios.ObtenerTodos();
                                        Existe = true;
                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show("No tiene los permisos suficientes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al autenticar."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Complete los campos de usuario y contraseña."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Existe == false)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Usuario no encontrado en la Base de Datos."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
