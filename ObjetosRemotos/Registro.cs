using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosRemotos
{
    public class Registro: MarshalByRefObject
    {
        private static readonly object synclock = new object();
        private static Registro instancia;
        public List<Alarma> ListaAlarmas { get; set; }
        public List<Tuple<Cliente, MessageQueue>> ListaClientes { get; set; }
        private int[] estadisticas;

        protected Registro()
        {
            estadisticas = new int[4];
            ListaAlarmas = new List<Alarma>();
            ListaClientes = new List<Tuple<Cliente, MessageQueue>>();
        }

        public static Registro Instancia()
        {
            if (instancia == null)
            {
                lock (synclock)
                {
                    if (instancia == null)
                    {
                        instancia = new Registro();
                    }
                }
            }
            return instancia;
        }

        public void IncrementarLogins()
        {
            this.estadisticas[0]++;
        }

        public void IncrementarLogouts()
        {
            this.estadisticas[3]++;
        }

        public void IncrementarAlarmasLocales()
        {
            this.estadisticas[1]++;
        }

        public void IncrementarAlarmasRemotas()
        {
            this.estadisticas[2]++;
        }

        public int[] GetEstadisticas()
        {
            return this.estadisticas;
        }

        public void ActualizarCliente(Cliente unCli, MessageQueue colaCliente)
        {
            Tuple<Cliente, MessageQueue> encontrado = ListaClientes.Find(c => c.Item1 != null && c.Item1.Identificacion == unCli.Identificacion);
            if (encontrado == null)
            {
                ListaClientes.Add(new Tuple<Cliente, MessageQueue>(unCli, colaCliente));
                IncrementarLogins();
            }
            else
            {
                encontrado.Item1.Configurado = unCli.Configurado;
                if (!unCli.Configurado)
                {
                    IncrementarLogouts();
                }
                else
                {
                    IncrementarLogins();
                }
            }
        }

        public void ActualizarAlarma(Alarma unaAlarma)
        {
            Alarma encontrada = ListaAlarmas.Find(a => a.Equals(unaAlarma));
            if (encontrada == null)
            {
                ListaAlarmas.Add(unaAlarma);
                if (unaAlarma.EsLocal)
                {
                    IncrementarAlarmasLocales();
                }
                else
                {
                    IncrementarAlarmasRemotas();
                }
            }
            else
            {
                encontrada.HoraConfigurada = DateTime.Now;
                encontrada.YaFueDisparada = unaAlarma.YaFueDisparada;
                encontrada.Timer = unaAlarma.Timer;
            }
        }


        public void ImprimirEstadoActualDelSistema()
        {
            Console.WriteLine("CLIENTES:");
            foreach (Tuple<Cliente, MessageQueue> c in ListaClientes)
            {
                Console.WriteLine(c.Item1.Identificacion + " - " + c.Item1.Configurado.ToString() + " - " + c.Item1.IP);
            }
            Console.WriteLine("ALARMAS:");
            foreach (Alarma a in ListaAlarmas)
            {
                Console.WriteLine(a.ToString());
            }
        }

        public MessageQueue ObtenerColaDeCliente(string idCliente)
        {
            if (idCliente != null)
                return ListaClientes.Find(c => c.Item1.Identificacion == idCliente).Item2;
            else
                throw new ExceptionNegocio("El id cliente llega como vacio");
        }
    }
}
