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
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCrearRol = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombreNuevoRol = new System.Windows.Forms.TextBox();
            this.clbFamiliasYPermisos = new System.Windows.Forms.CheckedListBox();
            this.lbRolesExistentes = new System.Windows.Forms.ListBox();
            this.tabFamiliasRol = new System.Windows.Forms.TabPage();
            this.lblFamiliasDelRol = new System.Windows.Forms.Label();
            this.lbFamiliasDelRol = new System.Windows.Forms.ListBox();
            this.btnQuitarFamiliaRol = new System.Windows.Forms.Button();
            this.btnAgregarFamiliaRol = new System.Windows.Forms.Button();
            this.lblFamiliasDisponiblesRol = new System.Windows.Forms.Label();
            this.clbFamiliasDisponiblesRol = new System.Windows.Forms.CheckedListBox();
            this.lblRolesFamTab = new System.Windows.Forms.Label();
            this.lbRolesFamTab = new System.Windows.Forms.ListBox();
            this.tabPermisosRol = new System.Windows.Forms.TabPage();
            this.lblPermisosEfectivosRol = new System.Windows.Forms.Label();
            this.lbPermisosEfectivosRol = new System.Windows.Forms.ListBox();
            this.lblPermisosDirectosDelRol = new System.Windows.Forms.Label();
            this.lbPermisosDirectosDelRol = new System.Windows.Forms.ListBox();
            this.btnQuitarPermisoRol = new System.Windows.Forms.Button();
            this.btnAgregarPermisoRol = new System.Windows.Forms.Button();
            this.lblPermisosDisponiblesRol = new System.Windows.Forms.Label();
            this.clbPermisosDisponiblesRol = new System.Windows.Forms.CheckedListBox();
            this.lblRolesPermTab = new System.Windows.Forms.Label();
            this.lbRolesPermTab = new System.Windows.Forms.ListBox();
            this.GestionRoles.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabFamiliasRol.SuspendLayout();
            this.tabPermisosRol.SuspendLayout();
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl GestionRoles;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCrearRol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreNuevoRol;
        private System.Windows.Forms.CheckedListBox clbFamiliasYPermisos;
        private System.Windows.Forms.ListBox lbRolesExistentes;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.TabPage tabFamiliasRol;
        private System.Windows.Forms.Label lblFamiliasDelRol;
        private System.Windows.Forms.ListBox lbFamiliasDelRol;
        private System.Windows.Forms.Button btnQuitarFamiliaRol;
        private System.Windows.Forms.Button btnAgregarFamiliaRol;
        private System.Windows.Forms.Label lblFamiliasDisponiblesRol;
        private System.Windows.Forms.CheckedListBox clbFamiliasDisponiblesRol;
        private System.Windows.Forms.Label lblRolesFamTab;
        private System.Windows.Forms.ListBox lbRolesFamTab;
        private System.Windows.Forms.TabPage tabPermisosRol;
        private System.Windows.Forms.Label lblPermisosEfectivosRol;
        private System.Windows.Forms.ListBox lbPermisosEfectivosRol;
        private System.Windows.Forms.Label lblPermisosDirectosDelRol;
        private System.Windows.Forms.ListBox lbPermisosDirectosDelRol;
        private System.Windows.Forms.Button btnQuitarPermisoRol;
        private System.Windows.Forms.Button btnAgregarPermisoRol;
        private System.Windows.Forms.Label lblPermisosDisponiblesRol;
        private System.Windows.Forms.CheckedListBox clbPermisosDisponiblesRol;
        private System.Windows.Forms.Label lblRolesPermTab;
        private System.Windows.Forms.ListBox lbRolesPermTab;
    }
}
