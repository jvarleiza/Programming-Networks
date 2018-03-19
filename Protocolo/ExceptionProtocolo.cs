using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolo
{
    public class ExceptionProtocolo: Exception
    {
        public ExceptionProtocolo()
        {
        }

        public ExceptionProtocolo(string message)
            : base(message)
        {
        }

        public ExceptionProtocolo(string message, Exception inner)
            : base(message, inner)
        {
        }
        }
}
