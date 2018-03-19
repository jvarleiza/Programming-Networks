using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    [DataContract]
    public class Cliente : MarshalByRefObject
    {
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public bool Configurado { get; set; }
        [DataMember]
        public int NroIdentificacion { get; set; }
        [DataMember]
        public string IP { get; set; }

        public Cliente()
        {
        }

        public Cliente(string identificacion, bool config, int NroId)
        {
            Identificacion = identificacion;
            Configurado = config;
            NroIdentificacion = NroId;
        }

        public override string ToString()
        {
            string valor;
            if (Configurado)
            {
                valor = NroIdentificacion + ") " + Identificacion + " (Conectado)";
            }
            else
            {
                valor = NroIdentificacion + ") " + Identificacion;
            }

            return valor;
        }

       /* public override bool Equals(object obj)
        {
            Cliente c = obj as Cliente;
            if (c != null)
            {
                return c.Identificacion == this.Identificacion;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }*/
    }
}
