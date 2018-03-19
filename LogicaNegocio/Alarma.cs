using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    [DataContract]
    public class Alarma : MarshalByRefObject
    {
        [DataMember]
        public string AlarmaId { get; set; }
        [DataMember]
        public string IdClienteReceptor { get; set; }
        [DataMember]
        public string IdClienteRemoto { get; set; }
        [DataMember]
        public bool EsLocal { get; set; }
        [DataMember]
        public int Timer { get; set; }
        [DataMember]
        public bool YaFueDisparada { get; set; }
        [DataMember]
        public DateTime HoraConfigurada { get; set; }

        public Alarma()
        {
            YaFueDisparada = false;
        }

        public void Tick()
        {
            double dif = (DateTime.Now - HoraConfigurada).TotalSeconds;
            if (dif > Timer)
            {
                YaFueDisparada = true;
            }
        }

        public override bool Equals(object obj)
        {
            Alarma a = obj as Alarma;
            if (a != null)
            {
                return (obj as Alarma).AlarmaId == this.AlarmaId && (obj as Alarma).IdClienteReceptor == this.IdClienteReceptor
                                                                 && (obj as Alarma).IdClienteRemoto == this.IdClienteRemoto;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (EsLocal)
            {
                return String.Format("{0}) LOCAL:'{1}' ::> CONFIG:{2} ::> TIMER:{3} ::> DISP:{4}",
                    AlarmaId,
                    IdClienteReceptor,
                    HoraConfigurada,
                    Timer,
                    YaFueDisparada);   
            }
            else
            {
                return String.Format("{0}) REMOTA:'{1}'=>'{2}' ::> CONFIG:{3} ::> TIMER:{4} ::> DISP:{5}",
                    AlarmaId,
                    IdClienteReceptor,
                    IdClienteRemoto,
                    HoraConfigurada,
                    Timer,
                    YaFueDisparada);
            }
        }
    }
}
