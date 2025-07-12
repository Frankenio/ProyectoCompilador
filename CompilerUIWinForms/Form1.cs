using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CompilerCore;
using FastColoredTextBoxNS;  // tu lógica de Lexico/Sintactico
namespace CompilerUIWinForms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    // ===========================================================
    private int index = 0;
       private void btnOpen_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog { Filter = "Archivos Java|*.java|Todos los archivos|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fctb.Text = File.ReadAllText(dlg.FileName);
                //HighlightJavaSyntax();
                toolStripStatusLabel.Text = $"Cargado: {Path.GetFileName(dlg.FileName)}";
                btnTranslate.Enabled = false;
            }
        }

        private void btnLexical_Click(object sender, EventArgs e)
        {
            dgvSymbols.Rows.Clear();
            listErrors.Items.Clear();

            var lexico = new Lexico();
            var symbols = lexico.Analizar(fctb.Text);

            foreach (var s in symbols)
                dgvSymbols.Rows.Add(s.Lexema, s.Token, s.Linea, s.Columna);
            foreach (var err in lexico._errores)
                listErrors.Items.Add($"Error Lexico({index++}): {err}");

            toolStripStatusLabel.Text = lexico._errores.Count == 0
                ? "Análisis Léxico: OK"
                : $"Análisis Léxico: {lexico._errores.Count} error(es)";
                btnTranslate.Enabled = false;
        }

        private void btnSyntactic_Click(object sender, EventArgs e)
        {
            listErrors.Items.Clear();

            var sintactico = new Sintactico();
            var symbols = GetSymbolsFromGrid();

            bool ok = sintactico.Analizar(symbols);

            foreach (var err in sintactico._errors)
                listErrors.Items.Add($"Error Sintactico({index++}): {err}");

            toolStripStatusLabel.Text = ok
                ? "Análisis Sintáctico: OK"
                : $"Análisis Sintáctico: {sintactico._errors.Count} error(es)";
            btnTranslate.Enabled = ok;
        }
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "SQL Files|*.sql|All Files|*.*" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var symbols = GetSymbolsFromGrid();
                string sql = SqlTranslator.TranslateClass(symbols); // implementar en CompilerCore
                File.WriteAllText(sfd.FileName, sql);
                toolStripStatusLabel.Text = $"Traducido a: {Path.GetFileName(sfd.FileName)}";
            }
        }
        private List<Simbolo> GetSymbolsFromGrid()
        {
            var list = new List<Simbolo>();
            foreach (DataGridViewRow row in dgvSymbols.Rows)
            {
                if (row.IsNewRow) continue;
                list.Add(new Simbolo
                {
                    Lexema = row.Cells[0].Value?.ToString(),
                    Token = row.Cells[1].Value?.ToString(),
                    Linea = Convert.ToInt32(row.Cells[2].Value),
                    Columna = Convert.ToInt32(row.Cells[3].Value)
                });
            }
            return list;
        }

        // ===========================================================
        private void HighlightJavaSyntax()
        {
            fctb.Language = Language.Custom;
            fctb.ClearStylesBuffer();
            fctb.Range.ClearStyle(StyleIndex.All);

            // Palabras clave
            fctb.Range.SetStyle(
                new TextStyle(Brushes.Blue, null, FontStyle.Regular),
                @"\b(public|private|protected|class|static|void|int|String|double|float|boolean|return|new|if|else|for|while|true|false|null)\b"
            );

            // Comentarios
            fctb.Range.SetStyle(
                new TextStyle(Brushes.Green, null, FontStyle.Italic),
                @"(//.*?$|/\*.*?\*/)",
                RegexOptions.Multiline
            );

            // Cadenas
            fctb.Range.SetStyle(
                new TextStyle(Brushes.Brown, null, FontStyle.Regular),
                "\".*?\""
            );
        }


}
