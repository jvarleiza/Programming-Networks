using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Protocolo
{
    public class ManejadorProtocolo
    {
        public string Header { get; set; }
        public int CMD { get; set; }
        public int Largo { get; set; }
        public string Datos { get; set; }
        int LargoEncabezado {get; set;}

        public ManejadorProtocolo()
        {
            this.LargoEncabezado = 9;
        }

        public ManejadorProtocolo(string header, int cmd, int largoDatos, string datos)
        {
            this.LargoEncabezado = 9;
            Header = header;
            CMD = cmd;
            Largo = largoDatos;
            Datos = datos;
        }

        public void RecibirMensaje(Socket unSocket)
        {
            try
            {
                byte[] bufferHeader = new byte[this.LargoEncabezado];
                int leidoHeader = 0;
                while (leidoHeader < this.LargoEncabezado)
                {
                    int recibido = unSocket.Receive(bufferHeader, 0, 9, SocketFlags.None);
                    
                    if (recibido == 0)
                    {
                        unSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                        unSocket.Close();
                        CMD = 0;
                        return;
                    }
                    else
                    {
                        leidoHeader += recibido;
                    }
                }

                String encabezado = System.Text.Encoding.ASCII.GetString(bufferHeader, 0, leidoHeader);
                this.Header = encabezado.Substring(0, 3);
                this.CMD = Convert.ToInt32(encabezado.Substring(3, 2));
                this.Largo = Convert.ToInt32(encabezado.Substring(5, 4));

                Byte[] bytesDatos = new Byte[this.Largo];

                int cantidadLeido = 0;
                int leidoRafaga = 0;
                while (cantidadLeido < this.Largo)
                {
                    leidoRafaga = unSocket.Receive(bytesDatos, cantidadLeido, (bytesDatos.Length - cantidadLeido), SocketFlags.None);
                    cantidadLeido += leidoRafaga;
                }

                String cuerpo = System.Text.Encoding.ASCII.GetString(bytesDatos, 0, cantidadLeido);
                this.Datos = cuerpo;
            }
            catch (Exception ex)
            {
                CMD = 0;
                throw new ExceptionProtocolo("Se ha cerrado abruptamente la conexión", ex);
            }
        }


        public string EnviarMensaje(Socket unSocket)
        {
            try
            {
                string cmdString = CMD.ToString();
                string largoDatosString = AgregarCerosAlLargoDelCuerpo();
                if (CMD < 10)
                {
                    cmdString = "0" + CMD.ToString();
                }

                string encabezado = Header + cmdString + largoDatosString;
                string texto = encabezado + this.Datos;
                Encoding ASCII = Encoding.ASCII;
                byte[] trama = ASCII.GetBytes(texto);
                int enviado = 0;
                while (enviado < trama.Length)
                {
                    enviado += unSocket.Send(trama, enviado, trama.Length - enviado, SocketFlags.None);
                }
                return "OK;Se mando la trama";
            }
            catch (Exception ex)
            {
                throw new ExceptionProtocolo("Error al enviar trama", ex);
            }
        }

        private string AgregarCerosAlLargoDelCuerpo()
        {
            switch (Largo.ToString().Length)
            {
                case 1:
                    return "000"+Largo;
                case 2:
                    return "00" + Largo;
                case 3:
                    return "0" + Largo;
                case 4:
                    return "" + Largo;
                default:
                    return "0000";
            }
        }

        private void Prueba()
        {
            string ip = "172.16.10.6";
            int puerto = 13000;
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), puerto);
            Socket server;
        }
    }
}
