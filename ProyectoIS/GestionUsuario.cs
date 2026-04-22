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
        public Usuario_54CS seleccionado = new Usuario_54CS();
        public List<Usuario_54CS> lista = new List<Usuario_54CS>();
        public GestionUsuario()
        {
            InitializeComponent();
            MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
            dgvUsuarios.DataSource = mpp.ObtenerUsuarios();
            lista = mpp.ObtenerUsuarios();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] datos = dgvUsuarios.CurrentRow.AccessibilityObject.Value.Split(';');
            foreach (Usuario_54CS user in lista)
            {
                if ( Convert.ToString(user.DNI_54cs) == datos[0])
                {
                    seleccionado = user; break;
                }
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            foreach (Usuario_54CS user in lista)
            {
                if (user.Login_54CS == seleccionado.Login_54CS)
                {
                    user.Block_54CS = false;
                    MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                    mpp.ActualizarUsuarios(lista);
                    break;
                }
            }
        }
    }
}
