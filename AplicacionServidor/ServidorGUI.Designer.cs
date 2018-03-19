using System;
namespace AplicacionServidor
{
    partial class ServidorGUI
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
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (InvalidOperationException)
            {
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMensajesClientes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstClientes = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.alarmasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarAlarmasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedirArchivosDeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMensajes
            // 
            this.txtMensajes.Location = new System.Drawing.Point(240, 66);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ReadOnly = true;
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMensajes.Size = new System.Drawing.Size(435, 95);
            this.txtMensajes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mensajes Servidor:";
            // 
            // txtMensajesClientes
            // 
            this.txtMensajesClientes.Location = new System.Drawing.Point(240, 191);
            this.txtMensajesClientes.Multiline = true;
            this.txtMensajesClientes.Name = "txtMensajesClientes";
            this.txtMensajesClientes.ReadOnly = true;
            this.txtMensajesClientes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMensajesClientes.Size = new System.Drawing.Size(435, 178);
            this.txtMensajesClientes.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mensajes Clientes:";
            // 
            // lstClientes
            // 
            this.lstClientes.FormattingEnabled = true;
            this.lstClientes.Location = new System.Drawing.Point(26, 66);
            this.lstClientes.Name = "lstClientes";
            this.lstClientes.Size = new System.Drawing.Size(169, 264);
            this.lstClientes.TabIndex = 5;
            this.lstClientes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstClientes_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Clientes:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(120, 346);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(25, 346);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 11;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alarmasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(705, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // alarmasToolStripMenuItem
            // 
            this.alarmasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarAlarmasToolStripMenuItem,
            this.pedirArchivosDeLogToolStripMenuItem});
            this.alarmasToolStripMenuItem.Name = "alarmasToolStripMenuItem";
            this.alarmasToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.alarmasToolStripMenuItem.Text = "Opciones";
            // 
            // generarAlarmasToolStripMenuItem
            // 
            this.generarAlarmasToolStripMenuItem.Name = "generarAlarmasToolStripMenuItem";
            this.generarAlarmasToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.generarAlarmasToolStripMenuItem.Text = "Generar Alarmas";
            this.generarAlarmasToolStripMenuItem.Click += new System.EventHandler(this.generarAlarmasToolStripMenuItem_Click);
            // 
            // pedirArchivosDeLogToolStripMenuItem
            // 
            this.pedirArchivosDeLogToolStripMenuItem.Name = "pedirArchivosDeLogToolStripMenuItem";
            this.pedirArchivosDeLogToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.pedirArchivosDeLogToolStripMenuItem.Text = "Pedir Archivos de Log";
            this.pedirArchivosDeLogToolStripMenuItem.Click += new System.EventHandler(this.pedirArchivosDeLogToolStripMenuItem_Click);
            // 
            // ServidorGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(705, 381);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lstClientes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMensajesClientes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMensajes);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ServidorGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servidor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServidorGUI_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMensajes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMensajesClientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstClientes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem alarmasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarAlarmasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedirArchivosDeLogToolStripMenuItem;
    }
}

