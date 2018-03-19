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
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ServidorDeRegistro
{
    public class RPCServidorRegistro
    {
        private static readonly object synclock = new object();
        private static RPCServidorRegistro instancia;

        private RPCServidorRegistro()
        {            
            IDictionary propiedadesRPC = new Hashtable();
            propiedadesRPC["port"] = ConfigurationManager.AppSettings["puertoRPC"];
            propiedadesRPC["name"] = "tcp";

            BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
            provider.TypeFilterLevel = TypeFilterLevel.Full;

            TcpChannel channel = new TcpChannel(propiedadesRPC, null, provider);
            ChannelServices.RegisterChannel(channel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(AccesoRegistro),"AccesoRegistro",WellKnownObjectMode.Singleton);
        }

        public static RPCServidorRegistro Instancia()
        {
            if (instancia == null)
            {
                lock (synclock)
                {
                    if (instancia == null)
                    {
                        instancia = new RPCServidorRegistro();
                    }
                }
            }
            return instancia;
        }

        public void IniciarServicioRemoto()
        {
            Console.WriteLine("***** INICIA SERVICIO REMOTO *****");
        }
    }
}
