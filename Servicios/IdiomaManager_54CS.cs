using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public static class IdiomaManager_54CS
    {
        public const string IdiomaPorDefecto = "es";

        private static Dictionary<string, Dictionary<string, string>> _textos;
        private static readonly List<IIdiomaObservador_54CS> _observadores = new List<IIdiomaObservador_54CS>();
        private static string _carpetaIdiomas;
        private static string _idiomaActual;
        public static string IdiomaActual => _idiomaActual;

        public static void Inicializar(string carpetaIdiomas)
        {
            _carpetaIdiomas = carpetaIdiomas;
        }
        public static List<string> ObtenerIdiomasDisponibles()
        {
            List<string> codigos = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(_carpetaIdiomas) && Directory.Exists(_carpetaIdiomas))
                {
                    foreach (string archivo in Directory.GetFiles(_carpetaIdiomas, "*.json"))
                    {
                        codigos.Add(Path.GetFileNameWithoutExtension(archivo));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar los idiomas disponibles: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return codigos.OrderBy(c => c).ToList();
        }
        public static string ObtenerNombreAmigable(string codigoIdioma)
        {
            try
            {
                string nombre = new CultureInfo(codigoIdioma).NativeName;
                int idx = nombre.IndexOf('(');
                if (idx > 0)
                {
                    nombre = nombre.Substring(0, idx).Trim();
                }
                return string.IsNullOrEmpty(nombre) ? codigoIdioma : char.ToUpper(nombre[0]) + nombre.Substring(1);
            }
            catch
            {
                return codigoIdioma;
            }
        }
        public static void CargarIdioma(string codigoIdioma)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigoIdioma))
                {
                    codigoIdioma = IdiomaPorDefecto;
                }

                string rutaArchivo = Path.Combine(_carpetaIdiomas ?? string.Empty, $"{codigoIdioma}.json");

                if (!File.Exists(rutaArchivo))
                    throw new FileNotFoundException($"No se encontró el archivo de idioma en: {rutaArchivo}");

                string json = File.ReadAllText(rutaArchivo);
                _textos = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
                _idiomaActual = codigoIdioma;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el idioma: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void CambiarIdioma(string codigoIdioma)
        {
            CargarIdioma(codigoIdioma);
            NotificarObservadores();
        }
        public static void Traducir(Form formulario)
        {
            if (_textos == null || !_textos.ContainsKey(formulario.Name))
                return; // Si no hay traducciones para este formulario, no hace nada

            var diccionarioForm = _textos[formulario.Name];

            //Traducir el título de la ventana
            if (diccionarioForm.ContainsKey("Text"))
            {
                formulario.Text = diccionarioForm["Text"];
            }

            //Traducir todos los controles internos
            TraducirControles(formulario.Controls, diccionarioForm);
        }

        private static void TraducirControles(Control.ControlCollection controles, Dictionary<string, string> diccionario)
        {
            foreach (Control ctrl in controles)
            {
                // Si el nombre del control existe en el json, le cambiamos el texto
                if (diccionario.ContainsKey(ctrl.Name))
                {
                    ctrl.Text = diccionario[ctrl.Name];
                }

                // DataGridView
                if (ctrl is DataGridView dgv)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (diccionario.ContainsKey(col.Name))
                            col.HeaderText = diccionario[col.Name];
                    }
                }
                // MenuStrip
                else if (ctrl is ToolStrip barra)
                {
                    TraducirToolStripItems(barra.Items, diccionario);
                }

                if (ctrl.HasChildren)
                {
                    TraducirControles(ctrl.Controls, diccionario);
                }
            }
        }

        private static void TraducirToolStripItems(ToolStripItemCollection items, Dictionary<string, string> diccionario)
        {
            foreach (ToolStripItem item in items)
            {
                if (diccionario.ContainsKey(item.Name))
                    item.Text = diccionario[item.Name];

                //para sub-menu
                if (item is ToolStripDropDownItem desplegable && desplegable.HasDropDownItems)
                {
                    TraducirToolStripItems(desplegable.DropDownItems, diccionario);
                }
            }
        }
        public static string ObtenerTexto(string seccion, string clave, string valorPorDefecto = "")
        {
            if (_textos != null && _textos.ContainsKey(seccion) && _textos[seccion].ContainsKey(clave))
                return _textos[seccion][clave];
            return valorPorDefecto;
        }
        // Observer
        public static void Suscribir(Form formulario)
        {
            if (!(formulario is IIdiomaObservador_54CS observador))
                return;

            if (!_observadores.Contains(observador))
            {
                _observadores.Add(observador);
                formulario.FormClosed += (s, e) => Desuscribir(observador);
            }

            observador.ActualizarIdioma(); // aplica el idioma actual apenas se susbcribe
        }
        public static void Desuscribir(IIdiomaObservador_54CS observador)
        {
            _observadores.Remove(observador);
        }

        private static void NotificarObservadores()
        {
            foreach (IIdiomaObservador_54CS observador in _observadores.ToList())
            {
                observador.ActualizarIdioma();
            }
        }
    }
}
