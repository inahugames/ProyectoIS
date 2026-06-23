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
    public partial class CambiarContraseña : Form, IIdiomaObservador_54CS
    {
        string usuario;
        string contraseña;
        public CambiarContraseña(Usuario_54CS user)
        {
            InitializeComponent();
            IdiomaManager_54CS.Suscribir(this);
            usuario = user.Login_54CS.Trim();
            txtUser.Text = usuario;
            contraseña = user.Password_54CS.Trim();
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtPass.Text.Trim() != "")
            {
                string password = txtPass.Text.Trim();
                Encriptador_54CS seg = new Encriptador_54CS();
                bool misma = seg.VerificarContraseña(password, contraseña);
                if (misma == false)
                {
                    password = seg.EncriptarContraseña(password);
                    BLLUsuarios_54CS bll = new BLLUsuarios_54CS();
                    bll.ActualizarContraseña(usuario, password, out string msj);
                    BLLEventos_54CS blle = new BLLEventos_54CS();
                    Eventos_54CS nuevo = new Eventos_54CS()
                    {
                        Criticidad_54CS = "2",
                        Modulo_54CS = "Login",
                        Login_54CS = usuario,
                        Evento_54CS = "Cambio de Contraseña",
                        Fecha_54CS = DateTime.Today
                    };
                    blle.GuardarEvento(nuevo, out string mens);
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Contraseña cambiada exitosamente, inicie sesión con su nueva contraseña"),IdiomaManager_54CS.TraducirMensaje("Aviso"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else if (misma == true)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Debe ingresar una contraseña diferente a la actual."), IdiomaManager_54CS.TraducirMensaje("Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
