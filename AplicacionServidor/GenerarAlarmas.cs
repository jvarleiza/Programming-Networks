using LogicaNegocio;
using Protocolo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionServidor
{
    public partial class GenerarAlarmas : Form
    {
        private Cliente cliente1 = null;
        private Cliente cliente2 = null;
        ServidorGUI inicio;

        public GenerarAlarmas(ServidorGUI gui)
        {
            InitializeComponent();
            CargarClientesConectados();
            inicio = gui;
        }

        private void CargarClientesConectados()
        {
            lstClientesConectados.Items.Clear();
            foreach (Cliente c in Sistema.Instancia().Clientes.Where(x => x.Configurado))
            {
                lstClientesConectados.Items.Add(c);
            }
        }

        private void lstClientesConectados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClientesConectados.SelectedIndex > -1)
            {
                Cliente unCli = lstClientesConectados.SelectedItem as Cliente;
                if (rbLocal.Checked)
                {
                    lblCliente1.Text = unCli.Identificacion;
                    cliente1 = unCli;
                }
                else
                {
                    if (lblCliente1.Text == String.Empty)
                    {
                        if (lblCliente2.Text == String.Empty)
                        {
                            lblCliente1.Text = "Cliente intermedio: "+ unCli.Identificacion;
                            cliente1 = unCli;
                        }
                        else
                        {
                            lblCliente2.Text = "Cliente remoto: "+ unCli.Identificacion;
                            cliente2 = unCli;
                        }
                    }
                    else
                    {
                        if (lblCliente1.Text != String.Empty && lblCliente2.Text == String.Empty)
                        {
                            lblCliente2.Text = "Cliente remoto: " + unCli.Identificacion;
                            cliente2 = unCli;
                        }
                    }
                }            
            }
        }

        private void Limpiar()
        {
            lblCliente1.Text = String.Empty;
            lblCliente2.Text = String.Empty;
            cliente1 = null;
            cliente2 = null;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void rbLocal_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void rbRemota_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEnviarAlarma_Click(object sender, EventArgs e)
        {
            if (rbLocal.Checked)
            {
                if (cliente1 != null)
                {
                    try
                    {
                        Sistema.Instancia().EnviarAlarmaLocal(cliente1, (int)numTimer.Value);
                    }
                    catch (ExceptionProtocolo ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un cliente");
                }
            }
            else
            {
                if (cliente1 != null)
                {
                    if (cliente2 != null)
                    {
                        try
                        {
                            if (cliente1.Identificacion != cliente2.Identificacion)
                            {
                                Sistema.Instancia().EnviarAlarmaRemota(cliente1, cliente2, (int)numTimer.Value);
                            }
                            else
                            {
                                MessageBox.Show("Los clientes deben ser distintos");
                                Limpiar();
                            }
                        }
                        catch (ExceptionProtocolo ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar el cliente remoto");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar el cliente intermediario");
                }
            }
        }

        private void GenerarAlarmas_FormClosing(object sender, FormClosingEventArgs e)
        {
            inicio.Show();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            CargarClientesConectados();
        }
    }
}
