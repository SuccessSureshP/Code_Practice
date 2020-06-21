using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Conversions
{
    class Program
    {
        static void Main(string[] args)
        {
            //Shofting the bits right
            Console.WriteLine(Convert.ToString(8, 2)); // This converts the vlaue 8 into base 2, which is binary
            Console.WriteLine(Convert.ToString(8 >> 1) + "=" + Convert.ToString(8 >> 1, 2)); //It will divide  8 into half
            var r = 8 >> 1;
            Console.WriteLine(Convert.ToString(r >> 1) + "= " + Convert.ToString(r >> 1, 2)); //It will further divide r into half
            Console.WriteLine(Convert.ToString(r >> 1) + "= " + Convert.ToString(r >> 1, 2)); //It will further divide r into half

            Console.WriteLine(Convert.ToString(16, 2));
            Console.WriteLine(Convert.ToString(16, 8));
            Console.WriteLine(Convert.ToString(16, 10));
            Console.WriteLine(Convert.ToString(16, 16));
            Console.WriteLine(Convert.ToString(17, 16));

            Console.WriteLine(Convert.ToString(10, 16));
            Console.WriteLine(Convert.ToString(11, 16));
            Console.WriteLine(Convert.ToString(12, 16));
            Console.WriteLine(Convert.ToString(13, 16));
            Console.WriteLine(Convert.ToString(14, 16));
            Console.WriteLine(Convert.ToString(15, 16));
            //https://medium.com/@Pinterest_Engineering/sharding-pinterest-how-we-scaled-our-mysql-fleet-3f341e96ca6f#.e1mz4tx9c
            var binaryValue = Convert.ToString(241294492511762325, 2);
            Console.WriteLine((241294492511762325 >> 46) & 0xFFFF); //= 3429
            Console.WriteLine(Convert.ToString(241294492511762325, 2)); //= 3429
            Console.WriteLine(Convert.ToString(241294492511762325 >> 46, 2)); //= 3429
            Console.WriteLine(Convert.ToString(0xFFFF, 2)); //= 3429
            Console.WriteLine(Convert.ToString(3429, 2)); //= 3429
            Console.WriteLine(Convert.ToString(0xFFFFFFFFF, 2)); //= 3429
            Console.WriteLine((241294492511762325 >> 36) & 0x3FF); //= 1
            Console.WriteLine((241294492511762325 >> 0) & 0xFFFFFFFFF); //= 7075733
            Console.ReadKey();
        }
    }
}
