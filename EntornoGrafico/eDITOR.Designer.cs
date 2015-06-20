namespace EntornoGrafico
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEditorCodigo = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lenguajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbControles = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdSimbolos = new System.Windows.Forms.DataGridView();
            this.txtErrores = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.tbControles.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSimbolos)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEditorCodigo
            // 
            this.txtEditorCodigo.AcceptsTab = true;
            this.txtEditorCodigo.Location = new System.Drawing.Point(12, 27);
            this.txtEditorCodigo.Name = "txtEditorCodigo";
            this.txtEditorCodigo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtEditorCodigo.Size = new System.Drawing.Size(807, 397);
            this.txtEditorCodigo.TabIndex = 10;
            this.txtEditorCodigo.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.lenguajeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(831, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // lenguajeToolStripMenuItem
            // 
            this.lenguajeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compilarToolStripMenuItem});
            this.lenguajeToolStripMenuItem.Name = "lenguajeToolStripMenuItem";
            this.lenguajeToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.lenguajeToolStripMenuItem.Text = "Lenguaje";
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.compilarToolStripMenuItem.Text = "Compilar";
            this.compilarToolStripMenuItem.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // tbControles
            // 
            this.tbControles.Controls.Add(this.tabPage1);
            this.tbControles.Controls.Add(this.tabPage2);
            this.tbControles.Location = new System.Drawing.Point(12, 430);
            this.tbControles.Name = "tbControles";
            this.tbControles.SelectedIndex = 0;
            this.tbControles.Size = new System.Drawing.Size(807, 144);
            this.tbControles.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtErrores);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(799, 118);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Salida Compilador";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdSimbolos);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(799, 118);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Simbolos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grdSimbolos
            // 
            this.grdSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSimbolos.Location = new System.Drawing.Point(6, 6);
            this.grdSimbolos.Name = "grdSimbolos";
            this.grdSimbolos.Size = new System.Drawing.Size(787, 106);
            this.grdSimbolos.TabIndex = 0;
            // 
            // txtErrores
            // 
            this.txtErrores.Location = new System.Drawing.Point(6, 6);
            this.txtErrores.Name = "txtErrores";
            this.txtErrores.Size = new System.Drawing.Size(787, 106);
            this.txtErrores.TabIndex = 0;
            this.txtErrores.Text = "";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 586);
            this.Controls.Add(this.tbControles);
            this.Controls.Add(this.txtEditorCodigo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.Text = "SIMPLE Studio";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tbControles.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSimbolos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox txtEditorCodigo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lenguajeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.TabControl tbControles;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView grdSimbolos;
        private System.Windows.Forms.RichTextBox txtErrores;
    }
}

