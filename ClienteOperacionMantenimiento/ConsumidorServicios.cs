using LogicaNegocio;
using ClienteOperacionMantenimiento.SistemaAlarmasSR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteOperacionMantenimiento
{
    public static class ConsumidorServicios
    {
        static ServiciosMantenimientoClient Conectar()
        {
            ServiciosMantenimientoClient cliente = null;
            try
            {
                cliente = new ServiciosMantenimientoClient();
            }
            catch (Exception ex)
            {
                Desconectar(cliente);
                throw new Exception(ex.Message);
            }
            return cliente;
        }

        static void Desconectar(ServiciosMantenimientoClient cliente)
        {
            if (cliente != null)
            {
                if (cliente.State != System.ServiceModel.CommunicationState.Closed)
                {
                    cliente.Close();
                }
                else
                {
                    cliente.Abort();
                }
            }
        }

        public static List<string> GetClientes()
        {
            ServiciosMantenimientoClient cliente = null;
            try
            {
                cliente = Conectar();
                return cliente.GetClientes();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
            finally
            {
                Desconectar(cliente);
            }
        }

        public static List<string> GetAlarmasConfiguradas(string idCliente)
        {
            ServiciosMantenimientoClient cliente = null;
            try
            {
                cliente = Conectar();
                return cliente.GetAlarmasConfiguradas(idCliente);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
            finally
            {
                Desconectar(cliente);
            }
        }

        public static void ModificarAlarma(Alarma a)
        {
            ServiciosMantenimientoClient cliente = null;
            try
            {
                cliente = Conectar();
                cliente.ModificarAlarma(a);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
            finally
            {
                Desconectar(cliente);
            }
        }

        internal static int[] ObtenerEstadisticas()
        {
            ServiciosMantenimientoClient cliente = null;
            try
            {
                cliente = Conectar();
                return cliente.ObtenerEstadisticas().ToArray();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.HandleBackEndException(ex);
            }
            finally
            {
                Desconectar(cliente);
            }
        }
    }

    //public static void ModificarAlarma(Alarma a, int timerNuevo)
    //    {
    //        ServiciosMantenimientoClient cliente = null;
    //        try
    //        {
    //            cliente = new ServiciosMantenimientoClient();
    //            return cliente.ModificarAlarma(a, timerNuevo);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ExceptionNegocio("Ha ocurrido un error al comunicarse con los servicios", ex);
    //        }
    //        finally
    //        {
    //            if (cliente != null)
    //            {
    //                if (cliente.State != System.ServiceModel.CommunicationState.Closed)
    //                {
    //                    cliente.Close();
    //                }
    //                else
    //                {
    //                    cliente.Abort();
    //                }
    //            }
    //        }
    //    }
    //}
}
