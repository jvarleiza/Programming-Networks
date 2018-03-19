using LogicaNegocio;
using ObjetosRemotos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorDeRegistro
{
    class ProgramaRegistro
    {
        static void Main(string[] args)
        {

            Thread thr_ComunicacionClientes = new Thread(ComunicarConClientes);
            thr_ComunicacionClientes.Start();

            Thread thr_ComunicacionServidorOP = new Thread(ComunicarConServidorOP);
            thr_ComunicacionServidorOP.Start();
        }

        internal static void ComunicarConClientes()
        {
            string queueName = ConfigurationManager.AppSettings["queueServidor"];
            MessageQueue myQ;

            if (MessageQueue.Exists(queueName))
            {
                myQ = new MessageQueue(queueName);
            }
            else
            {
                myQ = MessageQueue.Create(queueName);
            }
            RPCServidorRegistro.Instancia().IniciarServicioRemoto();

            Console.WriteLine("INICIO DE SERVICIO CON CLIENTES");
            bool ok = true;
            while (ok)
            {
                myQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente), typeof(Alarma), typeof(string) });
                Message msg = myQ.Receive();

                if (msg.Body is Cliente)
                {
                    Cliente unCli = msg.Body as Cliente;
                    string rutaColaCliente = ConfigurationManager.AppSettings["queueCliente"] + unCli.Identificacion;
                    string nombreColaCliente = String.Format(@"Formatname:DIRECT=TCP:{0}\Private$\{1}",unCli.IP ,rutaColaCliente);
                    MessageQueue colaCliente = new MessageQueue(nombreColaCliente);
                    Registro.Instancia().ActualizarCliente(unCli, colaCliente);
                }
                else
                {
                    if (msg.Body is Alarma)
                    {
                        Alarma unaAlarma = msg.Body as Alarma;
                        Registro.Instancia().ActualizarAlarma(unaAlarma);
                    }
                }
                Registro.Instancia().ImprimirEstadoActualDelSistema();
            }
        }

        internal static void ComunicarConServidorOP()
        {
            MOMClienteRegistro.Instancia().IniciarServicios();
        }
    }
}
