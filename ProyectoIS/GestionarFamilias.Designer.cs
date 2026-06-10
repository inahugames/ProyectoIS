namespace ProyectoIS
{
    partial class GestionarFamilias
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
            this.listFamilias = new System.Windows.Forms.ListBox();
            this.chklist = new System.Windows.Forms.CheckedListBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listFamilias
            // 
            this.listFamilias.FormattingEnabled = true;
            this.listFamilias.Location = new System.Drawing.Point(12, 12);
            this.listFamilias.Name = "listFamilias";
            this.listFamilias.Size = new System.Drawing.Size(200, 420);
            this.listFamilias.TabIndex = 0;
            this.listFamilias.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listFamilias_MouseClick);
            // 
            // chklist
            // 
            this.chklist.CheckOnClick = true;
            this.chklist.FormattingEnabled = true;
            this.chklist.Location = new System.Drawing.Point(604, 12);
            this.chklist.Name = "chklist";
            this.chklist.Size = new System.Drawing.Size(193, 424);
            this.chklist.TabIndex = 2;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(396, 98);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(178, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre de Familia";
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(286, 159);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(288, 118);
            this.btnCrear.TabIndex = 5;
            this.btnCrear.Text = "Crear Familia";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // GestionarFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.chklist);
            this.Controls.Add(this.listFamilias);
            this.Name = "GestionarFamilias";
            this.Text = "GestionarFamilias";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listFamilias;
        private System.Windows.Forms.CheckedListBox chklist;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCrear;
    }
}