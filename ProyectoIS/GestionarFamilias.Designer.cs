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
            this.tabsGestionFamilias = new System.Windows.Forms.TabControl();
            this.tabCrearFamilia = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEliminarFamilia = new UsuariosRoundedButton();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCrear = new UsuariosRoundedButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.chklist = new System.Windows.Forms.CheckedListBox();
            this.listFamilias = new System.Windows.Forms.ListBox();
            this.tabPermisosFamilia = new System.Windows.Forms.TabPage();
            this.lblPermisosDeFamilia = new System.Windows.Forms.Label();
            this.lbPermisosDeFamilia = new System.Windows.Forms.ListBox();
            this.btnQuitarPermisoFamilia = new UsuariosRoundedButton();
            this.btnAgregarPermisoFamilia = new UsuariosRoundedButton();
            this.lblPermisosDisponiblesFam = new System.Windows.Forms.Label();
            this.clbPermisosDisponiblesFam = new System.Windows.Forms.CheckedListBox();
            this.lblFamiliasGestion = new System.Windows.Forms.Label();
            this.lbFamiliasGestion = new System.Windows.Forms.CheckedListBox();
            this.tabsGestionFamilias.SuspendLayout();
            this.tabCrearFamilia.SuspendLayout();
            this.tabPermisosFamilia.SuspendLayout();
            this.components = new System.ComponentModel.Container();
            this.animacionFamilias = new System.Windows.Forms.Timer(this.components);
            this.lateralFamilias = new ProyectoIS.UsuariosRoundedPanel();
            this.resumenFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.datosFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.campoNombreFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.campoDescripcionFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.disponiblesNuevaFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.vistaPreviaFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.disponiblesPermisosFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.asignadosPermisosFamilia = new ProyectoIS.UsuariosRoundedPanel();
            this.cuerpoFamilias = new System.Windows.Forms.Panel();
            this.contenidoFamilias = new System.Windows.Forms.Panel();
            this.paginaNuevaFamilia = new System.Windows.Forms.Panel();
            this.paginaPermisosFamilia = new System.Windows.Forms.Panel();
            this.tituloFamiliasVista = new System.Windows.Forms.Label();
            this.subtituloFamiliasVista = new System.Windows.Forms.Label();
            this.pieFamiliasVista = new System.Windows.Forms.Label();
            this.ayudaFamiliaVista = new System.Windows.Forms.Label();
            this.ayudaEliminarFamilias = new System.Windows.Forms.Label();
            this.nombreFamiliaVista = new System.Windows.Forms.Label();
            this.nuevaFamiliaVista = new ProyectoIS.UsuariosRoundedButton();
            this.tabNuevaFamiliaVista = new ProyectoIS.UsuariosRoundedButton();
            this.tabPermisosFamiliaVista = new ProyectoIS.UsuariosRoundedButton();
            this.temaFamilias = new ProyectoIS.UsuariosThemeSwitch();
            this.iconoFamilias = new ProyectoIS.AltaGlyph();
            this.SuspendLayout();
            // 
            // tabsGestionFamilias
            // 
            this.tabsGestionFamilias.Controls.Add(this.tabCrearFamilia);
            this.tabsGestionFamilias.Controls.Add(this.tabPermisosFamilia);
            this.tabsGestionFamilias.Location = new System.Drawing.Point(0, 0);
            this.tabsGestionFamilias.Name = "tabsGestionFamilias";
            this.tabsGestionFamilias.SelectedIndex = 0;
            this.tabsGestionFamilias.Size = new System.Drawing.Size(900, 560);
            this.tabsGestionFamilias.TabIndex = 0;
            // 
            // tabCrearFamilia
            // 
            this.tabCrearFamilia.Controls.Add(this.label4);
            this.tabCrearFamilia.Controls.Add(this.btnEliminarFamilia);
            this.tabCrearFamilia.Controls.Add(this.txtDesc);
            this.tabCrearFamilia.Controls.Add(this.label3);
            this.tabCrearFamilia.Controls.Add(this.label2);
            this.tabCrearFamilia.Controls.Add(this.btnCrear);
            this.tabCrearFamilia.Controls.Add(this.label1);
            this.tabCrearFamilia.Controls.Add(this.txtNombre);
            this.tabCrearFamilia.Controls.Add(this.chklist);
            this.tabCrearFamilia.Controls.Add(this.listFamilias);
            this.tabCrearFamilia.Location = new System.Drawing.Point(4, 22);
            this.tabCrearFamilia.Name = "tabCrearFamilia";
            this.tabCrearFamilia.Padding = new System.Windows.Forms.Padding(3);
            this.tabCrearFamilia.Size = new System.Drawing.Size(892, 534);
            this.tabCrearFamilia.TabIndex = 0;
            this.tabCrearFamilia.Text = "Administrar Familias";
            this.tabCrearFamilia.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Descripción de Familia";
            // 
            // btnEliminarFamilia
            // 
            this.btnEliminarFamilia.Location = new System.Drawing.Point(255, 295);
            this.btnEliminarFamilia.Name = "btnEliminarFamilia";
            this.btnEliminarFamilia.Size = new System.Drawing.Size(288, 123);
            this.btnEliminarFamilia.TabIndex = 10;
            this.btnEliminarFamilia.Text = "Eliminar Familia(s)";
            this.btnEliminarFamilia.UseVisualStyleBackColor = true;
            this.btnEliminarFamilia.Click += new System.EventHandler(this.btnEliminarFamilia_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(365, 128);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(178, 20);
            this.txtDesc.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Permisos Nueva Familia:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(592, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Familias y Permisos Existentes:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(255, 163);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(288, 118);
            this.btnCrear.TabIndex = 5;
            this.btnCrear.Text = "Crear Familia";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre de Familia";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(365, 102);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(178, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // chklist
            // 
            this.chklist.CheckOnClick = true;
            this.chklist.FormattingEnabled = true;
            this.chklist.Location = new System.Drawing.Point(595, 37);
            this.chklist.Name = "chklist";
            this.chklist.Size = new System.Drawing.Size(193, 424);
            this.chklist.TabIndex = 2;
            // 
            // listFamilias
            // 
            this.listFamilias.FormattingEnabled = true;
            this.listFamilias.Location = new System.Drawing.Point(12, 37);
            this.listFamilias.Name = "listFamilias";
            this.listFamilias.Size = new System.Drawing.Size(200, 420);
            this.listFamilias.TabIndex = 0;
            this.listFamilias.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listFamilias_MouseClick);
            // 
            // tabPermisosFamilia
            // 
            this.tabPermisosFamilia.Controls.Add(this.lblPermisosDeFamilia);
            this.tabPermisosFamilia.Controls.Add(this.lbPermisosDeFamilia);
            this.tabPermisosFamilia.Controls.Add(this.btnQuitarPermisoFamilia);
            this.tabPermisosFamilia.Controls.Add(this.btnAgregarPermisoFamilia);
            this.tabPermisosFamilia.Controls.Add(this.lblPermisosDisponiblesFam);
            this.tabPermisosFamilia.Controls.Add(this.clbPermisosDisponiblesFam);
            this.tabPermisosFamilia.Controls.Add(this.lblFamiliasGestion);
            this.tabPermisosFamilia.Controls.Add(this.lbFamiliasGestion);
            this.tabPermisosFamilia.Location = new System.Drawing.Point(4, 22);
            this.tabPermisosFamilia.Name = "tabPermisosFamilia";
            this.tabPermisosFamilia.Padding = new System.Windows.Forms.Padding(3);
            this.tabPermisosFamilia.Size = new System.Drawing.Size(892, 534);
            this.tabPermisosFamilia.TabIndex = 1;
            this.tabPermisosFamilia.Text = "Gestionar Permisos de Familia";
            this.tabPermisosFamilia.UseVisualStyleBackColor = true;
            // 
            // lblPermisosDeFamilia
            // 
            this.lblPermisosDeFamilia.AutoSize = true;
            this.lblPermisosDeFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermisosDeFamilia.Location = new System.Drawing.Point(591, 15);
            this.lblPermisosDeFamilia.Name = "lblPermisosDeFamilia";
            this.lblPermisosDeFamilia.Size = new System.Drawing.Size(147, 16);
            this.lblPermisosDeFamilia.TabIndex = 7;
            this.lblPermisosDeFamilia.Text = "Permisos de la Familia:";
            // 
            // lbPermisosDeFamilia
            // 
            this.lbPermisosDeFamilia.FormattingEnabled = true;
            this.lbPermisosDeFamilia.Location = new System.Drawing.Point(594, 35);
            this.lbPermisosDeFamilia.Name = "lbPermisosDeFamilia";
            this.lbPermisosDeFamilia.Size = new System.Drawing.Size(220, 459);
            this.lbPermisosDeFamilia.TabIndex = 6;
            // 
            // btnQuitarPermisoFamilia
            // 
            this.btnQuitarPermisoFamilia.Location = new System.Drawing.Point(450, 260);
            this.btnQuitarPermisoFamilia.Name = "btnQuitarPermisoFamilia";
            this.btnQuitarPermisoFamilia.Size = new System.Drawing.Size(130, 40);
            this.btnQuitarPermisoFamilia.TabIndex = 5;
            this.btnQuitarPermisoFamilia.Text = "<< Quitar";
            this.btnQuitarPermisoFamilia.UseVisualStyleBackColor = true;
            this.btnQuitarPermisoFamilia.Click += new System.EventHandler(this.btnQuitarPermisoFamilia_Click);
            // 
            // btnAgregarPermisoFamilia
            // 
            this.btnAgregarPermisoFamilia.Location = new System.Drawing.Point(450, 210);
            this.btnAgregarPermisoFamilia.Name = "btnAgregarPermisoFamilia";
            this.btnAgregarPermisoFamilia.Size = new System.Drawing.Size(130, 40);
            this.btnAgregarPermisoFamilia.TabIndex = 4;
            this.btnAgregarPermisoFamilia.Text = "Agregar >>";
            this.btnAgregarPermisoFamilia.UseVisualStyleBackColor = true;
            this.btnAgregarPermisoFamilia.Click += new System.EventHandler(this.btnAgregarPermisoFamilia_Click);
            // 
            // lblPermisosDisponiblesFam
            // 
            this.lblPermisosDisponiblesFam.AutoSize = true;
            this.lblPermisosDisponiblesFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermisosDisponiblesFam.Location = new System.Drawing.Point(213, 15);
            this.lblPermisosDisponiblesFam.Name = "lblPermisosDisponiblesFam";
            this.lblPermisosDisponiblesFam.Size = new System.Drawing.Size(142, 16);
            this.lblPermisosDisponiblesFam.TabIndex = 3;
            this.lblPermisosDisponiblesFam.Text = "Permisos Disponibles:";
            // 
            // clbPermisosDisponiblesFam
            // 
            this.clbPermisosDisponiblesFam.CheckOnClick = true;
            this.clbPermisosDisponiblesFam.FormattingEnabled = true;
            this.clbPermisosDisponiblesFam.Location = new System.Drawing.Point(216, 35);
            this.clbPermisosDisponiblesFam.Name = "clbPermisosDisponiblesFam";
            this.clbPermisosDisponiblesFam.Size = new System.Drawing.Size(220, 454);
            this.clbPermisosDisponiblesFam.TabIndex = 2;
            // 
            // lblFamiliasGestion
            // 
            this.lblFamiliasGestion.AutoSize = true;
            this.lblFamiliasGestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFamiliasGestion.Location = new System.Drawing.Point(9, 15);
            this.lblFamiliasGestion.Name = "lblFamiliasGestion";
            this.lblFamiliasGestion.Size = new System.Drawing.Size(125, 16);
            this.lblFamiliasGestion.TabIndex = 1;
            this.lblFamiliasGestion.Text = "Familias Existentes:";
            // 
            // lbFamiliasGestion
            // 
            this.lbFamiliasGestion.FormattingEnabled = true;
            this.lbFamiliasGestion.Location = new System.Drawing.Point(12, 35);
            this.lbFamiliasGestion.Name = "lbFamiliasGestion";
            this.lbFamiliasGestion.Size = new System.Drawing.Size(190, 459);
            this.lbFamiliasGestion.TabIndex = 0;
            this.lbFamiliasGestion.SelectedIndexChanged += new System.EventHandler(this.lbFamiliasGestion_SelectedIndexChanged);
            // 
            // GestionarFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.tabsGestionFamilias);
            this.Name = "GestionarFamilias";
            this.Text = "GestionarFamilias";
            this.Load += new System.EventHandler(this.GestionarFamilias_Load);
            this.tabsGestionFamilias.ResumeLayout(false);
            this.tabCrearFamilia.ResumeLayout(false);
            this.tabCrearFamilia.PerformLayout();
            this.tabPermisosFamilia.ResumeLayout(false);
            this.tabPermisosFamilia.PerformLayout();
            this.lateralFamilias.Name = "lateralFamilias";
            this.resumenFamilia.Name = "resumenFamilia";
            this.datosFamilia.Name = "datosFamilia";
            this.campoNombreFamilia.Name = "campoNombreFamilia";
            this.campoDescripcionFamilia.Name = "campoDescripcionFamilia";
            this.disponiblesNuevaFamilia.Name = "disponiblesNuevaFamilia";
            this.vistaPreviaFamilia.Name = "vistaPreviaFamilia";
            this.disponiblesPermisosFamilia.Name = "disponiblesPermisosFamilia";
            this.asignadosPermisosFamilia.Name = "asignadosPermisosFamilia";
            this.cuerpoFamilias.Name = "cuerpoFamilias";
            this.contenidoFamilias.Name = "contenidoFamilias";
            this.paginaNuevaFamilia.Name = "paginaNuevaFamilia";
            this.paginaPermisosFamilia.Name = "paginaPermisosFamilia";
            this.tituloFamiliasVista.Name = "tituloFamiliasVista";
            this.subtituloFamiliasVista.Name = "subtituloFamiliasVista";
            this.pieFamiliasVista.Name = "pieFamiliasVista";
            this.ayudaFamiliaVista.Name = "ayudaFamiliaVista";
            this.ayudaEliminarFamilias.Name = "ayudaEliminarFamilias";
            this.nombreFamiliaVista.Name = "nombreFamiliaVista";
            this.nuevaFamiliaVista.Name = "nuevaFamiliaVista";
            this.tabNuevaFamiliaVista.Name = "tabNuevaFamiliaVista";
            this.tabPermisosFamiliaVista.Name = "tabPermisosFamiliaVista";
            this.temaFamilias.Name = "temaFamilias";
            this.iconoFamilias.Name = "iconoFamilias";
            this.lateralFamilias.CornerRadius = 16;
            this.lateralFamilias.BackColor = System.Drawing.Color.White;
            this.lateralFamilias.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.resumenFamilia.CornerRadius = 16;
            this.resumenFamilia.BackColor = System.Drawing.Color.White;
            this.resumenFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.datosFamilia.CornerRadius = 16;
            this.datosFamilia.BackColor = System.Drawing.Color.White;
            this.datosFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.campoNombreFamilia.CornerRadius = 16;
            this.campoNombreFamilia.BackColor = System.Drawing.Color.White;
            this.campoNombreFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.campoDescripcionFamilia.CornerRadius = 16;
            this.campoDescripcionFamilia.BackColor = System.Drawing.Color.White;
            this.campoDescripcionFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.disponiblesNuevaFamilia.CornerRadius = 16;
            this.disponiblesNuevaFamilia.BackColor = System.Drawing.Color.White;
            this.disponiblesNuevaFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.vistaPreviaFamilia.CornerRadius = 16;
            this.vistaPreviaFamilia.BackColor = System.Drawing.Color.White;
            this.vistaPreviaFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.disponiblesPermisosFamilia.CornerRadius = 16;
            this.disponiblesPermisosFamilia.BackColor = System.Drawing.Color.White;
            this.disponiblesPermisosFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.asignadosPermisosFamilia.CornerRadius = 16;
            this.asignadosPermisosFamilia.BackColor = System.Drawing.Color.White;
            this.asignadosPermisosFamilia.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.tituloFamiliasVista.Text = "Familias";
            this.subtituloFamiliasVista.Text = "Organizá los permisos en grupos reutilizables";
            this.pieFamiliasVista.Text = "Las acciones disponibles dependen de tus permisos.";
            this.ayudaFamiliaVista.Text = "Seleccioná los permisos que querés agregar o quitar.";
            this.ayudaEliminarFamilias.Text = "Marcá las casillas de las familias que quieras eliminar.";
            this.nuevaFamiliaVista.Text = "Crear familia";
            this.tabNuevaFamiliaVista.Text = "Nueva familia";
            this.tabPermisosFamiliaVista.Text = "Permisos";
            this.tituloFamiliasVista.Font = new System.Drawing.Font("Segoe UI", 23F, System.Drawing.FontStyle.Bold);
            this.tituloFamiliasVista.AutoEllipsis = true;
            this.tituloFamiliasVista.BackColor = System.Drawing.Color.Transparent;
            this.subtituloFamiliasVista.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtituloFamiliasVista.AutoEllipsis = true;
            this.subtituloFamiliasVista.BackColor = System.Drawing.Color.Transparent;
            this.pieFamiliasVista.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pieFamiliasVista.AutoEllipsis = true;
            this.pieFamiliasVista.BackColor = System.Drawing.Color.Transparent;
            this.ayudaFamiliaVista.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ayudaFamiliaVista.AutoEllipsis = true;
            this.ayudaFamiliaVista.BackColor = System.Drawing.Color.Transparent;
            this.ayudaEliminarFamilias.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ayudaEliminarFamilias.AutoEllipsis = true;
            this.ayudaEliminarFamilias.BackColor = System.Drawing.Color.Transparent;
            this.nombreFamiliaVista.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.nombreFamiliaVista.AutoEllipsis = true;
            this.nombreFamiliaVista.BackColor = System.Drawing.Color.Transparent;
            this.nombreFamiliaVista.Text = "Seleccioná una familia";
            this.tituloFamiliasVista.Location = new System.Drawing.Point(82, 18);
            this.tituloFamiliasVista.Size = new System.Drawing.Size(650, 42);
            this.subtituloFamiliasVista.Location = new System.Drawing.Point(84, 64);
            this.subtituloFamiliasVista.Size = new System.Drawing.Size(800, 28);
            this.pieFamiliasVista.Location = new System.Drawing.Point(28, 706);
            this.pieFamiliasVista.Size = new System.Drawing.Size(1084, 24);
            this.iconoFamilias.Location = new System.Drawing.Point(27, 26);
            this.iconoFamilias.Size = new System.Drawing.Size(40, 40);
            this.temaFamilias.Location = new System.Drawing.Point(1018, 27);
            this.temaFamilias.Size = new System.Drawing.Size(96, 38);
            this.cuerpoFamilias.Location = new System.Drawing.Point(24, 108);
            this.cuerpoFamilias.Size = new System.Drawing.Size(1092, 585);
            this.lateralFamilias.Location = new System.Drawing.Point(0, 0);
            this.lateralFamilias.Size = new System.Drawing.Size(262, 585);
            this.contenidoFamilias.Location = new System.Drawing.Point(280, 0);
            this.contenidoFamilias.Size = new System.Drawing.Size(812, 585);
            this.iconoFamilias.Kind = 3;
            this.iconoFamilias.BackColor = System.Drawing.Color.Transparent;
            this.iconoFamilias.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.temaFamilias.Click += new System.EventHandler(this.temaFamilias_Click);
            this.cuerpoFamilias.Controls.Add(this.lateralFamilias);
            this.cuerpoFamilias.Controls.Add(this.contenidoFamilias);
            this.lblFamiliasGestion.Location = new System.Drawing.Point(20, 18);
            this.lblFamiliasGestion.Size = new System.Drawing.Size(222, 28);
            this.lateralFamilias.Controls.Add(this.lblFamiliasGestion);
            this.lbFamiliasGestion.Location = new System.Drawing.Point(18, 60);
            this.lbFamiliasGestion.Size = new System.Drawing.Size(226, 337);
            this.lateralFamilias.Controls.Add(this.lbFamiliasGestion);
            this.ayudaEliminarFamilias.Location = new System.Drawing.Point(20, 407);
            this.ayudaEliminarFamilias.Size = new System.Drawing.Size(222, 52);
            this.lateralFamilias.Controls.Add(this.ayudaEliminarFamilias);
            this.nuevaFamiliaVista.Location = new System.Drawing.Point(18, 469);
            this.nuevaFamiliaVista.Size = new System.Drawing.Size(226, 42);
            this.lateralFamilias.Controls.Add(this.nuevaFamiliaVista);
            this.btnEliminarFamilia.Location = new System.Drawing.Point(18, 523);
            this.btnEliminarFamilia.Size = new System.Drawing.Size(226, 42);
            this.lateralFamilias.Controls.Add(this.btnEliminarFamilia);
            this.nuevaFamiliaVista.Click += new System.EventHandler(this.nuevaFamiliaVista_Click);
            this.lbFamiliasGestion.CheckOnClick = false;
            this.lbFamiliasGestion.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbFamiliasGestion_ItemCheck);
            this.tabNuevaFamiliaVista.Location = new System.Drawing.Point(0, 0);
            this.tabNuevaFamiliaVista.Size = new System.Drawing.Size(172, 40);
            this.tabNuevaFamiliaVista.Click += new System.EventHandler(this.tabNuevaFamiliaVista_Click);
            this.contenidoFamilias.Controls.Add(this.tabNuevaFamiliaVista);
            this.tabPermisosFamiliaVista.Location = new System.Drawing.Point(180, 0);
            this.tabPermisosFamiliaVista.Size = new System.Drawing.Size(172, 40);
            this.tabPermisosFamiliaVista.Click += new System.EventHandler(this.tabPermisosFamiliaVista_Click);
            this.contenidoFamilias.Controls.Add(this.tabPermisosFamiliaVista);
            this.paginaNuevaFamilia.Location = new System.Drawing.Point(0, 54);
            this.paginaNuevaFamilia.Size = new System.Drawing.Size(812, 531);
            this.paginaNuevaFamilia.Visible = false;
            this.contenidoFamilias.Controls.Add(this.paginaNuevaFamilia);
            this.paginaPermisosFamilia.Location = new System.Drawing.Point(0, 54);
            this.paginaPermisosFamilia.Size = new System.Drawing.Size(812, 531);
            this.paginaPermisosFamilia.Visible = true;
            this.contenidoFamilias.Controls.Add(this.paginaPermisosFamilia);
            this.datosFamilia.Location = new System.Drawing.Point(0, 0);
            this.datosFamilia.Size = new System.Drawing.Size(812, 166);
            this.paginaNuevaFamilia.Controls.Add(this.datosFamilia);
            this.label1.Location = new System.Drawing.Point(20, 14);
            this.label1.Size = new System.Drawing.Size(772, 24);
            this.campoNombreFamilia.Location = new System.Drawing.Point(18, 40);
            this.campoNombreFamilia.Size = new System.Drawing.Size(776, 42);
            this.txtNombre.Location = new System.Drawing.Point(12, 10);
            this.txtNombre.Size = new System.Drawing.Size(748, 24);
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.campoNombreFamilia.Controls.Add(this.txtNombre);
            this.datosFamilia.Controls.Add(this.label1);
            this.datosFamilia.Controls.Add(this.campoNombreFamilia);
            this.label4.Location = new System.Drawing.Point(20, 87);
            this.label4.Size = new System.Drawing.Size(772, 24);
            this.campoDescripcionFamilia.Location = new System.Drawing.Point(18, 113);
            this.campoDescripcionFamilia.Size = new System.Drawing.Size(776, 42);
            this.txtDesc.Location = new System.Drawing.Point(12, 10);
            this.txtDesc.Size = new System.Drawing.Size(748, 24);
            this.txtDesc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.campoDescripcionFamilia.Controls.Add(this.txtDesc);
            this.datosFamilia.Controls.Add(this.label4);
            this.datosFamilia.Controls.Add(this.campoDescripcionFamilia);
            this.disponiblesNuevaFamilia.Location = new System.Drawing.Point(0, 180);
            this.disponiblesNuevaFamilia.Size = new System.Drawing.Size(399, 293);
            this.label2.Location = new System.Drawing.Point(16, 15);
            this.label2.Size = new System.Drawing.Size(367, 44);
            this.chklist.Location = new System.Drawing.Point(18, 64);
            this.chklist.Size = new System.Drawing.Size(363, 209);
            this.disponiblesNuevaFamilia.Controls.Add(this.label2);
            this.disponiblesNuevaFamilia.Controls.Add(this.chklist);
            this.paginaNuevaFamilia.Controls.Add(this.disponiblesNuevaFamilia);
            this.vistaPreviaFamilia.Location = new System.Drawing.Point(413, 180);
            this.vistaPreviaFamilia.Size = new System.Drawing.Size(399, 293);
            this.label3.Location = new System.Drawing.Point(16, 15);
            this.label3.Size = new System.Drawing.Size(367, 44);
            this.listFamilias.Location = new System.Drawing.Point(18, 64);
            this.listFamilias.Size = new System.Drawing.Size(363, 209);
            this.vistaPreviaFamilia.Controls.Add(this.label3);
            this.vistaPreviaFamilia.Controls.Add(this.listFamilias);
            this.paginaNuevaFamilia.Controls.Add(this.vistaPreviaFamilia);
            this.btnCrear.Location = new System.Drawing.Point(602, 485);
            this.btnCrear.Size = new System.Drawing.Size(206, 42);
            this.paginaNuevaFamilia.Controls.Add(this.btnCrear);
            this.resumenFamilia.Location = new System.Drawing.Point(0, 0);
            this.resumenFamilia.Size = new System.Drawing.Size(812, 90);
            this.nombreFamiliaVista.Location = new System.Drawing.Point(22, 14);
            this.nombreFamiliaVista.Size = new System.Drawing.Size(768, 30);
            this.ayudaFamiliaVista.Location = new System.Drawing.Point(22, 49);
            this.ayudaFamiliaVista.Size = new System.Drawing.Size(768, 28);
            this.resumenFamilia.Controls.Add(this.nombreFamiliaVista);
            this.resumenFamilia.Controls.Add(this.ayudaFamiliaVista);
            this.paginaPermisosFamilia.Controls.Add(this.resumenFamilia);
            this.disponiblesPermisosFamilia.Location = new System.Drawing.Point(0, 106);
            this.disponiblesPermisosFamilia.Size = new System.Drawing.Size(332, 425);
            this.lblPermisosDisponiblesFam.Location = new System.Drawing.Point(16, 18);
            this.lblPermisosDisponiblesFam.Size = new System.Drawing.Size(300, 44);
            this.clbPermisosDisponiblesFam.Location = new System.Drawing.Point(18, 68);
            this.clbPermisosDisponiblesFam.Size = new System.Drawing.Size(296, 335);
            this.disponiblesPermisosFamilia.Controls.Add(this.lblPermisosDisponiblesFam);
            this.disponiblesPermisosFamilia.Controls.Add(this.clbPermisosDisponiblesFam);
            this.paginaPermisosFamilia.Controls.Add(this.disponiblesPermisosFamilia);
            this.asignadosPermisosFamilia.Location = new System.Drawing.Point(480, 106);
            this.asignadosPermisosFamilia.Size = new System.Drawing.Size(332, 425);
            this.lblPermisosDeFamilia.Location = new System.Drawing.Point(16, 18);
            this.lblPermisosDeFamilia.Size = new System.Drawing.Size(300, 44);
            this.lbPermisosDeFamilia.Location = new System.Drawing.Point(18, 68);
            this.lbPermisosDeFamilia.Size = new System.Drawing.Size(296, 335);
            this.asignadosPermisosFamilia.Controls.Add(this.lblPermisosDeFamilia);
            this.asignadosPermisosFamilia.Controls.Add(this.lbPermisosDeFamilia);
            this.paginaPermisosFamilia.Controls.Add(this.asignadosPermisosFamilia);
            this.btnAgregarPermisoFamilia.Location = new System.Drawing.Point(344, 273);
            this.btnAgregarPermisoFamilia.Size = new System.Drawing.Size(124, 40);
            this.paginaPermisosFamilia.Controls.Add(this.btnAgregarPermisoFamilia);
            this.btnQuitarPermisoFamilia.Location = new System.Drawing.Point(344, 323);
            this.btnQuitarPermisoFamilia.Size = new System.Drawing.Size(124, 40);
            this.paginaPermisosFamilia.Controls.Add(this.btnQuitarPermisoFamilia);
            this.lbFamiliasGestion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lbFamiliasGestion.IntegralHeight = false;
            this.lbFamiliasGestion.HorizontalScrollbar = true;
            this.lbFamiliasGestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbFamiliasGestion.BackColor = System.Drawing.Color.White;
            this.lbFamiliasGestion.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.chklist.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.chklist.IntegralHeight = false;
            this.chklist.HorizontalScrollbar = true;
            this.chklist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklist.BackColor = System.Drawing.Color.White;
            this.chklist.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.listFamilias.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.listFamilias.IntegralHeight = false;
            this.listFamilias.HorizontalScrollbar = true;
            this.listFamilias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listFamilias.BackColor = System.Drawing.Color.White;
            this.listFamilias.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.clbPermisosDisponiblesFam.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.clbPermisosDisponiblesFam.IntegralHeight = false;
            this.clbPermisosDisponiblesFam.HorizontalScrollbar = true;
            this.clbPermisosDisponiblesFam.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbPermisosDisponiblesFam.BackColor = System.Drawing.Color.White;
            this.clbPermisosDisponiblesFam.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.lbPermisosDeFamilia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lbPermisosDeFamilia.IntegralHeight = false;
            this.lbPermisosDeFamilia.HorizontalScrollbar = true;
            this.lbPermisosDeFamilia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPermisosDeFamilia.BackColor = System.Drawing.Color.White;
            this.lbPermisosDeFamilia.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.listFamilias.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listFamilias.ItemHeight = 32;
            this.listFamilias.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listaFamilias_DrawItem);
            this.lbPermisosDeFamilia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPermisosDeFamilia.ItemHeight = 32;
            this.lbPermisosDeFamilia.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listaFamilias_DrawItem);
            this.chklist.CheckOnClick = true;
            this.clbPermisosDisponiblesFam.CheckOnClick = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.AutoSize = false;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.AutoSize = false;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.AutoSize = false;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.AutoSize = false;
            this.lblFamiliasGestion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFamiliasGestion.AutoSize = false;
            this.lblPermisosDisponiblesFam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPermisosDisponiblesFam.AutoSize = false;
            this.lblPermisosDeFamilia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPermisosDeFamilia.AutoSize = false;
            this.btnCrear.Icon = ProyectoIS.UsuariosButtonIcon.Check;
            this.btnCrear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCrear.FlatAppearance.BorderSize = 0;
            this.btnCrear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnCrear.BackColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnCrear.ForeColor = System.Drawing.Color.White;
            this.btnEliminarFamilia.Icon = ProyectoIS.UsuariosButtonIcon.Delete;
            this.btnEliminarFamilia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnEliminarFamilia.FlatAppearance.BorderSize = 1;
            this.btnEliminarFamilia.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnEliminarFamilia.BackColor = System.Drawing.Color.White;
            this.btnEliminarFamilia.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.nuevaFamiliaVista.Icon = ProyectoIS.UsuariosButtonIcon.UserAdd;
            this.nuevaFamiliaVista.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nuevaFamiliaVista.FlatAppearance.BorderSize = 0;
            this.nuevaFamiliaVista.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.nuevaFamiliaVista.BackColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.nuevaFamiliaVista.ForeColor = System.Drawing.Color.White;
            this.btnAgregarPermisoFamilia.Icon = ProyectoIS.UsuariosButtonIcon.Arrow;
            this.btnAgregarPermisoFamilia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAgregarPermisoFamilia.FlatAppearance.BorderSize = 1;
            this.btnAgregarPermisoFamilia.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnAgregarPermisoFamilia.BackColor = System.Drawing.Color.White;
            this.btnAgregarPermisoFamilia.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnQuitarPermisoFamilia.Icon = ProyectoIS.UsuariosButtonIcon.Delete;
            this.btnQuitarPermisoFamilia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuitarPermisoFamilia.FlatAppearance.BorderSize = 1;
            this.btnQuitarPermisoFamilia.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnQuitarPermisoFamilia.BackColor = System.Drawing.Color.White;
            this.btnQuitarPermisoFamilia.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.tabNuevaFamiliaVista.Icon = ProyectoIS.UsuariosButtonIcon.None;
            this.tabNuevaFamiliaVista.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabNuevaFamiliaVista.FlatAppearance.BorderSize = 1;
            this.tabNuevaFamiliaVista.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.tabNuevaFamiliaVista.BackColor = System.Drawing.Color.White;
            this.tabNuevaFamiliaVista.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.tabPermisosFamiliaVista.Icon = ProyectoIS.UsuariosButtonIcon.None;
            this.tabPermisosFamiliaVista.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabPermisosFamiliaVista.FlatAppearance.BorderSize = 0;
            this.tabPermisosFamiliaVista.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.tabPermisosFamiliaVista.BackColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.tabPermisosFamiliaVista.ForeColor = System.Drawing.Color.White;
            this.btnAgregarPermisoFamilia.Text = "Agregar";
            this.btnQuitarPermisoFamilia.Text = "Quitar";
            this.tabsGestionFamilias.Visible = false;
            this.tabsGestionFamilias.TabStop = false;
            this.tabsGestionFamilias.Location = new System.Drawing.Point(-2000, -2000);
            this.tabCrearFamilia.Name = "estructuraCrearFamilia";
            this.tabPermisosFamilia.Name = "estructuraPermisosFamilia";
            this.Controls.Add(this.iconoFamilias);
            this.Controls.Add(this.tituloFamiliasVista);
            this.Controls.Add(this.subtituloFamiliasVista);
            this.Controls.Add(this.temaFamilias);
            this.Controls.Add(this.cuerpoFamilias);
            this.Controls.Add(this.pieFamiliasVista);
            this.animacionFamilias.Interval = 15;
            this.animacionFamilias.Tick += new System.EventHandler(this.animacionFamilias_Tick);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ClientSize = new System.Drawing.Size(1140, 740);
            this.MinimumSize = new System.Drawing.Size(960, 700);
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 249);
            this.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.Resize += new System.EventHandler(this.GestionarFamilias_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabsGestionFamilias;
        private System.Windows.Forms.TabPage tabCrearFamilia;
        private System.Windows.Forms.ListBox listFamilias;
        private System.Windows.Forms.CheckedListBox chklist;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private ProyectoIS.UsuariosRoundedButton btnCrear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDesc;
        private ProyectoIS.UsuariosRoundedButton btnEliminarFamilia;
        private System.Windows.Forms.TabPage tabPermisosFamilia;
        private System.Windows.Forms.CheckedListBox lbFamiliasGestion;
        private System.Windows.Forms.Label lblFamiliasGestion;
        private System.Windows.Forms.CheckedListBox clbPermisosDisponiblesFam;
        private System.Windows.Forms.Label lblPermisosDisponiblesFam;
        private ProyectoIS.UsuariosRoundedButton btnAgregarPermisoFamilia;
        private ProyectoIS.UsuariosRoundedButton btnQuitarPermisoFamilia;
        private System.Windows.Forms.ListBox lbPermisosDeFamilia;
        private System.Windows.Forms.Label lblPermisosDeFamilia;
        private System.Windows.Forms.Timer animacionFamilias;
        private ProyectoIS.UsuariosRoundedPanel lateralFamilias;
        private ProyectoIS.UsuariosRoundedPanel resumenFamilia;
        private ProyectoIS.UsuariosRoundedPanel datosFamilia;
        private ProyectoIS.UsuariosRoundedPanel campoNombreFamilia;
        private ProyectoIS.UsuariosRoundedPanel campoDescripcionFamilia;
        private ProyectoIS.UsuariosRoundedPanel disponiblesNuevaFamilia;
        private ProyectoIS.UsuariosRoundedPanel vistaPreviaFamilia;
        private ProyectoIS.UsuariosRoundedPanel disponiblesPermisosFamilia;
        private ProyectoIS.UsuariosRoundedPanel asignadosPermisosFamilia;
        private System.Windows.Forms.Panel cuerpoFamilias;
        private System.Windows.Forms.Panel contenidoFamilias;
        private System.Windows.Forms.Panel paginaNuevaFamilia;
        private System.Windows.Forms.Panel paginaPermisosFamilia;
        private System.Windows.Forms.Label tituloFamiliasVista;
        private System.Windows.Forms.Label subtituloFamiliasVista;
        private System.Windows.Forms.Label pieFamiliasVista;
        private System.Windows.Forms.Label ayudaFamiliaVista;
        private System.Windows.Forms.Label ayudaEliminarFamilias;
        private System.Windows.Forms.Label nombreFamiliaVista;
        private ProyectoIS.UsuariosRoundedButton nuevaFamiliaVista;
        private ProyectoIS.UsuariosRoundedButton tabNuevaFamiliaVista;
        private ProyectoIS.UsuariosRoundedButton tabPermisosFamiliaVista;
        private ProyectoIS.UsuariosThemeSwitch temaFamilias;
        private ProyectoIS.AltaGlyph iconoFamilias;
    }
}
