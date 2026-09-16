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
            if (disposing)
            {
                fuenteTablaBitacora.Dispose();
                fuenteEncabezadoBitacora.Dispose();
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
            this.fechaPickerInicio = new System.Windows.Forms.DateTimePicker();
            this.btnAplicarFiltros = new UsuariosRoundedButton();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.btnCancelarFiltros = new UsuariosRoundedButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboCriticidad = new System.Windows.Forms.ComboBox();
            this.comboMódulo = new System.Windows.Forms.ComboBox();
            this.btnSalir = new UsuariosRoundedButton();
            this.label5 = new System.Windows.Forms.Label();
            this.fechaPickerFin = new System.Windows.Forms.DateTimePicker();
            this.lblResponsable = new System.Windows.Forms.Label();
            this.lblNombreResp = new System.Windows.Forms.Label();
            this.txtNombreResponsable = new System.Windows.Forms.TextBox();
            this.lblApellidoResp = new System.Windows.Forms.Label();
            this.txtApellidoResponsable = new System.Windows.Forms.TextBox();
            this.btnExportarPDF = new UsuariosRoundedButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.campoLoginBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.campoModuloBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.campoCriticidadBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.campoInicioBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.campoFinBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.campoNombreBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.campoApellidoBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.filtrosBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.tablaBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.responsableBitacora = new ProyectoIS.UsuariosRoundedPanel();
            this.tituloBitacora = new System.Windows.Forms.Label();
            this.subtituloBitacora = new System.Windows.Forms.Label();
            this.tituloFiltrosBitacora = new System.Windows.Forms.Label();
            this.temaBitacora = new ProyectoIS.UsuariosThemeSwitch();
            this.iconoBitacora = new ProyectoIS.BitacoraGlyph();
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
            this.dgvEventos.SelectionChanged += new System.EventHandler(this.dgvEventos_SelectionChanged);
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
            // fechaPickerInicio
            // 
            this.fechaPickerInicio.Location = new System.Drawing.Point(749, 42);
            this.fechaPickerInicio.Name = "fechaPickerInicio";
            this.fechaPickerInicio.ShowCheckBox = true;
            this.fechaPickerInicio.Size = new System.Drawing.Size(200, 20);
            this.fechaPickerInicio.TabIndex = 3;
            this.fechaPickerInicio.ValueChanged += new System.EventHandler(this.fechaPickerInicio_ValueChanged);
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
            this.txtLogin.Location = new System.Drawing.Point(749, 92);
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
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha inicio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Login";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(648, 123);
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
            "Gestión de Usuario",
            "Idioma"});
            this.comboMódulo.Location = new System.Drawing.Point(749, 115);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(648, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Fecha fin";
            // 
            // fechaPickerFin
            // 
            this.fechaPickerFin.Location = new System.Drawing.Point(749, 68);
            this.fechaPickerFin.Name = "fechaPickerFin";
            this.fechaPickerFin.ShowCheckBox = true;
            this.fechaPickerFin.Size = new System.Drawing.Size(200, 20);
            this.fechaPickerFin.TabIndex = 16;
            this.fechaPickerFin.ValueChanged += new System.EventHandler(this.fechaPickerFin_ValueChanged);
            // 
            // lblResponsable
            // 
            this.lblResponsable.AutoSize = true;
            this.lblResponsable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponsable.Location = new System.Drawing.Point(10, 466);
            this.lblResponsable.Name = "lblResponsable";
            this.lblResponsable.Size = new System.Drawing.Size(243, 16);
            this.lblResponsable.TabIndex = 18;
            this.lblResponsable.Text = "Responsable del evento seleccionado:";
            // 
            // lblNombreResp
            // 
            this.lblNombreResp.AutoSize = true;
            this.lblNombreResp.Location = new System.Drawing.Point(10, 495);
            this.lblNombreResp.Name = "lblNombreResp";
            this.lblNombreResp.Size = new System.Drawing.Size(47, 13);
            this.lblNombreResp.TabIndex = 19;
            this.lblNombreResp.Text = "Nombre:";
            // 
            // txtNombreResponsable
            // 
            this.txtNombreResponsable.Location = new System.Drawing.Point(75, 492);
            this.txtNombreResponsable.Name = "txtNombreResponsable";
            this.txtNombreResponsable.ReadOnly = true;
            this.txtNombreResponsable.Size = new System.Drawing.Size(200, 20);
            this.txtNombreResponsable.TabIndex = 20;
            this.txtNombreResponsable.TabStop = false;
            // 
            // lblApellidoResp
            // 
            this.lblApellidoResp.AutoSize = true;
            this.lblApellidoResp.Location = new System.Drawing.Point(295, 495);
            this.lblApellidoResp.Name = "lblApellidoResp";
            this.lblApellidoResp.Size = new System.Drawing.Size(47, 13);
            this.lblApellidoResp.TabIndex = 21;
            this.lblApellidoResp.Text = "Apellido:";
            // 
            // txtApellidoResponsable
            // 
            this.txtApellidoResponsable.Location = new System.Drawing.Point(360, 492);
            this.txtApellidoResponsable.Name = "txtApellidoResponsable";
            this.txtApellidoResponsable.ReadOnly = true;
            this.txtApellidoResponsable.Size = new System.Drawing.Size(200, 20);
            this.txtApellidoResponsable.TabIndex = 22;
            this.txtApellidoResponsable.TabStop = false;
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Location = new System.Drawing.Point(651, 470);
            this.btnExportarPDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(301, 60);
            this.btnExportarPDF.TabIndex = 23;
            this.btnExportarPDF.Text = "Exportar a PDF";
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // BitacoraEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.Controls.Add(this.btnExportarPDF);
            this.Controls.Add(this.txtApellidoResponsable);
            this.Controls.Add(this.lblApellidoResp);
            this.Controls.Add(this.txtNombreResponsable);
            this.Controls.Add(this.lblNombreResp);
            this.Controls.Add(this.lblResponsable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fechaPickerFin);
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
            this.Controls.Add(this.fechaPickerInicio);
            this.Controls.Add(this.dgvEventos);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BitacoraEventos";
            this.Text = "BitacoraEventos";
            System.Windows.Forms.DataGridViewCellStyle fechaEstilo = new System.Windows.Forms.DataGridViewCellStyle();
            this.campoLoginBitacora.Name = "campoLoginBitacora";
            this.campoModuloBitacora.Name = "campoModuloBitacora";
            this.campoCriticidadBitacora.Name = "campoCriticidadBitacora";
            this.campoInicioBitacora.Name = "campoInicioBitacora";
            this.campoFinBitacora.Name = "campoFinBitacora";
            this.campoNombreBitacora.Name = "campoNombreBitacora";
            this.campoApellidoBitacora.Name = "campoApellidoBitacora";
            this.filtrosBitacora.Name = "filtrosBitacora";
            this.tablaBitacora.Name = "tablaBitacora";
            this.responsableBitacora.Name = "responsableBitacora";
            this.tituloBitacora.Name = "tituloBitacora";
            this.subtituloBitacora.Name = "subtituloBitacora";
            this.tituloFiltrosBitacora.Name = "tituloFiltrosBitacora";
            this.temaBitacora.Name = "temaBitacora";
            this.iconoBitacora.Name = "iconoBitacora";
            this.tituloBitacora.Location = new System.Drawing.Point(80, 18);
            this.tituloBitacora.Size = new System.Drawing.Size(730, 43);
            this.tituloBitacora.Font = new System.Drawing.Font("Segoe UI", 23F, System.Drawing.FontStyle.Bold);
            this.tituloBitacora.Text = "Bitácora de eventos";
            this.subtituloBitacora.Location = new System.Drawing.Point(82, 72);
            this.subtituloBitacora.Size = new System.Drawing.Size(650, 24);
            this.subtituloBitacora.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtituloBitacora.Text = "Consultá la actividad registrada en el sistema";
            this.tituloFiltrosBitacora.Location = new System.Drawing.Point(18, 14);
            this.tituloFiltrosBitacora.Size = new System.Drawing.Size(400, 26);
            this.tituloFiltrosBitacora.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tituloFiltrosBitacora.Text = "Filtros";
            this.iconoBitacora.Location = new System.Drawing.Point(28, 27);
            this.iconoBitacora.Size = new System.Drawing.Size(38, 38);
            this.iconoBitacora.BackColor = System.Drawing.Color.Transparent;
            this.iconoBitacora.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.temaBitacora.Location = new System.Drawing.Point(840, 28);
            this.temaBitacora.Size = new System.Drawing.Size(96, 38);
            this.temaBitacora.Click += new System.EventHandler(this.temaBitacora_Click);
            this.filtrosBitacora.Location = new System.Drawing.Point(24, 108);
            this.filtrosBitacora.Size = new System.Drawing.Size(1132, 166);
            this.filtrosBitacora.CornerRadius = 16;
            this.filtrosBitacora.BackColor = System.Drawing.Color.White;
            this.filtrosBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.tablaBitacora.Location = new System.Drawing.Point(24, 288);
            this.tablaBitacora.Size = new System.Drawing.Size(1132, 310);
            this.tablaBitacora.CornerRadius = 16;
            this.tablaBitacora.BackColor = System.Drawing.Color.White;
            this.tablaBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.responsableBitacora.Location = new System.Drawing.Point(24, 612);
            this.responsableBitacora.Size = new System.Drawing.Size(790, 108);
            this.responsableBitacora.CornerRadius = 16;
            this.responsableBitacora.BackColor = System.Drawing.Color.White;
            this.responsableBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.filtrosBitacora.Controls.Add(this.tituloFiltrosBitacora);
            this.label3.Location = new System.Drawing.Point(18, 43);
            this.label3.Size = new System.Drawing.Size(209, 24);
            this.label3.AutoSize = false;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.campoLoginBitacora.Location = new System.Drawing.Point(18, 70);
            this.campoLoginBitacora.Size = new System.Drawing.Size(209, 38);
            this.campoLoginBitacora.CornerRadius = 9;
            this.campoLoginBitacora.Padding = new System.Windows.Forms.Padding(0);
            this.campoLoginBitacora.TabIndex = 0;
            this.campoLoginBitacora.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.campoLoginBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.txtLogin.Location = new System.Drawing.Point(9, 7);
            this.txtLogin.Size = new System.Drawing.Size(191, 25);
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtLogin.TabIndex = 0;
            this.txtLogin.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.txtLogin.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.txtLogin.Enter += new System.EventHandler(this.campoBitacora_Enter);
            this.txtLogin.Leave += new System.EventHandler(this.campoBitacora_Leave);
            this.campoLoginBitacora.Controls.Add(this.txtLogin);
            this.filtrosBitacora.Controls.Add(this.label3);
            this.filtrosBitacora.Controls.Add(this.campoLoginBitacora);
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label4.Location = new System.Drawing.Point(239, 43);
            this.label4.Size = new System.Drawing.Size(209, 24);
            this.label4.AutoSize = false;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.campoModuloBitacora.Location = new System.Drawing.Point(239, 70);
            this.campoModuloBitacora.Size = new System.Drawing.Size(209, 38);
            this.campoModuloBitacora.CornerRadius = 9;
            this.campoModuloBitacora.Padding = new System.Windows.Forms.Padding(0);
            this.campoModuloBitacora.TabIndex = 1;
            this.campoModuloBitacora.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.campoModuloBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.comboMódulo.Location = new System.Drawing.Point(9, 7);
            this.comboMódulo.Size = new System.Drawing.Size(191, 25);
            this.comboMódulo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.comboMódulo.TabIndex = 0;
            this.comboMódulo.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.comboMódulo.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.comboMódulo.Enter += new System.EventHandler(this.campoBitacora_Enter);
            this.comboMódulo.Leave += new System.EventHandler(this.campoBitacora_Leave);
            this.campoModuloBitacora.Controls.Add(this.comboMódulo);
            this.filtrosBitacora.Controls.Add(this.label4);
            this.filtrosBitacora.Controls.Add(this.campoModuloBitacora);
            this.comboMódulo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboMódulo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboMódulo.ItemHeight = 23;
            this.comboMódulo.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBitacora_DrawItem);
            this.label1.Location = new System.Drawing.Point(460, 43);
            this.label1.Size = new System.Drawing.Size(209, 24);
            this.label1.AutoSize = false;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.campoCriticidadBitacora.Location = new System.Drawing.Point(460, 70);
            this.campoCriticidadBitacora.Size = new System.Drawing.Size(209, 38);
            this.campoCriticidadBitacora.CornerRadius = 9;
            this.campoCriticidadBitacora.Padding = new System.Windows.Forms.Padding(0);
            this.campoCriticidadBitacora.TabIndex = 2;
            this.campoCriticidadBitacora.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.campoCriticidadBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.comboCriticidad.Location = new System.Drawing.Point(9, 7);
            this.comboCriticidad.Size = new System.Drawing.Size(191, 25);
            this.comboCriticidad.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.comboCriticidad.TabIndex = 0;
            this.comboCriticidad.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.comboCriticidad.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.comboCriticidad.Enter += new System.EventHandler(this.campoBitacora_Enter);
            this.comboCriticidad.Leave += new System.EventHandler(this.campoBitacora_Leave);
            this.campoCriticidadBitacora.Controls.Add(this.comboCriticidad);
            this.filtrosBitacora.Controls.Add(this.label1);
            this.filtrosBitacora.Controls.Add(this.campoCriticidadBitacora);
            this.comboCriticidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboCriticidad.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboCriticidad.ItemHeight = 23;
            this.comboCriticidad.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBitacora_DrawItem);
            this.label2.Location = new System.Drawing.Point(681, 43);
            this.label2.Size = new System.Drawing.Size(209, 24);
            this.label2.AutoSize = false;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.campoInicioBitacora.Location = new System.Drawing.Point(681, 70);
            this.campoInicioBitacora.Size = new System.Drawing.Size(209, 38);
            this.campoInicioBitacora.CornerRadius = 9;
            this.campoInicioBitacora.Padding = new System.Windows.Forms.Padding(0);
            this.campoInicioBitacora.TabIndex = 3;
            this.campoInicioBitacora.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.campoInicioBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.fechaPickerInicio.Location = new System.Drawing.Point(9, 7);
            this.fechaPickerInicio.Size = new System.Drawing.Size(191, 25);
            this.fechaPickerInicio.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.fechaPickerInicio.TabIndex = 0;
            this.fechaPickerInicio.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.fechaPickerInicio.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.fechaPickerInicio.Enter += new System.EventHandler(this.campoBitacora_Enter);
            this.fechaPickerInicio.Leave += new System.EventHandler(this.campoBitacora_Leave);
            this.campoInicioBitacora.Controls.Add(this.fechaPickerInicio);
            this.filtrosBitacora.Controls.Add(this.label2);
            this.filtrosBitacora.Controls.Add(this.campoInicioBitacora);
            this.fechaPickerInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fechaPickerInicio.CustomFormat = "dd/MM/yyyy";
            this.label5.Location = new System.Drawing.Point(902, 43);
            this.label5.Size = new System.Drawing.Size(209, 24);
            this.label5.AutoSize = false;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.campoFinBitacora.Location = new System.Drawing.Point(902, 70);
            this.campoFinBitacora.Size = new System.Drawing.Size(209, 38);
            this.campoFinBitacora.CornerRadius = 9;
            this.campoFinBitacora.Padding = new System.Windows.Forms.Padding(0);
            this.campoFinBitacora.TabIndex = 4;
            this.campoFinBitacora.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.campoFinBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.fechaPickerFin.Location = new System.Drawing.Point(9, 7);
            this.fechaPickerFin.Size = new System.Drawing.Size(191, 25);
            this.fechaPickerFin.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.fechaPickerFin.TabIndex = 0;
            this.fechaPickerFin.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.fechaPickerFin.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.fechaPickerFin.Enter += new System.EventHandler(this.campoBitacora_Enter);
            this.fechaPickerFin.Leave += new System.EventHandler(this.campoBitacora_Leave);
            this.campoFinBitacora.Controls.Add(this.fechaPickerFin);
            this.filtrosBitacora.Controls.Add(this.label5);
            this.filtrosBitacora.Controls.Add(this.campoFinBitacora);
            this.fechaPickerFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fechaPickerFin.CustomFormat = "dd/MM/yyyy";
            this.lblNombreResp.Location = new System.Drawing.Point(18, 40);
            this.lblNombreResp.Size = new System.Drawing.Size(368, 23);
            this.lblNombreResp.AutoSize = false;
            this.lblNombreResp.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.campoNombreBitacora.Location = new System.Drawing.Point(18, 65);
            this.campoNombreBitacora.Size = new System.Drawing.Size(368, 32);
            this.campoNombreBitacora.CornerRadius = 9;
            this.campoNombreBitacora.Padding = new System.Windows.Forms.Padding(0);
            this.campoNombreBitacora.TabIndex = 5;
            this.campoNombreBitacora.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.campoNombreBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.txtNombreResponsable.Location = new System.Drawing.Point(9, 6);
            this.txtNombreResponsable.Size = new System.Drawing.Size(350, 24);
            this.txtNombreResponsable.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNombreResponsable.TabIndex = 0;
            this.txtNombreResponsable.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.txtNombreResponsable.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.txtNombreResponsable.Enter += new System.EventHandler(this.campoBitacora_Enter);
            this.txtNombreResponsable.Leave += new System.EventHandler(this.campoBitacora_Leave);
            this.campoNombreBitacora.Controls.Add(this.txtNombreResponsable);
            this.responsableBitacora.Controls.Add(this.lblNombreResp);
            this.responsableBitacora.Controls.Add(this.campoNombreBitacora);
            this.txtNombreResponsable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblApellidoResp.Location = new System.Drawing.Point(404, 40);
            this.lblApellidoResp.Size = new System.Drawing.Size(368, 23);
            this.lblApellidoResp.AutoSize = false;
            this.lblApellidoResp.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.campoApellidoBitacora.Location = new System.Drawing.Point(404, 65);
            this.campoApellidoBitacora.Size = new System.Drawing.Size(368, 32);
            this.campoApellidoBitacora.CornerRadius = 9;
            this.campoApellidoBitacora.Padding = new System.Windows.Forms.Padding(0);
            this.campoApellidoBitacora.TabIndex = 6;
            this.campoApellidoBitacora.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.campoApellidoBitacora.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.txtApellidoResponsable.Location = new System.Drawing.Point(9, 6);
            this.txtApellidoResponsable.Size = new System.Drawing.Size(350, 24);
            this.txtApellidoResponsable.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtApellidoResponsable.TabIndex = 0;
            this.txtApellidoResponsable.BackColor = System.Drawing.Color.FromArgb(249, 250, 252);
            this.txtApellidoResponsable.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.txtApellidoResponsable.Enter += new System.EventHandler(this.campoBitacora_Enter);
            this.txtApellidoResponsable.Leave += new System.EventHandler(this.campoBitacora_Leave);
            this.campoApellidoBitacora.Controls.Add(this.txtApellidoResponsable);
            this.responsableBitacora.Controls.Add(this.lblApellidoResp);
            this.responsableBitacora.Controls.Add(this.campoApellidoBitacora);
            this.txtApellidoResponsable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.btnAplicarFiltros.Location = new System.Drawing.Point(798, 118);
            this.btnAplicarFiltros.Size = new System.Drawing.Size(182, 36);
            this.btnAplicarFiltros.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAplicarFiltros.Icon = ProyectoIS.UsuariosButtonIcon.Filter;
            this.btnAplicarFiltros.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnAplicarFiltros.FlatAppearance.BorderSize = 0;
            this.btnAplicarFiltros.BackColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnAplicarFiltros.ForeColor = System.Drawing.Color.White;
            this.filtrosBitacora.Controls.Add(this.btnAplicarFiltros);
            this.btnCancelarFiltros.Location = new System.Drawing.Point(992, 118);
            this.btnCancelarFiltros.Size = new System.Drawing.Size(122, 36);
            this.btnCancelarFiltros.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelarFiltros.Icon = ProyectoIS.UsuariosButtonIcon.None;
            this.btnCancelarFiltros.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnCancelarFiltros.FlatAppearance.BorderSize = 1;
            this.btnCancelarFiltros.BackColor = System.Drawing.Color.White;
            this.btnCancelarFiltros.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.filtrosBitacora.Controls.Add(this.btnCancelarFiltros);
            this.btnExportarPDF.Location = new System.Drawing.Point(956, 26);
            this.btnExportarPDF.Size = new System.Drawing.Size(200, 42);
            this.btnExportarPDF.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnExportarPDF.Icon = ProyectoIS.UsuariosButtonIcon.Document;
            this.btnExportarPDF.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnExportarPDF.FlatAppearance.BorderSize = 1;
            this.btnExportarPDF.BackColor = System.Drawing.Color.White;
            this.btnExportarPDF.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.Controls.Add(this.btnExportarPDF);
            this.btnSalir.Location = new System.Drawing.Point(1006, 666);
            this.btnSalir.Size = new System.Drawing.Size(150, 42);
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSalir.Icon = ProyectoIS.UsuariosButtonIcon.Arrow;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnSalir.FlatAppearance.BorderSize = 1;
            this.btnSalir.BackColor = System.Drawing.Color.White;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.Controls.Add(this.btnSalir);
            this.btnCancelarFiltros.Text = "Limpiar";
            this.lblResponsable.Location = new System.Drawing.Point(18, 12);
            this.lblResponsable.Size = new System.Drawing.Size(754, 24);
            this.lblResponsable.AutoSize = false;
            this.lblResponsable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.responsableBitacora.Controls.Add(this.lblResponsable);
            this.dgvEventos.Location = new System.Drawing.Point(12, 10);
            this.dgvEventos.Size = new System.Drawing.Size(1108, 288);
            this.tablaBitacora.Controls.Add(this.dgvEventos);
            this.dgvEventos.ReadOnly = true;
            this.dgvEventos.AllowUserToAddRows = false;
            this.dgvEventos.AllowUserToDeleteRows = false;
            this.dgvEventos.RowHeadersVisible = false;
            this.dgvEventos.MultiSelect = false;
            this.dgvEventos.EnableHeadersVisualStyles = false;
            this.dgvEventos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEventos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEventos.ColumnHeadersHeight = 42;
            this.dgvEventos.RowTemplate.Height = 38;
            this.Login.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Login.FillWeight = 16F;
            this.Login.MinimumWidth = 100;
            this.Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fecha.FillWeight = 23F;
            this.Fecha.MinimumWidth = 100;
            this.Módulo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Módulo.FillWeight = 23F;
            this.Módulo.MinimumWidth = 100;
            this.Evento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Evento.FillWeight = 28F;
            this.Evento.MinimumWidth = 100;
            this.Criticidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Criticidad.FillWeight = 10F;
            this.Criticidad.MinimumWidth = 90;
            this.dgvEventos.BackgroundColor = System.Drawing.Color.White;
            this.dgvEventos.GridColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.dgvEventos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            System.Windows.Forms.DataGridViewCellStyle cuerpoEstilo = new System.Windows.Forms.DataGridViewCellStyle();
            cuerpoEstilo.BackColor = System.Drawing.Color.White;
            cuerpoEstilo.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            cuerpoEstilo.Font = new System.Drawing.Font("Segoe UI", 10F);
            cuerpoEstilo.SelectionBackColor = System.Drawing.Color.FromArgb(229, 237, 253);
            cuerpoEstilo.SelectionForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.dgvEventos.DefaultCellStyle = cuerpoEstilo;
            System.Windows.Forms.DataGridViewCellStyle encabezadoEstilo = new System.Windows.Forms.DataGridViewCellStyle();
            encabezadoEstilo.BackColor = System.Drawing.Color.White;
            encabezadoEstilo.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            encabezadoEstilo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            encabezadoEstilo.SelectionBackColor = System.Drawing.Color.White;
            encabezadoEstilo.SelectionForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.dgvEventos.ColumnHeadersDefaultCellStyle = encabezadoEstilo;
            fechaEstilo.Format = "dd/MM/yyyy HH:mm:ss";
            this.Fecha.DefaultCellStyle = fechaEstilo;
            this.dgvEventos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.PintarCriticidadBitacora);
            this.Controls.Add(this.tituloBitacora);
            this.Controls.Add(this.subtituloBitacora);
            this.Controls.Add(this.iconoBitacora);
            this.Controls.Add(this.temaBitacora);
            this.Controls.Add(this.filtrosBitacora);
            this.Controls.Add(this.tablaBitacora);
            this.Controls.Add(this.responsableBitacora);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ClientSize = new System.Drawing.Size(1180, 740);
            this.MinimumSize = new System.Drawing.Size(960, 680);
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 249);
            this.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.Resize += new System.EventHandler(this.BitacoraEventos_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProyectoIS.UsuariosRoundedPanel campoLoginBitacora;
        private ProyectoIS.UsuariosRoundedPanel campoModuloBitacora;
        private ProyectoIS.UsuariosRoundedPanel campoCriticidadBitacora;
        private ProyectoIS.UsuariosRoundedPanel campoInicioBitacora;
        private ProyectoIS.UsuariosRoundedPanel campoFinBitacora;
        private ProyectoIS.UsuariosRoundedPanel campoNombreBitacora;
        private ProyectoIS.UsuariosRoundedPanel campoApellidoBitacora;
        private ProyectoIS.UsuariosRoundedPanel filtrosBitacora;
        private ProyectoIS.UsuariosRoundedPanel tablaBitacora;
        private ProyectoIS.UsuariosRoundedPanel responsableBitacora;
        private System.Windows.Forms.Label tituloBitacora;
        private System.Windows.Forms.Label subtituloBitacora;
        private System.Windows.Forms.Label tituloFiltrosBitacora;
        private ProyectoIS.UsuariosThemeSwitch temaBitacora;
        private ProyectoIS.BitacoraGlyph iconoBitacora;
        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.DateTimePicker fechaPickerInicio;
        private ProyectoIS.UsuariosRoundedButton btnAplicarFiltros;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Módulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Evento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Criticidad;
        private ProyectoIS.UsuariosRoundedButton btnCancelarFiltros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboCriticidad;
        private System.Windows.Forms.ComboBox comboMódulo;
        private ProyectoIS.UsuariosRoundedButton btnSalir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker fechaPickerFin;
        private System.Windows.Forms.Label lblResponsable;
        private System.Windows.Forms.Label lblNombreResp;
        private System.Windows.Forms.TextBox txtNombreResponsable;
        private System.Windows.Forms.Label lblApellidoResp;
        private System.Windows.Forms.TextBox txtApellidoResponsable;
        private ProyectoIS.UsuariosRoundedButton btnExportarPDF;
    }
}
