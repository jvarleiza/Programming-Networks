namespace ClienteOperacionMantenimiento
{
    partial class UCEstadisticas
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLogins = new System.Windows.Forms.Label();
            this.lblCantAlarmasLocales = new System.Windows.Forms.Label();
            this.lblCantAlarmasRemotas = new System.Windows.Forms.Label();
            this.lblLogouts = new System.Windows.Forms.Label();
            this.timerActualizaInfo = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cantidad de logins totales en el sistema:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cantidad total de alarmas recibidas locales:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cantidad total de desconexiones:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cantidad total de alarmas recibidas remotas:";
            // 
            // lblLogins
            // 
            this.lblLogins.AutoSize = true;
            this.lblLogins.Location = new System.Drawing.Point(497, 118);
            this.lblLogins.Name = "lblLogins";
            this.lblLogins.Size = new System.Drawing.Size(13, 13);
            this.lblLogins.TabIndex = 4;
            this.lblLogins.Text = "0";
            // 
            // lblCantAlarmasLocales
            // 
            this.lblCantAlarmasLocales.AutoSize = true;
            this.lblCantAlarmasLocales.Location = new System.Drawing.Point(497, 157);
            this.lblCantAlarmasLocales.Name = "lblCantAlarmasLocales";
            this.lblCantAlarmasLocales.Size = new System.Drawing.Size(13, 13);
            this.lblCantAlarmasLocales.TabIndex = 5;
            this.lblCantAlarmasLocales.Text = "0";
            // 
            // lblCantAlarmasRemotas
            // 
            this.lblCantAlarmasRemotas.AutoSize = true;
            this.lblCantAlarmasRemotas.Location = new System.Drawing.Point(497, 195);
            this.lblCantAlarmasRemotas.Name = "lblCantAlarmasRemotas";
            this.lblCantAlarmasRemotas.Size = new System.Drawing.Size(13, 13);
            this.lblCantAlarmasRemotas.TabIndex = 6;
            this.lblCantAlarmasRemotas.Text = "0";
            // 
            // lblLogouts
            // 
            this.lblLogouts.AutoSize = true;
            this.lblLogouts.Location = new System.Drawing.Point(497, 230);
            this.lblLogouts.Name = "lblLogouts";
            this.lblLogouts.Size = new System.Drawing.Size(13, 13);
            this.lblLogouts.TabIndex = 7;
            this.lblLogouts.Text = "0";
            // 
            // timerActualizaInfo
            // 
            this.timerActualizaInfo.Interval = 5000;
            this.timerActualizaInfo.Tick += new System.EventHandler(this.timerActualizaInfo_Tick);
            // 
            // UCEstadisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLogouts);
            this.Controls.Add(this.lblCantAlarmasRemotas);
            this.Controls.Add(this.lblCantAlarmasLocales);
            this.Controls.Add(this.lblLogins);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCEstadisticas";
            this.Size = new System.Drawing.Size(704, 379);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLogins;
        private System.Windows.Forms.Label lblCantAlarmasLocales;
        private System.Windows.Forms.Label lblCantAlarmasRemotas;
        private System.Windows.Forms.Label lblLogouts;
        private System.Windows.Forms.Timer timerActualizaInfo;
    }
}
