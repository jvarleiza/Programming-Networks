using LogicaNegocio;
using Logs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AplicacionCliente
{
    public class HelperCliente
    {
        private static readonly object synclock = new object();
        private static HelperCliente instancia;
        
        private string queueNameServidor;
        private MessageQueue colaServidorRegistro;

        private string queueCliente;
        private MessageQueue colaLocal;

        public Socket SocketCliente { get; set; }
        public string IPLocal { get; set; }
        //public int Puerto { get; set; }
        public ILog InstanciaLog { get; set; }
        private List<Tuple<Alarma, Thread>> Alarmas;  
        private int AlarmasID;
        private static string idCliente;


        protected HelperCliente()
        {
            AlarmasID = 0;
            Alarmas = new List<Tuple<Alarma, Thread>>();
            queueNameServidor = ConfigurationManager.AppSettings["queueNameServidor"];
            colaServidorRegistro = new MessageQueue(queueNameServidor);
            queueCliente = ConfigurationManager.AppSettings["queueCliente"]+idCliente;
            if (MessageQueue.Exists(queueCliente))
            {
                colaLocal = new MessageQueue(queueCliente);
            }
            else
            {
                colaLocal = MessageQueue.Create(queueCliente);
            }
            InstanciaLog = new LogImp();
            IPLocal = ConfigurationManager.AppSettings["ipCliente"];
            //Puerto = Int32.Parse(ConfigurationManager.AppSettings["puertoComunicacion"]);
        }

        public static HelperCliente Instancia()
        {
            if (instancia == null)
            {
                lock (synclock)
                {
                    if (instancia == null)
                    {
                        instancia = new HelperCliente();
                    }
                }
            }
            return instancia;
        }

        internal static void SetIdCliente(string unIdCliente)
        {
            idCliente = unIdCliente;
        }

        internal MessageQueue GetColaLocal()
        {
            return colaLocal;
        }

        public void EncolarUnaConexion(string identificacion)
        {
            Cliente nuevaConexion = new Cliente() 
            {
                Identificacion = identificacion,
                Configurado = true,
                IP = IPLocal
            };

            Message msg = new Message(nuevaConexion);
            msg.Recoverable = true;
            colaServidorRegistro.Send(msg);
        }

        public void EncolarUnaDesconexion(string identificacion)
        {
            Cliente nuevaDesconexion = new Cliente()
            {
                Identificacion = identificacion,
                Configurado = false,
            };
            Message msg = new Message(nuevaDesconexion);
            msg.Recoverable = true;
            colaServidorRegistro.Send(msg);
        }

        internal void EncolarAlarmaDisparadaLocal(string idAlarma, string clienteLocal, int timer)
        {
            Alarma alarmaDisparada = new Alarma()
            {
                AlarmaId = idAlarma,
                EsLocal = true,
                HoraConfigurada = DateTime.MinValue,
                IdClienteReceptor = clienteLocal,
                Timer = timer,
                YaFueDisparada = true
            };
            Message msg = new Message(alarmaDisparada);
            msg.Recoverable = true;
            colaServidorRegistro.Send(msg);
        }

        internal string EncolarAlarma(string idAlarma,string clienteLocal, string cliRemotoId, int timerAlarma, bool remota, Thread thr)
        {
            Alarma alarma = new Alarma()
            {
                EsLocal = !remota,
                HoraConfigurada = DateTime.Now,
                IdClienteReceptor = clienteLocal,
                IdClienteRemoto = cliRemotoId,
                Timer = timerAlarma,
                YaFueDisparada = false
            };

            
            if (!Alarmas.Exists(a => a.Item1.AlarmaId == idAlarma))
            {
                AlarmasID++;
                alarma.AlarmaId = AlarmasID.ToString();
                Message msg = new Message(alarma);
                msg.Recoverable = true;
                colaServidorRegistro.Send(msg);
            }
            else
            {
                int i = Alarmas.FindIndex(t => t.Item1.AlarmaId == idAlarma);
                Alarmas.RemoveAt(i);
                alarma.AlarmaId = idAlarma;
            }

            Alarmas.Add(new Tuple<Alarma, Thread>(alarma, thr));
            return alarma.AlarmaId;
        }

        internal void EncolarAlarmaDisparadaRemota(string idAlarma, string clienteInter, string clienteReceptor, int timerRemoto)
        {
            Alarma alarmaDisparada = new Alarma()
            {
                AlarmaId = idAlarma,
                EsLocal = false,
                HoraConfigurada = DateTime.MinValue,
                IdClienteRemoto = clienteReceptor,
                IdClienteReceptor = clienteInter,
                Timer = timerRemoto,
                YaFueDisparada = true
            };
            Message msg = new Message(alarmaDisparada);
            msg.Recoverable = true;
            colaServidorRegistro.Send(msg);
        }

        internal string CortarThread(Principal p, Alarma alarmaConTiempoNuevo)
        {
            //Cortar el thread con la alarma vieja y lanzar uno nuevo con la alarma nueva
            Tuple<Alarma, Thread> tAlarma = Alarmas.Find(al => al.Item1.AlarmaId == alarmaConTiempoNuevo.AlarmaId);

            if (tAlarma != null)
            {
                tAlarma.Item2.Abort();
            }
            else
            {
                throw new ExceptionNegocio("No se encontró la alarma configurada");
            }


            Thread thSonarAlarma;
            if (alarmaConTiempoNuevo.EsLocal)
            {
                thSonarAlarma = new Thread(p.ThrActivarAlarma);
            }
            else
            {
                thSonarAlarma = new Thread(() => p.ThrEnviarAlarmaRemota(alarmaConTiempoNuevo));
            }

            string idAlarma = this.EncolarAlarma(alarmaConTiempoNuevo.AlarmaId, alarmaConTiempoNuevo.IdClienteReceptor, alarmaConTiempoNuevo.IdClienteRemoto,
                alarmaConTiempoNuevo.Timer, !alarmaConTiempoNuevo.EsLocal, thSonarAlarma);

            if (alarmaConTiempoNuevo.EsLocal)
            {
                thSonarAlarma.Start(alarmaConTiempoNuevo);
                return String.Format("Se modificó la alarma local {0}: de {1} a {2} segundos", idAlarma,
                    tAlarma.Item1.Timer, alarmaConTiempoNuevo.Timer);
            }
            else
            {
                thSonarAlarma.Start();
                return String.Format("Se modificó la alarma remota {0}: de {1} a {2} segundos para el cliente {3}",
                    idAlarma,tAlarma.Item1.Timer, alarmaConTiempoNuevo.Timer, alarmaConTiempoNuevo.IdClienteRemoto);
            }            
        }
    }
}
