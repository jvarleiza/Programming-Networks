using LogicaNegocio;
using Protocolo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AplicacionServidor
{
    public class Sistema
    {
        private static Sistema instancia;
        private static readonly object synclock = new object();
        private static readonly object synclockConectados = new object();
        public List<Cliente> Clientes { get; set; }
        public List<Tuple<Socket, Cliente, Thread>> ClientesActivos { get; set; }
        int nroIdCliente;


        private Sistema()
        {
            nroIdCliente = 0;
            Clientes = new List<Cliente>();
            ClientesActivos = new List<Tuple<Socket, Cliente, Thread>>();
            CargarDatosDePrueba();
        }

        public static Sistema Instancia()
        {
            if (instancia == null)
            {
                lock (synclock)
                {
                    if (instancia == null)
                    {
                        instancia = new Sistema();
                    }
                }
            }
            return instancia;
        }

        private void CargarDatosDePrueba()
        {
            AgregarCliente("a");
            AgregarCliente("b");
            AgregarCliente("c");
            AgregarCliente("d");
            AgregarCliente("e");
            AgregarCliente("f");
            AgregarCliente("g");
            AgregarCliente("h");
            AgregarCliente("i");
            AgregarCliente("j");
            AgregarCliente("k");
            AgregarCliente("l");
            AgregarCliente("m");
            AgregarCliente("n");
            AgregarCliente("o");
        }

        public void CerrarConexiones()
        {
            foreach(Tuple<Socket,Cliente,Thread> t in ClientesActivos)
            {
                t.Item1.Shutdown(SocketShutdown.Both);
                t.Item1.Close();
            }
        }


        internal bool AgregarCliente(string id)
        {
            bool existe = Clientes.Exists(x => x.Identificacion == id);
            if (!existe)
            {
                nroIdCliente++;
                Clientes.Add(new Cliente(id, false, nroIdCliente));
            }
            return existe;
        }

        internal void EliminarCliente(Cliente unCliente)
        {
            Clientes.Remove(unCliente);
        }

        internal bool ModificarCliente(string idViejo, string idNuevo)
        {
            if (Clientes.Find(c => c.Identificacion == idNuevo) == null)
            {
                Clientes.FirstOrDefault(c => c.Identificacion == idViejo).Identificacion = idNuevo;
                return true;
            }
            return false;
        }

        internal Cliente ObtenerCliente(string idCliente)
        {
            return Clientes.FirstOrDefault(c => c.Identificacion == idCliente);
        }

        internal List<Cliente> ObtenerClientes()
        {
            return this.Clientes;
        }

        internal void AgregarClienteConectado(Cliente unCli, Socket unSocket, Thread unThread)
        {
            lock (synclockConectados)
            {
                ClientesActivos.Add(new Tuple<Socket, Cliente,Thread>(unSocket, unCli, unThread));
            }
        }


        internal void BorrarClienteConectado(string id)
        {
            Tuple<Socket,Cliente,Thread> tupleSC = ClientesActivos.Find(x => x.Item2.Identificacion == id);
            Clientes.Find(c => c.Identificacion == tupleSC.Item2.Identificacion).Configurado = false;
            ClientesActivos.Remove(tupleSC);
        }

        internal void BorrarClienteConectado(Socket unSock)
        {
            Tuple<Socket, Cliente, Thread> tupleSC = ClientesActivos.Find(x => x.Item1.Equals(unSock));
            if (tupleSC != null)
            {
                Clientes.Find(c => c.Identificacion == tupleSC.Item2.Identificacion).Configurado = false;
                ClientesActivos.Remove(tupleSC);
            }
            else
            {
                throw new ExceptionProtocolo("No se pudo desconectar al cliente");
            }
        }

        internal bool ClienteEstaConectado(string id)
        {
            return ClientesActivos.Exists(t => t.Item2.Identificacion == id);
        }

        internal void EnviarAlarmaLocal(Cliente cliente, int timer)
        {
            try
            {
                string datos = timer + "$" + "LOCAL";
                ManejadorProtocolo mp = new ManejadorProtocolo("REQ", 2, datos.Length, datos);

                Tuple<Socket, Cliente, Thread> tuplaSC = ClientesActivos.SingleOrDefault(t => t.Item2.Identificacion == cliente.Identificacion);
                mp.EnviarMensaje(tuplaSC.Item1);
            }
            catch (Exception ex)
            {
                throw new ExceptionProtocolo("El cliente: " + cliente.Identificacion + " no está conectado", ex);
            }
        }

        internal void EnviarAlarmaRemota(Cliente cliIntermediario, Cliente cliRemoto, int tiempoDeEspera)
        {
            try
            {
                string datos = tiempoDeEspera + "$" + cliRemoto.Identificacion;
                ManejadorProtocolo mp = new ManejadorProtocolo("REQ", 3, datos.Length, datos);

                Tuple<Socket, Cliente, Thread> tuplaSC = ClientesActivos.SingleOrDefault(t => t.Item2.Identificacion == cliIntermediario.Identificacion);
                mp.EnviarMensaje(tuplaSC.Item1);
            }
            catch (Exception ex)
            {
                throw new ExceptionProtocolo("El cliente: " + cliRemoto.Identificacion + " no está conectado", ex);
            }
        }

        internal string Ejecutar(ManejadorProtocolo protocolo)
        {
            string header = protocolo.Header;
            int cmd = protocolo.CMD;
            int largo = protocolo.Largo;
            string datos = protocolo.Datos;
            string resultado = "";

            string[] info = datos.Split('$');

            if (header.Equals("REQ"))
            {
                switch (cmd)
                {
                    //Login
                    case 1:
                        resultado = OpLogin(datos);
                        break;
                    //Alarma remota
                    case 3:
                        resultado = OpAlarmaRemota(datos);
                        break;
                }
            }
            return resultado;
        }

        //Enviar alarma a cliente remoto con timer = 0
        private string OpAlarmaRemota(string datos)
        {
            //timer  $  cliente
            string[] texto = datos.Split('$');
            int timer = Int32.Parse(texto[0]);
            string identificador = texto[1];

            Cliente cliRemoto = Clientes.Find(x => x.Identificacion == identificador);
            if (cliRemoto != null)
            {
                string cuerpo = "0$REMOTA$"+timer;
                ManejadorProtocolo mp = new ManejadorProtocolo("REQ",2,cuerpo.Length,cuerpo);
                Tuple<Socket,Cliente, Thread> tupleSC = ClientesActivos.Find(x => x.Item2.Identificacion == identificador);

                if (tupleSC != null)
                {
                    mp.EnviarMensaje(tupleSC.Item1);
                    return "OK; " + identificador;
                }
                else
                {
                    return "ERROR;" + identificador + ". No fue enviada porque ya no se encuentra conectado.";
                }
            }
            else
            {
                return "ERROR; Se rechazó la conexión de: " + identificador + ", no se encuentra como usuario";
            }
        }

        private string OpLogin(string datos)
        {
            string[] texto = datos.Split('$');
            string identificador = texto[0];
            bool existe = Clientes.Exists(x => x.Identificacion == identificador);
            if (existe)
            {
                if (!ClienteEstaConectado(identificador))
                {
                    Clientes.Find(x => x.Identificacion == identificador).Configurado = true;
                    return "OK; " + identificador + ", ha establecido conexión";
                }
                else
                {
                    return "ERROR; Se rechazó la conexión de: " + identificador + ", ya se encuentra conectado";
                }
            }
            else
            {
                return "ERROR; Se rechazó la conexión de: " + identificador + ", no se encuentra como usuario";
            }
        }





        internal void SolicitarArchivo(Cliente unCli)
        {
            try
            {
                if (unCli != null)
                {
                    Tuple<Socket, Cliente, Thread> tuple = ClientesActivos.Find(t => t.Item2.Identificacion == unCli.Identificacion);
                    ManejadorProtocolo mp = new ManejadorProtocolo("REQ", 4, 0, String.Empty);
                    mp.EnviarMensaje(tuple.Item1);
                }
            }

            catch (Exception ex)
            {
                throw new ExceptionProtocolo("No se logro recibir el archivo", ex);
            }
        }
    }
}
