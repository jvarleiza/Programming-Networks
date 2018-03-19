using LogicaNegocio;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionServidor
{
    public partial class ServidorGUI : Form
    {
        static readonly object synclock = new object();
        delegate void MostrarMensajeCallback(string text);
        delegate void CargarClientesCallback();
        string mensajes = "";
        string mensajesClientes = "";
        Cliente seleccionado = null;
        Thread threadTemporario;
        bool cerrarServidor;

        public ServidorGUI()
        {
            InitializeComponent();
            IniciarServicios();
            CargarClientesThrSafe();
            cerrarServidor = false;
        }

        private void IniciarServicios()
        {
            try
            {
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(HelperServidor.ipAddr), HelperServidor.port);
                HelperServidor.myServer = new Socket(ipEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                if (HelperServidor.myServer == null)
                {
                    MostrarMensaje(String.Format("No se puede crear el socket servidor, IP:{0}, Port:{1}", ipEnd.Address, ipEnd.Port));
                }
                MostrarMensaje(String.Format("Servidor escuchando en IP:{0}, Port:{1}", ipEnd.Address, ipEnd.Port));
                HelperServidor.myServer.Bind(ipEnd);
                HelperServidor.myServer.Listen(2);

                Thread threadEscuchaConexiones = new Thread(ThrEscucharConexiones);
                threadEscuchaConexiones.Start();
            }
            catch (SocketException ex)
            {
                MostrarMensaje("Source : " + ex.Source);
                MostrarMensaje("Message : " + ex.Message);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Source : " + ex.Source);
                MostrarMensaje("Message : " + ex.Message);
            }
        }

        private void CargarClientesThrSafe()
        {
            if (!cerrarServidor)
            {
                if (this.lstClientes.InvokeRequired)
                {
                    CargarClientesCallback d = new CargarClientesCallback(CargarClientesThrSafe);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    lstClientes.Items.Clear();
                    List<Cliente> clientes = Sistema.Instancia().ObtenerClientes();
                    foreach (Cliente c in clientes)
                    {
                        lstClientes.Items.Add(c);
                    }
                }
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            if (mensajes == "")
            {
                mensajes += mensaje;
            }
            else
            {
                mensajes += Environment.NewLine + mensaje;
            }
            try
            {
                txtMensajes.Text = mensajes;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void MensajesClientesThrSafe(string mensaje)
        {
            if (!cerrarServidor)
            {
                if (this.txtMensajesClientes.InvokeRequired)
                {
                    MostrarMensajeCallback d = new MostrarMensajeCallback(MensajesClientesThrSafe);
                    this.Invoke(d, new object[] { mensaje });
                }
                else
                {
                    if (mensajesClientes == "")
                    {
                        mensajesClientes += mensaje;
                    }
                    else
                    {
                        mensajesClientes += Environment.NewLine + mensaje;
                    }
                    txtMensajesClientes.Text = mensajesClientes;
                }
            }
        }

        private void CMDLogin(ManejadorProtocolo protocolo, string resultado, Socket sock, Thread thr)
        {
            string cuerpo;
            string[] resultadoArray = resultado.Split(';');
            if (resultadoArray[0] == "OK")
            {
                string[] identificador = protocolo.Datos.Split('$');
                Cliente unCli = Sistema.Instancia().ObtenerCliente(identificador[0]);
                Sistema.Instancia().AgregarClienteConectado(unCli,sock, thr);
                cuerpo = "OK$";
            }
            else
            {
                cuerpo = "ERROR$Error al iniciar sesion";
            }
            MensajesClientesThrSafe(resultado);
            CargarClientesThrSafe();
            ManejadorProtocolo mp = new ManejadorProtocolo("RES", 1, cuerpo.Length, cuerpo);
            mp.EnviarMensaje(sock);
        }

        private void CMDAlarmaRemota(ManejadorProtocolo protocolo, string resultado)
        {
            string[] resultadoArray = resultado.Split(';');
            MensajesClientesThrSafe("Alarma remota programada para: "+resultadoArray[1]);
        }

        private bool CMDProcesarLog(string texto)
        {
            var archivo = ConfigurationManager.AppSettings["directorioLogs"]+seleccionado.Identificacion
                +"_"+DateTime.Now.Day
                +"_"+DateTime.Now.Month
                +"_"+DateTime.Now.Year
                +"_"+DateTime.Now.Hour
                +"." + DateTime.Now.Minute
                +".txt";
            StreamWriter file = new StreamWriter(archivo, true);
            file.WriteLine(texto);
            file.Close();

            if (texto.Contains("*FIN*"))
            {
                return true;
            }
            return false;
        }


        private void ThrComunicarConCliente(Socket unSocket)
        {
            bool clienteConectado = true;
            try
            {
                while (clienteConectado)
                {
                    ManejadorProtocolo protocolo = new ManejadorProtocolo();
                    protocolo.RecibirMensaje(unSocket);

                    if (protocolo.CMD != 0)
                    {
                        string resultado = Sistema.Instancia().Ejecutar(protocolo);
                        switch (protocolo.CMD)
                        {
                            //Login de cliente
                            case 1:
                                CMDLogin(protocolo, resultado, unSocket, threadTemporario);
                                break;

                            //Alarma remota
                            case 3:
                                CMDAlarmaRemota(protocolo, resultado);
                                break;

                            case 4:
                                if (CMDProcesarLog(protocolo.Datos))
                                {
                                    MensajesClientesThrSafe("Se recibió archivo de log de " + seleccionado.Identificacion);
                                }
                                break;
                        }
                    }
                    else//Desconectar cliente
                    {
                        Sistema.Instancia().BorrarClienteConectado(unSocket);
                        MensajesClientesThrSafe("Se ha desconectado un cliente");
                        CargarClientesThrSafe();
                        clienteConectado = false;
                    }
                }
            }
            catch(ExceptionProtocolo ex)
            {
                //Sistema.Instancia().BorrarClienteConectado(unSocket);
                MensajesClientesThrSafe(ex.Message);
                CargarClientesThrSafe();
            }
        }

        private void ThrEscucharConexiones()
        {
            try
            {
                while (!cerrarServidor)
                {
                    Socket socketTemporario = HelperServidor.myServer.Accept();
                    Thread th_1 = new Thread(() => ThrComunicarConCliente(socketTemporario));
                    threadTemporario = th_1;
                    th_1.Start();
                }
            }
            catch (SocketException ex)
            {
                MostrarMensaje("Source : " + ex.Source);
                MostrarMensaje("Message : " + ex.Message);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Source : " + ex.Source);
                MostrarMensaje("Message : " + ex.Message);
            }
        }

        private void lstClientes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstClientes.SelectedIndex > -1)
            {
                Cliente seleccionado = lstClientes.SelectedItem as Cliente;
                if (seleccionado != null && !seleccionado.Configurado)
                {
                    MantenimientoClientes mc = new MantenimientoClientes(false, seleccionado.Identificacion);
                    mc.ShowDialog();
                    CargarClientesThrSafe();
                }
                else
                {
                    MessageBox.Show("El cliente debe estar desconectado para ser modificado");
                } 
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MantenimientoClientes mc = new MantenimientoClientes(true, "");
            mc.ShowDialog();
            CargarClientesThrSafe();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstClientes.SelectedIndex > -1)
            {
                Cliente seleccionado = lstClientes.SelectedItem as Cliente;
                if (seleccionado != null && !seleccionado.Configurado)
                {
                    Sistema.Instancia().EliminarCliente(seleccionado);
                    CargarClientesThrSafe();
                }
                else
                {
                    MessageBox.Show("El cliente debe estar desconectado para ser eliminado");
                }                
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente");
            }
        }

        private void generarAlarmasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstClientes.Items.Count > 0)
            {
                GenerarAlarmas ventana = new GenerarAlarmas(this);
                ventana.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Debe haber clientes para generar alarmas");
            }
        }

        private void ServidorGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                cerrarServidor = true;
                Thread.Sleep(1000);
                HelperServidor.myServer.Close();
                Sistema.Instancia().CerrarConexiones();
            }
            catch(Exception)
            {

            }
        }

        private void pedirArchivosDeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstClientes.SelectedIndex > -1)
            {
                Cliente unCli = lstClientes.SelectedItem as Cliente;
                if (unCli.Configurado)
                {
                    try
                    {
                        Sistema.Instancia().SolicitarArchivo(unCli);
                        seleccionado = unCli;
                    }
                    catch (ExceptionProtocolo ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("El cliente no está configurado");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente");
            }
        }

               
    }
}
