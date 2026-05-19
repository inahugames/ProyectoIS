using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Actualizar();
            TxtModoConsulta();
        }


        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] datos = dgvUsuarios.CurrentRow.AccessibilityObject.Value.Split(';');
            foreach (Usuario_54CS user in lista)
            {
                try
                {
                    if (Convert.ToString(user.DNI_54cs) == datos[1])
                    {
                        seleccionado = user; break;
                    }
                }
                catch
                {
                    MessageBox.Show("El usuario seleccionado es inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            foreach (Usuario_54CS user in lista)
            {
                if (user.Login_54CS == seleccionado.Login_54CS && user.Block_54CS == true)
                {
                    user.Block_54CS = false;
                    MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                    mpp.ActualizarUsuarios(lista);
                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                    {
                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                        Fecha_54CS = System.DateTime.Now,
                        Modulo_54CS = "Gestión de Usuario",
                        Evento_54CS = "Usuario Desbloqueado",
                        Criticidad_54CS = "2"
                    };
                    MPPEventos_54CS mppe = new MPPEventos_54CS();
                    mppe.GuardarEvento(Evento);
                    MessageBox.Show($"Usuario con DNI {user.DNI_54cs} desbloqueado exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Eventos_54CS> listaev = mppe.ObtenerEventos();
                    foreach (Eventos_54CS ev in listaev)
                    {
                        if (DateTime.Now < ev.Fecha_54CS.AddHours(3))
                        {
                            mppe.EliminarEvento(ev);
                        }
                    }        
                    break;
                }
                else if (user.Login_54CS == seleccionado.Login_54CS && user.Block_54CS == false)
                {
                    MessageBox.Show("El usuario seleccionado no se encuentra bloqueado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Actualizar();
        }

        private void cbUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if(cbUsuario.Checked)
            {
                dgvUsuarios.Columns[0].Visible = true;
                btnAct.Visible = true;
                btnDesbloquear.Visible = true;
                btnModificar.Visible = true;
                btnEliminar.Visible = true;
                btnCrear.Visible = true;
                TxtModoModificar();
            }
            else
            {
                dgvUsuarios.Columns[0].Visible = false;
                btnAct.Visible = false;
                btnModificar.Visible = false;
                btnDesbloquear.Visible = false;
                btnEliminar.Visible = false;
                btnCrear.Visible = false;
                TxtModoConsulta();
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
            nuevo.ShowDialog();
            Actualizar();
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
                                if (user.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                                {
                                    MessageBox.Show("No puede modificar el usuario que se encuentra logeado actualmente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                                if (user.Activo_54CS == true)
                                {
                                    user.Activo_54CS = false;
                                    mpp.ActualizarUsuarios(lista);
                                    MessageBox.Show($"Se desactivó el usuario con DNI: {user.DNI_54cs}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                                    {
                                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Gestión de Usuario",
                                        Evento_54CS = "Desactivación de usuario",
                                        Criticidad_54CS = "2"
                                    };
                                    MPPEventos_54CS mppe = new MPPEventos_54CS();
                                    mppe.GuardarEvento(Evento);
                                    break;
                                }
                                else
                                {
                                    user.Activo_54CS = true;
                                    mpp.ActualizarUsuarios(lista);
                                    MessageBox.Show($"Se activó el usuario con DNI: {user.DNI_54cs}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                                    {
                                        Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                                        Fecha_54CS = System.DateTime.Now,
                                        Modulo_54CS = "Gestión de Usuario",
                                        Evento_54CS = "Activación de usuario",
                                        Criticidad_54CS = "2"
                                    };
                                    MPPEventos_54CS mppe = new MPPEventos_54CS();
                                    mppe.GuardarEvento(Evento);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            Actualizar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (seleccionado.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
            {
                MessageBox.Show("No puede modificar el usuario que se encuentra logeado actualmente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ModificarUsuario nuevo = new ModificarUsuario(seleccionado);
                nuevo.ShowDialog();
                Actualizar();
            }
        }

        public void Actualizar()
        {
            dgvUsuarios.Rows.Clear();
            MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
            lista = mpp.ObtenerUsuarios();
            foreach (Usuario_54CS v in lista)
            {
                dgvUsuarios.Rows.Add(false, v.DNI_54cs,v.Login_54CS, v.Nombre_54CS, v.Apellido_54CS, v.Email_54CS, v.Rol_54CS, v.Block_54CS, v.Activo_54CS);
            }
            dgvUsuarios.Columns[0].Visible = false;
            cbUsuario.Checked = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
            foreach ( Usuario_54CS user in lista )
            {
                if (user.DNI_54cs == seleccionado.DNI_54cs)
                {
                    if (seleccionado.Login_54CS == SessionManager_54CS.Instancia.Login_54CS)
                    {
                        MessageBox.Show("No puede modificar el usuario que se encuentra logeado actualmente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    DialogResult opcion;
                    opcion = MessageBox.Show($"Realmente quiere eliminar al usuario con DNI: {user.DNI_54cs}?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if ( opcion == DialogResult.OK)
                    {
                        mpp.EliminarUsuario(user.DNI_54cs);
                        Actualizar();
                        MessageBox.Show("Usuario eliminado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                        {
                            Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                            Fecha_54CS = System.DateTime.Now,
                            Modulo_54CS = "Gestión de Usuario",
                            Evento_54CS = "Eliminación de Usuario",
                            Criticidad_54CS = "3"
                        };
                        MPPEventos_54CS mppe = new MPPEventos_54CS();
                        mppe.GuardarEvento(Evento);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Operación cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtModoConsulta()
        {
            txtMsj.Text = "\t\tMODO CONSULTA. \r\n \r\nEn modo consulta, usted puede definir los parámetros que desea utilizar para realizar una búsqueda de los usuarios de la base de datos. \r\n \r\nCuando llene todos los parámetros, aprete el botón Aplicar para realizar el filtrado.";
        }

        private void TxtModoModificar()
        {
            txtMsj.Text = "\t\tMODO MODIFICAR. \r\n \r\n En modo modificar, usted puede seleccionar a un usuario en la grilla de arriba, sobre el cual puede realizar las acciones de modificación, eliminación, desbloqueo, y desactivación. \r\n \r\n También puede crear un usuario nuevo si así lo desea."; 
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (txtDNI.Text != ""|| txtApellido.Text != "" || txtNombre.Text != "" || txtEmail.Text != "" || txtLogin.Text != "" || txtRol.Text != "")
            {
                // limpio todos los txt para evitar espacios q rompan el código
                string filtroDNI = txtDNI.Text.Trim();
                string filtroNombre = txtNombre.Text.Trim();
                string filtroApellido = txtApellido.Text.Trim();
                string filtroEmail = txtEmail.Text.Trim();
                string filtroLogin = txtLogin.Text.Trim();
                string filtroRol = txtRol.Text.Trim();

                MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
                List<Usuario_54CS> lista = mpp.ObtenerUsuarios();
                // aplico linq
                IEnumerable<Usuario_54CS> consulta = lista; 

                if (string.IsNullOrEmpty(filtroDNI) == false)
                {
                    consulta = consulta.Where(u => u.DNI_54cs.ToString().Contains(filtroDNI));
                }

                if (string.IsNullOrEmpty(filtroNombre) == false)
                {
                    consulta = consulta.Where(u => u.Nombre_54CS.ToLower().Contains(filtroNombre.ToLower()));
                }

                if (string.IsNullOrEmpty(filtroApellido) == false)
                {
                    consulta = consulta.Where(u => u.Apellido_54CS.ToLower().Contains(filtroApellido.ToLower()));
                }

                if (string.IsNullOrEmpty(filtroEmail) == false)
                {
                    consulta = consulta.Where(u => u.Email_54CS.ToLower().Contains(filtroEmail.ToLower()));
                }

                if (string.IsNullOrEmpty(filtroLogin) == false)
                {
                    consulta = consulta.Where(u => u.Login_54CS.ToLower().Contains(filtroLogin.ToLower()));
                }

                if (string.IsNullOrEmpty(filtroRol) == false)
                {
                    consulta = consulta.Where(u => u.Rol_54CS.ToLower().Contains(filtroRol.ToLower()));
                }
                List<Usuario_54CS> usuariosFiltrados = consulta.ToList();
                dgvUsuarios.Rows.Clear();
                foreach (Usuario_54CS v in usuariosFiltrados)
                {
                    dgvUsuarios.Rows.Add(false, v.DNI_54cs, v.Login_54CS, v.Nombre_54CS, v.Apellido_54CS, v.Email_54CS, v.Rol_54CS, v.Block_54CS, v.Activo_54CS);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }
    }
}
