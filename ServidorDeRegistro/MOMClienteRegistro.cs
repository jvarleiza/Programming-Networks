using LogicaNegocio;
using ObjetosRemotos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ServidorDeRegistro
{
    public class MOMClienteRegistro
    {

        private static readonly object synclock = new object();
        private static MOMClienteRegistro instancia;

        string queueOP = ConfigurationManager.AppSettings["queueComunicacionOP"];
        MessageQueue myQOP;

        private MOMClienteRegistro()
        {
            if (MessageQueue.Exists(queueOP))
            {
                myQOP = new MessageQueue(queueOP);
            }
            else
            {
                myQOP = MessageQueue.Create(queueOP);
            }   
        }

        public static MOMClienteRegistro Instancia()
        {
            if (instancia == null)
            {
                lock (synclock)
                {
                    if (instancia == null)
                    {
                        instancia = new MOMClienteRegistro();
                    }
                }
            }
            return instancia;
        }

        internal void IniciarServicios()
        {
            Console.WriteLine("INICIO DE SERVICIO CON SERVIDOR O&M");
            bool ok = true;
            while (ok)
            {
                myQOP.Formatter = new XmlMessageFormatter(new Type[] { typeof(Alarma) });
                Message msg = myQOP.Receive();

                if (msg.Body is Alarma)
                {
                    Alarma unaAlarma = msg.Body as Alarma;
                    Registro.Instancia().ActualizarAlarma(unaAlarma);
                    EnviarMensajeACliente(unaAlarma);
                }
                Registro.Instancia().ImprimirEstadoActualDelSistema();
            }
        }

        private void EnviarMensajeACliente(Alarma unaAlarma)
        {
            try
            {
                MessageQueue colaConCliente = Registro.Instancia().ObtenerColaDeCliente(unaAlarma.IdClienteReceptor);
                if (colaConCliente != null)
                {
                    Message msg = new Message(unaAlarma);
                    msg.Recoverable = true;
                    colaConCliente.Send(msg);
                }
            }
            catch (MessageQueueException mqe)
            {
                Console.WriteLine(mqe.Message);
            }
            catch (ExceptionNegocio exn)
            {
                Console.WriteLine(exn.Message);
            }
        }
    }
}
