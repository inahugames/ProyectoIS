namespace ProyectoIS
{
    partial class BitacoraEventos
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
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.txtCriticidad = new System.Windows.Forms.TextBox();
            this.btnFiltraCrit = new System.Windows.Forms.Button();
            this.fechaPicker = new System.Windows.Forms.DateTimePicker();
            this.btnFiltraFecha = new System.Windows.Forms.Button();
            this.btnFiltraLogin = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Módulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Evento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Criticidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEventos
            // 
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Login,
            this.Fecha,
            this.Módulo,
            this.Evento,
            this.Criticidad});
            this.dgvEventos.Location = new System.Drawing.Point(10, 11);
            this.dgvEventos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.RowHeadersWidth = 51;
            this.dgvEventos.RowTemplate.Height = 24;
            this.dgvEventos.Size = new System.Drawing.Size(594, 445);
            this.dgvEventos.TabIndex = 0;
            this.dgvEventos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // txtCriticidad
            // 
            this.txtCriticidad.Location = new System.Drawing.Point(762, 11);
            this.txtCriticidad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCriticidad.Name = "txtCriticidad";
            this.txtCriticidad.Size = new System.Drawing.Size(107, 20);
            this.txtCriticidad.TabIndex = 1;
            // 
            // btnFiltraCrit
            // 
            this.btnFiltraCrit.Location = new System.Drawing.Point(872, 9);
            this.btnFiltraCrit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFiltraCrit.Name = "btnFiltraCrit";
            this.btnFiltraCrit.Size = new System.Drawing.Size(79, 52);
            this.btnFiltraCrit.TabIndex = 2;
            this.btnFiltraCrit.Text = "Filtrar por Criticidad";
            this.btnFiltraCrit.UseVisualStyleBackColor = true;
            this.btnFiltraCrit.Click += new System.EventHandler(this.btnFiltraCrit_Click);
            // 
            // fechaPicker
            // 
            this.fechaPicker.Location = new System.Drawing.Point(669, 142);
            this.fechaPicker.Name = "fechaPicker";
            this.fechaPicker.Size = new System.Drawing.Size(200, 20);
            this.fechaPicker.TabIndex = 3;
            // 
            // btnFiltraFecha
            // 
            this.btnFiltraFecha.Location = new System.Drawing.Point(874, 142);
            this.btnFiltraFecha.Margin = new System.Windows.Forms.Padding(2);
            this.btnFiltraFecha.Name = "btnFiltraFecha";
            this.btnFiltraFecha.Size = new System.Drawing.Size(79, 52);
            this.btnFiltraFecha.TabIndex = 4;
            this.btnFiltraFecha.Text = "Filtrar por Fecha";
            this.btnFiltraFecha.UseVisualStyleBackColor = true;
            this.btnFiltraFecha.Click += new System.EventHandler(this.btnFiltraFecha_Click);
            // 
            // btnFiltraLogin
            // 
            this.btnFiltraLogin.Location = new System.Drawing.Point(872, 239);
            this.btnFiltraLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnFiltraLogin.Name = "btnFiltraLogin";
            this.btnFiltraLogin.Size = new System.Drawing.Size(79, 52);
            this.btnFiltraLogin.TabIndex = 6;
            this.btnFiltraLogin.Text = "Filtrar por Login";
            this.btnFiltraLogin.UseVisualStyleBackColor = true;
            this.btnFiltraLogin.Click += new System.EventHandler(this.btnFiltraLogin_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(762, 241);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(2);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(107, 20);
            this.txtLogin.TabIndex = 5;
            // 
            // Login
            // 
            this.Login.HeaderText = "Login";
            this.Login.MinimumWidth = 6;
            this.Login.Name = "Login";
            this.Login.Width = 125;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.MinimumWidth = 6;
            this.Fecha.Name = "Fecha";
            this.Fecha.Width = 125;
            // 
            // Módulo
            // 
            this.Módulo.HeaderText = "Módulo";
            this.Módulo.MinimumWidth = 6;
            this.Módulo.Name = "Módulo";
            this.Módulo.Width = 125;
            // 
            // Evento
            // 
            this.Evento.HeaderText = "Evento";
            this.Evento.Name = "Evento";
            // 
            // Criticidad
            // 
            this.Criticidad.HeaderText = "Criticidad";
            this.Criticidad.MinimumWidth = 6;
            this.Criticidad.Name = "Criticidad";
            this.Criticidad.Width = 125;
            // 
            // BitacoraEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 466);
            this.Controls.Add(this.btnFiltraLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.btnFiltraFecha);
            this.Controls.Add(this.fechaPicker);
            this.Controls.Add(this.btnFiltraCrit);
            this.Controls.Add(this.txtCriticidad);
            this.Controls.Add(this.dgvEventos);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BitacoraEventos";
            this.Text = "BitacoraEventos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.TextBox txtCriticidad;
        private System.Windows.Forms.Button btnFiltraCrit;
        private System.Windows.Forms.DateTimePicker fechaPicker;
        private System.Windows.Forms.Button btnFiltraFecha;
        private System.Windows.Forms.Button btnFiltraLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Módulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Evento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Criticidad;
    }
}