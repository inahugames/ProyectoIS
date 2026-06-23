using BLL_54CS;
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
    public partial class BitacoraEventos : Form, IIdiomaObservador_54CS
    {
        public BitacoraEventos()
        {
            InitializeComponent();
            IdiomaManager_54CS.Suscribir(this); // 2.1 - Observer: nos traducimos solos en caliente
            Actualizar();
        }

        // 2.1 - Observer
        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFiltraCrit_Click(object sender, EventArgs e)
        {
        }

        private void btnFiltraFecha_Click(object sender, EventArgs e)
        {
        }

        private void btnFiltraLogin_Click(object sender, EventArgs e)
        {
            if ( comboCriticidad.Text != "" || txtLogin.Text != "" || fechaPickerInicio.Text != "" || comboMódulo.Text != "")
            {
                string filtroCriticidad = comboCriticidad.Text.ToLower();
                string filtroLogin = txtLogin.Text.ToLower();
                DateTime filtroFechaInicio = fechaPickerInicio.Value.Date;
                DateTime filtroFechaFin = fechaPickerFin.Value.Date.AddDays(1).AddTicks(-1); // añado un dia y le resto un segundo para cubrir hasta las 23:59:59 del dia seleccionado
                string filtroModulo = comboMódulo.Text.ToLower();
                BLLEventos_54CS blle = new BLLEventos_54CS();
                List<Eventos_54CS> lista = blle.ObtenerTodos();
                IEnumerable<Eventos_54CS> consulta = lista;
                
                if (string.IsNullOrEmpty(filtroCriticidad) == false)
                {
                    consulta = consulta.Where(ev => ev.Criticidad_54CS.ToLower().Contains(filtroCriticidad.ToLower()));
                }
                if (string.IsNullOrEmpty(filtroLogin) == false)
                {
                    consulta = consulta.Where(ev => ev.Login_54CS.ToLower().Contains(filtroLogin.ToLower()));
                }
                if (fechaPickerInicio.Checked && fechaPickerFin.Checked)
                {
                    if (string.IsNullOrEmpty(filtroFechaInicio.ToString()) == false && string.IsNullOrEmpty(filtroFechaFin.ToString()) == false)
                    {
                        consulta = consulta.Where(evento => evento.Fecha_54CS >= filtroFechaInicio && evento.Fecha_54CS <= filtroFechaFin);
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
            BLLEventos_54CS blle = new BLLEventos_54CS();
            List<Eventos_54CS> lista = blle.ObtenerTodos();
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
