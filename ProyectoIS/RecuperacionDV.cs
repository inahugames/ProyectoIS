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
    // REPARACIÓN: pantalla que se le presenta al Administrador del Sistema
    // cuando la REVISIÓN del login detecta una inconsistencia en los datos.
    // Ofrece tres acciones:
    //  - RECALCULAR EL DV: fuerza la GENERACIÓN del DV (acepta la inconsistencia
    //    como nuevo estado válido) y vuelve al Login.
    //  - RESTORE BD: restaura un backup para normalizar la BD y vuelve al Login.
    //  - SALIR: sale del sistema sin resolver la inconsistencia.
    public partial class RecuperacionDV : Form, IIdiomaObservador_54CS
    {
        public RecuperacionDV(string detalle)
        {
            InitializeComponent();
            Tema_54CS.Aplicar(this);
            lblTitulo.ForeColor = Tema_54CS.ColorAlerta; // rojo legible en el tema activo
            txtDetalle.Text = detalle ?? string.Empty;
        }

        private void RecuperacionDV_Load(object sender, EventArgs e)
        {
            IdiomaManager_54CS.Suscribir(this);
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
        }

        private void btnRecalcular_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("¿Está seguro que desea recalcular el Dígito Verificador? La inconsistencia no se resuelve: se acepta como nuevo estado válido de los datos."), IdiomaManager_54CS.TraducirMensaje("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes)
            {
                return;
            }
            try
            {
                BLLDigitoVerificador_54CS bllDV = new BLLDigitoVerificador_54CS();
                bllDV.RecalcularDV();
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Dígito Verificador recalculado correctamente. Vuelva a iniciar sesión."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("Error: {0}"), ex.Message), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            using (RestaurarBD formulario = new RestaurarBD())
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Se limpia la pantalla y se sale del sistema. La inconsistencia
            // no se resuelve: se marca la integridad como comprometida para
            // que ninguna escritura durante el cierre recalcule (y oculte) el DV.
            SessionManager_54CS.IntegridadComprometida = true;
            Application.Exit();
        }
    }
}
