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
    public partial class GestionarRoles : Form
    {
        private List<Rol_54CS> _rolesDelSistema = new List<Rol_54CS>();
        Rol_54CS Usuarios = new Familia_54CS("Usuarios");
        Rol_54CS Administradores = new Familia_54CS("Administradores");
        Rol_54CS Login = new Permiso_54CS("Login");
        Rol_54CS AñadirUsuario = new Permiso_54CS("Añadir Usuario");
        private List<Usuario_54CS> _usuarios = new List<Usuario_54CS>();
        public GestionarRoles()
        {
            InitializeComponent();
            _rolesDelSistema.Add(Usuarios);
            _rolesDelSistema.Add(Administradores);
            _rolesDelSistema.Add(Login);
            _rolesDelSistema.Add(AñadirUsuario);
        }

        private void GestionarRoles_Load(object sender, EventArgs e)
        {
            // Configurar los DisplayMember para que no salte el error de casteo
            ((ListBox)clbFamiliasYPermisos).DisplayMember = "Nombre";
            ((ListBox)clbRolesParaAsignar).DisplayMember = "Nombre";
            lbRolesExistentes.DisplayMember = "Nombre";
            cmbUsuarios.DisplayMember = "Nombre";
            CargarDatosDesdeBaseDeDatos();
        }

        // ==========================================
        // PESTAÑA 1: CREACIÓN Y ELIMINACIÓN DE ROLES
        // ==========================================

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (lbRolesExistentes.SelectedItem == null) return;

            Rol_54CS rolAEliminar = (Rol_54CS)lbRolesExistentes.SelectedItem;
        }

            // PRECAUCIÓN: Antes de eliminar, deberías consultar a la BD si algún usuario tiene este rol asignado
            /*bool estaEnUso = _usuarios.Any(u => u.RolesAsignados.Any(r => r.Nombre == rolAEliminar.Nombre));

            if (estaEnUso)
            {
                MessageBox.Show("No se puede eliminar este Rol porque hay usuarios que lo tienen asignado.",
                                "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Seguro que desea eliminar el rol '{rolAEliminar.Nombre}'?",
                                               "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                _rolesDelSistema.Remove(rolAEliminar);
                // Aquí harías el DELETE en tu base de datos

                RefrescarListasDeRoles();
            }
        }

        // ==========================================
        // PESTAÑA 2: ASIGNACIÓN A USUARIOS
        // ==========================================

        private void btnAsignarUsuario_Click(object sender, EventArgs e)
        {
            Usuario_54CS usuarioSeleccionado = (Usuario_54CS)cmbUsuarios.SelectedItem;
            if (usuarioSeleccionado == null) return;

            usuarioSeleccionado.RolesAsignados.Clear();

            foreach (object itemChecked in clbRolesParaAsignar.CheckedItems)
            {
                Rol_54CS rol = (Rol_54CS)itemChecked;
                usuarioSeleccionado.RolesAsignados.Add(rol);
            }

            // Aquí harías los UPDATE/INSERT en la tabla intermedia Usuario_Rol de tu BD

            MessageBox.Show($"Roles asignados exitosamente al usuario {usuarioSeleccionado.Nombre_54CS}.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==========================================
        // MÉTODOS AUXILIARES
        // ==========================================*/

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

            // 1. Usamos la clase 'Familia' como base para nuestro Rol Principal
            Familia_54CS nuevoRol = new Familia_54CS(txtNombreNuevoRol.Text);

            try
            {
                // 2. Cargamos los elementos tildados usando nuestra validación robusta
                foreach (object itemChecked in clbFamiliasYPermisos.CheckedItems)
                {
                    Rol_54CS elementoSeleccionado = (Rol_54CS)itemChecked;
                    nuevoRol.Agregar(elementoSeleccionado); // Lanzará excepción si hay duplicados
                }

                if (nuevoRol.ObtenerHijos().Count == 0)
                {
                    MessageBox.Show("Un rol debe contener al menos un permiso o familia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Si todo es válido, guardamos
                _rolesDelSistema.Add(nuevoRol);

                // Aquí deberías hacer el INSERT en tu base de datos

                RefrescarListasDeRoles();
                txtNombreNuevoRol.Clear();
                MessageBox.Show("Rol creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Conflicto de Jerarquía", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
