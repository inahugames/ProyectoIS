using System.Drawing.Drawing2D;
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
    public partial class RestaurarBD : Form, IIdiomaObservador_54CS
    {
        public RestaurarBD()
        {
            InitializeComponent();

        }

        private void RestaurarBD_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Tema_54CS.Aplicar(this);
            ActiveControl = btnExaminar;
            IdiomaManager_54CS.Suscribir(this);
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosRestore();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Backup de SQL Server (*.bak)|*.bak|Todos los archivos (*.*)|*.*";
                dialogo.Title = IdiomaManager_54CS.TraducirMensaje("Seleccione el backup a restaurar");
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    txtRuta.Text = dialogo.FileName;
                }
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRuta.Text))
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Seleccione el archivo de backup a restaurar."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmacion = MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Se reemplazará TODA la información actual de la Base de Datos con el contenido del backup seleccionado. ¿Desea continuar?"), IdiomaManager_54CS.TraducirMensaje("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            try
            {
                BLLDigitoVerificador_54CS bllDV = new BLLDigitoVerificador_54CS();
                if (bllDV.RestaurarBackup(txtRuta.Text, out string mensaje))
                {
                    MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("Restore ejecutado correctamente. Vuelva a iniciar sesión."), IdiomaManager_54CS.TraducirMensaje("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(string.Format(IdiomaManager_54CS.TraducirMensaje("No se pudo restaurar el backup: {0}"), mensaje), IdiomaManager_54CS.TraducirMensaje("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private Color AcentoRestore => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);

        private void temaRestore_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void txtRuta_Enter(object sender, EventArgs e)
        {
            campoRestore.BorderColor = AcentoRestore;
            campoRestore.Invalidate();
        }

        private void txtRuta_Leave(object sender, EventArgs e)
        {
            AplicarTemaRestore();
        }

        private void ActualizarTextosRestore()
        {
            if (temaRestore == null) return;
            foreach (Label label in new[] { tituloRestore, subtituloRestore, tituloArchivoRestore, tituloAvisoRestore, detalleAvisoRestore, pieRestore })
                label.Text = IdiomaManager_54CS.ObtenerTexto("RestaurarBD", label.Name, label.Name);
            lblAviso.Text = IdiomaManager_54CS.ObtenerTexto("RestaurarBD", "recomendacionRestore", lblAviso.Text);
            temaRestore.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro");
            txtRuta.AccessibleName = lblArchivo.Text;
        }

        public void AplicarTemaRestore()
        {
            if (temaRestore == null) return;
            bool dark = Tema_54CS.EsOscuro;
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            Color superficie = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            Color campo = dark ? Color.FromArgb(35, 35, 34) : Color.FromArgb(249, 250, 252);
            Color borde = dark ? Color.FromArgb(74, 74, 70) : Color.FromArgb(219, 223, 231);
            archivoRestore.BackColor = superficie;
            archivoRestore.BorderColor = borde;
            archivoRestore.ForeColor = AcentoRestore;
            campoRestore.BackColor = txtRuta.BackColor = campo;
            campoRestore.BorderColor = campoRestore.ContainsFocus ? AcentoRestore : borde;
            txtRuta.ForeColor = ForeColor;
            txtRuta.BorderStyle = BorderStyle.None;
            insigniaRestore.BackColor = insigniaRestore.BorderColor = AcentoRestore;
            avisoRestore.BackColor = dark ? Color.FromArgb(54, 45, 32) : Color.FromArgb(255, 247, 232);
            avisoRestore.BorderColor = dark ? Color.FromArgb(137, 105, 56) : Color.FromArgb(231, 191, 127);
            avisoRestore.ForeColor = tituloAvisoRestore.ForeColor = dark ? Color.FromArgb(241, 186, 100) : Color.FromArgb(152, 91, 16);
            foreach (Label label in new[] { tituloRestore, tituloArchivoRestore, lblArchivo, detalleAvisoRestore, lblAviso }) { label.ForeColor = ForeColor; label.BackColor = Color.Transparent; }
            subtituloRestore.ForeColor = pieRestore.ForeColor = dark ? Color.Silver : Color.DimGray;
            foreach (Button boton in new[] { btnExaminar, btnRestaurar, btnCancelar })
            {
                bool primario = boton == btnRestaurar;
                boton.BackColor = primario ? AcentoRestore : superficie;
                boton.ForeColor = primario ? (dark ? Color.FromArgb(35, 30, 27) : Color.White) : AcentoRestore;
                boton.FlatAppearance.BorderColor = borde;
                boton.FlatAppearance.BorderSize = primario ? 0 : 1;
                ((UsuariosRoundedButton)boton).HoverBackColor = primario ? (dark ? Color.FromArgb(246, 183, 152) : Color.FromArgb(35, 78, 170)) : (dark ? Color.FromArgb(94, 72, 58) : Color.FromArgb(216, 230, 254));
            }
            temaRestore.DarkMode = dark;
            ActualizarTextosRestore();
            Invalidate(true);
        }
    }

}
