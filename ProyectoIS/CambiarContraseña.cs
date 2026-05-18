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
    public partial class CambiarContraseña : Form
    {
        string usuario;
        string contraseña;
        public CambiarContraseña(Usuario_54CS user)
        {
            InitializeComponent();
            usuario = user.Login_54CS.Trim();
            txtUser.Text = usuario;
            contraseña = user.Password_54CS.Trim();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtPass.Text.Trim() != "")
            {
                string password = txtPass.Text.Trim();
                Seguridad_54CS seg = new Seguridad_54CS();
                bool misma = seg.VerificarContraseña(password, contraseña);
                if (misma == false)
                {
                    password = seg.EncriptarContraseña(password);
                    MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                    mpp.ActualizarContraseña(usuario, password);
                    MPPEventos_54CS mppev = new MPPEventos_54CS();
                    Eventos_54CS nuevo = new Eventos_54CS()
                    {
                        Criticidad_54CS = "2",
                        Modulo_54CS = "Login",
                        Login_54CS = usuario,
                        Evento_54CS = "Cambio de Contraseña",
                        Fecha_54CS = DateTime.Today
                    };
                    mppev.GuardarEvento(nuevo);
                    MessageBox.Show("Contraseña cambiada exitosamente, inicie sesión con su nueva contraseña","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else if (misma == true)
                {
                    MessageBox.Show("Debe ingresar una contraseña diferente a la actual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
