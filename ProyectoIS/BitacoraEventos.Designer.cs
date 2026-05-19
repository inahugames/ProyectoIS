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
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Módulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Evento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Criticidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaPicker = new System.Windows.Forms.DateTimePicker();
            this.btnAplicarFiltros = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.btnCancelarFiltros = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboCriticidad = new System.Windows.Forms.ComboBox();
            this.comboMódulo = new System.Windows.Forms.ComboBox();
            this.btnSalir = new System.Windows.Forms.Button();
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
            this.dgvEventos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.RowHeadersWidth = 51;
            this.dgvEventos.RowTemplate.Height = 24;
            this.dgvEventos.Size = new System.Drawing.Size(594, 445);
            this.dgvEventos.TabIndex = 0;
            this.dgvEventos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            // fechaPicker
            // 
            this.fechaPicker.Location = new System.Drawing.Point(749, 42);
            this.fechaPicker.Name = "fechaPicker";
            this.fechaPicker.ShowCheckBox = true;
            this.fechaPicker.Size = new System.Drawing.Size(200, 20);
            this.fechaPicker.TabIndex = 3;
            // 
            // btnAplicarFiltros
            // 
            this.btnAplicarFiltros.Location = new System.Drawing.Point(648, 153);
            this.btnAplicarFiltros.Margin = new System.Windows.Forms.Padding(2);
            this.btnAplicarFiltros.Name = "btnAplicarFiltros";
            this.btnAplicarFiltros.Size = new System.Drawing.Size(301, 89);
            this.btnAplicarFiltros.TabIndex = 6;
            this.btnAplicarFiltros.Text = "Aplicar Filtros";
            this.btnAplicarFiltros.UseVisualStyleBackColor = true;
            this.btnAplicarFiltros.Click += new System.EventHandler(this.btnFiltraLogin_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(749, 67);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(2);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(200, 20);
            this.txtLogin.TabIndex = 5;
            // 
            // btnCancelarFiltros
            // 
            this.btnCancelarFiltros.Location = new System.Drawing.Point(648, 265);
            this.btnCancelarFiltros.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelarFiltros.Name = "btnCancelarFiltros";
            this.btnCancelarFiltros.Size = new System.Drawing.Size(301, 89);
            this.btnCancelarFiltros.TabIndex = 7;
            this.btnCancelarFiltros.Text = "Cancelar Filtros";
            this.btnCancelarFiltros.UseVisualStyleBackColor = true;
            this.btnCancelarFiltros.Click += new System.EventHandler(this.btnCancelarFiltros_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(648, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Criticidad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(648, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Login";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(648, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Módulo";
            // 
            // comboCriticidad
            // 
            this.comboCriticidad.FormattingEnabled = true;
            this.comboCriticidad.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboCriticidad.Location = new System.Drawing.Point(749, 14);
            this.comboCriticidad.Name = "comboCriticidad";
            this.comboCriticidad.Size = new System.Drawing.Size(199, 21);
            this.comboCriticidad.TabIndex = 13;
            // 
            // comboMódulo
            // 
            this.comboMódulo.FormattingEnabled = true;
            this.comboMódulo.Items.AddRange(new object[] {
            "Login",
            "Gestión de Usuario"});
            this.comboMódulo.Location = new System.Drawing.Point(749, 90);
            this.comboMódulo.Name = "comboMódulo";
            this.comboMódulo.Size = new System.Drawing.Size(199, 21);
            this.comboMódulo.TabIndex = 14;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(651, 367);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(301, 89);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // BitacoraEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 466);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.comboMódulo);
            this.Controls.Add(this.comboCriticidad);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelarFiltros);
            this.Controls.Add(this.btnAplicarFiltros);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.fechaPicker);
            this.Controls.Add(this.dgvEventos);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BitacoraEventos";
            this.Text = "BitacoraEventos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.DateTimePicker fechaPicker;
        private System.Windows.Forms.Button btnAplicarFiltros;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Módulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Evento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Criticidad;
        private System.Windows.Forms.Button btnCancelarFiltros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboCriticidad;
        private System.Windows.Forms.ComboBox comboMódulo;
        private System.Windows.Forms.Button btnSalir;
    }
}