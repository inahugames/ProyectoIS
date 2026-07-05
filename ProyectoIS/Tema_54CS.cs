using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProyectoIS
{
    public static class Tema_54CS
    {
        public enum Modo
        {
            Claro,
            Oscuro
        }

        private class Paleta
        {
            public bool EsOscura;
            public Color FondoFormulario;
            public Color FondoControl;
            public Color FondoMdi;
            public Color TextoPrincipal;
            public Color Primario;
            public Color PrimarioHover;
            public Color PrimarioPresionado;
            public Color Peligro;
            public Color PeligroHover;
            public Color PeligroPresionado;
            public Color SecundarioFondo;
            public Color SecundarioBorde;
            public Color SecundarioHover;
            public Color SecundarioPresionado;
            public Color Barra;
            public Color BarraHover;
            public Color GrillaLinea;
            public Color GrillaFondoCelda;
            public Color GrillaFilaAlterna;
            public Color GrillaSeleccion;
            public Color GrillaSeleccionTexto;
            public Color GrillaEncabezadoFondo;
            public Color GrillaEncabezadoTexto;
            public Color GrillaFilaEncabezadoFondo;
            public Color MenuItemBorde;
            public Color MenuBorde;
            public Color Separador;
            public Color Alerta;
        }

        // ------------------------- Tema claro -------------------------
        private static readonly Paleta PaletaClara = new Paleta
        {
            EsOscura = false,
            FondoFormulario = Color.FromArgb(245, 247, 250),
            FondoControl = Color.FromArgb(255, 255, 255),
            FondoMdi = Color.FromArgb(228, 233, 240),
            TextoPrincipal = Color.FromArgb(33, 43, 54),
            Primario = Color.FromArgb(31, 78, 121),          // azul corporativo
            PrimarioHover = Color.FromArgb(43, 102, 156),
            PrimarioPresionado = Color.FromArgb(23, 58, 90),
            Peligro = Color.FromArgb(176, 58, 58),
            PeligroHover = Color.FromArgb(200, 76, 76),
            PeligroPresionado = Color.FromArgb(140, 45, 45),
            SecundarioFondo = Color.FromArgb(255, 255, 255),
            SecundarioBorde = Color.FromArgb(190, 200, 212),
            SecundarioHover = Color.FromArgb(233, 238, 244),
            SecundarioPresionado = Color.FromArgb(214, 222, 231),
            Barra = Color.FromArgb(33, 47, 61),
            BarraHover = Color.FromArgb(55, 76, 98),
            GrillaLinea = Color.FromArgb(222, 228, 235),
            GrillaFondoCelda = Color.FromArgb(255, 255, 255),
            GrillaFilaAlterna = Color.FromArgb(238, 243, 248),
            GrillaSeleccion = Color.FromArgb(198, 219, 240),
            GrillaSeleccionTexto = Color.FromArgb(33, 43, 54),
            GrillaEncabezadoFondo = Color.FromArgb(31, 78, 121),
            GrillaEncabezadoTexto = Color.FromArgb(255, 255, 255),
            GrillaFilaEncabezadoFondo = Color.FromArgb(245, 247, 250),
            MenuItemBorde = Color.FromArgb(55, 76, 98),
            MenuBorde = Color.FromArgb(33, 47, 61),
            Separador = Color.FromArgb(55, 76, 98),
            Alerta = Color.FromArgb(139, 0, 0)               // rojo oscuro sobre fondo claro
        };

        // ------------------------- Tema oscuro -------------------------
        private static readonly Paleta PaletaOscura = new Paleta
        {
            EsOscura = true,
            FondoFormulario = Color.FromArgb(38, 38, 36),
            FondoControl = Color.FromArgb(48, 48, 45),
            FondoMdi = Color.FromArgb(30, 30, 28),
            TextoPrincipal = Color.FromArgb(232, 230, 227),
            Primario = Color.FromArgb(217, 119, 87),         // naranja de acento
            PrimarioHover = Color.FromArgb(230, 141, 111),
            PrimarioPresionado = Color.FromArgb(191, 98, 68),
            Peligro = Color.FromArgb(191, 74, 74),
            PeligroHover = Color.FromArgb(212, 95, 95),
            PeligroPresionado = Color.FromArgb(160, 58, 58),
            SecundarioFondo = Color.FromArgb(58, 58, 55),
            SecundarioBorde = Color.FromArgb(90, 89, 84),
            SecundarioHover = Color.FromArgb(72, 72, 68),
            SecundarioPresionado = Color.FromArgb(50, 50, 47),
            Barra = Color.FromArgb(28, 28, 26),
            BarraHover = Color.FromArgb(64, 52, 45),         // hover cálido
            GrillaLinea = Color.FromArgb(62, 62, 58),
            GrillaFondoCelda = Color.FromArgb(41, 41, 39),
            GrillaFilaAlterna = Color.FromArgb(46, 46, 43),
            GrillaSeleccion = Color.FromArgb(122, 74, 53),   // naranja apagado
            GrillaSeleccionTexto = Color.FromArgb(255, 255, 255),
            GrillaEncabezadoFondo = Color.FromArgb(28, 28, 26),
            GrillaEncabezadoTexto = Color.FromArgb(217, 119, 87),
            GrillaFilaEncabezadoFondo = Color.FromArgb(28, 28, 26),
            MenuItemBorde = Color.FromArgb(217, 119, 87),
            MenuBorde = Color.FromArgb(62, 62, 58),
            Separador = Color.FromArgb(62, 62, 58),
            Alerta = Color.FromArgb(235, 120, 106)           // rojo claro sobre fondo oscuro
        };

        private static Modo _modo = CargarPreferencia();
        private static Paleta _paleta = _modo == Modo.Claro ? PaletaClara : PaletaOscura;
        private const string FamiliaFuente = "Segoe UI";

        public static Modo ModoActual => _modo;
        public static bool EsOscuro => _paleta.EsOscura;

        public static Color ColorAlerta => _paleta.Alerta;

        public static void AlternarModo()
        {
            _modo = _modo == Modo.Claro ? Modo.Oscuro : Modo.Claro;
            _paleta = _modo == Modo.Claro ? PaletaClara : PaletaOscura;
            GuardarPreferencia(_modo);
            foreach (Form formulario in Application.OpenForms)
            {
                Aplicar(formulario);
                formulario.Invalidate(true);
            }
        }

        public static void Aplicar(Form formulario)
        {
            formulario.BackColor = _paleta.FondoFormulario;
            formulario.ForeColor = _paleta.TextoPrincipal;
            AplicarBarraTitulo(formulario);
            AplicarAControles(formulario.Controls);
        }

        private static void AplicarAControles(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                AplicarFuente(control);

                if (control is MdiClient areaMdi)
                {
                    // fondo del área de trabajo del formulario principal (MDI)
                    areaMdi.BackColor = _paleta.FondoMdi;
                    continue;
                }

                if (control is Button boton)
                {
                    EstilarBoton(boton);
                }
                else if (control is TextBox caja)
                {
                    caja.BorderStyle = BorderStyle.FixedSingle;
                    caja.BackColor = _paleta.FondoControl;
                    caja.ForeColor = _paleta.TextoPrincipal;
                }
                else if (control is DataGridView grilla)
                {
                    EstilarGrilla(grilla);
                }
                else if (control is ToolStrip barra) // incluye MenuStrip y StatusStrip
                {
                    EstilarBarra(barra);
                }
                else if (control is CheckedListBox listaCheck)
                {
                    listaCheck.BorderStyle = BorderStyle.FixedSingle;
                    listaCheck.BackColor = _paleta.FondoControl;
                    listaCheck.ForeColor = _paleta.TextoPrincipal;
                }
                else if (control is ListBox lista)
                {
                    lista.BorderStyle = BorderStyle.FixedSingle;
                    lista.BackColor = _paleta.FondoControl;
                    lista.ForeColor = _paleta.TextoPrincipal;
                }
                else if (control is ComboBox combo)
                {
                    combo.BackColor = _paleta.FondoControl;
                    combo.ForeColor = _paleta.TextoPrincipal;
                }
                else if (control is Label etiqueta)
                {
                    if (EsTextoDeTema(etiqueta.ForeColor))
                    {
                        etiqueta.ForeColor = _paleta.TextoPrincipal;
                    }
                    if (etiqueta.BackColor == SystemColors.Control)
                    {
                        etiqueta.BackColor = Color.Transparent;
                    }
                }
                else if (control is CheckBox casilla)
                {
                    casilla.ForeColor = _paleta.TextoPrincipal;
                    if (casilla.BackColor == SystemColors.Control)
                    {
                        casilla.BackColor = Color.Transparent;
                    }
                }
                else if (control is RadioButton opcion)
                {
                    opcion.ForeColor = _paleta.TextoPrincipal;
                    if (opcion.BackColor == SystemColors.Control)
                    {
                        opcion.BackColor = Color.Transparent;
                    }
                }
                else if (control is GroupBox grupo)
                {
                    grupo.ForeColor = _paleta.TextoPrincipal;
                    if (EsFondoDeTema(grupo.BackColor))
                    {
                        grupo.BackColor = _paleta.FondoFormulario;
                    }
                }
                else if (control is TabPage pagina)
                {
                    if (pagina.BackColor == Color.Transparent || EsFondoDeTema(pagina.BackColor))
                    {
                        pagina.BackColor = _paleta.FondoFormulario;
                    }
                    pagina.ForeColor = _paleta.TextoPrincipal;
                }
                else if (control is Panel panel)
                {
                    if (EsFondoDeTema(panel.BackColor))
                    {
                        panel.BackColor = _paleta.FondoFormulario;
                    }
                }

                if (control.HasChildren && !(control is DataGridView))
                {
                    AplicarAControles(control.Controls);
                }
            }
        }

        private static bool EsTextoDeTema(Color color)
        {
            return color == SystemColors.ControlText
                || color == PaletaClara.TextoPrincipal
                || color == PaletaOscura.TextoPrincipal;
        }

        private static bool EsFondoDeTema(Color color)
        {
            return color == SystemColors.Control
                || color == PaletaClara.FondoFormulario
                || color == PaletaOscura.FondoFormulario;
        }

        private static void AplicarFuente(Control control)
        {
            if (control.Font != null && control.Font.FontFamily.Name != FamiliaFuente)
            {
                control.Font = new Font(FamiliaFuente, control.Font.Size, control.Font.Style);
            }
        }

        private static void EstilarBoton(Button boton)
        {
            string nombre = (boton.Name ?? string.Empty).ToLowerInvariant();
            boton.FlatStyle = FlatStyle.Flat;
            boton.Cursor = Cursors.Hand;
            boton.UseVisualStyleBackColor = false;

            // "quitar" también es destructivo: esos botones se muestran como
            // "<< Eliminar" en pantalla a través de las traducciones.
            if (nombre.Contains("eliminar") || nombre.Contains("quitar"))
            {
                // acción destructiva
                boton.FlatAppearance.BorderSize = 0;
                boton.BackColor = _paleta.Peligro;
                boton.ForeColor = Color.White;
                boton.FlatAppearance.MouseOverBackColor = _paleta.PeligroHover;
                boton.FlatAppearance.MouseDownBackColor = _paleta.PeligroPresionado;
            }
            else if (nombre.Contains("cancelar") || nombre.Contains("salir"))
            {
                // acción secundaria / de salida
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = _paleta.SecundarioBorde;
                boton.BackColor = _paleta.SecundarioFondo;
                boton.ForeColor = _paleta.TextoPrincipal;
                boton.FlatAppearance.MouseOverBackColor = _paleta.SecundarioHover;
                boton.FlatAppearance.MouseDownBackColor = _paleta.SecundarioPresionado;
            }
            else
            {
                // acción principal
                boton.FlatAppearance.BorderSize = 0;
                boton.BackColor = _paleta.Primario;
                boton.ForeColor = Color.White;
                boton.FlatAppearance.MouseOverBackColor = _paleta.PrimarioHover;
                boton.FlatAppearance.MouseDownBackColor = _paleta.PrimarioPresionado;
            }
        }

        private static void EstilarGrilla(DataGridView grilla)
        {
            grilla.BorderStyle = BorderStyle.None;
            grilla.BackgroundColor = _paleta.FondoFormulario;
            grilla.GridColor = _paleta.GrillaLinea;
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = _paleta.GrillaEncabezadoFondo;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = _paleta.GrillaEncabezadoTexto;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = _paleta.GrillaEncabezadoFondo;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = _paleta.GrillaEncabezadoTexto;
            grilla.ColumnHeadersDefaultCellStyle.Font = new Font(FamiliaFuente, 9F, FontStyle.Bold);
            grilla.DefaultCellStyle.BackColor = _paleta.GrillaFondoCelda;
            grilla.DefaultCellStyle.ForeColor = _paleta.TextoPrincipal;
            grilla.DefaultCellStyle.SelectionBackColor = _paleta.GrillaSeleccion;
            grilla.DefaultCellStyle.SelectionForeColor = _paleta.GrillaSeleccionTexto;
            grilla.DefaultCellStyle.Font = new Font(FamiliaFuente, 9F);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = _paleta.GrillaFilaAlterna;
            grilla.RowHeadersDefaultCellStyle.BackColor = _paleta.GrillaFilaEncabezadoFondo;
            grilla.RowHeadersDefaultCellStyle.SelectionBackColor = _paleta.GrillaSeleccion;
        }

        private static void EstilarBarra(ToolStrip barra)
        {
            barra.Renderer = new RendererBarra_54CS(_paleta);
            barra.BackColor = _paleta.Barra;
            barra.ForeColor = _paleta.TextoPrincipal;
            EstilarItems(barra.Items);
        }

        private static void EstilarItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = Color.White;
                if (item.Font != null && item.Font.FontFamily.Name != FamiliaFuente)
                {
                    item.Font = new Font(FamiliaFuente, item.Font.Size, item.Font.Style);
                }
                if (item is ToolStripDropDownItem desplegable && desplegable.HasDropDownItems)
                {
                    desplegable.DropDown.BackColor = _paleta.Barra;
                    desplegable.DropDown.ForeColor = Color.White;
                    EstilarItems(desplegable.DropDownItems);
                }
            }
        }

        private static string RutaPreferencia()
        {
            return Path.Combine(Application.StartupPath, "tema.cfg");
        }

        private static Modo CargarPreferencia()
        {
            try
            {
                string ruta = RutaPreferencia();
                if (File.Exists(ruta) && File.ReadAllText(ruta).Trim().ToLowerInvariant() == "claro")
                {
                    return Modo.Claro;
                }
            }
            catch
            {
            }
            return Modo.Oscuro;
        }

        private static void GuardarPreferencia(Modo modo)
        {
            try
            {
                File.WriteAllText(RutaPreferencia(), modo == Modo.Claro ? "claro" : "oscuro");
            }
            catch
            {
                // si no se puede guardar, el tema igual queda aplicado en la sesión
            }
        }

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private static void AplicarBarraTitulo(Form formulario)
        {
            EventHandler aplicar = (s, e) =>
            {
                try
                {
                    int modoOscuro = _paleta.EsOscura ? 1 : 0;
                    if (DwmSetWindowAttribute(formulario.Handle, 20, ref modoOscuro, sizeof(int)) != 0)
                    {
                        DwmSetWindowAttribute(formulario.Handle, 19, ref modoOscuro, sizeof(int));
                    }
                }
                catch
                {
                    // sistemas sin soporte: la barra de título queda con el color estándar
                }
            };

            if (formulario.IsHandleCreated)
            {
                aplicar(formulario, EventArgs.Empty);
            }
            else
            {
                formulario.HandleCreated += aplicar;
            }
        }

        private class RendererBarra_54CS : ToolStripProfessionalRenderer
        {
            private readonly Paleta _p;

            public RendererBarra_54CS(Paleta paleta) : base(new TablaColores_54CS(paleta))
            {
                _p = paleta;
                RoundedEdges = false;
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = Color.White;
                base.OnRenderArrow(e);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (e.Item != null && e.Item.Enabled)
                {
                    e.TextColor = Color.White;
                }
                base.OnRenderItemText(e);
            }
        }

        private class TablaColores_54CS : ProfessionalColorTable
        {
            private readonly Paleta _p;

            public TablaColores_54CS(Paleta paleta)
            {
                _p = paleta;
            }

            public override Color MenuStripGradientBegin => _p.Barra;
            public override Color MenuStripGradientEnd => _p.Barra;
            public override Color ToolStripGradientBegin => _p.Barra;
            public override Color ToolStripGradientMiddle => _p.Barra;
            public override Color ToolStripGradientEnd => _p.Barra;
            public override Color ToolStripDropDownBackground => _p.Barra;
            public override Color ToolStripBorder => _p.Barra;
            public override Color ImageMarginGradientBegin => _p.Barra;
            public override Color ImageMarginGradientMiddle => _p.Barra;
            public override Color ImageMarginGradientEnd => _p.Barra;
            public override Color MenuItemSelected => _p.BarraHover;
            public override Color MenuItemSelectedGradientBegin => _p.BarraHover;
            public override Color MenuItemSelectedGradientEnd => _p.BarraHover;
            public override Color MenuItemPressedGradientBegin => _p.Barra;
            public override Color MenuItemPressedGradientMiddle => _p.Barra;
            public override Color MenuItemPressedGradientEnd => _p.Barra;
            public override Color MenuItemBorder => _p.MenuItemBorde;
            public override Color MenuBorder => _p.MenuBorde;
            public override Color SeparatorDark => _p.Separador;
            public override Color SeparatorLight => _p.Barra;
            public override Color StatusStripGradientBegin => _p.Barra;
            public override Color StatusStripGradientEnd => _p.Barra;
            public override Color GripDark => _p.BarraHover;
            public override Color GripLight => _p.Barra;
        }
    }
}
