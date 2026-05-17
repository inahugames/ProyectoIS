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
            dgvEventos.Rows.Clear();
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS v in lista )
            {
                dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS,v.Modulo_54CS,v.Evento_54CS,v.Criticidad_54CS);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFiltraCrit_Click(object sender, EventArgs e)
        {
            dgvEventos.Rows.Clear();
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS evento in lista )
            {
                if (txtCriticidad.Text == evento.Criticidad_54CS)
                {
                    dgvEventos.Rows.Add(evento.Login_54CS, evento.Fecha_54CS, evento.Modulo_54CS, evento.Criticidad_54CS);
                }
            }
        }

        private void btnFiltraFecha_Click(object sender, EventArgs e)
        {
            dgvEventos.Rows.Clear();
            DateTime fecha = Convert.ToDateTime(fechaPicker.Value.Date);
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS v in lista)
            {
                if (v.Fecha_54CS.Date == fecha)
                {
                    dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS, v.Modulo_54CS, v.Evento_54CS, v.Criticidad_54CS);
                }
            }
        }

        private void btnFiltraLogin_Click(object sender, EventArgs e)
        {
            dgvEventos.Rows.Clear();
            string login = txtLogin.Text;
            MPPEventos_54CS mpp = new MPPEventos_54CS();
            List<Eventos_54CS> lista = mpp.ObtenerEventos();
            foreach (Eventos_54CS v in lista)
            {
                if (v.Login_54CS == login)
                {
                    dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS, v.Modulo_54CS, v.Evento_54CS, v.Criticidad_54CS);
                }
            }
        }
    }
}
