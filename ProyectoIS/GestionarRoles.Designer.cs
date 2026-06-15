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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAsignarUsuario = new System.Windows.Forms.Button();
            this.clbRolesParaAsignar = new System.Windows.Forms.CheckedListBox();
            this.combobox = new System.Windows.Forms.ComboBox();
            this.GestionRoles.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GestionRoles
            // 
            this.GestionRoles.Controls.Add(this.tabPage1);
            this.GestionRoles.Controls.Add(this.tabPage2);
            this.GestionRoles.Location = new System.Drawing.Point(1, -1);
            this.GestionRoles.Name = "GestionRoles";
            this.GestionRoles.SelectedIndex = 0;
            this.GestionRoles.Size = new System.Drawing.Size(796, 527);
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
            this.tabPage1.Size = new System.Drawing.Size(788, 501);
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
            this.label7.Text = "Permisos Nuevo Rol:";
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnAsignarUsuario);
            this.tabPage2.Controls.Add(this.clbRolesParaAsignar);
            this.tabPage2.Controls.Add(this.combobox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(788, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Asignación a Usuarios";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnAsignarUsuario
            // 
            this.btnAsignarUsuario.Location = new System.Drawing.Point(298, 191);
            this.btnAsignarUsuario.Name = "btnAsignarUsuario";
            this.btnAsignarUsuario.Size = new System.Drawing.Size(189, 98);
            this.btnAsignarUsuario.TabIndex = 2;
            this.btnAsignarUsuario.Text = "Asignar a Usuario";
            this.btnAsignarUsuario.UseVisualStyleBackColor = true;
            this.btnAsignarUsuario.Click += new System.EventHandler(this.btnAsignarUsuario_Click_1);
            // 
            // clbRolesParaAsignar
            // 
            this.clbRolesParaAsignar.FormattingEnabled = true;
            this.clbRolesParaAsignar.Location = new System.Drawing.Point(554, 66);
            this.clbRolesParaAsignar.Name = "clbRolesParaAsignar";
            this.clbRolesParaAsignar.Size = new System.Drawing.Size(228, 424);
            this.clbRolesParaAsignar.TabIndex = 1;
            // 
            // combobox
            // 
            this.combobox.FormattingEnabled = true;
            this.combobox.Location = new System.Drawing.Point(329, 132);
            this.combobox.Name = "combobox";
            this.combobox.Size = new System.Drawing.Size(121, 21);
            this.combobox.TabIndex = 0;
            // 
            // GestionarRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 527);
            this.Controls.Add(this.GestionRoles);
            this.Name = "GestionarRoles";
            this.Text = "GestionarRoles";
            this.Load += new System.EventHandler(this.GestionarRoles_Load);
            this.GestionRoles.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnAsignarUsuario;
        private System.Windows.Forms.CheckedListBox clbRolesParaAsignar;
        private System.Windows.Forms.ComboBox combobox;
    }
}