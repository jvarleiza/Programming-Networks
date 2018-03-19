namespace AplicacionCliente
{
    partial class Principal
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
            this.lblAlarma = new System.Windows.Forms.Label();
            this.picAlarma = new System.Windows.Forms.PictureBox();
            this.btnDesconectar = new System.Windows.Forms.Button();
            this.txtMensajes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picAlarma)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAlarma
            // 
            this.lblAlarma.AutoSize = true;
            this.lblAlarma.Location = new System.Drawing.Point(95, 48);
            this.lblAlarma.Name = "lblAlarma";
            this.lblAlarma.Size = new System.Drawing.Size(53, 13);
            this.lblAlarma.TabIndex = 0;
            this.lblAlarma.Text = "NORMAL";
            // 
            // picAlarma
            // 
            this.picAlarma.Location = new System.Drawing.Point(91, 73);
            this.picAlarma.Margin = new System.Windows.Forms.Padding(2);
            this.picAlarma.Name = "picAlarma";
            this.picAlarma.Size = new System.Drawing.Size(57, 57);
            this.picAlarma.TabIndex = 1;
            this.picAlarma.TabStop = false;
            // 
            // btnDesconectar
            // 
            this.btnDesconectar.Location = new System.Drawing.Point(77, 12);
            this.btnDesconectar.Name = "btnDesconectar";
            this.btnDesconectar.Size = new System.Drawing.Size(83, 23);
            this.btnDesconectar.TabIndex = 2;
            this.btnDesconectar.Text = "Desconectar";
            this.btnDesconectar.UseVisualStyleBackColor = true;
            this.btnDesconectar.Click += new System.EventHandler(this.btnDesconectar_Click);
            // 
            // txtMensajes
            // 
            this.txtMensajes.Location = new System.Drawing.Point(12, 144);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMensajes.Size = new System.Drawing.Size(216, 154);
            this.txtMensajes.TabIndex = 3;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(241, 310);
            this.ControlBox = false;
            this.Controls.Add(this.txtMensajes);
            this.Controls.Add(this.btnDesconectar);
            this.Controls.Add(this.picAlarma);
            this.Controls.Add(this.lblAlarma);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Principal_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picAlarma)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAlarma;
        private System.Windows.Forms.PictureBox picAlarma;
        private System.Windows.Forms.Button btnDesconectar;
        private System.Windows.Forms.TextBox txtMensajes;
    }
}