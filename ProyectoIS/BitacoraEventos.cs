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
                dgvEventos.Rows.Add(v.Login_54CS, v.Fecha_54CS,v.Hora_54CS,v.Modulo_54CS,v.Criticidad_54CS);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
