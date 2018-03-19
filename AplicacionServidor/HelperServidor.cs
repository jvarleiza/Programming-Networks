using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionServidor
{
    public class HelperServidor
    {
        public static Socket myServer;
        public static string ipAddr = ConfigurationManager.AppSettings["ipServidor"];
        public static int port = Int32.Parse(ConfigurationManager.AppSettings["puertoComunicacion"]);
    }
}
