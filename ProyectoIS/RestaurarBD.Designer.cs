namespace ProyectoIS
{
    partial class RestaurarBD
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
            this.lblArchivo = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.lblAviso = new System.Windows.Forms.Label();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblArchivo
            //
            this.lblArchivo.AutoSize = true;
            this.lblArchivo.Location = new System.Drawing.Point(12, 15);
            this.lblArchivo.Name = "lblArchivo";
            this.lblArchivo.Size = new System.Drawing.Size(137, 13);
            this.lblArchivo.TabIndex = 0;
            this.lblArchivo.Text = "Archivo de backup (.bak):";
            //
            // txtRuta
            //
            this.txtRuta.Location = new System.Drawing.Point(15, 40);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(400, 20);
            this.txtRuta.TabIndex = 1;
            //
            // btnExaminar
            //
            this.btnExaminar.Location = new System.Drawing.Point(425, 37);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(80, 26);
            this.btnExaminar.TabIndex = 2;
            this.btnExaminar.Text = "Examinar...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            //
            // lblAviso
            //
            this.lblAviso.Location = new System.Drawing.Point(12, 75);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(493, 55);
            this.lblAviso.TabIndex = 3;
            this.lblAviso.Text = "Se recomienda elegir el backup más reciente, de manera de perder la mínima canti" +
    "dad de datos de las operaciones normales del sistema.";
            //
            // btnRestaurar
            //
            this.btnRestaurar.Location = new System.Drawing.Point(148, 140);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(105, 35);
            this.btnRestaurar.TabIndex = 4;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(268, 140);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 35);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // RestaurarBD
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 191);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.lblAviso);
            this.Controls.Add(this.btnExaminar);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.lblArchivo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestaurarBD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Restaurar Base de Datos";
            this.Load += new System.EventHandler(this.RestaurarBD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblArchivo;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
