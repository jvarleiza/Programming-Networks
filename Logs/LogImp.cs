using log4net.Appender;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Logs
{
    public class LogImp : ILog
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LogImp()
        {
            
        }

        public void Log(string mensaje)
        {
            log.Info(mensaje);
        }

        public string RecuperarLogs()
        {
            try
            {
                string ruta = (log4net.LogManager.GetCurrentLoggers()[0].Logger.Repository.GetAppenders()[0] as FileAppender).File;
                return Leer(ruta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //private List<string> Leer(string ruta)
        //{
        //    try
        //    {
        //        Stream stream = File.Open(ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //        StreamReader archivo = new StreamReader(stream);
        //        string linea = archivo.ReadLine();
        //        List<string> retorno = new List<string>();
        //        while (linea != null)
        //        {
        //            string[] strParametros = linea.Split(new string[] { " - " }, StringSplitOptions.None);
        //            DateTime fecha = DateTime.Parse(strParametros[0].Substring(0, 19));
        //            string mensaje = strParametros[1];
        //            string log = fecha.ToString() + " " + mensaje;
        //            retorno.Add(log);
        //            linea = archivo.ReadLine();
        //        }
        //        return retorno;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Error al leer el archivo que contiene los logs", e);
        //    }
        //}

        private string Leer(string ruta)
        {
            try
            {
                Stream stream = File.Open(ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader archivo = new StreamReader(stream);
                string linea = archivo.ReadLine();
                string retorno = "";
                while (linea != null)
                {
                    string[] strParametros = linea.Split(new string[] { " - " }, StringSplitOptions.None);
                    DateTime fecha = DateTime.Parse(strParametros[0].Substring(0, 19));
                    string mensaje = strParametros[1];
                    string log = fecha.ToString() + " " + mensaje;
                    retorno += log;
                    retorno += "$";
                    linea = archivo.ReadLine();
                }
                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception("Error al leer el archivo que contiene los logs", e);
            }




        }

    }
}
