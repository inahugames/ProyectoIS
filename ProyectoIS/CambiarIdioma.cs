using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using BLL_54CS;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProyectoIS
{
    public partial class CambiarIdioma : Form, IIdiomaObservador_54CS
    {
        private class ItemIdioma
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
        }

        public CambiarIdioma()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            Tema_54CS.Aplicar(this);
            IdiomaManager_54CS.Suscribir(this);
            CargarIdiomasDisponibles();
            CargarOpcionesIdioma();
        }
        public void ActualizarIdioma()
        {
            IdiomaManager_54CS.Traducir(this);
            ActualizarTextosIdioma();
        }

        private void CargarIdiomasDisponibles()
        {
            var codigos = IdiomaManager_54CS.ObtenerIdiomasDisponibles();
            var items = codigos
                .Select(c => new ItemIdioma { Codigo = c, Nombre = IdiomaManager_54CS.ObtenerNombreAmigable(c) })
                .ToList();

            cmbIdiomas.DisplayMember = "Nombre";
            cmbIdiomas.ValueMember = "Codigo";
            cmbIdiomas.DataSource = items;

            string actual = IdiomaManager_54CS.IdiomaActual;
            var seleccionado = items.FirstOrDefault(i => i.Codigo == actual);
            if (seleccionado != null)
            {
                cmbIdiomas.SelectedItem = seleccionado;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!SessionManager_54CS.Instancia.TienePermiso("CambiarIdioma"))
            {
                MessageBox.Show(IdiomaManager_54CS.TraducirMensaje("No tiene permisos suficientes."), IdiomaManager_54CS.TraducirMensaje("Error"));
                return;
            }
            if (cmbIdiomas.SelectedItem == null)
            {
                MessageBox.Show(
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "MsjSeleccione", "Debe seleccionar un idioma."),
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "TituloAviso", "Aviso"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idiomaAnterior = IdiomaManager_54CS.IdiomaActual;
            string idiomaSeleccionado = ((ItemIdioma)cmbIdiomas.SelectedItem).Codigo;

            if (idiomaSeleccionado == idiomaAnterior)
            {
                this.Close();
                return;
            }

            IdiomaManager_54CS.CambiarIdioma(idiomaSeleccionado);

            BLLUsuarios_54CS bllUsuarios = new BLLUsuarios_54CS();
            bool guardadoOk = bllUsuarios.GuardarIdiomaUsuario(SessionManager_54CS.Instancia.Login_54CS, idiomaSeleccionado, out string mensajeError);

            Eventos_54CS evento = new Eventos_54CS()
            {
                Login_54CS = SessionManager_54CS.Instancia.Login_54CS, // mismo login que el usuario que se logeo
                Fecha_54CS = DateTime.Now,
                Modulo_54CS = "Idioma",
                Evento_54CS = $"Cambio de idioma de '{idiomaAnterior}' a '{idiomaSeleccionado}'",
                Criticidad_54CS = "1"
            };
            BLLEventos_54CS bllEventos = new BLLEventos_54CS();
            bllEventos.GuardarEvento(evento, out string mensajeBitacora);

            if (guardadoOk)
            {
                MessageBox.Show(
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "MsjExito", "Idioma cambiado correctamente."),
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "TituloAviso", "Aviso"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "MsjErrorGuardado", "El idioma se aplicó, pero no se pudo guardar la preferencia para la próxima sesión.") + " " + mensajeError,
                    IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "TituloError", "Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private readonly List<OpcionIdiomaButton> botonesIdioma = new List<OpcionIdiomaButton>();
        private Color AcentoIdioma => Tema_54CS.EsOscuro ? Color.FromArgb(238, 162, 126) : Color.FromArgb(45, 96, 196);

        private void temaIdioma_Click(object sender, EventArgs e)
        {
            Tema_54CS.AlternarModo();
        }

        private void cmbIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarSeleccionIdioma();
        }

        private void CargarOpcionesIdioma()
        {
            opcionesIdioma.SuspendLayout();
            var existentes = opcionesIdioma.Controls.OfType<OpcionIdiomaButton>().ToList();
            botonesIdioma.Clear();
            opcionesIdioma.Controls.Clear();
            foreach (ItemIdioma item in cmbIdiomas.Items.Cast<ItemIdioma>().OrderBy(i => i.Codigo == "es" ? 0 : i.Codigo == "en" ? 1 : i.Codigo == "pt" ? 2 : 3))
            {
                var boton = existentes.FirstOrDefault(b => b.Codigo.Equals(item.Codigo, StringComparison.OrdinalIgnoreCase));
                if (boton == null)
                {
                    boton = new OpcionIdiomaButton
                    {
                    Name = "idioma_" + item.Codigo,
                    Codigo = item.Codigo.ToUpperInvariant(),
                    NombreIdioma = item.Nombre,
                    AccessibleName = item.Nombre,
                    Size = new Size(508, 68),
                    Margin = new Padding(0, 0, 0, 12),
                    Tag = item,
                    TabIndex = botonesIdioma.Count,
                    Font = new Font("Segoe UI", 12F)
                    };
                }
                boton.Tag = item;
                boton.NombreIdioma = item.Nombre;
                boton.AccessibleName = item.Nombre;
                boton.TabIndex = botonesIdioma.Count;
                boton.Click += (s, e) => cmbIdiomas.SelectedItem = item;
                botonesIdioma.Add(boton);
                opcionesIdioma.Controls.Add(boton);
            }
            foreach (var boton in existentes.Where(b => !botonesIdioma.Contains(b))) boton.Dispose();
            opcionesIdioma.ResumeLayout(true);
            AplicarTemaIdioma();
        }

        private void ActualizarSeleccionIdioma()
        {
            foreach (OpcionIdiomaButton boton in botonesIdioma)
            {
                boton.Seleccionado = ReferenceEquals(boton.Tag, cmbIdiomas.SelectedItem);
                boton.Accent = AcentoIdioma;
                boton.BackColor = boton.Seleccionado
                    ? (Tema_54CS.EsOscuro ? Color.FromArgb(78, 57, 47) : Color.FromArgb(229, 237, 253))
                    : (Tema_54CS.EsOscuro ? Color.FromArgb(43, 43, 41) : Color.White);
                boton.ForeColor = boton.Seleccionado ? AcentoIdioma : ForeColor;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = boton.Seleccionado ? AcentoIdioma : (Tema_54CS.EsOscuro ? Color.FromArgb(74, 74, 70) : Color.FromArgb(219, 223, 231));
                boton.HoverBackColor = Tema_54CS.EsOscuro ? Color.FromArgb(94, 72, 58) : Color.FromArgb(216, 230, 254);
                boton.Invalidate();
            }
        }

        private void ActualizarTextosIdioma()
        {
            if (temaIdioma == null) return;
            foreach (Label label in new[] { tituloIdioma, subtituloIdioma, ayudaIdioma }) label.Text = IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", label.Name, label.Name);
            btnAceptar.Text = IdiomaManager_54CS.ObtenerTexto("CambiarIdioma", "aplicarIdioma", btnAceptar.Text);
            temaIdioma.Text = IdiomaManager_54CS.ObtenerTexto("GestionUsuario", Tema_54CS.EsOscuro ? "TemaClaro" : "TemaOscuro");
        }

        public void AplicarTemaIdioma()
        {
            if (temaIdioma == null) return;
            bool dark = Tema_54CS.EsOscuro;
            BackColor = dark ? Color.FromArgb(31, 31, 30) : Color.FromArgb(245, 246, 249);
            ForeColor = dark ? Color.FromArgb(242, 240, 237) : Color.FromArgb(35, 39, 45);
            Color superficie = dark ? Color.FromArgb(43, 43, 41) : Color.White;
            tarjetaIdioma.BackColor = opcionesIdioma.BackColor = superficie;
            tarjetaIdioma.BorderColor = dark ? Color.FromArgb(74, 74, 70) : Color.FromArgb(219, 223, 231);
            insigniaIdioma.BackColor = insigniaIdioma.BorderColor = AcentoIdioma;
            tituloIdioma.ForeColor = ForeColor;
            subtituloIdioma.ForeColor = ayudaIdioma.ForeColor = dark ? Color.Silver : Color.DimGray;
            btnAceptar.BackColor = AcentoIdioma;
            btnAceptar.ForeColor = dark ? Color.FromArgb(35, 30, 27) : Color.White;
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnCancelar.BackColor = superficie;
            btnCancelar.ForeColor = AcentoIdioma;
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.BorderColor = tarjetaIdioma.BorderColor;
            ((UsuariosRoundedButton)btnAceptar).HoverBackColor = dark ? Color.FromArgb(246, 183, 152) : Color.FromArgb(35, 78, 170);
            ((UsuariosRoundedButton)btnCancelar).HoverBackColor = dark ? Color.FromArgb(94, 72, 58) : Color.FromArgb(216, 230, 254);
            temaIdioma.DarkMode = dark;
            ActualizarSeleccionIdioma();
            ActualizarTextosIdioma();
            Invalidate(true);
        }
    }

}
