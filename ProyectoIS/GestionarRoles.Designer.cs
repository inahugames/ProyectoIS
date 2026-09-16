namespace ProyectoIS
{
    partial class GestionarRoles
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
            this.GestionRoles = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnEliminarRol = new UsuariosRoundedButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCrearRol = new UsuariosRoundedButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombreNuevoRol = new System.Windows.Forms.TextBox();
            this.clbFamiliasYPermisos = new System.Windows.Forms.CheckedListBox();
            this.lbRolesExistentes = new System.Windows.Forms.ListBox();
            this.tabFamiliasRol = new System.Windows.Forms.TabPage();
            this.lblFamiliasDelRol = new System.Windows.Forms.Label();
            this.lbFamiliasDelRol = new System.Windows.Forms.ListBox();
            this.btnQuitarFamiliaRol = new UsuariosRoundedButton();
            this.btnAgregarFamiliaRol = new UsuariosRoundedButton();
            this.lblFamiliasDisponiblesRol = new System.Windows.Forms.Label();
            this.clbFamiliasDisponiblesRol = new System.Windows.Forms.CheckedListBox();
            this.lblRolesFamTab = new System.Windows.Forms.Label();
            this.lbRolesFamTab = new System.Windows.Forms.ListBox();
            this.tabPermisosRol = new System.Windows.Forms.TabPage();
            this.lblPermisosEfectivosRol = new System.Windows.Forms.Label();
            this.lbPermisosEfectivosRol = new System.Windows.Forms.ListBox();
            this.lblPermisosDirectosDelRol = new System.Windows.Forms.Label();
            this.lbPermisosDirectosDelRol = new System.Windows.Forms.ListBox();
            this.btnQuitarPermisoRol = new UsuariosRoundedButton();
            this.btnAgregarPermisoRol = new UsuariosRoundedButton();
            this.lblPermisosDisponiblesRol = new System.Windows.Forms.Label();
            this.clbPermisosDisponiblesRol = new System.Windows.Forms.CheckedListBox();
            this.lblRolesPermTab = new System.Windows.Forms.Label();
            this.lbRolesPermTab = new System.Windows.Forms.ListBox();
            this.GestionRoles.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabFamiliasRol.SuspendLayout();
            this.tabPermisosRol.SuspendLayout();
            this.components = new System.ComponentModel.Container();
            this.animacionRoles = new System.Windows.Forms.Timer(this.components);
            this.lateralRoles = new ProyectoIS.UsuariosRoundedPanel();
            this.generalRolesTarjeta = new ProyectoIS.UsuariosRoundedPanel();
            this.campoNombreRol = new ProyectoIS.UsuariosRoundedPanel();
            this.familiasDisponiblesTarjeta = new ProyectoIS.UsuariosRoundedPanel();
            this.familiasAsignadasTarjeta = new ProyectoIS.UsuariosRoundedPanel();
            this.permisosDisponiblesTarjeta = new ProyectoIS.UsuariosRoundedPanel();
            this.permisosAsignadosTarjeta = new ProyectoIS.UsuariosRoundedPanel();
            this.efectivosRolesTarjeta = new ProyectoIS.UsuariosRoundedPanel();
            this.cuerpoRoles = new System.Windows.Forms.Panel();
            this.contenidoRoles = new System.Windows.Forms.Panel();
            this.paginaRoles0 = new System.Windows.Forms.Panel();
            this.paginaRoles1 = new System.Windows.Forms.Panel();
            this.paginaRoles2 = new System.Windows.Forms.Panel();
            this.tituloRolesVista = new System.Windows.Forms.Label();
            this.subtituloRolesVista = new System.Windows.Forms.Label();
            this.pieRolesVista = new System.Windows.Forms.Label();
            this.ayudaEfectivosRoles = new System.Windows.Forms.Label();
            this.nuevoRolVista = new ProyectoIS.UsuariosRoundedButton();
            this.generalRoles = new ProyectoIS.UsuariosRoundedButton();
            this.familiasRoles = new ProyectoIS.UsuariosRoundedButton();
            this.permisosRoles = new ProyectoIS.UsuariosRoundedButton();
            this.temaRoles = new ProyectoIS.UsuariosThemeSwitch();
            this.iconoRoles = new ProyectoIS.AltaGlyph();
            this.SuspendLayout();
            // 
            // GestionRoles
            // 
            this.GestionRoles.Controls.Add(this.tabPage1);
            this.GestionRoles.Controls.Add(this.tabFamiliasRol);
            this.GestionRoles.Controls.Add(this.tabPermisosRol);
            this.GestionRoles.Location = new System.Drawing.Point(0, 0);
            this.GestionRoles.Name = "GestionRoles";
            this.GestionRoles.SelectedIndex = 0;
            this.GestionRoles.Size = new System.Drawing.Size(1000, 580);
            this.GestionRoles.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnEliminarRol);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnCrearRol);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtNombreNuevoRol);
            this.tabPage1.Controls.Add(this.clbFamiliasYPermisos);
            this.tabPage1.Controls.Add(this.lbRolesExistentes);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(992, 554);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gestión de Roles";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnEliminarRol
            // 
            this.btnEliminarRol.Location = new System.Drawing.Point(249, 310);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(288, 118);
            this.btnEliminarRol.TabIndex = 15;
            this.btnEliminarRol.Text = "Eliminar Rol";
            this.btnEliminarRol.UseVisualStyleBackColor = true;
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Roles Existentes:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(586, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Familias y Permisos Existentes:";
            // 
            // btnCrearRol
            // 
            this.btnCrearRol.Location = new System.Drawing.Point(249, 174);
            this.btnCrearRol.Name = "btnCrearRol";
            this.btnCrearRol.Size = new System.Drawing.Size(288, 118);
            this.btnCrearRol.TabIndex = 12;
            this.btnCrearRol.Text = "Crear Rol";
            this.btnCrearRol.UseVisualStyleBackColor = true;
            this.btnCrearRol.Click += new System.EventHandler(this.btnCrearRol_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Nombre de Rol";
            // 
            // txtNombreNuevoRol
            // 
            this.txtNombreNuevoRol.Location = new System.Drawing.Point(359, 113);
            this.txtNombreNuevoRol.Name = "txtNombreNuevoRol";
            this.txtNombreNuevoRol.Size = new System.Drawing.Size(178, 20);
            this.txtNombreNuevoRol.TabIndex = 10;
            // 
            // clbFamiliasYPermisos
            // 
            this.clbFamiliasYPermisos.CheckOnClick = true;
            this.clbFamiliasYPermisos.FormattingEnabled = true;
            this.clbFamiliasYPermisos.Location = new System.Drawing.Point(589, 48);
            this.clbFamiliasYPermisos.Name = "clbFamiliasYPermisos";
            this.clbFamiliasYPermisos.Size = new System.Drawing.Size(193, 424);
            this.clbFamiliasYPermisos.TabIndex = 9;
            // 
            // lbRolesExistentes
            // 
            this.lbRolesExistentes.FormattingEnabled = true;
            this.lbRolesExistentes.Location = new System.Drawing.Point(6, 48);
            this.lbRolesExistentes.Name = "lbRolesExistentes";
            this.lbRolesExistentes.Size = new System.Drawing.Size(200, 420);
            this.lbRolesExistentes.TabIndex = 8;
            //
            // tabFamiliasRol
            // 
            this.tabFamiliasRol.Controls.Add(this.lblFamiliasDelRol);
            this.tabFamiliasRol.Controls.Add(this.lbFamiliasDelRol);
            this.tabFamiliasRol.Controls.Add(this.btnQuitarFamiliaRol);
            this.tabFamiliasRol.Controls.Add(this.btnAgregarFamiliaRol);
            this.tabFamiliasRol.Controls.Add(this.lblFamiliasDisponiblesRol);
            this.tabFamiliasRol.Controls.Add(this.clbFamiliasDisponiblesRol);
            this.tabFamiliasRol.Controls.Add(this.lblRolesFamTab);
            this.tabFamiliasRol.Controls.Add(this.lbRolesFamTab);
            this.tabFamiliasRol.Location = new System.Drawing.Point(4, 22);
            this.tabFamiliasRol.Name = "tabFamiliasRol";
            this.tabFamiliasRol.Padding = new System.Windows.Forms.Padding(3);
            this.tabFamiliasRol.Size = new System.Drawing.Size(992, 554);
            this.tabFamiliasRol.TabIndex = 2;
            this.tabFamiliasRol.Text = "Familias del Rol";
            this.tabFamiliasRol.UseVisualStyleBackColor = true;
            // 
            // lblFamiliasDelRol
            // 
            this.lblFamiliasDelRol.AutoSize = true;
            this.lblFamiliasDelRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFamiliasDelRol.Location = new System.Drawing.Point(627, 15);
            this.lblFamiliasDelRol.Name = "lblFamiliasDelRol";
            this.lblFamiliasDelRol.Size = new System.Drawing.Size(107, 16);
            this.lblFamiliasDelRol.TabIndex = 7;
            this.lblFamiliasDelRol.Text = "Familias del Rol:";
            // 
            // lbFamiliasDelRol
            // 
            this.lbFamiliasDelRol.FormattingEnabled = true;
            this.lbFamiliasDelRol.Location = new System.Drawing.Point(630, 35);
            this.lbFamiliasDelRol.Name = "lbFamiliasDelRol";
            this.lbFamiliasDelRol.Size = new System.Drawing.Size(330, 498);
            this.lbFamiliasDelRol.TabIndex = 6;
            // 
            // btnQuitarFamiliaRol
            // 
            this.btnQuitarFamiliaRol.Location = new System.Drawing.Point(462, 280);
            this.btnQuitarFamiliaRol.Name = "btnQuitarFamiliaRol";
            this.btnQuitarFamiliaRol.Size = new System.Drawing.Size(150, 45);
            this.btnQuitarFamiliaRol.TabIndex = 5;
            this.btnQuitarFamiliaRol.Text = "<< Quitar Familia";
            this.btnQuitarFamiliaRol.UseVisualStyleBackColor = true;
            this.btnQuitarFamiliaRol.Click += new System.EventHandler(this.btnQuitarFamiliaRol_Click);
            // 
            // btnAgregarFamiliaRol
            // 
            this.btnAgregarFamiliaRol.Location = new System.Drawing.Point(462, 220);
            this.btnAgregarFamiliaRol.Name = "btnAgregarFamiliaRol";
            this.btnAgregarFamiliaRol.Size = new System.Drawing.Size(150, 45);
            this.btnAgregarFamiliaRol.TabIndex = 4;
            this.btnAgregarFamiliaRol.Text = "Agregar Familia(s) >>";
            this.btnAgregarFamiliaRol.UseVisualStyleBackColor = true;
            this.btnAgregarFamiliaRol.Click += new System.EventHandler(this.btnAgregarFamiliaRol_Click);
            // 
            // lblFamiliasDisponiblesRol
            // 
            this.lblFamiliasDisponiblesRol.AutoSize = true;
            this.lblFamiliasDisponiblesRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFamiliasDisponiblesRol.Location = new System.Drawing.Point(225, 15);
            this.lblFamiliasDisponiblesRol.Name = "lblFamiliasDisponiblesRol";
            this.lblFamiliasDisponiblesRol.Size = new System.Drawing.Size(136, 16);
            this.lblFamiliasDisponiblesRol.TabIndex = 2;
            this.lblFamiliasDisponiblesRol.Text = "Familias Disponibles:";
            // 
            // clbFamiliasDisponiblesRol
            // 
            this.clbFamiliasDisponiblesRol.CheckOnClick = true;
            this.clbFamiliasDisponiblesRol.FormattingEnabled = true;
            this.clbFamiliasDisponiblesRol.Location = new System.Drawing.Point(228, 35);
            this.clbFamiliasDisponiblesRol.Name = "clbFamiliasDisponiblesRol";
            this.clbFamiliasDisponiblesRol.Size = new System.Drawing.Size(220, 484);
            this.clbFamiliasDisponiblesRol.TabIndex = 3;
            // 
            // lblRolesFamTab
            // 
            this.lblRolesFamTab.AutoSize = true;
            this.lblRolesFamTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRolesFamTab.Location = new System.Drawing.Point(9, 15);
            this.lblRolesFamTab.Name = "lblRolesFamTab";
            this.lblRolesFamTab.Size = new System.Drawing.Size(119, 16);
            this.lblRolesFamTab.TabIndex = 0;
            this.lblRolesFamTab.Text = "Seleccione un Rol:";
            // 
            // lbRolesFamTab
            // 
            this.lbRolesFamTab.FormattingEnabled = true;
            this.lbRolesFamTab.Location = new System.Drawing.Point(12, 35);
            this.lbRolesFamTab.Name = "lbRolesFamTab";
            this.lbRolesFamTab.Size = new System.Drawing.Size(200, 498);
            this.lbRolesFamTab.TabIndex = 1;
            this.lbRolesFamTab.SelectedIndexChanged += new System.EventHandler(this.lbRolesFamTab_SelectedIndexChanged);
            // 
            // tabPermisosRol
            // 
            this.tabPermisosRol.Controls.Add(this.lblPermisosEfectivosRol);
            this.tabPermisosRol.Controls.Add(this.lbPermisosEfectivosRol);
            this.tabPermisosRol.Controls.Add(this.lblPermisosDirectosDelRol);
            this.tabPermisosRol.Controls.Add(this.lbPermisosDirectosDelRol);
            this.tabPermisosRol.Controls.Add(this.btnQuitarPermisoRol);
            this.tabPermisosRol.Controls.Add(this.btnAgregarPermisoRol);
            this.tabPermisosRol.Controls.Add(this.lblPermisosDisponiblesRol);
            this.tabPermisosRol.Controls.Add(this.clbPermisosDisponiblesRol);
            this.tabPermisosRol.Controls.Add(this.lblRolesPermTab);
            this.tabPermisosRol.Controls.Add(this.lbRolesPermTab);
            this.tabPermisosRol.Location = new System.Drawing.Point(4, 22);
            this.tabPermisosRol.Name = "tabPermisosRol";
            this.tabPermisosRol.Padding = new System.Windows.Forms.Padding(3);
            this.tabPermisosRol.Size = new System.Drawing.Size(992, 554);
            this.tabPermisosRol.TabIndex = 3;
            this.tabPermisosRol.Text = "Permisos del Rol";
            this.tabPermisosRol.UseVisualStyleBackColor = true;
            // 
            // lblPermisosEfectivosRol
            // 
            this.lblPermisosEfectivosRol.AutoSize = true;
            this.lblPermisosEfectivosRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermisosEfectivosRol.Location = new System.Drawing.Point(701, 15);
            this.lblPermisosEfectivosRol.Name = "lblPermisosEfectivosRol";
            this.lblPermisosEfectivosRol.Size = new System.Drawing.Size(276, 16);
            this.lblPermisosEfectivosRol.TabIndex = 9;
            this.lblPermisosEfectivosRol.Text = "Todos los Permisos Efectivos (con Familias):";
            // 
            // lbPermisosEfectivosRol
            // 
            this.lbPermisosEfectivosRol.FormattingEnabled = true;
            this.lbPermisosEfectivosRol.Location = new System.Drawing.Point(704, 35);
            this.lbPermisosEfectivosRol.Name = "lbPermisosEfectivosRol";
            this.lbPermisosEfectivosRol.Size = new System.Drawing.Size(270, 498);
            this.lbPermisosEfectivosRol.TabIndex = 8;
            // 
            // lblPermisosDirectosDelRol
            // 
            this.lblPermisosDirectosDelRol.AutoSize = true;
            this.lblPermisosDirectosDelRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermisosDirectosDelRol.Location = new System.Drawing.Point(507, 15);
            this.lblPermisosDirectosDelRol.Name = "lblPermisosDirectosDelRol";
            this.lblPermisosDirectosDelRol.Size = new System.Drawing.Size(166, 16);
            this.lblPermisosDirectosDelRol.TabIndex = 7;
            this.lblPermisosDirectosDelRol.Text = "Permisos Directos del Rol:";
            // 
            // lbPermisosDirectosDelRol
            // 
            this.lbPermisosDirectosDelRol.FormattingEnabled = true;
            this.lbPermisosDirectosDelRol.Location = new System.Drawing.Point(510, 35);
            this.lbPermisosDirectosDelRol.Name = "lbPermisosDirectosDelRol";
            this.lbPermisosDirectosDelRol.Size = new System.Drawing.Size(180, 498);
            this.lbPermisosDirectosDelRol.TabIndex = 6;
            // 
            // btnQuitarPermisoRol
            // 
            this.btnQuitarPermisoRol.Location = new System.Drawing.Point(388, 270);
            this.btnQuitarPermisoRol.Name = "btnQuitarPermisoRol";
            this.btnQuitarPermisoRol.Size = new System.Drawing.Size(110, 40);
            this.btnQuitarPermisoRol.TabIndex = 5;
            this.btnQuitarPermisoRol.Text = "<< Eliminar";
            this.btnQuitarPermisoRol.UseVisualStyleBackColor = true;
            this.btnQuitarPermisoRol.Click += new System.EventHandler(this.btnQuitarPermisoRol_Click);
            // 
            // btnAgregarPermisoRol
            // 
            this.btnAgregarPermisoRol.Location = new System.Drawing.Point(388, 220);
            this.btnAgregarPermisoRol.Name = "btnAgregarPermisoRol";
            this.btnAgregarPermisoRol.Size = new System.Drawing.Size(110, 40);
            this.btnAgregarPermisoRol.TabIndex = 4;
            this.btnAgregarPermisoRol.Text = "Agregar >>";
            this.btnAgregarPermisoRol.UseVisualStyleBackColor = true;
            this.btnAgregarPermisoRol.Click += new System.EventHandler(this.btnAgregarPermisoRol_Click);
            // 
            // lblPermisosDisponiblesRol
            // 
            this.lblPermisosDisponiblesRol.AutoSize = true;
            this.lblPermisosDisponiblesRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermisosDisponiblesRol.Location = new System.Drawing.Point(193, 15);
            this.lblPermisosDisponiblesRol.Name = "lblPermisosDisponiblesRol";
            this.lblPermisosDisponiblesRol.Size = new System.Drawing.Size(196, 16);
            this.lblPermisosDisponiblesRol.TabIndex = 2;
            this.lblPermisosDisponiblesRol.Text = "Permisos Disponibles (sueltos):";
            // 
            // clbPermisosDisponiblesRol
            // 
            this.clbPermisosDisponiblesRol.CheckOnClick = true;
            this.clbPermisosDisponiblesRol.FormattingEnabled = true;
            this.clbPermisosDisponiblesRol.Location = new System.Drawing.Point(196, 35);
            this.clbPermisosDisponiblesRol.Name = "clbPermisosDisponiblesRol";
            this.clbPermisosDisponiblesRol.Size = new System.Drawing.Size(180, 484);
            this.clbPermisosDisponiblesRol.TabIndex = 3;
            // 
            // lblRolesPermTab
            // 
            this.lblRolesPermTab.AutoSize = true;
            this.lblRolesPermTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRolesPermTab.Location = new System.Drawing.Point(9, 15);
            this.lblRolesPermTab.Name = "lblRolesPermTab";
            this.lblRolesPermTab.Size = new System.Drawing.Size(119, 16);
            this.lblRolesPermTab.TabIndex = 0;
            this.lblRolesPermTab.Text = "Seleccione un Rol:";
            // 
            // lbRolesPermTab
            // 
            this.lbRolesPermTab.FormattingEnabled = true;
            this.lbRolesPermTab.Location = new System.Drawing.Point(12, 35);
            this.lbRolesPermTab.Name = "lbRolesPermTab";
            this.lbRolesPermTab.Size = new System.Drawing.Size(170, 498);
            this.lbRolesPermTab.TabIndex = 1;
            this.lbRolesPermTab.SelectedIndexChanged += new System.EventHandler(this.lbRolesPermTab_SelectedIndexChanged);
            // 
            // GestionarRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 580);
            this.Controls.Add(this.GestionRoles);
            this.Name = "GestionarRoles";
            this.Text = "GestionarRoles";
            this.Load += new System.EventHandler(this.GestionarRoles_Load);
            this.GestionRoles.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabFamiliasRol.ResumeLayout(false);
            this.tabFamiliasRol.PerformLayout();
            this.tabPermisosRol.ResumeLayout(false);
            this.tabPermisosRol.PerformLayout();
            this.lateralRoles.Name = "lateralRoles";
            this.generalRolesTarjeta.Name = "generalRolesTarjeta";
            this.campoNombreRol.Name = "campoNombreRol";
            this.familiasDisponiblesTarjeta.Name = "familiasDisponiblesTarjeta";
            this.familiasAsignadasTarjeta.Name = "familiasAsignadasTarjeta";
            this.permisosDisponiblesTarjeta.Name = "permisosDisponiblesTarjeta";
            this.permisosAsignadosTarjeta.Name = "permisosAsignadosTarjeta";
            this.efectivosRolesTarjeta.Name = "efectivosRolesTarjeta";
            this.cuerpoRoles.Name = "cuerpoRoles";
            this.contenidoRoles.Name = "contenidoRoles";
            this.paginaRoles0.Name = "paginaRoles0";
            this.paginaRoles1.Name = "paginaRoles1";
            this.paginaRoles2.Name = "paginaRoles2";
            this.tituloRolesVista.Name = "tituloRolesVista";
            this.subtituloRolesVista.Name = "subtituloRolesVista";
            this.pieRolesVista.Name = "pieRolesVista";
            this.ayudaEfectivosRoles.Name = "ayudaEfectivosRoles";
            this.nuevoRolVista.Name = "nuevoRolVista";
            this.generalRoles.Name = "generalRoles";
            this.familiasRoles.Name = "familiasRoles";
            this.permisosRoles.Name = "permisosRoles";
            this.temaRoles.Name = "temaRoles";
            this.iconoRoles.Name = "iconoRoles";
            this.lateralRoles.CornerRadius = 16;
            this.lateralRoles.Padding = new System.Windows.Forms.Padding(18);
            this.lateralRoles.BackColor = System.Drawing.Color.White;
            this.lateralRoles.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.generalRolesTarjeta.CornerRadius = 16;
            this.generalRolesTarjeta.Padding = new System.Windows.Forms.Padding(18);
            this.generalRolesTarjeta.BackColor = System.Drawing.Color.White;
            this.generalRolesTarjeta.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.campoNombreRol.CornerRadius = 16;
            this.campoNombreRol.Padding = new System.Windows.Forms.Padding(18);
            this.campoNombreRol.BackColor = System.Drawing.Color.White;
            this.campoNombreRol.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.familiasDisponiblesTarjeta.CornerRadius = 16;
            this.familiasDisponiblesTarjeta.Padding = new System.Windows.Forms.Padding(18);
            this.familiasDisponiblesTarjeta.BackColor = System.Drawing.Color.White;
            this.familiasDisponiblesTarjeta.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.familiasAsignadasTarjeta.CornerRadius = 16;
            this.familiasAsignadasTarjeta.Padding = new System.Windows.Forms.Padding(18);
            this.familiasAsignadasTarjeta.BackColor = System.Drawing.Color.White;
            this.familiasAsignadasTarjeta.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.permisosDisponiblesTarjeta.CornerRadius = 16;
            this.permisosDisponiblesTarjeta.Padding = new System.Windows.Forms.Padding(18);
            this.permisosDisponiblesTarjeta.BackColor = System.Drawing.Color.White;
            this.permisosDisponiblesTarjeta.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.permisosAsignadosTarjeta.CornerRadius = 16;
            this.permisosAsignadosTarjeta.Padding = new System.Windows.Forms.Padding(18);
            this.permisosAsignadosTarjeta.BackColor = System.Drawing.Color.White;
            this.permisosAsignadosTarjeta.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.efectivosRolesTarjeta.CornerRadius = 16;
            this.efectivosRolesTarjeta.Padding = new System.Windows.Forms.Padding(18);
            this.efectivosRolesTarjeta.BackColor = System.Drawing.Color.White;
            this.efectivosRolesTarjeta.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.tituloRolesVista.Text = "Perfiles / Roles";
            this.subtituloRolesVista.Text = "Administrá las familias y los permisos de cada rol";
            this.pieRolesVista.Text = "Los cambios respetan los permisos de tu cuenta.";
            this.nuevoRolVista.Text = "Crear rol";
            this.generalRoles.Text = "General";
            this.familiasRoles.Text = "Familias";
            this.permisosRoles.Text = "Permisos";
            this.ayudaEfectivosRoles.Text = "Incluye los permisos heredados de familias";
            this.tituloRolesVista.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.tituloRolesVista.AutoEllipsis = true;
            this.tituloRolesVista.BackColor = System.Drawing.Color.Transparent;
            this.subtituloRolesVista.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtituloRolesVista.AutoEllipsis = true;
            this.subtituloRolesVista.BackColor = System.Drawing.Color.Transparent;
            this.pieRolesVista.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pieRolesVista.AutoEllipsis = true;
            this.pieRolesVista.BackColor = System.Drawing.Color.Transparent;
            this.ayudaEfectivosRoles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ayudaEfectivosRoles.AutoEllipsis = true;
            this.ayudaEfectivosRoles.BackColor = System.Drawing.Color.Transparent;
            this.tituloRolesVista.Location = new System.Drawing.Point(82, 17);
            this.tituloRolesVista.Size = new System.Drawing.Size(650, 42);
            this.subtituloRolesVista.Location = new System.Drawing.Point(84, 63);
            this.subtituloRolesVista.Size = new System.Drawing.Size(1025, 27);
            this.pieRolesVista.Location = new System.Drawing.Point(28, 666);
            this.pieRolesVista.Size = new System.Drawing.Size(1084, 24);
            this.iconoRoles.Location = new System.Drawing.Point(27, 26);
            this.iconoRoles.Size = new System.Drawing.Size(40, 40);
            this.iconoRoles.Kind = 3;
            this.iconoRoles.BackColor = System.Drawing.Color.Transparent;
            this.iconoRoles.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.temaRoles.Location = new System.Drawing.Point(1018, 27);
            this.temaRoles.Size = new System.Drawing.Size(96, 38);
            this.temaRoles.Click += new System.EventHandler(this.temaRoles_Click);
            this.cuerpoRoles.Location = new System.Drawing.Point(24, 108);
            this.cuerpoRoles.Size = new System.Drawing.Size(1092, 545);
            this.lateralRoles.Location = new System.Drawing.Point(0, 0);
            this.lateralRoles.Size = new System.Drawing.Size(252, 545);
            this.contenidoRoles.Location = new System.Drawing.Point(272, 0);
            this.contenidoRoles.Size = new System.Drawing.Size(820, 545);
            this.cuerpoRoles.Controls.Add(this.lateralRoles);
            this.cuerpoRoles.Controls.Add(this.contenidoRoles);
            this.label7.Location = new System.Drawing.Point(20, 18);
            this.label7.Size = new System.Drawing.Size(224, 28);
            this.lateralRoles.Controls.Add(this.label7);
            this.lbRolesExistentes.Location = new System.Drawing.Point(18, 60);
            this.lbRolesExistentes.Size = new System.Drawing.Size(216, 353);
            this.lateralRoles.Controls.Add(this.lbRolesExistentes);
            this.nuevoRolVista.Location = new System.Drawing.Point(18, 429);
            this.nuevoRolVista.Size = new System.Drawing.Size(216, 42);
            this.nuevoRolVista.Click += new System.EventHandler(this.nuevoRolVista_Click);
            this.lateralRoles.Controls.Add(this.nuevoRolVista);
            this.btnEliminarRol.Location = new System.Drawing.Point(18, 483);
            this.btnEliminarRol.Size = new System.Drawing.Size(216, 42);
            this.lateralRoles.Controls.Add(this.btnEliminarRol);
            this.generalRoles.Location = new System.Drawing.Point(0, 0);
            this.generalRoles.Size = new System.Drawing.Size(150, 38);
            this.generalRoles.Click += new System.EventHandler(this.pestanaRoles_Click);
            this.contenidoRoles.Controls.Add(this.generalRoles);
            this.paginaRoles0.Location = new System.Drawing.Point(0, 54);
            this.paginaRoles0.Size = new System.Drawing.Size(820, 491);
            this.paginaRoles0.Visible = true;
            this.contenidoRoles.Controls.Add(this.paginaRoles0);
            this.familiasRoles.Location = new System.Drawing.Point(158, 0);
            this.familiasRoles.Size = new System.Drawing.Size(150, 38);
            this.familiasRoles.Click += new System.EventHandler(this.pestanaRoles_Click);
            this.contenidoRoles.Controls.Add(this.familiasRoles);
            this.paginaRoles1.Location = new System.Drawing.Point(0, 54);
            this.paginaRoles1.Size = new System.Drawing.Size(820, 491);
            this.paginaRoles1.Visible = false;
            this.contenidoRoles.Controls.Add(this.paginaRoles1);
            this.permisosRoles.Location = new System.Drawing.Point(316, 0);
            this.permisosRoles.Size = new System.Drawing.Size(150, 38);
            this.permisosRoles.Click += new System.EventHandler(this.pestanaRoles_Click);
            this.contenidoRoles.Controls.Add(this.permisosRoles);
            this.paginaRoles2.Location = new System.Drawing.Point(0, 54);
            this.paginaRoles2.Size = new System.Drawing.Size(820, 491);
            this.paginaRoles2.Visible = false;
            this.contenidoRoles.Controls.Add(this.paginaRoles2);
            this.generalRolesTarjeta.Location = new System.Drawing.Point(0, 0);
            this.generalRolesTarjeta.Size = new System.Drawing.Size(820, 491);
            this.generalRolesTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paginaRoles0.Controls.Add(this.generalRolesTarjeta);
            this.campoNombreRol.Location = new System.Drawing.Point(20, 54);
            this.campoNombreRol.Size = new System.Drawing.Size(776, 44);
            this.campoNombreRol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.generalRolesTarjeta.Controls.Add(this.campoNombreRol);
            this.txtNombreNuevoRol.Location = new System.Drawing.Point(12, 10);
            this.txtNombreNuevoRol.Size = new System.Drawing.Size(744, 26);
            this.txtNombreNuevoRol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtNombreNuevoRol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombreNuevoRol.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.campoNombreRol.Controls.Add(this.txtNombreNuevoRol);
            this.label6.Location = new System.Drawing.Point(22, 24);
            this.label6.Size = new System.Drawing.Size(550, 26);
            this.generalRolesTarjeta.Controls.Add(this.label6);
            this.label5.Location = new System.Drawing.Point(22, 118);
            this.label5.Size = new System.Drawing.Size(600, 26);
            this.generalRolesTarjeta.Controls.Add(this.label5);
            this.clbFamiliasYPermisos.Location = new System.Drawing.Point(24, 154);
            this.clbFamiliasYPermisos.Size = new System.Drawing.Size(772, 265);
            this.generalRolesTarjeta.Controls.Add(this.clbFamiliasYPermisos);
            this.btnCrearRol.Location = new System.Drawing.Point(590, 431);
            this.btnCrearRol.Size = new System.Drawing.Size(206, 40);
            this.generalRolesTarjeta.Controls.Add(this.btnCrearRol);
            this.familiasDisponiblesTarjeta.Location = new System.Drawing.Point(0, 0);
            this.familiasDisponiblesTarjeta.Size = new System.Drawing.Size(336, 491);
            this.familiasAsignadasTarjeta.Location = new System.Drawing.Point(484, 0);
            this.familiasAsignadasTarjeta.Size = new System.Drawing.Size(336, 491);
            this.paginaRoles1.Controls.Add(this.familiasDisponiblesTarjeta);
            this.paginaRoles1.Controls.Add(this.familiasAsignadasTarjeta);
            this.lblFamiliasDisponiblesRol.Location = new System.Drawing.Point(16, 18);
            this.lblFamiliasDisponiblesRol.Size = new System.Drawing.Size(304, 44);
            this.familiasDisponiblesTarjeta.Controls.Add(this.lblFamiliasDisponiblesRol);
            this.lblFamiliasDelRol.Location = new System.Drawing.Point(16, 18);
            this.lblFamiliasDelRol.Size = new System.Drawing.Size(304, 44);
            this.familiasAsignadasTarjeta.Controls.Add(this.lblFamiliasDelRol);
            this.clbFamiliasDisponiblesRol.Location = new System.Drawing.Point(18, 68);
            this.clbFamiliasDisponiblesRol.Size = new System.Drawing.Size(300, 401);
            this.familiasDisponiblesTarjeta.Controls.Add(this.clbFamiliasDisponiblesRol);
            this.lbFamiliasDelRol.Location = new System.Drawing.Point(18, 68);
            this.lbFamiliasDelRol.Size = new System.Drawing.Size(300, 401);
            this.familiasAsignadasTarjeta.Controls.Add(this.lbFamiliasDelRol);
            this.btnAgregarFamiliaRol.Location = new System.Drawing.Point(348, 200);
            this.btnAgregarFamiliaRol.Size = new System.Drawing.Size(124, 40);
            this.btnQuitarFamiliaRol.Location = new System.Drawing.Point(348, 250);
            this.btnQuitarFamiliaRol.Size = new System.Drawing.Size(124, 40);
            this.paginaRoles1.Controls.Add(this.btnAgregarFamiliaRol);
            this.paginaRoles1.Controls.Add(this.btnQuitarFamiliaRol);
            this.permisosDisponiblesTarjeta.Location = new System.Drawing.Point(0, 0);
            this.permisosDisponiblesTarjeta.Size = new System.Drawing.Size(336, 233);
            this.permisosAsignadosTarjeta.Location = new System.Drawing.Point(484, 0);
            this.permisosAsignadosTarjeta.Size = new System.Drawing.Size(336, 233);
            this.paginaRoles2.Controls.Add(this.permisosDisponiblesTarjeta);
            this.paginaRoles2.Controls.Add(this.permisosAsignadosTarjeta);
            this.lblPermisosDisponiblesRol.Location = new System.Drawing.Point(16, 18);
            this.lblPermisosDisponiblesRol.Size = new System.Drawing.Size(304, 44);
            this.permisosDisponiblesTarjeta.Controls.Add(this.lblPermisosDisponiblesRol);
            this.lblPermisosDirectosDelRol.Location = new System.Drawing.Point(16, 18);
            this.lblPermisosDirectosDelRol.Size = new System.Drawing.Size(304, 44);
            this.permisosAsignadosTarjeta.Controls.Add(this.lblPermisosDirectosDelRol);
            this.clbPermisosDisponiblesRol.Location = new System.Drawing.Point(18, 68);
            this.clbPermisosDisponiblesRol.Size = new System.Drawing.Size(300, 143);
            this.permisosDisponiblesTarjeta.Controls.Add(this.clbPermisosDisponiblesRol);
            this.lbPermisosDirectosDelRol.Location = new System.Drawing.Point(18, 68);
            this.lbPermisosDirectosDelRol.Size = new System.Drawing.Size(300, 143);
            this.permisosAsignadosTarjeta.Controls.Add(this.lbPermisosDirectosDelRol);
            this.btnAgregarPermisoRol.Location = new System.Drawing.Point(348, 71);
            this.btnAgregarPermisoRol.Size = new System.Drawing.Size(124, 40);
            this.btnQuitarPermisoRol.Location = new System.Drawing.Point(348, 121);
            this.btnQuitarPermisoRol.Size = new System.Drawing.Size(124, 40);
            this.paginaRoles2.Controls.Add(this.btnAgregarPermisoRol);
            this.paginaRoles2.Controls.Add(this.btnQuitarPermisoRol);
            this.efectivosRolesTarjeta.Location = new System.Drawing.Point(0, 247);
            this.efectivosRolesTarjeta.Size = new System.Drawing.Size(820, 244);
            this.paginaRoles2.Controls.Add(this.efectivosRolesTarjeta);
            this.lblPermisosEfectivosRol.Location = new System.Drawing.Point(18, 16);
            this.lblPermisosEfectivosRol.Size = new System.Drawing.Size(784, 27);
            this.efectivosRolesTarjeta.Controls.Add(this.lblPermisosEfectivosRol);
            this.ayudaEfectivosRoles.Location = new System.Drawing.Point(18, 46);
            this.ayudaEfectivosRoles.Size = new System.Drawing.Size(784, 25);
            this.efectivosRolesTarjeta.Controls.Add(this.ayudaEfectivosRoles);
            this.lbPermisosEfectivosRol.Location = new System.Drawing.Point(18, 80);
            this.lbPermisosEfectivosRol.Size = new System.Drawing.Size(784, 144);
            this.efectivosRolesTarjeta.Controls.Add(this.lbPermisosEfectivosRol);
            this.lbRolesExistentes.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lbRolesExistentes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbRolesExistentes.ItemHeight = 44;
            this.lbRolesExistentes.IntegralHeight = false;
            this.lbRolesExistentes.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DibujarFilaRoles);
            this.lbFamiliasDelRol.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lbFamiliasDelRol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbFamiliasDelRol.ItemHeight = 34;
            this.lbFamiliasDelRol.IntegralHeight = false;
            this.lbFamiliasDelRol.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DibujarFilaRoles);
            this.lbPermisosDirectosDelRol.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lbPermisosDirectosDelRol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPermisosDirectosDelRol.ItemHeight = 34;
            this.lbPermisosDirectosDelRol.IntegralHeight = false;
            this.lbPermisosDirectosDelRol.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DibujarFilaRoles);
            this.lbPermisosEfectivosRol.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lbPermisosEfectivosRol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPermisosEfectivosRol.ItemHeight = 34;
            this.lbPermisosEfectivosRol.IntegralHeight = false;
            this.lbPermisosEfectivosRol.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DibujarFilaRoles);
            this.clbFamiliasYPermisos.CheckOnClick = true;
            this.clbFamiliasYPermisos.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.clbFamiliasYPermisos.IntegralHeight = false;
            this.clbFamiliasDisponiblesRol.CheckOnClick = true;
            this.clbFamiliasDisponiblesRol.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.clbFamiliasDisponiblesRol.IntegralHeight = false;
            this.clbPermisosDisponiblesRol.CheckOnClick = true;
            this.clbPermisosDisponiblesRol.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.clbPermisosDisponiblesRol.IntegralHeight = false;
            this.lbRolesExistentes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbRolesExistentes.BackColor = System.Drawing.Color.White;
            this.lbRolesExistentes.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.lbFamiliasDelRol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbFamiliasDelRol.BackColor = System.Drawing.Color.White;
            this.lbFamiliasDelRol.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.lbPermisosDirectosDelRol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPermisosDirectosDelRol.BackColor = System.Drawing.Color.White;
            this.lbPermisosDirectosDelRol.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.lbPermisosEfectivosRol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPermisosEfectivosRol.BackColor = System.Drawing.Color.White;
            this.lbPermisosEfectivosRol.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.clbFamiliasYPermisos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbFamiliasYPermisos.BackColor = System.Drawing.Color.White;
            this.clbFamiliasYPermisos.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.clbFamiliasDisponiblesRol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbFamiliasDisponiblesRol.BackColor = System.Drawing.Color.White;
            this.clbFamiliasDisponiblesRol.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.clbPermisosDisponiblesRol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbPermisosDisponiblesRol.BackColor = System.Drawing.Color.White;
            this.clbPermisosDisponiblesRol.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.AutoSize = false;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label6.AutoSize = false;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label7.AutoSize = false;
            this.lblFamiliasDelRol.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFamiliasDelRol.AutoSize = false;
            this.lblFamiliasDisponiblesRol.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFamiliasDisponiblesRol.AutoSize = false;
            this.lblPermisosDisponiblesRol.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPermisosDisponiblesRol.AutoSize = false;
            this.lblPermisosDirectosDelRol.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPermisosDirectosDelRol.AutoSize = false;
            this.lblPermisosEfectivosRol.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPermisosEfectivosRol.AutoSize = false;
            this.btnCrearRol.Icon = ProyectoIS.UsuariosButtonIcon.Check;
            this.btnCrearRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCrearRol.FlatAppearance.BorderSize = 0;
            this.btnCrearRol.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnCrearRol.BackColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnCrearRol.ForeColor = System.Drawing.Color.White;
            this.btnEliminarRol.Icon = ProyectoIS.UsuariosButtonIcon.Delete;
            this.btnEliminarRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnEliminarRol.FlatAppearance.BorderSize = 1;
            this.btnEliminarRol.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnEliminarRol.BackColor = System.Drawing.Color.White;
            this.btnEliminarRol.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.nuevoRolVista.Icon = ProyectoIS.UsuariosButtonIcon.UserAdd;
            this.nuevoRolVista.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nuevoRolVista.FlatAppearance.BorderSize = 0;
            this.nuevoRolVista.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.nuevoRolVista.BackColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.nuevoRolVista.ForeColor = System.Drawing.Color.White;
            this.btnAgregarFamiliaRol.Icon = ProyectoIS.UsuariosButtonIcon.Arrow;
            this.btnAgregarFamiliaRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAgregarFamiliaRol.FlatAppearance.BorderSize = 1;
            this.btnAgregarFamiliaRol.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnAgregarFamiliaRol.BackColor = System.Drawing.Color.White;
            this.btnAgregarFamiliaRol.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnAgregarFamiliaRol.Text = "Agregar";
            this.btnAgregarPermisoRol.Icon = ProyectoIS.UsuariosButtonIcon.Arrow;
            this.btnAgregarPermisoRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAgregarPermisoRol.FlatAppearance.BorderSize = 1;
            this.btnAgregarPermisoRol.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnAgregarPermisoRol.BackColor = System.Drawing.Color.White;
            this.btnAgregarPermisoRol.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnAgregarPermisoRol.Text = "Agregar";
            this.btnQuitarFamiliaRol.Icon = ProyectoIS.UsuariosButtonIcon.Delete;
            this.btnQuitarFamiliaRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuitarFamiliaRol.FlatAppearance.BorderSize = 1;
            this.btnQuitarFamiliaRol.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnQuitarFamiliaRol.BackColor = System.Drawing.Color.White;
            this.btnQuitarFamiliaRol.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnQuitarFamiliaRol.Text = "Quitar";
            this.btnQuitarPermisoRol.Icon = ProyectoIS.UsuariosButtonIcon.Delete;
            this.btnQuitarPermisoRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuitarPermisoRol.FlatAppearance.BorderSize = 1;
            this.btnQuitarPermisoRol.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.btnQuitarPermisoRol.BackColor = System.Drawing.Color.White;
            this.btnQuitarPermisoRol.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.btnQuitarPermisoRol.Text = "Quitar";
            this.generalRoles.Icon = ProyectoIS.UsuariosButtonIcon.None;
            this.generalRoles.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.generalRoles.FlatAppearance.BorderSize = 0;
            this.generalRoles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.generalRoles.BackColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.generalRoles.ForeColor = System.Drawing.Color.White;
            this.familiasRoles.Icon = ProyectoIS.UsuariosButtonIcon.None;
            this.familiasRoles.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.familiasRoles.FlatAppearance.BorderSize = 1;
            this.familiasRoles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.familiasRoles.BackColor = System.Drawing.Color.White;
            this.familiasRoles.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.permisosRoles.Icon = ProyectoIS.UsuariosButtonIcon.None;
            this.permisosRoles.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.permisosRoles.FlatAppearance.BorderSize = 1;
            this.permisosRoles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(219, 223, 231);
            this.permisosRoles.BackColor = System.Drawing.Color.White;
            this.permisosRoles.ForeColor = System.Drawing.Color.FromArgb(45, 96, 196);
            this.GestionRoles.Location = new System.Drawing.Point(-2000, -2000);
            this.GestionRoles.Visible = false;
            this.GestionRoles.TabStop = false;
            this.Controls.Add(this.iconoRoles);
            this.Controls.Add(this.tituloRolesVista);
            this.Controls.Add(this.subtituloRolesVista);
            this.Controls.Add(this.temaRoles);
            this.Controls.Add(this.cuerpoRoles);
            this.Controls.Add(this.pieRolesVista);
            this.animacionRoles.Interval = 15;
            this.animacionRoles.Tick += new System.EventHandler(this.animacionRoles_Tick);
            this.lbRolesExistentes.SelectedIndexChanged += new System.EventHandler(this.lbRolesExistentes_SelectedIndexChanged);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ClientSize = new System.Drawing.Size(1140, 700);
            this.MinimumSize = new System.Drawing.Size(960, 640);
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 249);
            this.ForeColor = System.Drawing.Color.FromArgb(35, 39, 45);
            this.contenidoRoles.Controls.SetChildIndex(this.paginaRoles0, 0);
            this.Resize += new System.EventHandler(this.GestionarRoles_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl GestionRoles;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private ProyectoIS.UsuariosRoundedButton btnCrearRol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreNuevoRol;
        private System.Windows.Forms.CheckedListBox clbFamiliasYPermisos;
        private System.Windows.Forms.ListBox lbRolesExistentes;
        private ProyectoIS.UsuariosRoundedButton btnEliminarRol;
        private System.Windows.Forms.TabPage tabFamiliasRol;
        private System.Windows.Forms.Label lblFamiliasDelRol;
        private System.Windows.Forms.ListBox lbFamiliasDelRol;
        private ProyectoIS.UsuariosRoundedButton btnQuitarFamiliaRol;
        private ProyectoIS.UsuariosRoundedButton btnAgregarFamiliaRol;
        private System.Windows.Forms.Label lblFamiliasDisponiblesRol;
        private System.Windows.Forms.CheckedListBox clbFamiliasDisponiblesRol;
        private System.Windows.Forms.Label lblRolesFamTab;
        private System.Windows.Forms.ListBox lbRolesFamTab;
        private System.Windows.Forms.TabPage tabPermisosRol;
        private System.Windows.Forms.Label lblPermisosEfectivosRol;
        private System.Windows.Forms.ListBox lbPermisosEfectivosRol;
        private System.Windows.Forms.Label lblPermisosDirectosDelRol;
        private System.Windows.Forms.ListBox lbPermisosDirectosDelRol;
        private ProyectoIS.UsuariosRoundedButton btnQuitarPermisoRol;
        private ProyectoIS.UsuariosRoundedButton btnAgregarPermisoRol;
        private System.Windows.Forms.Label lblPermisosDisponiblesRol;
        private System.Windows.Forms.CheckedListBox clbPermisosDisponiblesRol;
        private System.Windows.Forms.Label lblRolesPermTab;
        private System.Windows.Forms.ListBox lbRolesPermTab;
        private System.Windows.Forms.Timer animacionRoles;
        private ProyectoIS.UsuariosRoundedPanel lateralRoles;
        private ProyectoIS.UsuariosRoundedPanel generalRolesTarjeta;
        private ProyectoIS.UsuariosRoundedPanel campoNombreRol;
        private ProyectoIS.UsuariosRoundedPanel familiasDisponiblesTarjeta;
        private ProyectoIS.UsuariosRoundedPanel familiasAsignadasTarjeta;
        private ProyectoIS.UsuariosRoundedPanel permisosDisponiblesTarjeta;
        private ProyectoIS.UsuariosRoundedPanel permisosAsignadosTarjeta;
        private ProyectoIS.UsuariosRoundedPanel efectivosRolesTarjeta;
        private System.Windows.Forms.Panel cuerpoRoles;
        private System.Windows.Forms.Panel contenidoRoles;
        private System.Windows.Forms.Panel paginaRoles0;
        private System.Windows.Forms.Panel paginaRoles1;
        private System.Windows.Forms.Panel paginaRoles2;
        private System.Windows.Forms.Label tituloRolesVista;
        private System.Windows.Forms.Label subtituloRolesVista;
        private System.Windows.Forms.Label pieRolesVista;
        private System.Windows.Forms.Label ayudaEfectivosRoles;
        private ProyectoIS.UsuariosRoundedButton nuevoRolVista;
        private ProyectoIS.UsuariosRoundedButton generalRoles;
        private ProyectoIS.UsuariosRoundedButton familiasRoles;
        private ProyectoIS.UsuariosRoundedButton permisosRoles;
        private ProyectoIS.UsuariosThemeSwitch temaRoles;
        private ProyectoIS.AltaGlyph iconoRoles;
    }
}
