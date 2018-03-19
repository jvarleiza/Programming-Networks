using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClienteOperacionMantenimiento
{
    internal static class ExceptionHelper
    {
        public static Exception HandleBackEndException(Exception ex)
        {
            try
            {
                FaultException fex = ex as FaultException;
                if (fex != null)
                {
                    System.ServiceModel.Channels.MessageFault mf = fex.CreateMessageFault();
                    string typeName = mf.Code.Name;
                    Type type = Type.GetType(typeName);
                    Exception returnEx = (Exception)Activator.CreateInstance(type, new string[] { ex.Message });
                    return returnEx;
                }
                else
                {
                    throw ex;
                }
            }
            catch
            {
                throw ex;
            }
        }
    }
}
