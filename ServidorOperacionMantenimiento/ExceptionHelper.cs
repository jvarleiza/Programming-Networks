using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace ServidorOperacionMantenimiento
{
    internal static class ExceptionHelper
    {
        public static FaultException HandleBackEndException(Exception ex)
        {
            return FaultException.CreateFault(MessageFault.CreateFault(new FaultCode(ex.GetType().AssemblyQualifiedName), new FaultReason(ex.Message)));
        }
    }
}