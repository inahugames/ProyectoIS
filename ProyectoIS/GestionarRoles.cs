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
    public partial class GestionarRoles : Form, IIdiomaObservador_54CS
    {
        private BLLUsuarios_54CS bllusuarios = new BLLUsuarios_54CS();
        private BLLRoles_54CS _rolesBLL = new BLLRoles_54CS();
        private BLLFamilias_54CS _familiasBLL = new BLLFamilias_54CS();
        private BLLPermisos_54CS _permisosBLL = new BLLPermisos_54CS();
        private List<Rol_54CS> _rolesDelSistema = new List<Rol_54CS>();
        private List<Usuario_54CS> _usuarios = new List<Usuario_54CS>();

        private List<Rol_54CS> _todasLasFamilias = new List<Rol_54CS>();
        private Familia_54CS _rolSeleccionadoFamilias;

        private List<Rol_54CS> _todosLosPermisosSueltos = new List<Rol_54CS>();
        private Familia_54CS _rolSeleccionadoPermisos;

        public GestionarRoles()
        {
            InitializeComponent();
            IdiomaManager_54CS.Suscribir(this);
            _usuarios = bllusuarios.ObtenerTodos();
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void GestionarRoles_Load(object sender, EventArgs e)
        {
            ((ListBox)clbFamiliasYPermisos).DisplayMember = "Nombre";
            ((ListBox)clbRolesParaAsignar).DisplayMember = "Nombre";
            lbRolesExistentes.DisplayMember = "Nombre";
            combobox.DisplayMember = "Nombre";

            lbRolesFamTab.DisplayMember = "Nombre";
            clbFamiliasDisponiblesRol.DisplayMember = "Nombre";
            lbFamiliasDelRol.DisplayMember = "Nombre";
            lbRolesPermTab.DisplayMember = "Nombre";
            clbPermisosDisponiblesRol.DisplayMember = "Nombre";
            lbPermisosDirectosDelRol.DisplayMember = "Nombre";

            RefrescarListas();

            btnCrearRol.Visible = false;
            btnEliminarRol.Visible = false;
            btnAgregarFamiliaRol.Visible = false;
            btnAgregarPermisoRol.Visible = false;
            btnAsignarUsuario.Visible = false;
            btnQuitarFamiliaRol.Visible = false;
            btnQuitarPermisoRol.Visible = false;
            GestionRoles.TabPages.Remove(tabPage1);
            GestionRoles.TabPages.Remove(tabPage2);
            GestionRoles.TabPages.Remove(tabFamiliasRol);
            GestionRoles.TabPages.Remove(tabPermisosRol);
            int index = 0;
            if (SessionManager_54CS.Instancia.TienePermiso("CrearRoles") || SessionManager_54CS.Instancia.TienePermiso("EliminarRoles"))
            {
                GestionRoles.TabPages.Insert(index, tabPage1);
                index++;
                if (SessionManager_54CS.Instancia.TienePermiso("CrearRoles"))
                {
                    btnCrearRol.Visible = true;
                }
                if (SessionManager_54CS.Instancia.TienePermiso("EliminarRoles"))
                {
                    btnEliminarRol.Visible = true;
                }
            }
            if (SessionManager_54CS.Instancia.TienePermiso("AsignarRoles"))
            {
                GestionRoles.TabPages.Insert(index, tabPage2);
                index++;
                btnAsignarUsuario.Visible = true;
            }
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarRoles"))
            {
                GestionRoles.TabPages.Insert(index, tabFamiliasRol);
                index++;
                GestionRoles.TabPages.Insert(index, tabPermisosRol);
                index++;
                btnAgregarFamiliaRol.Visible = true;
                btnAgregarPermisoRol.Visible = true;
                btnQuitarFamiliaRol.Visible = true;
                btnQuitarPermisoRol.Visible = true;
            }
            
        }

        private void RefrescarListasDeRoles()
        {
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

                _usuarios = bllusuarios.ObtenerTodos();
                combobox.DataSource = null;
                combobox.DataSource = _usuarios;
                combobox.DisplayMember = "Login_54CS";
                combobox.ValueMember = "DNI_54CS";

                //cargar elementos para armar el Rol (Familias + Permisos)
                var elementosDisponibles = _rolesBLL.ObtenerElementosParaCrearRol();
                foreach (var elemento in elementosDisponibles)
                {
                    clbFamiliasYPermisos.Items.Add(elemento);
                }

                //cargar Roles ya creados
                _rolesDelSistema = _rolesBLL.ObtenerRolesDelSistema();
                foreach (var rol in _rolesDelSistema)
                {
                    lbRolesExistentes.Items.Add(rol);
                    clbRolesParaAsignar.Items.Add(rol); //para la pestaña de asignacion
                }

                // cargar Familias y Permisos sueltos del sistema
                _todasLasFamilias = _familiasBLL.ObtenerFamilias();
                _todosLosPermisosSueltos = _permisosBLL.ObtenerPermisosParaCrearFamilia();

                RefrescarComposicionFamiliasRol();
                RefrescarComposicionPermisosRol();
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al cargar los datos: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosDesdeBaseDeDatos()
        {
            foreach (Rol_54CS rol in _rolesDelSistema)
            {
                clbFamiliasYPermisos.Items.Add(rol);
            }
        }

        private void RegistrarEvento(string nombreEvento)
        {
            try
            {
                BLLEventos_54CS bllev = new BLLEventos_54CS();
                Eventos_54CS evento = new Eventos_54CS()
                {
                    Login_54CS = SessionManager_54CS.Instancia.Login_54CS,
                    Fecha_54CS = DateTime.Now,
                    Modulo_54CS = "Gestión de Usuarios",
                    Evento_54CS = nombreEvento,
                    Criticidad_54CS = "3"
                };
                bllev.GuardarEvento(evento, out string msj);
            }
            catch
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error en bitácora."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearRol_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("CrearRoles"))
            {
                if (string.IsNullOrWhiteSpace(txtNombreNuevoRol.Text))
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Ingrese un nombre para el Rol."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Un rol debe contener al menos un permiso o familia."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Si todo es válido, guarda
                    _rolesBLL.CrearRol(nuevoRol);
                    RefrescarListas();
                    txtNombreNuevoRol.Clear();
                    RegistrarEvento("Crear Rol");
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Rol creado exitosamente."), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("Conflicto de Jerarquía"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarRol_Click_1(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("EliminarRoles"))
            {
                if (lbRolesExistentes.SelectedItem == null) return;
                Rol_54CS rolAEliminar = (Rol_54CS)lbRolesExistentes.SelectedItem;
                var confirmacion = MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("¿Eliminar el rol '{0}'?"), rolAEliminar.Nombre), IdiomaManager_54CS.TraducirMensaje("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    bool estaEnUso = _usuarios.Any(u => u.RolesAsignados.Any(r => r.Nombre == rolAEliminar.Nombre));
                    if (estaEnUso)
                    {
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No se puede eliminar este Rol porque hay usuarios que lo tienen asignado."),
                                        IdiomaManager_54CS.TraducirMensaje("Acción Denegada"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    try
                    {
                        _rolesBLL.EliminarRol(rolAEliminar.ID);
                        RegistrarEvento("Eliminar Rol");
                        MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Rol eliminado."), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefrescarListas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("Acción Denegada"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarUsuario_Click_1(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("AsignarRoles"))
            {
                Usuario_54CS usuarioSeleccionado = (Usuario_54CS)combobox.SelectedItem;
                if (usuarioSeleccionado == null) return;

                if (clbRolesParaAsignar.CheckedItems.Count == 0)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Los campos están vacíos."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (combobox.Text == SessionManager_54CS.Instancia.Login_54CS)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No se pueden modificar los roles del usuario que se encuentra logeado actualmente."), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
                    _rolesBLL.ActualizarRolesDeUsuario(usuarioSeleccionado.DNI_54cs, rolesSeleccionados);
                    List<Usuario_54CS> act = new List<Usuario_54CS>();
                    act.Add(usuarioSeleccionado);
                    bllusuarios.ActualizarUsuario(act, out string msj);
                    RegistrarEvento("Asignar Rol");
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Roles asignados exitosamente al usuario {0}."), usuarioSeleccionado.Nombre_54CS),
                                IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Error al asignar: ") + ex.Message, IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefrescarComposicionFamiliasRol()
        {
            string nombrePrevio = _rolSeleccionadoFamilias?.Nombre;

            lbRolesFamTab.Items.Clear();
            foreach (var rol in _rolesDelSistema)
            {
                lbRolesFamTab.Items.Add(rol);
            }

            if (nombrePrevio != null)
            {
                foreach (var item in lbRolesFamTab.Items)
                {
                    var rol = item as Rol_54CS;
                    if (rol != null && rol.Nombre.Equals(nombrePrevio, StringComparison.OrdinalIgnoreCase))
                    {
                        lbRolesFamTab.SelectedItem = item; // refresca los paneles
                        return;
                    }
                }
            }

            _rolSeleccionadoFamilias = null;
            ActualizarPanelFamiliasDeRol();
        }

        private void lbRolesFamTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            _rolSeleccionadoFamilias = lbRolesFamTab.SelectedItem as Familia_54CS;
            ActualizarPanelFamiliasDeRol();
        }

        private void ActualizarPanelFamiliasDeRol()
        {
            clbFamiliasDisponiblesRol.Items.Clear();
            lbFamiliasDelRol.Items.Clear();

            if (_rolSeleccionadoFamilias == null)
                return;

            foreach (var hijo in _rolSeleccionadoFamilias.ObtenerHijos())
            {
                if (hijo is Familia_54CS familiaHija)
                    lbFamiliasDelRol.Items.Add(familiaHija);
            }

            var nombresYaEnRol = new HashSet<string>(
                _rolSeleccionadoFamilias.ObtenerHijos()
                    .OfType<Familia_54CS>()
                    .Select(f => f.Nombre),
                StringComparer.OrdinalIgnoreCase);

            foreach (var item in _todasLasFamilias)
            {
                if (!(item is Familia_54CS familia))
                    continue;

                if (!nombresYaEnRol.Contains(familia.Nombre))
                    clbFamiliasDisponiblesRol.Items.Add(familia);
            }
        }

        private void btnAgregarFamiliaRol_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarRoles"))
            {
                if (_rolSeleccionadoFamilias == null)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione primero un Rol de la lista."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var familiasAAgregar = clbFamiliasDisponiblesRol.CheckedItems.Cast<object>().Select(o => (Familia_54CS)o).ToList();
                if (familiasAAgregar.Count == 0)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione (tilde) al menos una familia para agregar."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombreRol = _rolSeleccionadoFamilias.Nombre;
                int agregadas = 0;
                try
                {
                    foreach (var familia in familiasAAgregar)
                    {
                        _rolesBLL.AgregarFamiliaARol(_rolSeleccionadoFamilias, familia);
                        agregadas++;
                    }

                    RegistrarEvento("Agregar Familia a Rol");
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Se agregaron {0} familia(s) al rol '{1}'."), agregadas, nombreRol), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (nombreRol == SessionManager_54CS.Instancia.Rol_54CS.Trim())
                    {
                        foreach (Usuario_54CS user in _usuarios)
                        {
                            if (user.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                            {
                                bllusuarios.CargarPermisosDelUsuarioEnSesion(user);
                                SessionManager_54CS.Logout();
                                SessionManager_54CS.Login(user.Login_54CS, user.Nombre_54CS, user.Rol_54CS, user.RolesAsignados, user.Idioma_54CS);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("No se pudo agregar la familia"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    RefrescarListas();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarFamiliaRol_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarRoles"))
            {
                if (_rolSeleccionadoFamilias == null)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione primero un Rol de la lista."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var familiaSeleccionada = lbFamiliasDelRol.SelectedItem as Familia_54CS;
                if (familiaSeleccionada == null)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione la familia que desea quitar del rol."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmacion = MessageBox.Show(
                    string.Format(IdiomaManager_54CS.TraducirMensaje("¿Quitar la familia '{0}' del rol '{1}'?"), familiaSeleccionada.Nombre, _rolSeleccionadoFamilias.Nombre),
                    IdiomaManager_54CS.TraducirMensaje("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                    return;

                try
                {
                    _rolesBLL.QuitarFamiliaDeRol(_rolSeleccionadoFamilias, familiaSeleccionada);
                    RegistrarEvento("Quitar Familia de Rol");
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Familia quitada del rol."), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_rolSeleccionadoFamilias.Nombre == SessionManager_54CS.Instancia.Rol_54CS.Trim())
                    {
                        foreach (Usuario_54CS user in _usuarios)
                        {
                            if (user.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                            {
                                bllusuarios.CargarPermisosDelUsuarioEnSesion(user);
                                SessionManager_54CS.Logout();
                                SessionManager_54CS.Login(user.Login_54CS, user.Nombre_54CS, user.Rol_54CS, user.RolesAsignados, user.Idioma_54CS);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("No se pudo quitar la familia"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    RefrescarListas();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefrescarComposicionPermisosRol()
        {
            string nombrePrevio = _rolSeleccionadoPermisos?.Nombre;

            lbRolesPermTab.Items.Clear();
            foreach (var rol in _rolesDelSistema)
            {
                lbRolesPermTab.Items.Add(rol);
            }

            if (nombrePrevio != null)
            {
                foreach (var item in lbRolesPermTab.Items)
                {
                    var rol = item as Rol_54CS;
                    if (rol != null && rol.Nombre.Equals(nombrePrevio, StringComparison.OrdinalIgnoreCase))
                    {
                        lbRolesPermTab.SelectedItem = item; // dispara SelectedIndexChanged y refresca los paneles
                        return;
                    }
                }
            }

            _rolSeleccionadoPermisos = null;
            ActualizarPanelPermisosDeRol();
        }

        private void lbRolesPermTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            _rolSeleccionadoPermisos = lbRolesPermTab.SelectedItem as Familia_54CS;
            ActualizarPanelPermisosDeRol();
        }

        private void ActualizarPanelPermisosDeRol()
        {
            clbPermisosDisponiblesRol.Items.Clear();
            lbPermisosDirectosDelRol.Items.Clear();
            lbPermisosEfectivosRol.Items.Clear();

            if (_rolSeleccionadoPermisos == null)
                return;

            foreach (var hijo in _rolSeleccionadoPermisos.ObtenerHijos())
            {
                var permisoDirecto = hijo as Permiso_54CS;
                if (permisoDirecto != null)
                {
                    lbPermisosDirectosDelRol.Items.Add(permisoDirecto);
                }
            }

            var efectivos = _rolSeleccionadoPermisos.ObtenerListaPermisos().Distinct().OrderBy(p => p).ToList();
            foreach (var permiso in efectivos)
            {
                lbPermisosEfectivosRol.Items.Add(permiso);
            }

            foreach (var item in _todosLosPermisosSueltos)
            {
                var permiso = item as Permiso_54CS;
                if (permiso == null)
                    continue;

                if (!_rolSeleccionadoPermisos.TienePermiso(permiso.Nombre))
                {
                    clbPermisosDisponiblesRol.Items.Add(permiso);
                }
            }
        }

        private void btnAgregarPermisoRol_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarRoles"))
            {
                if (_rolSeleccionadoPermisos == null)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione primero un Rol de la lista."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var permisosAAgregar = clbPermisosDisponiblesRol.CheckedItems.Cast<object>().Select(o => (Permiso_54CS)o).ToList();
                if (permisosAAgregar.Count == 0)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione (tilde) al menos un permiso para agregar."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombreRol = _rolSeleccionadoPermisos.Nombre;
                int agregados = 0;
                try
                {
                    foreach (var permiso in permisosAAgregar)
                    {
                        _rolesBLL.AgregarPermisoARol(_rolSeleccionadoPermisos, permiso);
                        agregados++;
                    }

                    RegistrarEvento("Agregar Permiso a Rol");
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Se agregaron {0} permiso(s) al rol '{1}'."), agregados, nombreRol), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (nombreRol == SessionManager_54CS.Instancia.Rol_54CS.Trim())
                    {
                        foreach (Usuario_54CS user in _usuarios)
                        {
                            if (user.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                            {
                                bllusuarios.CargarPermisosDelUsuarioEnSesion(user);
                                SessionManager_54CS.Logout();
                                SessionManager_54CS.Login(user.Login_54CS, user.Nombre_54CS, user.Rol_54CS, user.RolesAsignados, user.Idioma_54CS);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("No se pudo agregar el permiso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    RefrescarListas();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarPermisoRol_Click(object sender, EventArgs e)
        {
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarRoles"))
            {
                if (_rolSeleccionadoPermisos == null)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione primero un Rol de la lista."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var permisoSeleccionado = lbPermisosDirectosDelRol.SelectedItem as Permiso_54CS;
                if (permisoSeleccionado == null)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione el permiso directo que desea quitar del rol."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmacion = MessageBox.Show(
                    string.Format(IdiomaManager_54CS.TraducirMensaje("¿Eliminar el permiso '{0}' del rol '{1}'?"), permisoSeleccionado.Nombre, _rolSeleccionadoPermisos.Nombre),
                    IdiomaManager_54CS.TraducirMensaje("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                    return;

                try
                {
                    _rolesBLL.QuitarPermisoDeRol(_rolSeleccionadoPermisos, permisoSeleccionado);
                    RegistrarEvento("Eliminar Permiso de Rol");
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Permiso eliminado del rol."), IdiomaManager_54CS.TraducirMensaje("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_rolSeleccionadoPermisos.Nombre == SessionManager_54CS.Instancia.Rol_54CS.Trim())
                    {
                        foreach (Usuario_54CS user in _usuarios)
                        {
                            if (user.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                            {
                                bllusuarios.CargarPermisosDelUsuarioEnSesion(user);
                                SessionManager_54CS.Logout();
                                SessionManager_54CS.Login(user.Login_54CS, user.Nombre_54CS, user.Rol_54CS, user.RolesAsignados, user.Idioma_54CS);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_54CS.TraducirMensaje("No se pudo quitar el permiso"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    RefrescarListas();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes"), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
