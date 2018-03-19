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
    public class ServiciosMantenimientoImp : IServiciosMantenimiento
    {
        public List<string> GetClientes()
        {
            try
            {
                return RPCClienteOperacionMant.Instancia().GetClientes();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
        }

        public List<string> GetAlarmasConfiguradas(string idCliente)
        {
            try
            {
            return RPCClienteOperacionMant.Instancia().GetAlarmasConfiguradas(idCliente);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
        }

        public void ModificarAlarma(Alarma a)
        {
            try
            {
                MOMServidorOperacionMant.Instancia().EnviarAlarmaModificada(a);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
        }

        public List<int> ObtenerEstadisticas()
        {
            try
            {
                return RPCClienteOperacionMant.Instancia().ObtenerEstadisticas().ToList();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
        }
    }
}
