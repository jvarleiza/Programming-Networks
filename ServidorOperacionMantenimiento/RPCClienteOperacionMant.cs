using LogicaNegocio;
using ObjetosRemotos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Web;

namespace ServidorOperacionMantenimiento
{
    public class RPCClienteOperacionMant
    {
        private static readonly object synclock = new object();
        private static RPCClienteOperacionMant instancia;
        private AccesoRegistro accesoARegistro;

        private RPCClienteOperacionMant()
        {
            IDictionary RemoteChannelProperties = new Hashtable();
            RemoteChannelProperties["name"] = "tcp";
            TcpChannel chan = new TcpChannel(RemoteChannelProperties, null, null);
            ChannelServices.RegisterChannel(chan, false);

            // Create an instance of the remote object
            string ipServRegistro = ConfigurationManager.AppSettings["ipServRegistro"];
            accesoARegistro = (AccesoRegistro)Activator.GetObject(typeof(AccesoRegistro),ipServRegistro);
        }

        public static RPCClienteOperacionMant Instancia()
        {
            if (instancia == null)
            {
                lock (synclock)
                {
                    if (instancia == null)
                    {
                        instancia = new RPCClienteOperacionMant();
                    }
                }
            }
            return instancia;
        }

        internal List<string> GetClientes()
        {
            try
            {
                return accesoARegistro.GetClientes();
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal List<string> GetAlarmasConfiguradas(string idCliente)
        {
            try
            {
                return accesoARegistro.GetAlarmasConfiguradas(idCliente);
            }
            catch (Exception)
            {
                return null;
            }
        }


        internal int[] ObtenerEstadisticas()
        {
            try
            {
                return accesoARegistro.ObtenerEstadisticas();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}