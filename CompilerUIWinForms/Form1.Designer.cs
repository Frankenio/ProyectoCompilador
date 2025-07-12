// Form1.Designer.cs
namespace CompilerUIWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private FastColoredTextBoxNS.FastColoredTextBox fctb;
        private System.Windows.Forms.DataGridView dgvSymbols;
        private System.Windows.Forms.ListBox listErrors;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel panelTop;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnLexical;
        private System.Windows.Forms.Button btnSyntactic;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TabControl tabErrors;
        private System.Windows.Forms.TabPage tabPageErrors;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) 
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Text = "Analizador Léxico y Sintáctico";
            this.Width = 1000;
            this.Height = 700;

            // Top button panel
            panelTop = new System.Windows.Forms.FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(5)
            };
            btnOpen = new Button { Text = "📂 Abrir", AutoSize = true };
            btnLexical = new Button { Text = "📑 Léxico", AutoSize = true };
            btnSyntactic = new Button { Text = "🔍 Sintáctico", AutoSize = true };
            btnTranslate = new Button { Text = "🔄 Traducir",  AutoSize = true, Enabled = false };
            btnOpen.Click += btnOpen_Click;
            btnLexical.Click += btnLexical_Click;
            btnSyntactic.Click += btnSyntactic_Click;
            btnTranslate.Click += btnTranslate_Click;
            panelTop.Controls.AddRange(new Control[] { btnOpen, btnLexical, btnSyntactic, btnTranslate });

            // FastColoredTextBox editor
            fctb = new FastColoredTextBoxNS.FastColoredTextBox
            {
                Dock = DockStyle.Fill,
                Language = FastColoredTextBoxNS.Language.CSharp,
                Font = new System.Drawing.Font("Consolas", 10F),
                ShowLineNumbers = true,
                BackColor = Color.White
            };

            // Symbol table grid
            dgvSymbols = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvSymbols.Columns.Add("Lexema", "Lexema");
            dgvSymbols.Columns.Add("Token", "Token");
            dgvSymbols.Columns.Add("Linea", "Línea");
            dgvSymbols.Columns.Add("Columna", "Columna");

            // Split container (editor | grid)
            splitMain = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 600
            };
            splitMain.Panel1.Controls.Add(fctb);
            splitMain.Panel2.Controls.Add(dgvSymbols);

            // Errors tab
            listErrors = new ListBox { Dock = DockStyle.Fill };
            tabPageErrors = new TabPage("Errores");
            tabPageErrors.Controls.Add(listErrors);
            tabErrors = new TabControl
            {
                Dock = DockStyle.Bottom,
                Height = 180
            };
            tabErrors.TabPages.Add(tabPageErrors);

            // Status strip
            statusStrip = new StatusStrip { Dock = DockStyle.Bottom };
            toolStripStatusLabel = new ToolStripStatusLabel { Text = "Listo" };
            statusStrip.Items.Add(toolStripStatusLabel);

            // ---- Add controls in correct z-order ----
            this.Controls.Add(statusStrip);
            this.Controls.Add(tabErrors);
            this.Controls.Add(splitMain);
            this.Controls.Add(panelTop);
        }

        #endregion
    }
}
