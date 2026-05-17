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
        }


        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] datos = dgvUsuarios.CurrentRow.AccessibilityObject.Value.Split(';');
            foreach (Usuario_54CS user in lista)
            {
                if ( Convert.ToString(user.DNI_54cs) == datos[1])
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
                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                    {
                        Login_54CS = SessionManager_54CS.Login_54CS, // mismo login que el usuario que se logeo
                        Fecha_54CS = System.DateTime.Now,
                        Modulo_54CS = "Gestión de Usuario",
                        Evento_54CS = "Usuario Desbloqueado",
                        Criticidad_54CS = "2"
                    };
                    MPPEventos_54CS mppe = new MPPEventos_54CS();
                    mppe.GuardarEvento(Evento);
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
                btnDesbloquear.Visible = true;
                btnModificar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                dgvUsuarios.Columns[0].Visible = false;
                btnAct.Visible = false;
                btnModificar.Visible = false;
                btnDesbloquear.Visible = false;
                btnEliminar.Visible = false;
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
                                if (user.Activo_54CS == true)
                                {
                                    user.Activo_54CS = false;
                                    mpp.ActualizarUsuarios(lista);
                                    MessageBox.Show($"Se desactivó el usuario con DNI: {user.DNI_54cs}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                                    {
                                        Login_54CS = SessionManager_54CS.Login_54CS, // mismo login que el usuario que se logeo
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
                                        Login_54CS = SessionManager_54CS.Login_54CS, // mismo login que el usuario que se logeo
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
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarUsuario nuevo = new ModificarUsuario(seleccionado);
            nuevo.ShowDialog();
            Actualizar();
        }

        public void Actualizar()
        {
            MPPUsuarios_54CS mpp = new MPPUsuarios_54CS();
            lista = mpp.ObtenerUsuarios();
            foreach (Usuario_54CS v in lista)
            {
                dgvUsuarios.Rows.Add(v.Activo_54CS, v.DNI_54cs,v.Login_54CS, v.Nombre_54CS, v.Apellido_54CS, v.Email_54CS, v.Rol_54CS);
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
                    DialogResult opcion;
                    opcion = MessageBox.Show($"Realmente quiere eliminar al usuario con DNI: {user.DNI_54cs}?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if ( opcion == DialogResult.OK)
                    {
                        mpp.EliminarUsuario(user.DNI_54cs);
                        Actualizar();
                        MessageBox.Show("Usuario eliminado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Eventos_54CS Evento = new Eventos_54CS() //Crear un evento
                        {
                            Login_54CS = SessionManager_54CS.Login_54CS, // mismo login que el usuario que se logeo
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
    }
}
