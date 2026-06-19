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
    public partial class GestionarRoles : Form
    {
        private BLLUsuarios_54CS bllusuarios = new BLLUsuarios_54CS();
        private BLLPermisos_54CS _permisosBLL = new BLLPermisos_54CS();
        private List<Rol_54CS> _rolesDelSistema = new List<Rol_54CS>();
        private List<Usuario_54CS> _usuarios = new List<Usuario_54CS>();
        public GestionarRoles()
        {
            InitializeComponent();
            _usuarios = bllusuarios.ObtenerTodos();
        }

        private void GestionarRoles_Load(object sender, EventArgs e)
        {
            // Configurar los DisplayMember para que no salte el error de casteo
            ((ListBox)clbFamiliasYPermisos).DisplayMember = "Nombre";
            ((ListBox)clbRolesParaAsignar).DisplayMember = "Nombre";
            lbRolesExistentes.DisplayMember = "Nombre";
            combobox.DisplayMember = "Nombre";
            RefrescarListas();
        }

        // ==========================================
        // PESTAÑA 2: ASIGNACIÓN A USUARIOS
        // ==========================================

        // ==========================================
        // MÉTODOS AUXILIARES
        // ==========================================

        private void RefrescarListasDeRoles()
        {
            // Actualiza los controles en ambas pestañas simultáneamente
            lbRolesExistentes.Items.Clear();
            clbRolesParaAsignar.Items.Clear();

            foreach (var rol in _rolesDelSistema)
            {
                lbRolesExistentes.Items.Add(rol);
                clbRolesParaAsignar.Items.Add(rol);
            }
        }

        private void RefrescarListas()
        {
            try
            {
                clbFamiliasYPermisos.Items.Clear();
                lbRolesExistentes.Items.Clear();
                clbRolesParaAsignar.Items.Clear();
                combobox.Items.Clear();

                _usuarios = bllusuarios.ObtenerTodos();
                combobox.DataSource = _usuarios;
                combobox.DisplayMember = "Login_54CS";
                combobox.ValueMember = "DNI_54CS";

                // Cargar elementos para armar el Rol (Familias + Permisos)
                var elementosDisponibles = _permisosBLL.ObtenerElementosParaCrearRol();
                foreach (var elemento in elementosDisponibles)
                {
                    clbFamiliasYPermisos.Items.Add(elemento);
                }

                // Cargar Roles ya creados
                var rolesDelSistema = _permisosBLL.ObtenerRolesDelSistema();
                foreach (var rol in rolesDelSistema)
                {
                    lbRolesExistentes.Items.Add(rol);
                    clbRolesParaAsignar.Items.Add(rol); // Para la pestaña de asignación
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosDesdeBaseDeDatos()
        {
            // Método mock para cargar listas iniciales (reemplazar con llamadas a BD)
            // ...
            foreach (Rol_54CS rol in _rolesDelSistema)
            {
                clbFamiliasYPermisos.Items.Add(rol);
            }
        }

        private void btnCrearRol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreNuevoRol.Text))
            {
                MessageBox.Show("Ingrese un nombre para el Rol.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Familia_54CS nuevoRol = new Familia_54CS(txtNombreNuevoRol.Text);

            try
            {
                foreach (object itemChecked in clbFamiliasYPermisos.CheckedItems)
                {
                    Rol_54CS elementoSeleccionado = (Rol_54CS)itemChecked;
                    nuevoRol.Agregar(elementoSeleccionado); // tira excepción si hay duplicados
                }

                if (nuevoRol.ObtenerHijos().Count == 0)
                {
                    MessageBox.Show("Un rol debe contener al menos un permiso o familia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Si todo es válido, guarda
                _permisosBLL.CrearRol(nuevoRol);
                RefrescarListas();
                txtNombreNuevoRol.Clear();
                MessageBox.Show("Rol creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Conflicto de Jerarquía", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarRol_Click_1(object sender, EventArgs e)
        {
            if (lbRolesExistentes.SelectedItem == null) return;

            Rol_54CS rolAEliminar = (Rol_54CS)lbRolesExistentes.SelectedItem;
            var confirmacion = MessageBox.Show($"¿Eliminar el rol '{rolAEliminar.Nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                bool estaEnUso = _usuarios.Any(u => u.RolesAsignados.Any(r => r.Nombre == rolAEliminar.Nombre));
                if (estaEnUso)
                {
                    MessageBox.Show("No se puede eliminar este Rol porque hay usuarios que lo tienen asignado.",
                                    "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                try
                {
                    _permisosBLL.EliminarRol(rolAEliminar.ID);
                    MessageBox.Show("Rol eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarListas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnAsignarUsuario_Click_1(object sender, EventArgs e)
        {
            Usuario_54CS usuarioSeleccionado = (Usuario_54CS)combobox.SelectedItem;
            if (usuarioSeleccionado == null) return;

            //usuarioSeleccionado.RolesAsignados.Clear();

            /*foreach (object itemChecked in clbRolesParaAsignar.CheckedItems)
            {
                Rol_54CS rol = (Rol_54CS)itemChecked;
                usuarioSeleccionado.RolesAsignados.Add(rol);
            }*/

            try
            {
                List<Rol_54CS> rolesSeleccionados = new List<Rol_54CS>();
                foreach (object itemChecked in clbRolesParaAsignar.CheckedItems)
                {
                    rolesSeleccionados.Add((Rol_54CS)itemChecked);
                }
                usuarioSeleccionado.RolesAsignados = rolesSeleccionados;
                usuarioSeleccionado.Rol_54CS = rolesSeleccionados.FirstOrDefault()?.Nombre ?? "Sin Asignar";
                _permisosBLL.ActualizarRolesDeUsuario(usuarioSeleccionado.DNI_54cs, rolesSeleccionados);
                List<Usuario_54CS> act = new List<Usuario_54CS>();
                act.Add(usuarioSeleccionado);
                bllusuarios.ActualizarUsuario(act, out string msj);
                MessageBox.Show($"Roles asignados exitosamente al usuario {usuarioSeleccionado.Nombre_54CS}.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}