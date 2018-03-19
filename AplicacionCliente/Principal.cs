using LogicaNegocio;
using Protocolo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Messaging;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionCliente
{
    public partial class Principal : Form
    {
        //delegates para manejar la concurrencia con los componentes del Form
        delegate void LblAlarmaCallback(string text);
        delegate void TxtAlarmaCallback(string text);
        delegate void ImgAlarmaCallback(bool muestro);
        delegate void FormPrincipalCallback(bool show);
        delegate void FormInicialCallback(bool show);
        // Sonido alarma
        SoundPlayer sonido = new SoundPlayer();

        private bool estaConectado = true;
        private IniciarSesion sesion;
        private string cliente;
        private string mensajesAcumulados = "";
        

        public Principal(IniciarSesion unaSesion, string identificacion)
        {
            InitializeComponent();

            cliente = identificacion;
            this.Text = identificacion;
            sesion = unaSesion;

            // Animaciones alarma
            string rutaImg = ConfigurationManager.AppSettings["rutaImagen"];
            picAlarma.Load(rutaImg);
            picAlarma.Visible = false;
            string rutaSon = ConfigurationManager.AppSettings["rutaSonido"];
            sonido.SoundLocation = rutaSon;

            Thread thrEscucharMensajes = new Thread(EscucharMensajes);
            thrEscucharMensajes.Start();

            Thread thrEscucharCola = new Thread(EscucharCola);
            thrEscucharCola.Start();
        }

        private void EscucharCola()
        {
            bool estaEscuchando = true;
            while (estaEscuchando)
            {
                try
                {
                    MessageQueue colaLocal = HelperCliente.Instancia().GetColaLocal();
                    colaLocal.Formatter = new XmlMessageFormatter(new Type[] { typeof(Alarma) });
                    System.Messaging.Message msg = colaLocal.Receive();

                    if (msg.Body is Alarma)
                    {
                        Alarma unaAlarma = msg.Body as Alarma;
                        //HelperCliente.Instancia().ModificarAlarma(unaAlarma);
                        string resultado = HelperCliente.Instancia().CortarThread(this, unaAlarma);
                        ThrSafeTxtAlarma(resultado);
                    }
                }
                catch (ExceptionNegocio en)
                {
                    ThrSafeTxtAlarma(en.Message);
                }
                catch (MessageQueueException mqe)
                {
                    ThrSafeTxtAlarma("Excepcion de cola " + mqe.Message);
                }
                catch (Exception ex)
                {
                    ThrSafeTxtAlarma("Excepcion general " + ex.Message);
                }
            }
        }

        private void EscucharMensajes()
        {
            while (estaConectado)
            {
                try
                {
                    ManejadorProtocolo protocolo = new ManejadorProtocolo();
                    protocolo.RecibirMensaje(HelperCliente.Instancia().SocketCliente);
                    if (protocolo.Header == "REQ")
                    {
                        string[] texto = protocolo.Datos.Split('$');
                        int timerAlarma = -1;
                        bool remota = false;
                        switch (protocolo.CMD)
                        {
                            //Recibir Alarma Local
                            case 2:                                
                                timerAlarma = Int32.Parse(texto[0]);
                                string tipo = texto[1];
                                if (tipo == "LOCAL")
                                {
                                    HelperCliente.Instancia().InstanciaLog.Log("Se programa alarma local para sonar en " + timerAlarma + " segundos.");
                                }
                                else
                                {
                                    remota = true;
                                    int timerRemoto = Int32.Parse(texto[2]);                                    
                                    HelperCliente.Instancia().InstanciaLog.Log("Se ha programado alarma remota para sonar en " + timerRemoto + " segundos.");
                                }
                                
                                Thread thSonarAlarma = new Thread(ThrActivarAlarma);
                                //Si la alarma es local entonces encolo mensaje y la agrego a la lista
                                //sino, Ni encolo ni la agrego a la lista ya que viene con tiempo 0 como para sonar
                                string idAlarma = "";
                                if(!remota)
                                {
                                    idAlarma = HelperCliente.Instancia().EncolarAlarma(String.Empty,cliente, null, timerAlarma, false, thSonarAlarma);
                                }

                                Alarma a = new Alarma() 
                                {
                                    AlarmaId = idAlarma,
                                    Timer = timerAlarma,
                                    EsLocal = !remota
                                };
                                thSonarAlarma.Start(a);
                                break;

                            //Recibir Alarma Remota
                            case 3:
                                timerAlarma = Int32.Parse(texto[0]);
                                string cliRemotoId = texto[1];
                                HelperCliente.Instancia().InstanciaLog.Log(String.Format("Se ha programado alarma remota en {0}sec para el cliente: {1}",
                                    timerAlarma, cliRemotoId));

                                Thread thr_alarmaRemota = new Thread(ThrEnviarAlarmaRemota);
                                string alarmaid = HelperCliente.Instancia().EncolarAlarma(String.Empty,cliente, cliRemotoId, timerAlarma, true, thr_alarmaRemota);

                                Alarma alarma = new Alarma()
                                {
                                    AlarmaId = alarmaid,
                                    IdClienteRemoto = cliRemotoId,
                                    Timer = timerAlarma,
                                    HoraConfigurada = DateTime.Now
                                };
                                thr_alarmaRemota.Start(alarma);
                                break;

                            //Enviar archivo log
                            case 4:
                                HelperCliente.Instancia().InstanciaLog.Log("Se envia el archivo de logs al servidor");
                                Thread thr_EnviarLog = new Thread(EnviarLog);
                                thr_EnviarLog.Start();
                                break;
                        }
                    }
                }

                catch (ExceptionProtocolo)
                {
                    MessageBox.Show("Cliente desconectado");
                    HelperCliente.Instancia().InstanciaLog.Log("Se ha desconetado el cliente " + cliente);
                    ThrSafeFormPrincipal(false);
                    ThrSafeFormInicial(true);
                    estaConectado = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ESCUCHAR MENSAJES: "+ex.Message);
                    estaConectado = false;
                }
            } 
        }

        private void EnviarLog()
        {
            try
            {
                string datos = HelperCliente.Instancia().InstanciaLog.RecuperarLogs();
                string[] logs = datos.Split('$');

                string acumulado = String.Empty;
                for (int i = 0; i < logs.Length; i++)
                {
                    acumulado += logs[i] + Environment.NewLine;
                    if (acumulado.Length > 9000 || i==(logs.Length-1))
                    {
                        if (i == (logs.Length - 1))
                        {
                            acumulado += "*FIN*";
                        }
                        ManejadorProtocolo mp = new ManejadorProtocolo("RES", 4, acumulado.Length, acumulado);
                        mp.EnviarMensaje(HelperCliente.Instancia().SocketCliente);
                        acumulado = String.Empty;
                        
                    }
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ThrSafeFormPrincipal(bool show)
        {
            if (this.InvokeRequired)
            {
                FormPrincipalCallback d = new FormPrincipalCallback(ThrSafeFormPrincipal);
                this.Invoke(d, new object[] {show});
            }
            else
            {
                if (show)
                {
                    this.Show();
                }
                else
                {
                    this.Hide();
                }
            }
        }

        private void ThrSafeFormInicial(bool show)
        {
            if (sesion.InvokeRequired)
            {
                FormInicialCallback d = new FormInicialCallback(ThrSafeFormInicial);
                this.Invoke(d, new object[] { show });
            }
            else
            {
                if (show)
                {
                    sesion.Show();
                }
                else
                {
                    sesion.Hide();
                }
            }
        }

        private void ThrSafeTxtAlarma(string texto)
        {
            if (this.txtMensajes.InvokeRequired)
            {
                TxtAlarmaCallback d = new TxtAlarmaCallback(ThrSafeTxtAlarma);
                this.Invoke(d, new object[] {texto});
            }
            else
            {
                mensajesAcumulados += texto + Environment.NewLine;
                txtMensajes.Text = mensajesAcumulados;
            }
        }

        private void ThrSafeLblAlarma(string texto)
        {
            if (this.lblAlarma.InvokeRequired)
            {
                LblAlarmaCallback d = new LblAlarmaCallback(ThrSafeLblAlarma);
                this.Invoke(d, new object[] { texto });
            }
            else
            {
                lblAlarma.Text = texto;
            }
        }

        private void ThrSafePicAlarma(bool muestro)
        {
            if (this.picAlarma.InvokeRequired)
            {
                ImgAlarmaCallback d = new ImgAlarmaCallback(ThrSafePicAlarma);
                this.Invoke(d, new object[] { muestro });
            }
            else
            {
                picAlarma.Visible = muestro;
            }
        }

        public void ThrEnviarAlarmaRemota(Object alarma)
        {
            Alarma a = alarma as Alarma;
            if (!a.YaFueDisparada)
            {
                int tiempoEspera;
                if (a.Timer < 1)
                {
                    tiempoEspera = 1;
                }
                else
                {
                    tiempoEspera = a.Timer * 1000;
                }
                Thread.Sleep(tiempoEspera);
                string datos = a.Timer + "$" + a.IdClienteRemoto;
                ManejadorProtocolo mp = new ManejadorProtocolo("REQ", 3, datos.Length, datos);
                mp.EnviarMensaje(HelperCliente.Instancia().SocketCliente);
                HelperCliente.Instancia().EncolarAlarmaDisparadaRemota(a.AlarmaId, cliente, a.IdClienteRemoto, a.Timer);
            }
        }

        /*void timerEsperaRemota_Elapsed(object sender, System.Timers.ElapsedEventArgs e, string cliRemotoId,
            int timerAlarma, System.Timers.Timer timer, string idAlarma)
        {
            timer.Stop();

            string datos = timerAlarma + "$" + cliRemotoId;
            ManejadorProtocolo mp = new ManejadorProtocolo("REQ", 3, datos.Length, datos);
            mp.EnviarMensaje(HelperCliente.Instancia().SocketCliente);

            HelperCliente.Instancia().EncolarAlarmaDisparadaRemota(idAlarma,cliente, cliRemotoId, timerAlarma);
        }*/

        
        public void ThrActivarAlarma(Object alarma)
        {
            Alarma a = alarma as Alarma;

            if (!a.YaFueDisparada)
            {
                int tiempoEspera;
                if (a.Timer < 1)
                {
                    tiempoEspera = 1;
                }
                else
                {
                    tiempoEspera = a.Timer * 1000;
                }

                Thread.Sleep(tiempoEspera);
                ThrSafeLblAlarma("ALARMA !!!");
                ThrSafePicAlarma(true);
                sonido.Play();
                if (a.EsLocal)
                    HelperCliente.Instancia().EncolarAlarmaDisparadaLocal(a.AlarmaId, cliente, a.Timer);
                Thread.Sleep(5000);
                ThrSafeLblAlarma("NORMAL");
                ThrSafePicAlarma(false);
                sonido.Stop();
            }

            //string idALarma, int tiempoEspera, bool esRemota
            //Alarma a = alarma as Alarma;
            //Thread.Sleep(a.Timer * 1000);
            //ThrSafeLblAlarma("ALARMA !!!");
            //ThrSafePicAlarma(true);
            //sonido.Play();

            //if (a.EsLocal)
            //{
            //    HelperCliente.Instancia().EncolarAlarmaDisparadaLocal(a.AlarmaId, cliente, a.Timer);
            //}

            //Thread.Sleep(5000);
            //ThrSafeLblAlarma("NORMAL");
            //ThrSafePicAlarma(false);
            //sonido.Stop();

            //TODO:  dipose del timer !!!!!!
        }

       /* void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e, Alarma a,System.Timers.Timer timer)
        {
            //Alarma a = sender as Alarma;
            //Thread.Sleep(a.Timer * 1000);
            ThrSafeLblAlarma("ALARMA !!!");
            ThrSafePicAlarma(true);
            sonido.Play();
            if (a.EsLocal)
            {
                HelperCliente.Instancia().EncolarAlarmaDisparadaLocal(a.AlarmaId, cliente, a.Timer);
            }
            timer.Stop();

            System.Timers.Timer timerFin = new System.Timers.Timer(5000);
            timerFin.Elapsed += (s, evento) => timerFin_Elapsed(s, evento, timerFin);
            timerFin.Enabled = true;
        }*/

       /* void timerFin_Elapsed(object sender, System.Timers.ElapsedEventArgs e, System.Timers.Timer timer)
        {
            ThrSafeLblAlarma("NORMAL");
            ThrSafePicAlarma(false);
            sonido.Stop();
            timer.Stop();
        }*/


        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            HelperCliente.Instancia().SocketCliente.Shutdown(SocketShutdown.Both);
            HelperCliente.Instancia().SocketCliente.Close();

            this.Hide();
            sesion.Show();
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            HelperCliente.Instancia().SocketCliente.Shutdown(SocketShutdown.Both);
            HelperCliente.Instancia().SocketCliente.Close();

            ThrSafeFormPrincipal(false);
            ThrSafeFormInicial(true);

            HelperCliente.Instancia().EncolarUnaDesconexion(cliente);
        }               
    }
}
