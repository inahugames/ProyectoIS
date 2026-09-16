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
        public RecuperacionDV()
        {
            InitializeComponent();
        }

        public RecuperacionDV(string detalle) : this()
        {
            SessionManager_54CS.IntegridadComprometida = true;

            txtDetalle.Text = detalle ?? string.Empty;
        }

        private void RecuperacionDV_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            SessionManager_54CS.IntegridadComprometida = true;
            Tema_54CS.Aplicar(this);
            ActiveControl = txtDetalle;
            IdiomaManager_54CS.Suscribir(this);
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosIntegridad();
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
                SessionManager_54CS.IntegridadComprometida = false;
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
                    SessionManager_54CS.IntegridadComprometida = false;
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

        private Color AcentoIntegridad => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);

        private void temaIntegridad_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void ActualizarTextosIntegridad()
        {
            if (temaIntegridad == null) return;
            foreach (Label label in new[] { tituloIntegridad, subtituloIntegridad, tituloDetalleIntegridad, pieIntegridad })
                label.Text = IdiomaManager_54CS.ObtenerTexto("RecuperacionDV", label.Name, label.Name);
            lblTitulo.Text = IdiomaManager_54CS.ObtenerTexto("RecuperacionDV", "avisoIntegridad", lblTitulo.Text);
            lblInfo.Text = IdiomaManager_54CS.ObtenerTexto("RecuperacionDV", "explicacionIntegridad", lblInfo.Text);
            btnRestore.Text = IdiomaManager_54CS.ObtenerTexto("RecuperacionDV", "restaurarIntegridad", btnRestore.Text);
            btnRecalcular.Text = IdiomaManager_54CS.ObtenerTexto("RecuperacionDV", "recalcularIntegridad", btnRecalcular.Text);
            temaIntegridad.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro");
            txtDetalle.AccessibleName = tituloDetalleIntegridad.Text;
        }

        public void AplicarTemaIntegridad()
        {
            if (temaIntegridad == null) return;
            bool dark = Tema_54CS.EsOscuro;
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            Color superficie = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color borde = dark ? Color.FromArgb(74, 74, 70) : Color.FromArgb(219, 223, 231);
            detalleIntegridad.BackColor = superficie;
            detalleIntegridad.BorderColor = borde;
            campoIntegridad.BackColor = txtDetalle.BackColor = dark ? Color.FromArgb(35, 35, 34) : Color.FromArgb(249, 250, 252);
            campoIntegridad.BorderColor = borde;
            txtDetalle.ForeColor = ForeColor;
            txtDetalle.BorderStyle = BorderStyle.None;
            insigniaIntegridad.BackColor = insigniaIntegridad.BorderColor = AcentoIntegridad;
            alertaIntegridad.BackColor = dark ? Color.FromArgb(54, 45, 32) : Color.FromArgb(255, 247, 232);
            alertaIntegridad.BorderColor = dark ? Color.FromArgb(137, 105, 56) : Color.FromArgb(231, 191, 127);
            alertaIntegridad.ForeColor = lblTitulo.ForeColor = dark ? Color.FromArgb(241, 186, 100) : Color.FromArgb(152, 91, 16);
            foreach (Label label in new[] { tituloIntegridad, tituloDetalleIntegridad, lblInfo }) { label.ForeColor = ForeColor; label.BackColor = Color.Transparent; }
            lblTitulo.BackColor = Color.Transparent;
            subtituloIntegridad.ForeColor = pieIntegridad.ForeColor = dark ? Color.Silver : Color.DimGray;
            foreach (Button boton in new[] { btnRestore, btnRecalcular, btnSalir })
            {
                bool primario = boton == btnRecalcular;
                boton.BackColor = primario ? AcentoIntegridad : superficie;
                boton.ForeColor = primario ? (dark ? Color.FromArgb(35, 30, 27) : Color.White) : AcentoIntegridad;
                boton.FlatAppearance.BorderColor = borde;
                boton.FlatAppearance.BorderSize = primario ? 0 : 1;
                ((UsuariosRoundedButton)boton).HoverBackColor = primario ? (dark ? Color.FromArgb(246, 183, 152) : Color.FromArgb(35, 78, 170)) : (dark ? Color.FromArgb(94, 72, 58) : Color.FromArgb(216, 230, 254));
            }
            temaIntegridad.DarkMode = dark;
            ActualizarTextosIntegridad();
            Invalidate(true);
        }
    }
}
