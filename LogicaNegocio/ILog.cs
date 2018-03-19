using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public interface ILog
    {
        void Log(string mensaje);
        string RecuperarLogs();
    }
}
