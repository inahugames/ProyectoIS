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

        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosRoles();
        }

        private void GestionarRoles_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            ConfigurarVistaRoles();
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            _usuarios = bllusuarios.ObtenerTodos();
            ((ListBox)clbFamiliasYPermisos).DisplayMember = "Nombre";
            lbRolesExistentes.DisplayMember = "Nombre";

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
            btnQuitarFamiliaRol.Visible = false;
            btnQuitarPermisoRol.Visible = false;
            GestionRoles.TabPages.Remove(tabPage1);
            GestionRoles.TabPages.Remove(tabFamiliasRol);
            GestionRoles.TabPages.Remove(tabPermisosRol);
            if (SessionManager_54CS.Instancia.TienePermiso("VerRoles") || SessionManager_54CS.Instancia.TienePermiso("CrearRoles") || SessionManager_54CS.Instancia.TienePermiso("EliminarRoles"))
            {
                GestionRoles.TabPages.Add(tabPage1);
                if (SessionManager_54CS.Instancia.TienePermiso("CrearRoles"))
                {
                    btnCrearRol.Visible = true;
                }
                if (SessionManager_54CS.Instancia.TienePermiso("EliminarRoles"))
                {
                    btnEliminarRol.Visible = true;
                }
            }
            if (SessionManager_54CS.Instancia.TienePermiso("ModificarRoles"))
            {
                GestionRoles.TabPages.Add(tabFamiliasRol);
                GestionRoles.TabPages.Add(tabPermisosRol);
                btnAgregarFamiliaRol.Visible = true;
                btnAgregarPermisoRol.Visible = true;
                btnQuitarFamiliaRol.Visible = true;
                btnQuitarPermisoRol.Visible = true;
            }
            SincronizarVistaRoles();
        }

        private void RefrescarListasDeRoles()
        {
            lbRolesExistentes.Items.Clear();

            foreach (var rol in _rolesDelSistema)
            {
                lbRolesExistentes.Items.Add(rol);
            }
        }

        private void RefrescarListas()
        {
            int? seleccionado = (lbRolesExistentes.SelectedItem as Rol_54CS)?.ID;
            sincronizandoRoles = true;
            try
            {
                clbFamiliasYPermisos.Items.Clear();
                lbRolesExistentes.Items.Clear();

                _usuarios = bllusuarios.ObtenerTodos();

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
            finally
            {
                sincronizandoRoles = false;
                lbRolesExistentes.SelectedItem = _rolesDelSistema.FirstOrDefault(r => r.ID == seleccionado);
                SincronizarSeleccionRoles();
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

        private UsuariosRoundedPanel[] tarjetasRoles => new[] { lateralRoles, generalRolesTarjeta, campoNombreRol, familiasDisponiblesTarjeta, familiasAsignadasTarjeta, permisosDisponiblesTarjeta, permisosAsignadosTarjeta, efectivosRolesTarjeta };
        private Dictionary<Control, string> textosRoles => new Dictionary<Control, string>
        {
            { tituloRolesVista, "tituloRolesVista" },
            { subtituloRolesVista, "subtituloRolesVista" },
            { pieRolesVista, "pieRolesVista" },
            { nuevoRolVista, "nuevoRolVista" },
            { generalRoles, "generalRoles" },
            { familiasRoles, "familiasRoles" },
            { permisosRoles, "permisosRoles" },
            { ayudaEfectivosRoles, "ayudaEfectivosRoles" }
        };
        private Panel[] paginasRoles => new[] { paginaRoles0, paginaRoles1, paginaRoles2 };
        private UsuariosRoundedButton[] pestanasRoles => new[] { generalRoles, familiasRoles, permisosRoles };
        private int paginaRoles = -1, desplazamientoRoles;
        private bool sincronizandoRoles;

        private Color AcentoRoles => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);
        private Color SuperficieRoles => Tema_54CS.EsOscuro ? Color.FromArgb(43, 43, 41) : Color.White;
        private Color BordeRoles => Tema_54CS.EsOscuro ? Color.FromArgb(74, 74, 70) : Color.FromArgb(219, 223, 231);

        private void ConfigurarVistaRoles()
        {
            generalRolesTarjeta.Resize += (s, e) => DistribuirGeneralRoles();
            paginaRoles1.Resize += (s, e) => DistribuirComposicionRoles(paginaRoles1, familiasDisponiblesTarjeta, familiasAsignadasTarjeta, clbFamiliasDisponiblesRol, lbFamiliasDelRol, lblFamiliasDisponiblesRol, lblFamiliasDelRol, btnAgregarFamiliaRol, btnQuitarFamiliaRol, false);
            paginaRoles2.Resize += (s, e) => DistribuirComposicionRoles(paginaRoles2, permisosDisponiblesTarjeta, permisosAsignadosTarjeta, clbPermisosDisponiblesRol, lbPermisosDirectosDelRol, lblPermisosDisponiblesRol, lblPermisosDirectosDelRol, btnAgregarPermisoRol, btnQuitarPermisoRol, true);
            DistribuirVistaRoles();
            DistribuirGeneralRoles();
        }

        private void DistribuirGeneralRoles()
        {
            campoNombreRol.Width = Math.Max(100, generalRolesTarjeta.Width - 44);
            clbFamiliasYPermisos.SetBounds(24, 154, Math.Max(100, generalRolesTarjeta.Width - 48), Math.Max(40, generalRolesTarjeta.Height - 226));
            btnCrearRol.SetBounds(Math.Max(24, generalRolesTarjeta.Width - 230), generalRolesTarjeta.Height - 60, 206, 40);
        }

        private void DistribuirComposicionRoles(Panel page, UsuariosRoundedPanel izquierda, UsuariosRoundedPanel derecha, CheckedListBox disponibles, ListBox asignados, Label tituloDisponibles, Label tituloAsignados, Button agregar, Button quitar, bool efectivos)
        {
            UsuariosRoundedPanel inferior = efectivos ? efectivosRolesTarjeta : null;
            Label ayuda = ayudaEfectivosRoles;
                int ancho = Math.Max(130, (page.Width - 148) / 2);
                int alto = efectivos ? Math.Max(160, page.Height / 2 - 12) : page.Height;
                izquierda.SetBounds(0, 0, ancho, alto);
                derecha.SetBounds(ancho + 148, 0, ancho, alto);
                tituloDisponibles.SetBounds(16, 18, ancho - 32, 44);
                tituloAsignados.SetBounds(16, 18, ancho - 32, 44);
                disponibles.SetBounds(18, 68, ancho - 36, Math.Max(40, alto - 90));
                asignados.SetBounds(18, 68, ancho - 36, Math.Max(40, alto - 90));
                agregar.SetBounds(ancho + 12, Math.Max(64, alto / 2 - 45), 124, 40);
                quitar.SetBounds(ancho + 12, Math.Max(114, alto / 2 + 5), 124, 40);
                if (inferior != null)
                {
                    inferior.SetBounds(0, alto + 14, page.Width, page.Height - alto - 14);
                    lblPermisosEfectivosRol.SetBounds(18, 16, inferior.Width - 36, 27);
                    ayuda.SetBounds(18, 46, inferior.Width - 36, 25);
                    lbPermisosEfectivosRol.SetBounds(18, 80, inferior.Width - 36, Math.Max(30, inferior.Height - 100));
                }
        }

        private void temaRoles_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void nuevoRolVista_Click(object sender, EventArgs e)
        {
            MostrarPaginaRoles(0, true);
            txtNombreNuevoRol.Focus();
        }

        private void pestanaRoles_Click(object sender, EventArgs e)
        {
            MostrarPaginaRoles(Array.IndexOf(pestanasRoles, sender), true);
        }

        private void lbRolesExistentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SincronizarSeleccionRoles();
        }

        private void animacionRoles_Tick(object sender, EventArgs e)
        {
            desplazamientoRoles = Math.Max(0, desplazamientoRoles - 3);
            PosicionarPaginasRoles();
            if (desplazamientoRoles == 0) animacionRoles.Stop();
        }

        private void GestionarRoles_Resize(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) DistribuirVistaRoles();
        }

        private void DistribuirVistaRoles()
        {
            if (cuerpoRoles == null) return;
            temaRoles.Location = new Point(ClientSize.Width - 122, 27);
            subtituloRolesVista.Width = Math.Max(100, ClientSize.Width - 115);
            cuerpoRoles.SetBounds(24, 108, Math.Max(880, ClientSize.Width - 48), Math.Max(430, ClientSize.Height - 155));
            lateralRoles.SetBounds(0, 0, 252, cuerpoRoles.Height);
            lbRolesExistentes.SetBounds(18, 60, 216, Math.Max(70, lateralRoles.Height - 192));
            nuevoRolVista.SetBounds(18, lateralRoles.Height - 116, 216, 42);
            btnEliminarRol.SetBounds(18, lateralRoles.Height - 62, 216, 42);
            contenidoRoles.SetBounds(272, 0, cuerpoRoles.Width - 272, cuerpoRoles.Height);
            int x = 0;
            foreach (var boton in pestanasRoles)
            {
                boton.Location = new Point(x, 0);
                if (boton.Visible) x += 158;
            }
            PosicionarPaginasRoles();
            pieRolesVista.SetBounds(28, ClientSize.Height - 34, ClientSize.Width - 56, 24);
        }

        private void PosicionarPaginasRoles()
        {
            foreach (Panel pagina in paginasRoles)
                pagina.SetBounds(0, 54 + desplazamientoRoles, contenidoRoles.Width, Math.Max(100, contenidoRoles.Height - 54 - desplazamientoRoles));
        }

        private void SincronizarSeleccionRoles()
        {
            if (sincronizandoRoles) return;
            sincronizandoRoles = true;
            try
            {
                var seleccionado = lbRolesExistentes.SelectedItem as Rol_54CS;
                foreach (ListBox lista in new[] { lbRolesFamTab, lbRolesPermTab })
                    lista.SelectedItem = seleccionado == null ? null : lista.Items.Cast<Rol_54CS>().FirstOrDefault(r => r.ID == seleccionado.ID);
            }
            finally { sincronizandoRoles = false; }
        }

        private void SincronizarVistaRoles()
        {
            if (pestanasRoles == null) return;
            TabPage[] originales = { tabPage1, tabFamiliasRol, tabPermisosRol };
            for (int i = 0; i < originales.Length; i++) pestanasRoles[i].Visible = GestionRoles.TabPages.Contains(originales[i]);
            nuevoRolVista.Visible = SessionManager_54CS.Instancia.TienePermiso("CrearRoles");
            txtNombreNuevoRol.Enabled = clbFamiliasYPermisos.Enabled = nuevoRolVista.Visible;
            if (paginaRoles < 0 || !GestionRoles.TabPages.Contains(originales[paginaRoles]))
                paginaRoles = Array.FindIndex(originales, p => GestionRoles.TabPages.Contains(p));
            MostrarPaginaRoles(paginaRoles, false);
            SincronizarSeleccionRoles();
            DistribuirVistaRoles();
        }

        private void MostrarPaginaRoles(int indice, bool animar)
        {
            TabPage[] originales = { tabPage1, tabFamiliasRol, tabPermisosRol };
            if (indice >= 0 && !GestionRoles.TabPages.Contains(originales[indice])) return;
            paginaRoles = indice;
            for (int i = 0; i < paginasRoles.Length; i++) paginasRoles[i].Visible = i == indice;
            desplazamientoRoles = animar ? 15 : 0;
            if (animar) animacionRoles.Start();
            PosicionarPaginasRoles();
            AplicarTemaRoles();
        }

        private void DibujarFilaRoles(object sender, DrawItemEventArgs e)
        {
            var lista = (ListBox)sender;
            if (e.Index < 0 || e.Index >= lista.Items.Count) return;
            bool seleccionado = (e.State & DrawItemState.Selected) != 0;
            Color fondo = seleccionado ? (Tema_54CS.EsOscuro ? Color.FromArgb(78, 57, 47) : Color.FromArgb(229, 237, 253)) : lista.BackColor;
            using (var brush = new SolidBrush(fondo)) e.Graphics.FillRectangle(brush, e.Bounds);
            string nombre = lista.GetItemText(lista.Items[e.Index]);
            var texto = new Rectangle(e.Bounds.X + 10, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height);
            if (lista == lbPermisosEfectivosRol)
            {
                bool directo = lbPermisosDirectosDelRol.Items.Cast<Rol_54CS>().Any(p => p.Nombre == nombre);
                string badge = TextoRoles(directo ? "directoRoles" : "heredadoRoles");
                var rect = new Rectangle(e.Bounds.Right - 116, e.Bounds.Y + 5, 108, e.Bounds.Height - 10);
                Color badgeColor = directo ? AcentoRoles : (Tema_54CS.EsOscuro ? Color.FromArgb(196, 171, 240) : Color.FromArgb(113, 75, 162));
                TextRenderer.DrawText(e.Graphics, badge, lista.Font, rect, badgeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                texto.Width -= 120;
            }
            TextRenderer.DrawText(e.Graphics, nombre, lista.Font, texto, seleccionado ? AcentoRoles : ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            if ((e.State & DrawItemState.Focus) != 0) e.DrawFocusRectangle();
        }

        private string TextoRoles(string clave) => IdiomaManager_54CS.ObtenerTexto("GestionarRoles", clave, clave);

        private void ActualizarTextosRoles()
        {
            if (temaRoles == null) return;
            foreach (var par in textosRoles) par.Key.Text = TextoRoles(par.Value);
            foreach (Button boton in new[] { btnAgregarFamiliaRol, btnAgregarPermisoRol }) boton.Text = TextoRoles("agregarRoles");
            foreach (Button boton in new[] { btnQuitarFamiliaRol, btnQuitarPermisoRol }) boton.Text = TextoRoles("quitarRoles");
            temaRoles.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro");
            txtNombreNuevoRol.AccessibleName = label6.Text;
            lbRolesExistentes.AccessibleName = label7.Text;
            clbFamiliasYPermisos.AccessibleName = label5.Text;
            clbFamiliasDisponiblesRol.AccessibleName = lblFamiliasDisponiblesRol.Text;
            lbFamiliasDelRol.AccessibleName = lblFamiliasDelRol.Text;
            clbPermisosDisponiblesRol.AccessibleName = lblPermisosDisponiblesRol.Text;
            lbPermisosDirectosDelRol.AccessibleName = lblPermisosDirectosDelRol.Text;
            lbPermisosEfectivosRol.AccessibleName = lblPermisosEfectivosRol.Text;
            lbPermisosEfectivosRol.Invalidate();
        }

        public void AplicarTemaRoles()
        {
            if (temaRoles == null) return;
            BackColor = Tema_54CS.EsOscuro ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = Tema_54CS.EsOscuro ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            cuerpoRoles.BackColor = contenidoRoles.BackColor = BackColor;
            foreach (Panel pagina in paginasRoles) pagina.BackColor = BackColor;
            foreach (var tarjeta in tarjetasRoles) { tarjeta.BackColor = SuperficieRoles; tarjeta.BorderColor = BordeRoles; }
            foreach (ListBox lista in new ListBox[] { lbRolesExistentes, lbFamiliasDelRol, lbPermisosDirectosDelRol, lbPermisosEfectivosRol, clbFamiliasYPermisos, clbFamiliasDisponiblesRol, clbPermisosDisponiblesRol })
            {
                lista.BackColor = SuperficieRoles;
                lista.ForeColor = ForeColor;
                lista.BorderStyle = BorderStyle.None;
            }
            txtNombreNuevoRol.BorderStyle = BorderStyle.None;
            txtNombreNuevoRol.BackColor = SuperficieRoles;
            txtNombreNuevoRol.ForeColor = ForeColor;
            foreach (Label label in new[] { label5, label6, label7, lblFamiliasDelRol, lblFamiliasDisponiblesRol, lblPermisosDisponiblesRol, lblPermisosDirectosDelRol, lblPermisosEfectivosRol })
            {
                label.ForeColor = ForeColor;
                label.BackColor = Color.Transparent;
            }
            foreach (var par in textosRoles) if (par.Key is Label) par.Key.ForeColor = ForeColor;
            subtituloRolesVista.ForeColor = pieRolesVista.ForeColor = Tema_54CS.EsOscuro ? Color.Silver : Color.DimGray;
            foreach (Control control in Controls) if (control is AltaGlyph) control.ForeColor = AcentoRoles;
            foreach (var boton in new[] { btnCrearRol, btnEliminarRol, btnAgregarFamiliaRol, btnQuitarFamiliaRol, btnAgregarPermisoRol, btnQuitarPermisoRol, nuevoRolVista }.Concat(pestanasRoles))
            {
                bool primario = boton == btnCrearRol || boton == nuevoRolVista || (paginaRoles >= 0 && boton == pestanasRoles[paginaRoles]);
                boton.BackColor = primario ? AcentoRoles : SuperficieRoles;
                boton.ForeColor = primario ? (Tema_54CS.EsOscuro ? Color.FromArgb(35, 30, 27) : Color.White) : AcentoRoles;
                boton.FlatAppearance.BorderColor = boton == btnEliminarRol ? Color.FromArgb(215, 98, 86) : BordeRoles;
                boton.FlatAppearance.BorderSize = primario ? 0 : 1;
                ((UsuariosRoundedButton)boton).HoverBackColor = primario
                    ? (Tema_54CS.EsOscuro ? Color.FromArgb(246, 183, 152) : Color.FromArgb(35, 78, 170))
                    : (Tema_54CS.EsOscuro ? Color.FromArgb(94, 72, 58) : Color.FromArgb(216, 230, 254));
            }
            temaRoles.DarkMode = Tema_54CS.EsOscuro;
            ActualizarTextosRoles();
            Invalidate(true);
        }
    }
}
