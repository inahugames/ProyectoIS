using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MPP;
using Servicios;

namespace ProyectoIS
{
    public partial class GestionUsuario : Form
    {
        public Usuario_74CS seleccionado = new Usuario_74CS();
        public List<Usuario_74CS> lista = new List<Usuario_74CS>();
        public GestionUsuario()
        {
            InitializeComponent();
            MPPUsuarios_74CS mpp = new MPPUsuarios_74CS();
            dgvUsuarios.DataSource = mpp.ObtenerUsuarios();
            lista = mpp.ObtenerUsuarios();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] datos = dgvUsuarios.CurrentRow.AccessibilityObject.Value.Split(';');
            foreach (Usuario_74CS user in lista)
            {
                if ( Convert.ToString(user.DNI_74cs) == datos[0])
                {
                    seleccionado = user; break;
                }
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            foreach (Usuario_74CS user in lista)
            {
                if (user.Login_74CS == seleccionado.Login_74CS)
                {
                    user.Block_74CS = false;
                    MPPUsuarios_74CS mpp = new MPPUsuarios_74CS();
                    mpp.ActualizarUsuarios(lista);
                    break;
                }
            }
        }
    }
}
