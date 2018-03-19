using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaNegocio;

namespace ClienteOperacionMantenimiento
{
    public partial class UCInfoCliente : UserControl
    {
        public UCInfoCliente(Principal pri)
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                lstClientes.Items.Clear();
                List<string> clientes = ConsumidorServicios.GetClientes();
                foreach (string c in clientes)
                {
                    lstClientes.Items.Add(c);
                }
            }
            catch (ExceptionNegocio ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<string> ObtenerAlarmasDeUnCliente(string idCliente)
        {
            return ConsumidorServicios.GetAlarmasConfiguradas(idCliente);
        }

        private void CargarAlarmas(string idCliente)
        {
            try
            {
                lstAlarmas.Items.Clear();
                List<string> alarmasCli = ObtenerAlarmasDeUnCliente(idCliente);
                if (alarmasCli != null)
                {
                    foreach (string a in alarmasCli)
                    {
                        string[] datos = a.Split('$');
                        Alarma alarma = new Alarma();

                        alarma.AlarmaId = datos[0];
                        alarma.EsLocal = Boolean.Parse(datos[1]);
                        alarma.HoraConfigurada = DateTime.Parse(datos[2]);
                        if (alarma.EsLocal)
                        {
                            alarma.IdClienteReceptor = datos[3];
                            alarma.Timer = Int32.Parse(datos[4]);
                            alarma.YaFueDisparada = Boolean.Parse(datos[5]);
                        }
                        else
                        {
                            alarma.IdClienteReceptor = datos[3];
                            alarma.IdClienteRemoto = datos[4];
                            alarma.Timer = Int32.Parse(datos[5]);
                            alarma.YaFueDisparada = Boolean.Parse(datos[6]);
                        }
                        lstAlarmas.Items.Add(alarma);
                    }
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("No se pudo parear un dato: " + fe.Message);
            }
            catch (ExceptionNegocio ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void lstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClientes.SelectedIndex > -1)
            {
                string idCliente = lstClientes.SelectedItem as string;
                CargarAlarmas(idCliente);
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (lstClientes.SelectedIndex > -1)
            {
                lstTimer.Items.Clear();
                string idCli = lstClientes.SelectedItem as string;
                List<string> alarmas = ObtenerAlarmasDeUnCliente(idCli);

                if (alarmas != null)
                {
                    foreach (string alarmaString in alarmas)
                    {
                        string[] datos = alarmaString.Split('$');
                        bool esLocal = Boolean.Parse(datos[1]);
                        DateTime horaConfig;
                        int timer;

                        if (esLocal)
                        {
                            horaConfig = DateTime.Parse(datos[2]);
                            timer = Int32.Parse(datos[4]);
                        }
                        else
                        {
                            horaConfig = DateTime.Parse(datos[2]);
                            timer = Int32.Parse(datos[5]);
                        }

                        int num = (int)(timer - (DateTime.Now - horaConfig).TotalSeconds);
                        lstTimer.Items.Add(num);
                    }

                    if (lstTimer.Items.Count > lstAlarmas.Items.Count || lstTimer.Items.Count < lstAlarmas.Items.Count)
                    {
                        CargarAlarmas(idCli);
                    }
                    if (lstTimer.Items.Count == 0)
                    {
                        lstAlarmas.Items.Clear();
                    }
                }
            }
        }

        private void lstAlarmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAlarmas.SelectedIndex > -1)
            {
                Alarma a = lstAlarmas.SelectedItem as Alarma;
                numTiempo.Value = a.Timer;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (lstAlarmas.SelectedIndex > -1)
            {
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (lstAlarmas.SelectedIndex > -1 && lstClientes.SelectedIndex > -1)
            {
                if (numTiempo.Value > -1)
                {
                    try
                    {
                        Alarma a = lstAlarmas.SelectedItem as Alarma;
                        a.Timer = (int)numTiempo.Value;
                        ConsumidorServicios.ModificarAlarma(a);
                        CargarAlarmas(lstClientes.SelectedItem as string);
                    }
                    catch (ExceptionNegocio ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe haber un cliente y una alama seleccionada para modificar la alarma");
            }
        }
    }
}
