using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class ExceptionNegocio : Exception
    {
        public ExceptionNegocio()
        {
        }

        public ExceptionNegocio(string message)
            : base(message)
        {
        }

        public ExceptionNegocio(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
