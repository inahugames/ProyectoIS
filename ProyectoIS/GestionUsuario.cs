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
            dgvUsuarios.Columns[0].Visible = false;
            cbUsuario.Checked=false;
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

        private void cbUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if(cbUsuario.Checked)
            {
                dgvUsuarios.Columns[0].Visible = true;
                btnAct.Visible = true;
            }
            else
            {
                dgvUsuarios.Columns[0].Visible = false;
                btnAct.Visible = false;
            }
           
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvUsuarios.Columns["CSeleccionar"].Index)
            {
                DataGridViewCheckBoxCell cbActivar = (DataGridViewCheckBoxCell)dgvUsuarios.Rows[e.RowIndex].Cells["CSeleccionar"];
                cbActivar.Value = !Convert.ToBoolean(cbActivar.Value);
            }
        
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CrearUsuario nuevo = new CrearUsuario();
            nuevo.Show();
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    DialogResult opcion;
                    opcion = MessageBox.Show("Realmente quiere activar/desactivar el usuario?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (opcion == DialogResult.OK)
                    {
                        MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                        List<Usuario_54CS> lista = mpp.ObtenerUsuarios();
                        foreach (Usuario_54CS user in lista)
                        {
                            if (user.DNI_54cs == Convert.ToInt32(row.Cells[1].Value))
                            {
                                if (user.Activo_54CS == true)
                                {
                                    user.Activo_54CS = false;
                                    mpp.ActualizarUsuarios(lista);
                                    MessageBox.Show("Se activo/desactivo el usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                                else
                                {
                                    user.Activo_54CS = true;
                                    mpp.ActualizarUsuarios(lista);
                                    MessageBox.Show("Se activo/desactivo el usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
