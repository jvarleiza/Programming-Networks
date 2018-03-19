using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteOperacionMantenimiento
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            this.panelContenedor.Controls.Add(new UCInicio(this));
        }

        private void infoDeLosClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panelContenedor.Controls.Clear();
            this.panelContenedor.Controls.Add(new UCInfoCliente(this));
        }

        private void estadisticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panelContenedor.Controls.Clear();
            this.panelContenedor.Controls.Add(new UCEstadisticas(this));
        }

       

        
    }
}
