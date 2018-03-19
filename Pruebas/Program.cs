using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pruebas
{
    class Program
    {
        static void PrintA()
        {
            Console.WriteLine("Imprimir A");
        }
        static void PrintB()
        {
            Console.WriteLine("Imprimir B");
        }
        static void PrintC()
        {
            Console.WriteLine("Imprimir C");
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread threadA = new Thread(PrintA);
                Thread threadB = new Thread(PrintB);
                threadA.Start();
                threadB.Start();
                threadA.Join();
                threadB.Join();
                for (int j = 0; j < 2; j++)
                {
                    Thread threadC = new Thread(PrintC);
                    threadC.Start();
                    threadC.Join();
                    threadA = new Thread(PrintA);
                    threadB = new Thread(PrintB);
                    threadA.Start();
                    threadB.Start();
                    threadA.Join();
                    threadB.Join();
                }
            }
            Console.ReadLine();
        }
    }
}
