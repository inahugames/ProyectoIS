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

namespace ProyectoIS
{
    public partial class GestionarFamilias : Form
    {
        List<Rol_54CS> listfam = new List<Rol_54CS>();
        List<Rol_54CS> listperm = new List<Rol_54CS>();
        private BLLPermisos_54CS _permisosBLL = new BLLPermisos_54CS();
        public GestionarFamilias()
        {
            InitializeComponent();
            foreach (Familia_54CS fam in listfam)
            {
                chklist.Items.Add(fam);
            }
            foreach (Permiso_54CS perm in listperm)
            {
                chklist.Items.Add(perm);
            }
            ((ListBox)chklist).DisplayMember = "Nombre";
            chklist.ItemCheck += Chklist_ItemCheck;
        }

        private void Chklist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<Rol_54CS> rolesQueQuedaranTildados = new List<Rol_54CS>();

            for (int i = 0; i < chklist.Items.Count; i++)
            {
                if (i == e.Index)
                {
                    if (e.NewValue == CheckState.Checked)
                    {
                        rolesQueQuedaranTildados.Add((Rol_54CS)chklist.Items[i]);
                    }
                }
                else
                {
                    if (chklist.GetItemChecked(i))
                    {
                        rolesQueQuedaranTildados.Add((Rol_54CS)chklist.Items[i]);
                    }
                }
            }
            List<string> todosLosPermisosAcumulados = new List<string>();

            foreach (var rol in rolesQueQuedaranTildados)
            {
                todosLosPermisosAcumulados.AddRange(rol.ObtenerListaPermisos());
            }
            var permisosFinales = todosLosPermisosAcumulados.Distinct().OrderBy(p => p).ToList();
            ActualizarListBoxPermisos(permisosFinales);
        }

        private void ActualizarListBoxPermisos(List<string> permisos)
        {
            listFamilias.Items.Clear();

            foreach (var permiso in permisos)
            {
                listFamilias.Items.Add(permiso);
            }
        }

        private void listFamilias_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre de la nueva familia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Familia_54CS fam = new Familia_54CS(txtNombre.Text);
            try
            {
                foreach (object itemChecked in chklist.CheckedItems)
                {
                    Rol_54CS rolSeleccionado = (Rol_54CS)itemChecked;
                    fam.Agregar(rolSeleccionado);
                }
                _permisosBLL.CrearFamilia(fam, txtDesc.Text);
                MessageBox.Show("Familia creada con éxito sin conflictos de permisos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chklist.Items.Add(fam);
                txtDesc.Clear();
                txtNombre.Clear();
                for (int i = 0; i < chklist.Items.Count; i++)
                    chklist.SetItemChecked(i, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Conflicto de Permisos Redundantes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GestionarFamilias_Load(object sender, EventArgs e)
        {
            try
            {
                var permisos = _permisosBLL.ObtenerPermisosParaCrearFamilia();
                foreach (var permiso in permisos)
                {
                    chklist.Items.Add(permiso);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar permisos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}