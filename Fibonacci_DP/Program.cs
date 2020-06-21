using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
//Fibonacci normal and Dynamic programming : page : 108
namespace Fibonacci_DP
{
    class Program
    {

        //Normal Fibonacci
        static int Fibonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }


        static private int[] cache = new int[1000];

        static int Fibonacci_DP(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            if (cache[n] != 0)
                return cache[n];
            cache[n] = Fibonacci_DP(n - 1) + Fibonacci_DP(n - 2);
            return cache[n];
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Fibonacci Sum of 4 Is " + Fibonacci(4));
            Console.WriteLine("Fibonacci Sum with DP for 4 Is " + Fibonacci_DP(4));


            Console.WriteLine("Fibonacci Sum of 5 Is " + Fibonacci(5));
            Console.WriteLine("Fibonacci Sum with DP for 5 Is " + Fibonacci_DP(5));


            Console.WriteLine("Fibonacci Sum of 10 Is " + Fibonacci(10));
            Console.WriteLine("Fibonacci Sum with DP for 10 Is " + Fibonacci_DP(10));

            Console.ReadKey();
        }
    }
}
