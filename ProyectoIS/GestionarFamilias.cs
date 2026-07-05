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
        private Familia_54CS _familiaSeleccionada;

        public GestionarFamilias()
        {
            InitializeComponent();
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
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
            lbFamiliasGestion.DisplayMember = "Nombre";
            clbPermisosDisponiblesFam.DisplayMember = "Nombre";
            lbPermisosDeFamilia.DisplayMember = "Nombre";
        }

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
            if (SessionManager_54CS.Instancia.TienePermiso("CrearFamilias"))
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Por favor, ingrese el nombre de la nueva familia."), IdiomaManager_54CS.TraducirMensaje("Validación"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Familia creada con éxito sin conflictos de permisos."), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDesc.Clear();
                    txtNombre.Clear();

                    CargarChecklistAdministrarFamilias();
                    listFamilias.Items.Clear();
                    CargarFamiliasGestion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("Conflicto de Permisos Redundantes"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("EliminarFamilias"))
            {
                // solo se permite eliminar familias. aunque el usuario tilde permisos en la lista, se ignoran
                var familiasAEliminar = chklist.CheckedItems
                    .Cast<object>()
                    .OfType<Familia_54CS>()
                    .ToList();

                bool habiaPermisosTildados = chklist.CheckedItems
                    .Cast<object>()
                    .Any(o => o is Permiso_54CS);

                if (familiasAEliminar.Count == 0)
                {
                    string mensaje = habiaPermisosTildados
                        ? "Solo se pueden eliminar familias, no permisos. Tilde al menos una familia."
                        : "Tilde al menos una familia para eliminar.";
                    MessageBox.Show(mensaje, IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string listado = string.Join(", ", familiasAEliminar.Select(f => f.Nombre));
                var confirmacion = MessageBox.Show(
                    string.Format(IdiomaManager_54CS.TraducirMensaje("¿Eliminar la(s) siguiente(s) familia(s)?\n\n{0}\n\nLos permisos no se verán afectados."), listado),
                    IdiomaManager_54CS.TraducirMensaje("Confirmar eliminación"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion == DialogResult.No)
                {
                    return;
                }

                int eliminadas = 0;
                var errores = new List<string>();
                foreach (var familia in familiasAEliminar)
                {
                    try
                    {
                        _familiasBLL.EliminarFamilia(familia.ID);
                        eliminadas++;
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"- {familia.Nombre}: {ex.Message}");
                    }
                }

                if (eliminadas > 0)
                {
                    BLLEventos_54CS bllev = new BLLEventos_54CS();
                    Eventos_54CS evento = new Eventos_54CS()
                    {
                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS,
                        Fecha_54CS = System.DateTime.Now,
                        Modulo_54CS = "Gestión de Usuarios",
                        Evento_54CS = "Eliminar Familia",
                        Criticidad_54CS = "3"
                    };
                    bllev.GuardarEvento(evento, out string msj);
                }
                CargarChecklistAdministrarFamilias();
                listFamilias.Items.Clear();
                CargarFamiliasGestion();

                if (errores.Count == 0)
                {
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Se eliminó/eliminaron {0} familia(s) con éxito."), eliminadas),
                        IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string detalle = string.Join(Environment.NewLine, errores);
                    MessageBox.Show(
                        string.Format(IdiomaManager_54CS.TraducirMensaje("Familias eliminadas: {0}."), eliminadas) + Environment.NewLine + Environment.NewLine +
                        IdiomaManager_54CS.TraducirMensaje("No se pudieron eliminar:") + Environment.NewLine + detalle,
                        IdiomaManager_54CS.TraducirMensaje("Resultado"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GestionarFamilias_Load(object sender, EventArgs e)
        {
            try
            {
                CargarChecklistAdministrarFamilias();
                CargarFamiliasGestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al cargar permisos: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnCrear.Visible = false;
            btnEliminarFamilia.Visible = false;
            btnAgregarPermisoFamilia.Visible = false;
            btnQuitarPermisoFamilia.Visible = false;
            tabsGestionFamilias.TabPages.Remove(tabCrearFamilia);
            tabsGestionFamilias.TabPages.Remove(tabPermisosFamilia);
            txtNombre.Visible = false;
            txtDesc.Visible = false;
            label1.Visible = false;
            label4.Visible = false;
            listFamilias.Visible = false;
            label3.Visible = false;
            int index = 0;
            if (SessionManager_54CS.Instancia.TienePermiso("CrearFamilias") || SessionManager_54CS.Instancia.TienePermiso("EliminarFamilias") || SessionManager_54CS.Instancia.TienePermiso("VerFamilias"))
            {
                tabsGestionFamilias.TabPages.Insert(index, tabCrearFamilia);
                index++;
                if (SessionManager_54CS.Instancia.TienePermiso("CrearFamilias"))
                {
                    btnCrear.Visible = true;
                    txtNombre.Visible = true;
                    txtDesc.Visible = true;
                    label1.Visible = true;
                    label4.Visible = true;
                    listFamilias.Visible = true;
                    label3.Visible = true;
                }
                if (SessionManager_54CS.Instancia.TienePermiso("EliminarFamilias"))
                {
                    btnEliminarFamilia.Visible = true;
                }
            }
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarFamilias"))
            {
                tabsGestionFamilias.TabPages.Insert(index, tabPermisosFamilia);
                index++;
                btnAgregarPermisoFamilia.Visible = true;
                btnQuitarPermisoFamilia.Visible = true;
            }
        }

        private void CargarChecklistAdministrarFamilias()
        {
            chklist.Items.Clear();
            var familias = _familiasBLL.ObtenerFamilias();
            foreach (var familia in familias)
            {
                chklist.Items.Add(familia);
            }

            var permisos = _permisosBLL.ObtenerPermisosParaCrearFamilia();
            foreach (var permiso in permisos)
            {
                chklist.Items.Add(permiso);
            }
        }

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
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al cargar las familias: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbFamiliasGestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            _familiaSeleccionada = lbFamiliasGestion.SelectedItem as Familia_54CS;
            ActualizarPanelPermisosDeFamilia();
        }

        private void ActualizarPanelPermisosDeFamilia()
        {
            clbPermisosDisponiblesFam.Items.Clear();
            lbPermisosDeFamilia.Items.Clear();

            if (_familiaSeleccionada == null)
                return;

            // permisos que la familia ya tiene
            foreach (var hijo in _familiaSeleccionada.ObtenerHijos())
            {
                lbPermisosDeFamilia.Items.Add(hijo);
            }

            // permisos que todavia no estan en esta familia
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
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al cargar los permisos disponibles: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarPermisoFamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione primero una familia de la lista."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var permisosAAgregar = clbPermisosDisponiblesFam.CheckedItems.Cast<object>().Select(o => (Permiso_54CS)o).ToList();
            if (permisosAAgregar.Count == 0)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione (tilde) al menos un permiso para agregar."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Se agregaron {0} permiso(s) a la familia '{1}'."), agregados, _familiaSeleccionada.Nombre), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("No se pudo agregar el permiso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione primero una familia de la lista."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var permisoSeleccionado = lbPermisosDeFamilia.SelectedItem as Permiso_54CS;
            if (permisoSeleccionado == null)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione el permiso que desea quitar de la familia."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                string.Format(IdiomaManager_54CS.TraducirMensaje("¿Quitar el permiso '{0}' de la familia '{1}'?"), permisoSeleccionado.Nombre, _familiaSeleccionada.Nombre),
                IdiomaManager_54CS.TraducirMensaje("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Permiso quitado de la familia."), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("No se pudo quitar el permiso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ActualizarPanelPermisosDeFamilia();
            }
        }
    }
}
