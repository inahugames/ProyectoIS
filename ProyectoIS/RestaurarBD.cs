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
    // REPARACIÓN - RESTORE BD: permite elegir un backup (.bak) y restaurarlo
    // para normalizar la situación de la Base de Datos. Conviene elegir el
    // backup más reciente para perder la mínima cantidad de datos.
    public partial class RestaurarBD : Form, IIdiomaObservador_54CS
    {
        public RestaurarBD()
        {
            InitializeComponent();
            Tema_54CS.Aplicar(this);
        }

        private void RestaurarBD_Load(object sender, EventArgs e)
        {
            IdiomaManager_54CS.Suscribir(this);
        }

        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
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
    }
}
