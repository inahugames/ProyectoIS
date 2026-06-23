using System;
using System.Collections.Generic;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace ProyectoIS
{
    public static class PdfBitacoraExporter_54CS
    {
        public static void Exportar(
            string rutaArchivo,
            string titulo,
            IList<string> lineasInfo,
            IList<string> encabezados,
            IList<float> anchosRelativos,
            IList<string[]> filas)
        {
            if (encabezados == null || encabezados.Count == 0)
                throw new ArgumentException("Se requieren encabezados para exportar.");
            if (filas == null)
                filas = new List<string[]>();

            int numColumnas = encabezados.Count;
            Document document = new Document();
            document.Info.Title = string.IsNullOrEmpty(titulo) ? "Bitácora de Eventos" : titulo;

            Style normal = document.Styles["Normal"];
            normal.Font.Name = "Arial";
            normal.Font.Size = 9;

            Section section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1.5);

            Paragraph pie = section.Footers.Primary.AddParagraph();
            pie.Format.Alignment = ParagraphAlignment.Center;
            pie.Format.Font.Size = 8;
            pie.AddText("Página ");
            pie.AddPageField();
            pie.AddText(" de ");
            pie.AddNumPagesField();

            Paragraph pTitulo = section.AddParagraph(document.Info.Title);
            pTitulo.Format.Font.Size = 16;
            pTitulo.Format.Font.Bold = true;
            pTitulo.Format.SpaceAfter = Unit.FromPoint(6);

            if (lineasInfo != null)
            {
                foreach (string info in lineasInfo)
                {
                    Paragraph p = section.AddParagraph(info ?? string.Empty);
                    p.Format.Font.Size = 9;
                    p.Format.SpaceAfter = Unit.FromPoint(2);
                }
            }
            section.AddParagraph().Format.SpaceAfter = Unit.FromPoint(4);

            Table table = section.AddTable();
            table.Borders.Width = 0.5;
            table.Borders.Color = Colors.Gray;
            table.LeftPadding = Unit.FromPoint(3);
            table.RightPadding = Unit.FromPoint(3);

            const double anchoUtilCm = 18.0;
            double sumaPesos = 0;
            for (int i = 0; i < numColumnas; i++)
                sumaPesos += (anchosRelativos != null && i < anchosRelativos.Count) ? anchosRelativos[i] : 1f;
            if (sumaPesos <= 0) sumaPesos = numColumnas;

            for (int i = 0; i < numColumnas; i++)
            {
                float peso = (anchosRelativos != null && i < anchosRelativos.Count) ? anchosRelativos[i] : 1f;
                Column col = table.AddColumn(Unit.FromCentimeter(anchoUtilCm * (peso / sumaPesos)));
                col.Format.Alignment = ParagraphAlignment.Left;
            }

            Row filaEncabezado = table.AddRow();
            filaEncabezado.HeadingFormat = true;
            filaEncabezado.Format.Font.Bold = true;
            filaEncabezado.Shading.Color = Colors.LightGray;
            for (int i = 0; i < numColumnas; i++)
            {
                Paragraph ph = filaEncabezado.Cells[i].AddParagraph(encabezados[i] ?? string.Empty);
                ph.Format.Font.Size = 10;
            }

            // Filas de datos
            foreach (string[] datos in filas)
            {
                Row fila = table.AddRow();
                for (int i = 0; i < numColumnas; i++)
                {
                    string valor = (datos != null && i < datos.Length) ? (datos[i] ?? string.Empty) : string.Empty;
                    fila.Cells[i].AddParagraph(valor);
                }
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer();
            renderer.Document = document;
            renderer.RenderDocument();
            renderer.Save(rutaArchivo);
        }
    }
}
