namespace ClienteOperacionMantenimiento
{
    partial class UCInfoCliente
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstClientes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstAlarmas = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numTiempo = new System.Windows.Forms.NumericUpDown();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnModificar = new System.Windows.Forms.Button();
            this.lstTimer = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numTiempo)).BeginInit();
            this.SuspendLayout();
            // 
            // lstClientes
            // 
            this.lstClientes.FormattingEnabled = true;
            this.lstClientes.HorizontalScrollbar = true;
            this.lstClientes.Location = new System.Drawing.Point(30, 75);
            this.lstClientes.Name = "lstClientes";
            this.lstClientes.Size = new System.Drawing.Size(147, 212);
            this.lstClientes.TabIndex = 0;
            this.lstClientes.SelectedIndexChanged += new System.EventHandler(this.lstClientes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Clientes:";
            // 
            // lstAlarmas
            // 
            this.lstAlarmas.FormattingEnabled = true;
            this.lstAlarmas.Location = new System.Drawing.Point(193, 75);
            this.lstAlarmas.Name = "lstAlarmas";
            this.lstAlarmas.Size = new System.Drawing.Size(403, 212);
            this.lstAlarmas.TabIndex = 2;
            this.lstAlarmas.SelectedIndexChanged += new System.EventHandler(this.lstAlarmas_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Alarmas Configuradas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 304);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Timer inicial:";
            // 
            // numTiempo
            // 
            this.numTiempo.Location = new System.Drawing.Point(265, 302);
            this.numTiempo.Margin = new System.Windows.Forms.Padding(2);
            this.numTiempo.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numTiempo.Name = "numTiempo";
            this.numTiempo.Size = new System.Drawing.Size(80, 20);
            this.numTiempo.TabIndex = 8;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(474, 297);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(122, 27);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "Modificar alarma";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // lstTimer
            // 
            this.lstTimer.FormattingEnabled = true;
            this.lstTimer.Location = new System.Drawing.Point(603, 75);
            this.lstTimer.Name = "lstTimer";
            this.lstTimer.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstTimer.Size = new System.Drawing.Size(45, 212);
            this.lstTimer.TabIndex = 11;
            // 
            // UCInfoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstTimer);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.numTiempo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstAlarmas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstClientes);
            this.Name = "UCInfoCliente";
            this.Size = new System.Drawing.Size(704, 379);
            ((System.ComponentModel.ISupportInitialize)(this.numTiempo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstClientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstAlarmas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numTiempo;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.ListBox lstTimer;
    }
}
