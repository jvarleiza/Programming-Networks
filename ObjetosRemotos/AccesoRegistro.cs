using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosRemotos
{
    public class AccesoRegistro : MarshalByRefObject
    {
        public AccesoRegistro()
        {
        }

        public List<string> GetClientes()
        {
            List<string> lst = new List<string>();
            foreach (Tuple<Cliente, MessageQueue> item in Registro.Instancia().ListaClientes)
	        {
                if (item.Item1 != null && item.Item1.Configurado)
                {
                    lst.Add(item.Item1.Identificacion);
                }
	        }
            return lst;
        }

        //Alarmas que detonan en el cliente: <<idCliente>>
        public List<string> GetAlarmasConfiguradas(string idCliente)
        {
            List<Alarma> alarmas = Registro.Instancia().ListaAlarmas;
            List<string> lstAux = new List<string>();

            foreach (Alarma a in alarmas)
            {
                if (!a.YaFueDisparada)
                {
                    if (a.EsLocal)
                    {
                        if (a.IdClienteReceptor == idCliente)
                        {
                            string aux = String.Format("{0}${1}${2}${3}${4}${5}",a.AlarmaId, a.EsLocal,
                                a.HoraConfigurada,a.IdClienteReceptor,a.Timer,a.YaFueDisparada);
                            lstAux.Add(aux);
                        }
                    }
                    else
                    {
                        if (a.IdClienteRemoto == idCliente)
                        {
                            string aux = String.Format("{0}${1}${2}${3}${4}${5}${6}",a.AlarmaId, a.EsLocal,
                                a.HoraConfigurada,a.IdClienteReceptor,a.IdClienteRemoto,a.Timer,a.YaFueDisparada);
                            lstAux.Add(aux);
                        }
                    }
                }
            }
            return lstAux;
        }

        public int[] ObtenerEstadisticas()
        {
            return Registro.Instancia().GetEstadisticas();
        }
    }
}
