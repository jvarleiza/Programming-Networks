using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteOperacionMantenimiento
{
    public partial class UCEstadisticas : UserControl
    {
        public UCEstadisticas(Principal pri)
        {
            InitializeComponent();
            ActualizarDatos();
            timerActualizaInfo.Start();
        }

        void ActualizarDatos()
        {
            int[] datos = ConsumidorServicios.ObtenerEstadisticas();
            lblLogins.Text = datos[0].ToString();
            lblCantAlarmasLocales.Text = datos[1].ToString();
            lblCantAlarmasRemotas.Text = datos[2].ToString();
            lblLogouts.Text = datos[3].ToString();
        }

        private void timerActualizaInfo_Tick(object sender, EventArgs e)
        {
            ActualizarDatos();
        }        
    }
}
