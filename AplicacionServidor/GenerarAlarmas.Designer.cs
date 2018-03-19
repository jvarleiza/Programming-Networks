namespace AplicacionServidor
{
    partial class GenerarAlarmas
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
            this.lstClientesConectados = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numTimer = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbRemota = new System.Windows.Forms.RadioButton();
            this.rbLocal = new System.Windows.Forms.RadioButton();
            this.lblCliente1 = new System.Windows.Forms.Label();
            this.lblCliente2 = new System.Windows.Forms.Label();
            this.btnEnviarAlarma = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnRecargar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numTimer)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstClientesConectados
            // 
            this.lstClientesConectados.FormattingEnabled = true;
            this.lstClientesConectados.Location = new System.Drawing.Point(48, 45);
            this.lstClientesConectados.Name = "lstClientesConectados";
            this.lstClientesConectados.Size = new System.Drawing.Size(138, 134);
            this.lstClientesConectados.TabIndex = 0;
            this.lstClientesConectados.SelectedIndexChanged += new System.EventHandler(this.lstClientesConectados_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Clientes:";
            // 
            // numTimer
            // 
            this.numTimer.Location = new System.Drawing.Point(207, 159);
            this.numTimer.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numTimer.Name = "numTimer";
            this.numTimer.Size = new System.Drawing.Size(123, 20);
            this.numTimer.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRemota);
            this.groupBox1.Controls.Add(this.rbLocal);
            this.groupBox1.Location = new System.Drawing.Point(207, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 108);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de alarma";
            // 
            // rbRemota
            // 
            this.rbRemota.AutoSize = true;
            this.rbRemota.Location = new System.Drawing.Point(6, 70);
            this.rbRemota.Name = "rbRemota";
            this.rbRemota.Size = new System.Drawing.Size(103, 17);
            this.rbRemota.TabIndex = 1;
            this.rbRemota.TabStop = true;
            this.rbRemota.Text = "Generar Remota";
            this.rbRemota.UseVisualStyleBackColor = true;
            this.rbRemota.CheckedChanged += new System.EventHandler(this.rbRemota_CheckedChanged);
            // 
            // rbLocal
            // 
            this.rbLocal.AutoSize = true;
            this.rbLocal.Checked = true;
            this.rbLocal.Location = new System.Drawing.Point(6, 34);
            this.rbLocal.Name = "rbLocal";
            this.rbLocal.Size = new System.Drawing.Size(92, 17);
            this.rbLocal.TabIndex = 0;
            this.rbLocal.TabStop = true;
            this.rbLocal.Text = "Generar Local";
            this.rbLocal.UseVisualStyleBackColor = true;
            this.rbLocal.CheckedChanged += new System.EventHandler(this.rbLocal_CheckedChanged);
            // 
            // lblCliente1
            // 
            this.lblCliente1.AutoSize = true;
            this.lblCliente1.Location = new System.Drawing.Point(45, 197);
            this.lblCliente1.Name = "lblCliente1";
            this.lblCliente1.Size = new System.Drawing.Size(0, 13);
            this.lblCliente1.TabIndex = 6;
            // 
            // lblCliente2
            // 
            this.lblCliente2.AutoSize = true;
            this.lblCliente2.Location = new System.Drawing.Point(45, 225);
            this.lblCliente2.Name = "lblCliente2";
            this.lblCliente2.Size = new System.Drawing.Size(0, 13);
            this.lblCliente2.TabIndex = 7;
            // 
            // btnEnviarAlarma
            // 
            this.btnEnviarAlarma.Location = new System.Drawing.Point(207, 251);
            this.btnEnviarAlarma.Name = "btnEnviarAlarma";
            this.btnEnviarAlarma.Size = new System.Drawing.Size(123, 24);
            this.btnEnviarAlarma.TabIndex = 8;
            this.btnEnviarAlarma.Text = "Enviar";
            this.btnEnviarAlarma.UseVisualStyleBackColor = true;
            this.btnEnviarAlarma.Click += new System.EventHandler(this.btnEnviarAlarma_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(48, 251);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(66, 24);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnRecargar
            // 
            this.btnRecargar.Location = new System.Drawing.Point(120, 251);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Size = new System.Drawing.Size(66, 23);
            this.btnRecargar.TabIndex = 10;
            this.btnRecargar.Text = "Recargar";
            this.btnRecargar.UseVisualStyleBackColor = true;
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            // 
            // GenerarAlarmas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(391, 287);
            this.Controls.Add(this.btnRecargar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEnviarAlarma);
            this.Controls.Add(this.lblCliente2);
            this.Controls.Add(this.lblCliente1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numTimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstClientesConectados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GenerarAlarmas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Alarmas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GenerarAlarmas_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numTimer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstClientesConectados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbRemota;
        private System.Windows.Forms.RadioButton rbLocal;
        private System.Windows.Forms.Label lblCliente1;
        private System.Windows.Forms.Label lblCliente2;
        private System.Windows.Forms.Button btnEnviarAlarma;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnRecargar;
    }
}