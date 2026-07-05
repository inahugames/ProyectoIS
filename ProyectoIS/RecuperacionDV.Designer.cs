namespace ProyectoIS
{
    partial class RecuperacionDV
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.btnRecalcular = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(120)))), ((int)(((byte)(106)))));
            this.lblTitulo.Location = new System.Drawing.Point(12, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(383, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "¡INCONSISTENCIA DE DATOS DETECTADA!";
            //
            // lblInfo
            //
            this.lblInfo.Location = new System.Drawing.Point(12, 48);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(536, 48);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Los Dígitos Verificadores calculados sobre los datos no coinciden con los almace" +
    "nados en la tabla DV. Como Administrador del Sistema, seleccione una acción para" +
    " continuar:";
            //
            // txtDetalle
            //
            this.txtDetalle.Location = new System.Drawing.Point(15, 99);
            this.txtDetalle.Multiline = true;
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.ReadOnly = true;
            this.txtDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetalle.Size = new System.Drawing.Size(533, 175);
            this.txtDetalle.TabIndex = 2;
            //
            // btnRecalcular
            //
            this.btnRecalcular.Location = new System.Drawing.Point(15, 290);
            this.btnRecalcular.Name = "btnRecalcular";
            this.btnRecalcular.Size = new System.Drawing.Size(170, 45);
            this.btnRecalcular.TabIndex = 3;
            this.btnRecalcular.Text = "RECALCULAR EL DV";
            this.btnRecalcular.UseVisualStyleBackColor = true;
            this.btnRecalcular.Click += new System.EventHandler(this.btnRecalcular_Click);
            //
            // btnRestore
            //
            this.btnRestore.Location = new System.Drawing.Point(197, 290);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(170, 45);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "RESTORE BD";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            //
            // btnSalir
            //
            this.btnSalir.Location = new System.Drawing.Point(378, 290);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(170, 45);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // RecuperacionDV
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 350);
            this.ControlBox = false;
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnRecalcular);
            this.Controls.Add(this.txtDetalle);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecuperacionDV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recuperación de Integridad - Dígito Verificador";
            this.Load += new System.EventHandler(this.RecuperacionDV_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Button btnRecalcular;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnSalir;
    }
}
