using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreeThreadsInSync
{
    class Program
    {
         /// <summary>
    /// There are three threads in a process. The first thread prints 1 1 1 …, 
    /// the second one prints 2 2 2 …, and the third one prints 3 3 3 … endlessly.
    /// How do you schedule these three threads in order to print 1 2 3 1 2 3 …? 
    /// </summary>
    
        private static object lockObject = new object();

        private static int State = 1;
        static void Main(string[] args)
        {
            Thread t1 = new Thread(MethodA);
            Thread t2 = new Thread(MethodB);
            Thread t3 = new Thread(MethodC);

            t1.Start();
            t2.Start();
            t3.Start();

            Console.ReadKey();
        }

        internal static void MethodA()
        {
            while (true)
            {
                if (State == 1)
                {
                    lock (lockObject)
                    {
                        Console.WriteLine("1");
                        State = 2;
                    }
                }
            }
        }
        internal static void MethodB()
        {
            while (true)
            {
                if (State == 2)
                {
                    lock (lockObject)
                    {
                        Console.WriteLine("2");
                        State = 3;
                    }
                }
            }
        }
        internal static void MethodC()
        {
            while (true)
            {
                if (State == 3)
                {
                    lock (lockObject)
                    {
                        Console.WriteLine("3"); 
                        State = 1;
                    }
                }
            }
        }
    }
}
