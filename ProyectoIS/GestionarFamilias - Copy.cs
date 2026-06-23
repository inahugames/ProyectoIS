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
    public partial class GestionarFamilias : Form, IIdiomaObservador_54CS
    {
        List<Rol_54CS> listfam = new List<Rol_54CS>();
        List<Rol_54CS> listperm = new List<Rol_54CS>();
        private BLLPermisos_54CS _permisosBLL = new BLLPermisos_54CS();
        private BLLFamilias_54CS _familiasBLL = new BLLFamilias_54CS();

        // --- Estado de la pestaña "Gestionar Permisos de Familia" ---
        private Familia_54CS _familiaSeleccionada;

        public GestionarFamilias()
        {
            InitializeComponent();
            IdiomaManager_54CS.Suscribir(this); // 2.1 - Observer: nos traducimos solos en caliente
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

            // Configuramos los DisplayMember de los controles de la nueva pestaña
            lbFamiliasGestion.DisplayMember = "Nombre";
            clbPermisosDisponiblesFam.DisplayMember = "Nombre";
            lbPermisosDeFamilia.DisplayMember = "Nombre";
        }

        // 2.1 - Observer
        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
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
                _familiasBLL.CrearFamilia(fam, txtDesc.Text);
                BLLEventos_54CS bllev = new BLLEventos_54CS();
                Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                {
                    Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                    Fecha_54CS = System.DateTime.Now,
                    Modulo_54CS = "Gestión de Usuarios",
                    Evento_54CS = "Crear Familia",
                    Criticidad_54CS = "3"
                };
                bllev.GuardarEvento(Evento, out string msj);
                MessageBox.Show("Familia creada con éxito sin conflictos de permisos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chklist.Items.Add(fam);
                txtDesc.Clear();
                txtNombre.Clear();
                for (int i = 0; i < chklist.Items.Count; i++)
                    chklist.SetItemChecked(i, false);

                // La familia recién creada también debe quedar disponible en la pestaña de gestión
                CargarFamiliasGestion();
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

                CargarFamiliasGestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar permisos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // PESTAÑA: GESTIONAR PERMISOS DE FAMILIA
        // (Agregar / Quitar Permisos de una Familia existente + visualización)
        // ==========================================

        /// <summary>
        /// Recarga la lista de Familias existentes y limpia los paneles dependientes de la selección.
        /// </summary>
        private void CargarFamiliasGestion()
        {
            try
            {
                lbFamiliasGestion.Items.Clear();
                clbPermisosDisponiblesFam.Items.Clear();
                lbPermisosDeFamilia.Items.Clear();
                _familiaSeleccionada = null;

                var familias = _familiasBLL.ObtenerFamilias();
                foreach (var familia in familias)
                {
                    lbFamiliasGestion.Items.Add(familia);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las familias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbFamiliasGestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            _familiaSeleccionada = lbFamiliasGestion.SelectedItem as Familia_54CS;
            ActualizarPanelPermisosDeFamilia();
        }

        /// <summary>
        /// Refresca los dos listados dependientes de la familia seleccionada:
        /// los permisos que ya tiene (visualización) y los permisos disponibles para agregarle.
        /// </summary>
        private void ActualizarPanelPermisosDeFamilia()
        {
            clbPermisosDisponiblesFam.Items.Clear();
            lbPermisosDeFamilia.Items.Clear();

            if (_familiaSeleccionada == null)
                return;

            // Visualización: permisos que la familia ya tiene
            foreach (var hijo in _familiaSeleccionada.ObtenerHijos())
            {
                lbPermisosDeFamilia.Items.Add(hijo);
            }

            // Permisos del sistema que todavía NO están en esta familia
            try
            {
                var todosLosPermisos = _permisosBLL.ObtenerPermisosParaCrearFamilia();
                foreach (var permiso in todosLosPermisos)
                {
                    if (!_familiaSeleccionada.TienePermiso(permiso.Nombre))
                    {
                        clbPermisosDisponiblesFam.Items.Add(permiso);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los permisos disponibles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarPermisoFamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null)
            {
                MessageBox.Show("Seleccione primero una familia de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var permisosAAgregar = clbPermisosDisponiblesFam.CheckedItems.Cast<object>().Select(o => (Permiso_54CS)o).ToList();
            if (permisosAAgregar.Count == 0)
            {
                MessageBox.Show("Seleccione (tilde) al menos un permiso para agregar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int agregados = 0;
            try
            {
                foreach (var permiso in permisosAAgregar)
                {
                    _familiasBLL.AgregarPermisoAFamilia(_familiaSeleccionada, permiso);
                    agregados++;
                }

                BLLEventos_54CS bllev = new BLLEventos_54CS();
                Eventos_54CS evento = new Eventos_54CS()
                {
                    Login_54CS = SessionManager_54CS.Instancia.Login_54CS,
                    Fecha_54CS = DateTime.Now,
                    Modulo_54CS = "Gestión de Usuarios",
                    Evento_54CS = "Agregar Permiso a Familia",
                    Criticidad_54CS = "3"
                };
                bllev.GuardarEvento(evento, out string msj);

                MessageBox.Show($"Se agregaron {agregados} permiso(s) a la familia '{_familiaSeleccionada.Nombre}'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo agregar el permiso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ActualizarPanelPermisosDeFamilia();
            }
        }

        private void btnQuitarPermisoFamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null)
            {
                MessageBox.Show("Seleccione primero una familia de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var permisoSeleccionado = lbPermisosDeFamilia.SelectedItem as Permiso_54CS;
            if (permisoSeleccionado == null)
            {
                MessageBox.Show("Seleccione el permiso que desea quitar de la familia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Quitar el permiso '{permisoSeleccionado.Nombre}' de la familia '{_familiaSeleccionada.Nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                _familiasBLL.QuitarPermisoDeFamilia(_familiaSeleccionada, permisoSeleccionado);

                BLLEventos_54CS bllev = new BLLEventos_54CS();
                Eventos_54CS evento = new Eventos_54CS()
                {
                    Login_54CS = SessionManager_54CS.Instancia.Login_54CS,
                    Fecha_54CS = DateTime.Now,
                    Modulo_54CS = "Gestión de Usuarios",
                    Evento_54CS = "Quitar Permiso de Familia",
                    Criticidad_54CS = "3"
                };
                bllev.GuardarEvento(evento, out string msj);

                MessageBox.Show("Permiso quitado de la familia.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo quitar el permiso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ActualizarPanelPermisosDeFamilia();
            }
        }
    }
}
