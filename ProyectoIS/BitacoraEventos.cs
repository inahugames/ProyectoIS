using MPP;
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
    public partial class BitacoraEventos : Form
    {
        public BitacoraEventos()
        {
            InitializeComponent();
            Actualizar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFiltraCrit_Click(object sender, EventArgs e)
        {
            /*dgvEventos.Rows.Clear();
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS evento in lista )
            {
                if (txtCriticidad.Text == evento.Criticidad_54CS)
                {
                    dgvEventos.Rows.Add(evento.Login_54CS, evento.Fecha_54CS, evento.Modulo_54CS, evento.Evento_54CS, evento.Criticidad_54CS);
                }
            }*/
        }

        private void btnFiltraFecha_Click(object sender, EventArgs e)
        {
            /*dgvEventos.Rows.Clear();
            DateTime fecha = Convert.ToDateTime(fechaPicker.Value.Date);
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS v in lista)
            {
                if (v.Fecha_54CS.Date == fecha)
                {
                    dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS, v.Modulo_54CS, v.Evento_54CS, v.Criticidad_54CS);
                }
            }*/
        }

        private void btnFiltraLogin_Click(object sender, EventArgs e)
        {
            /*dgvEventos.Rows.Clear();
            string login = txtLogin.Text;
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS v in lista)
            {
                if (v.Login_54CS == login)
                {
                    dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS, v.Modulo_54CS, v.Evento_54CS, v.Criticidad_54CS);
                }
            }*/
            if ( comboCriticidad.Text != "" || txtLogin.Text != "" || fechaPicker.Text != "" || comboMódulo.Text != "")
            {
                string filtroCriticidad = comboCriticidad.Text.ToLower();
                string filtroLogin = txtLogin.Text.ToLower();
                DateTime filtroFecha = fechaPicker.Value.Date;
                string filtroModulo = comboMódulo.Text.ToLower();
                MPPEventos_54CS mpp = new MPPEventos_54CS();
                List<Eventos_54CS> lista = mpp.ObtenerEventos();
                IEnumerable<Eventos_54CS> consulta = lista;
                
                if (string.IsNullOrEmpty(filtroCriticidad) == false)
                {
                    consulta = consulta.Where(ev => ev.Criticidad_54CS.ToLower().Contains(filtroCriticidad.ToLower()));
                }
                if (string.IsNullOrEmpty(filtroLogin) == false)
                {
                    consulta = consulta.Where(ev => ev.Login_54CS.ToLower().Contains(filtroLogin.ToLower()));
                }
                if (fechaPicker.Checked)
                {
                    if (string.IsNullOrEmpty(filtroFecha.ToString()) == false)
                    {
                        consulta = consulta.Where(ev => ev.Fecha_54CS.Date == filtroFecha);
                    }
                }
                if (string.IsNullOrEmpty(filtroModulo) == false)
                {
                    consulta = consulta.Where(ev => ev.Modulo_54CS.ToLower().Contains(filtroModulo.ToLower()));
                }
                List<Eventos_54CS> filtrados = consulta.ToList();
                dgvEventos.Rows.Clear();
                foreach (Eventos_54CS evento in filtrados)
                {
                    dgvEventos.Rows.Add(evento.Login_54CS, evento.Fecha_54CS, evento.Modulo_54CS, evento.Evento_54CS, evento.Criticidad_54CS);
                }
            }
        }

        private void btnCancelarFiltros_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            dgvEventos.Rows.Clear();
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS v in lista)
            {
                dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS, v.Modulo_54CS, v.Evento_54CS, v.Criticidad_54CS);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
