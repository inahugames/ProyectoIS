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
            ActualizarTextosFamilias();
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
                var familiasAEliminar = lbFamiliasGestion.CheckedItems.Cast<Familia_54CS>().ToList();
                if (familiasAEliminar.Count == 0)
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Tilde al menos una familia para eliminar."), IdiomaManager_54CS.TraducirMensaje("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            paginaNuevaFamilia.Resize += (s, a) => DistribuirNuevaFamilia();
            paginaPermisosFamilia.Resize += (s, a) => DistribuirPermisosFamilia();
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            DistribuirVistaFamilias();
            DistribuirNuevaFamilia();
            DistribuirPermisosFamilia();
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
            if (SessionManager_54CS.Instancia.TienePermiso("CrearFamilias") || SessionManager_54CS.Instancia.TienePermiso("EliminarFamilias") || SessionManager_54CS.Instancia.TienePermiso("VerFamilias"))
            {
                tabsGestionFamilias.TabPages.Add(tabCrearFamilia);
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
                tabsGestionFamilias.TabPages.Add(tabPermisosFamilia);
                btnAgregarPermisoFamilia.Visible = true;
                btnQuitarPermisoFamilia.Visible = true;
            }
            SincronizarVistaFamilias();
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
            int? seleccionado = _familiaSeleccionada?.ID;
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
                lbFamiliasGestion.SelectedItem = lbFamiliasGestion.Items.Cast<Familia_54CS>().FirstOrDefault(f => f.ID == seleccionado);
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
            ActualizarResumenFamilia();
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
            if (!SessionManager_54CS.Instancia.TienePermiso("ModificarFamilias"))
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."),
                    IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            if (!SessionManager_54CS.Instancia.TienePermiso("ModificarFamilias"))
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."),
                    IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

        private UsuariosRoundedPanel[] tarjetasFamilias => new[] { lateralFamilias, resumenFamilia, datosFamilia, campoNombreFamilia, campoDescripcionFamilia, disponiblesNuevaFamilia, vistaPreviaFamilia, disponiblesPermisosFamilia, asignadosPermisosFamilia };
        private Dictionary<Control, string> textosFamilias => new Dictionary<Control, string>
        {
            { tituloFamiliasVista, "tituloFamiliasVista" },
            { subtituloFamiliasVista, "subtituloFamiliasVista" },
            { pieFamiliasVista, "pieFamiliasVista" },
            { ayudaFamiliaVista, "ayudaFamiliaVista" },
            { ayudaEliminarFamilias, "ayudaEliminarFamilias" },
            { nuevaFamiliaVista, "nuevaFamiliaVista" },
            { tabNuevaFamiliaVista, "tabNuevaFamiliaVista" },
            { tabPermisosFamiliaVista, "tabPermisosFamiliaVista" }
        };
        private Panel[] paginasFamilias => new[] { paginaNuevaFamilia, paginaPermisosFamilia };
        private int paginaFamilias = -1, desplazamientoFamilias;
        private Color AcentoFamilias => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);
        private Color SuperficieFamilias => Tema_54CS.EsOscuro ? Color.FromArgb(43, 43, 41) : Color.White;
        private Color BordeFamilias => Tema_54CS.EsOscuro ? Color.FromArgb(74, 74, 70) : Color.FromArgb(219, 223, 231);

        private void DistribuirNuevaFamilia()
        {
            var pagina = paginaNuevaFamilia;
            var campoNombre = campoNombreFamilia;
            var campoDescripcion = campoDescripcionFamilia;
            var disponibles = disponiblesNuevaFamilia;
            var vistaPrevia = vistaPreviaFamilia;
                int ancho = pagina.Width;
                datosFamilia.SetBounds(0, 0, ancho, 166);
                label1.SetBounds(20, 14, ancho - 40, 24);
                campoNombre.SetBounds(18, 40, ancho - 36, 42);
                txtNombre.SetBounds(12, 10, ancho - 64, 24);
                label4.SetBounds(20, 87, ancho - 40, 24);
                campoDescripcion.SetBounds(18, 113, ancho - 36, 42);
                txtDesc.SetBounds(12, 10, ancho - 64, 24);
                int mitad = (ancho - 14) / 2;
                bool crear = SessionManager_54CS.HaySesion && SessionManager_54CS.Instancia.TienePermiso("CrearFamilias");
                int inicio = crear ? 180 : 0;
                int alto = Math.Max(130, pagina.Height - inicio - (crear ? 58 : 0));
                disponibles.SetBounds(0, inicio, mitad, alto);
                vistaPrevia.SetBounds(mitad + 14, inicio, mitad, alto);
                label2.SetBounds(16, 15, mitad - 32, 44);
                label3.SetBounds(16, 15, mitad - 32, 44);
                chklist.SetBounds(18, 64, mitad - 36, alto - 84);
                listFamilias.SetBounds(18, 64, mitad - 36, alto - 84);
                btnCrear.SetBounds(ancho - 210, pagina.Height - 46, 206, 42);
        }

        private void DistribuirPermisosFamilia()
        {
            var pagina = paginaPermisosFamilia;
            var disponibles = disponiblesPermisosFamilia;
            var asignados = asignadosPermisosFamilia;
                resumenFamilia.SetBounds(0, 0, pagina.Width, 90);
                nombreFamiliaVista.SetBounds(22, 14, pagina.Width - 44, 30);
                ayudaFamiliaVista.SetBounds(22, 49, pagina.Width - 44, 28);
                int ancho = Math.Max(130, (pagina.Width - 148) / 2);
                int alto = Math.Max(150, pagina.Height - 106);
                disponibles.SetBounds(0, 106, ancho, alto);
                asignados.SetBounds(ancho + 148, 106, ancho, alto);
                lblPermisosDisponiblesFam.SetBounds(16, 18, ancho - 32, 44);
                lblPermisosDeFamilia.SetBounds(16, 18, ancho - 32, 44);
                clbPermisosDisponiblesFam.SetBounds(18, 68, ancho - 36, alto - 90);
                lbPermisosDeFamilia.SetBounds(18, 68, ancho - 36, alto - 90);
                btnAgregarPermisoFamilia.SetBounds(ancho + 12, 106 + alto / 2 - 45, 124, 40);
                btnQuitarPermisoFamilia.SetBounds(ancho + 12, 106 + alto / 2 + 5, 124, 40);
        }

        private void temaFamilias_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void nuevaFamiliaVista_Click(object sender, EventArgs e)
        {
            MostrarPaginaFamilias(0, true);
            txtNombre.Focus();
        }

        private void tabNuevaFamiliaVista_Click(object sender, EventArgs e)
        {
            MostrarPaginaFamilias(0, true);
        }

        private void tabPermisosFamiliaVista_Click(object sender, EventArgs e)
        {
            MostrarPaginaFamilias(1, true);
        }

        private void lbFamiliasGestion_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            if (!SessionManager_54CS.Instancia.TienePermiso("EliminarFamilias")) e.NewValue = CheckState.Unchecked;
        }

        private void listaFamilias_DrawItem(object sender, DrawItemEventArgs e)
        {
            var lista = (ListBox)sender;
            if (e.Index < 0 || e.Index >= lista.Items.Count) return;
            bool selected = (e.State & DrawItemState.Selected) != 0;
            using (var brush = new SolidBrush(selected ? AcentoFamilias : lista.BackColor)) e.Graphics.FillRectangle(brush, e.Bounds);
            TextRenderer.DrawText(e.Graphics, lista.GetItemText(lista.Items[e.Index]), lista.Font, e.Bounds, selected ? Color.Black : ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
            e.DrawFocusRectangle();
        }

        private void animacionFamilias_Tick(object sender, EventArgs e)
        {
            desplazamientoFamilias = Math.Max(0, desplazamientoFamilias - 3);
            PosicionarPaginasFamilias();
            if (desplazamientoFamilias == 0) animacionFamilias.Stop();
        }

        private void GestionarFamilias_Resize(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) DistribuirVistaFamilias();
        }

        private void DistribuirVistaFamilias()
        {
            if (cuerpoFamilias == null) return;
            temaFamilias.Location = new Point(ClientSize.Width - 122, 27);
            cuerpoFamilias.SetBounds(24, 108, Math.Max(800, ClientSize.Width - 48), Math.Max(510, ClientSize.Height - 155));
            lateralFamilias.SetBounds(0, 0, 262, cuerpoFamilias.Height);
            lblFamiliasGestion.SetBounds(20, 18, 222, 28);
            lbFamiliasGestion.SetBounds(18, 60, 226, lateralFamilias.Height - 248);
            ayudaEliminarFamilias.SetBounds(20, lateralFamilias.Height - 178, 222, 52);
            nuevaFamiliaVista.SetBounds(18, lateralFamilias.Height - 116, 226, 42);
            btnEliminarFamilia.SetBounds(18, lateralFamilias.Height - 62, 226, 42);
            contenidoFamilias.SetBounds(280, 0, cuerpoFamilias.Width - 280, cuerpoFamilias.Height);
            tabNuevaFamiliaVista.Location = Point.Empty;
            tabPermisosFamiliaVista.Location = new Point(tabsGestionFamilias.TabPages.Contains(tabCrearFamilia) ? 180 : 0, 0);
            PosicionarPaginasFamilias();
            pieFamiliasVista.SetBounds(28, ClientSize.Height - 34, ClientSize.Width - 56, 24);
        }

        private void PosicionarPaginasFamilias()
        {
            foreach (Panel pagina in paginasFamilias) pagina.SetBounds(0, 54 + desplazamientoFamilias, contenidoFamilias.Width, contenidoFamilias.Height - 54 - desplazamientoFamilias);
        }

        private void SincronizarVistaFamilias()
        {
            bool crear = SessionManager_54CS.Instancia.TienePermiso("CrearFamilias");
            bool modificar = SessionManager_54CS.Instancia.TienePermiso("ModificarFamilias");
            bool eliminar = SessionManager_54CS.Instancia.TienePermiso("EliminarFamilias");
            nuevaFamiliaVista.Visible = crear;
            tabNuevaFamiliaVista.Visible = tabsGestionFamilias.TabPages.Contains(tabCrearFamilia);
            datosFamilia.Visible = crear;
            listFamilias.Visible = label3.Visible = true;
            tabPermisosFamiliaVista.Visible = modificar;
            ayudaEliminarFamilias.Visible = eliminar;
            lateralFamilias.Visible = crear || modificar || eliminar || SessionManager_54CS.Instancia.TienePermiso("VerFamilias");
            MostrarPaginaFamilias(modificar ? 1 : tabsGestionFamilias.TabPages.Contains(tabCrearFamilia) ? 0 : -1, false);
            DistribuirVistaFamilias();
        }

        private void MostrarPaginaFamilias(int indice, bool animar)
        {
            if (indice == 0 && !tabsGestionFamilias.TabPages.Contains(tabCrearFamilia)) return;
            if (indice == 1 && !SessionManager_54CS.Instancia.TienePermiso("ModificarFamilias")) return;
            paginaFamilias = indice;
            for (int i = 0; i < paginasFamilias.Length; i++) paginasFamilias[i].Visible = i == indice;
            btnAgregarPermisoFamilia.Visible = btnQuitarPermisoFamilia.Visible = indice == 1 && SessionManager_54CS.Instancia.TienePermiso("ModificarFamilias");
            btnCrear.Visible = indice == 0 && SessionManager_54CS.Instancia.TienePermiso("CrearFamilias");
            desplazamientoFamilias = animar ? 15 : 0;
            if (animar) animacionFamilias.Start();
            PosicionarPaginasFamilias();
            AplicarTemaFamilias();
        }

        private string TextoFamilias(string clave) => IdiomaManager_54CS.ObtenerTexto("GestionarFamilias", clave, clave);

        private void ActualizarResumenFamilia()
        {
            if (nombreFamiliaVista == null) return;
            nombreFamiliaVista.Text = _familiaSeleccionada == null ? TextoFamilias("seleccionarFamiliaVista") : _familiaSeleccionada.Nombre;
            ayudaFamiliaVista.Text = TextoFamilias("ayudaFamiliaVista");
        }

        private void ActualizarTextosFamilias()
        {
            if (temaFamilias == null) return;
            foreach (var par in textosFamilias) par.Key.Text = TextoFamilias(par.Value);
            if (SessionManager_54CS.HaySesion && !SessionManager_54CS.Instancia.TienePermiso("CrearFamilias")) tabNuevaFamiliaVista.Text = TextoFamilias("composicionFamiliaVista");
            btnAgregarPermisoFamilia.Text = TextoFamilias("agregarFamiliaVista");
            btnQuitarPermisoFamilia.Text = TextoFamilias("quitarFamiliaVista");
            temaFamilias.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro");
            txtNombre.AccessibleName = label1.Text;
            txtDesc.AccessibleName = label4.Text;
            lbFamiliasGestion.AccessibleName = lblFamiliasGestion.Text;
            chklist.AccessibleName = label2.Text;
            listFamilias.AccessibleName = label3.Text;
            clbPermisosDisponiblesFam.AccessibleName = lblPermisosDisponiblesFam.Text;
            lbPermisosDeFamilia.AccessibleName = lblPermisosDeFamilia.Text;
            ActualizarResumenFamilia();
        }

        public void AplicarTemaFamilias()
        {
            if (temaFamilias == null) return;
            BackColor = Tema_54CS.EsOscuro ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = Tema_54CS.EsOscuro ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            cuerpoFamilias.BackColor = contenidoFamilias.BackColor = BackColor;
            foreach (Panel pagina in paginasFamilias) pagina.BackColor = BackColor;
            foreach (var tarjeta in tarjetasFamilias) { tarjeta.BackColor = SuperficieFamilias; tarjeta.BorderColor = BordeFamilias; }
            foreach (ListBox lista in new ListBox[] { lbFamiliasGestion, chklist, listFamilias, clbPermisosDisponiblesFam, lbPermisosDeFamilia })
            {
                lista.BackColor = SuperficieFamilias;
                lista.ForeColor = ForeColor;
                lista.BorderStyle = BorderStyle.None;
            }
            foreach (TextBox input in new[] { txtNombre, txtDesc }) { input.BorderStyle = BorderStyle.None; input.BackColor = SuperficieFamilias; input.ForeColor = ForeColor; }
            foreach (Label label in new[] { label1, label2, label3, label4, lblFamiliasGestion, lblPermisosDisponiblesFam, lblPermisosDeFamilia, nombreFamiliaVista }) { label.ForeColor = ForeColor; label.BackColor = Color.Transparent; }
            foreach (var par in textosFamilias) if (par.Key is Label) par.Key.ForeColor = ForeColor;
            subtituloFamiliasVista.ForeColor = pieFamiliasVista.ForeColor = ayudaEliminarFamilias.ForeColor = ayudaFamiliaVista.ForeColor = Tema_54CS.EsOscuro ? Color.Silver : Color.DimGray;
            foreach (Control control in Controls) if (control is AltaGlyph) control.ForeColor = AcentoFamilias;
            foreach (Button boton in new Button[] { btnCrear, btnEliminarFamilia, btnAgregarPermisoFamilia, btnQuitarPermisoFamilia, nuevaFamiliaVista, tabNuevaFamiliaVista, tabPermisosFamiliaVista })
            {
                bool primario = boton == btnCrear || boton == nuevaFamiliaVista || (paginaFamilias == 0 && boton == tabNuevaFamiliaVista) || (paginaFamilias == 1 && boton == tabPermisosFamiliaVista);
                boton.BackColor = primario ? AcentoFamilias : SuperficieFamilias;
                boton.ForeColor = primario ? (Tema_54CS.EsOscuro ? Color.FromArgb(35, 30, 27) : Color.White) : AcentoFamilias;
                boton.FlatAppearance.BorderColor = boton == btnEliminarFamilia ? Color.FromArgb(215, 98, 86) : BordeFamilias;
                boton.FlatAppearance.BorderSize = primario ? 0 : 1;
                ((UsuariosRoundedButton)boton).HoverBackColor = primario ? (Tema_54CS.EsOscuro ? Color.FromArgb(246, 183, 152) : Color.FromArgb(35, 78, 170)) : (Tema_54CS.EsOscuro ? Color.FromArgb(94, 72, 58) : Color.FromArgb(216, 230, 254));
            }
            temaFamilias.DarkMode = Tema_54CS.EsOscuro;
            ActualizarTextosFamilias();
            Invalidate(true);
        }
    }
}
