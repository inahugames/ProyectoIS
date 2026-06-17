using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public static class IdiomaManager
    {
        private static Dictionary<string, Dictionary<string, string>> _textos;
        public static void CargarIdioma(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                    throw new FileNotFoundException($"No se encontró el archivo de idioma en: {rutaArchivo}");

                string json = File.ReadAllText(rutaArchivo);
                _textos = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el idioma: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                // Si el nombre del control existe en el JSON, le cambiamos el texto
                if (diccionario.ContainsKey(ctrl.Name))
                {
                    ctrl.Text = diccionario[ctrl.Name];
                }

                // CASO ESPECIAL 1: DataGridView (Las columnas no son controles normales)
                if (ctrl is DataGridView dgv)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (diccionario.ContainsKey(col.Name))
                            col.HeaderText = diccionario[col.Name];
                    }
                }

                // CASO ESPECIAL 2: MenuStrip (Los menús desplegables de tu Formulario Principal)
                else if (ctrl is MenuStrip menu)
                {
                    foreach (ToolStripItem item in menu.Items)
                    {
                        if (diccionario.ContainsKey(item.Name))
                            item.Text = diccionario[item.Name];
                    }
                }
                if (ctrl.HasChildren)
                {
                    TraducirControles(ctrl.Controls, diccionario);
                }
            }
        }
    }
}
