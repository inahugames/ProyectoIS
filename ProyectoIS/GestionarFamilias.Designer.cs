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
            this.btnEliminarFamilia = new System.Windows.Forms.Button();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.chklist = new System.Windows.Forms.CheckedListBox();
            this.listFamilias = new System.Windows.Forms.ListBox();
            this.tabPermisosFamilia = new System.Windows.Forms.TabPage();
            this.lblPermisosDeFamilia = new System.Windows.Forms.Label();
            this.lbPermisosDeFamilia = new System.Windows.Forms.ListBox();
            this.btnQuitarPermisoFamilia = new System.Windows.Forms.Button();
            this.btnAgregarPermisoFamilia = new System.Windows.Forms.Button();
            this.lblPermisosDisponiblesFam = new System.Windows.Forms.Label();
            this.clbPermisosDisponiblesFam = new System.Windows.Forms.CheckedListBox();
            this.lblFamiliasGestion = new System.Windows.Forms.Label();
            this.lbFamiliasGestion = new System.Windows.Forms.ListBox();
            this.tabsGestionFamilias.SuspendLayout();
            this.tabCrearFamilia.SuspendLayout();
            this.tabPermisosFamilia.SuspendLayout();
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabsGestionFamilias;
        private System.Windows.Forms.TabPage tabCrearFamilia;
        private System.Windows.Forms.ListBox listFamilias;
        private System.Windows.Forms.CheckedListBox chklist;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Button btnEliminarFamilia;
        private System.Windows.Forms.TabPage tabPermisosFamilia;
        private System.Windows.Forms.ListBox lbFamiliasGestion;
        private System.Windows.Forms.Label lblFamiliasGestion;
        private System.Windows.Forms.CheckedListBox clbPermisosDisponiblesFam;
        private System.Windows.Forms.Label lblPermisosDisponiblesFam;
        private System.Windows.Forms.Button btnAgregarPermisoFamilia;
        private System.Windows.Forms.Button btnQuitarPermisoFamilia;
        private System.Windows.Forms.ListBox lbPermisosDeFamilia;
        private System.Windows.Forms.Label lblPermisosDeFamilia;
    }
}
