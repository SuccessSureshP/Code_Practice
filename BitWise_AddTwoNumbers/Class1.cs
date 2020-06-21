using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWise_AddTwoNumbers
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            int a = 999;
            int b = 99;
           
            //int r =  Add(a, b);
            int r = Add2(a, b);
            Console.WriteLine($"{a}+{b}={r}");
            a = 5;
            b = 2;
            
             r = Add2(a, b);
            Console.WriteLine($"{a}+{b}={r}");
            Console.ReadKey();
        }

        public static int Add(int a,int b)
        {
            if (a == 0 || b == 0)
                return a ^ b;

            int sum = a ^ b;
            int carry = a & b;
            carry = carry << 1;
            return Add(sum, carry);
        }

        public static int Add2(int a,int b)
        {
            int c = 0;
            int s = 0;
            do
            {
                s = a ^ b;
                c = (a & b) << 1;
                a = s;
                b = c;
            } while (c != 0);

            return s;
        }
    }
}
