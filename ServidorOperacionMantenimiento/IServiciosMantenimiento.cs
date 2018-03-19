using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServidorOperacionMantenimiento
{
    [ServiceContract]
    public interface IServiciosMantenimiento
    {
        [OperationContract]
        List<string> GetClientes();

        [OperationContract]
        List<string> GetAlarmasConfiguradas(string idCliente);

        [OperationContract]
        void ModificarAlarma(Alarma a);

        [OperationContract]
        List<int> ObtenerEstadisticas();
    }
}
