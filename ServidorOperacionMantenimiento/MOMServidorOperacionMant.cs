using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Web;

namespace ServidorOperacionMantenimiento
{
    public class MOMServidorOperacionMant
    {
        private static readonly object synclock = new object();
        private static MOMServidorOperacionMant instancia;
        private string queueServidorRegistro = ConfigurationManager.AppSettings["queueServidorRegistro"];
        private MessageQueue colaServerRegistro;


        protected MOMServidorOperacionMant()
        {
            colaServerRegistro = new MessageQueue(queueServidorRegistro);
        }

        public static MOMServidorOperacionMant Instancia()
        {
            if (instancia == null)
            {
                lock (synclock)
                {
                    if (instancia == null)
                    {
                        instancia = new MOMServidorOperacionMant();
                    }
                }
            }
            return instancia;
        }

        public void EnviarAlarmaModificada(Alarma a)
        {
            Message msg = new Message(a);
            msg.Recoverable = true;
            colaServerRegistro.Send(msg);
        }
    }
}