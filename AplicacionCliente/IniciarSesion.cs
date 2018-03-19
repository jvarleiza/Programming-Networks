using LogicaNegocio;
using Logs;
using Protocolo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionCliente
{  
    public partial class IniciarSesion : Form
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }


        private int MakeConnection(string IPaddr, int port)
        {
            string identificacion = txtId.Text;
            HelperCliente.SetIdCliente(identificacion);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(IPaddr), port);
            try
            {
                HelperCliente.Instancia().SocketCliente = new Socket(ipEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                if (HelperCliente.Instancia().SocketCliente == null)
                {
                    MessageBox.Show(String.Format("No se puede crear el socket IP:{0}, Port:{1}", ipEnd.Address, ipEnd.Port));
                    return 1;
                }
                HelperCliente.Instancia().SocketCliente.Connect(ipEnd);
            }
            catch (SocketException e)
            {
                MessageBox.Show("Error, " + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, " + e.Message);
            }
            return 0;
        }

        private void BtnIniciarSesion()
        {
            string ipServidor = ConfigurationManager.AppSettings["ipServidor"];
            int puertoComunicacion;
            bool parseo = Int32.TryParse(ConfigurationManager.AppSettings["puertoComunicacion"], out puertoComunicacion);

            if (MakeConnection(ipServidor, puertoComunicacion) != 0  && !parseo)
            {
                MessageBox.Show("Falló la conexión");
                return;
            }
            try
            {
                string identificacion = txtId.Text;
                if (identificacion != "" && identificacion.Length > 0 && identificacion.Length < 100)
                {
                    HelperCliente.SetIdCliente(identificacion);
                    string datos = identificacion + "$";
                    ManejadorProtocolo mp = new ManejadorProtocolo("REQ", 1, datos.Length, datos);
                    string REQ = mp.EnviarMensaje(HelperCliente.Instancia().SocketCliente);
                    mp.RecibirMensaje(HelperCliente.Instancia().SocketCliente);
                    string[] RES = mp.Datos.Split('$');
                    if (REQ.Split(';')[0] == "OK" && mp.Header == "RES" && RES[0] == "OK")
                    {
                        Principal ventana = new Principal(this, identificacion);
                        ventana.Show();
                        this.Hide();
                        HelperCliente.Instancia().InstanciaLog.Log("Login de " + identificacion);
                        HelperCliente.Instancia().EncolarUnaConexion(identificacion);
                    }
                    else
                    {
                        MessageBox.Show(RES[1]);
                    }
                }
                else
                {
                    MessageBox.Show("La ID no puede ser vacía");
                }
            }
            catch (ExceptionProtocolo)
            {
                MessageBox.Show("El servidor no está conectado");
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            BtnIniciarSesion();
        }

        private void txtId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnIniciarSesion();
            }
        }

        private void IniciarSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }       
}
