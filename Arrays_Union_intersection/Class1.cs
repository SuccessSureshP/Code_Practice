using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays_Union_intersection
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            int[] a = { 2, 3, 4, 4, 5, 8, 9 };
            int[] b = { 1, 3, 5, 6, 10, 20 };
            ComputeUnitonAndInterSEction(a, b);
            Console.ReadKey();
        }


        static void ComputeUnitonAndInterSEction(int[] a, int[] b)
        {
            int len1 = a.Length;
            int len2 = b.Length;
            int[] union = new int[len1 + len2];
            int[] intersection = new int[len1 + len2];

            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            while (i < len1 && j < len2)
            {
                int ur = int.MinValue, ir = int.MinValue;
                if (a[i] < b[j])
                {
                    ur = a[i];
                    i++;
                } else if (b[j] < a[i])
                {
                    ur = b[j];
                    j++;
                } else if (a[i] == b[j])
                {
                    ur = a[i];
                    ir = a[i];
                    i++;
                    j++;
                }

                if (k != 0)
                {
                    if (union[k - 1] != ur && ur != int.MinValue)
                    {
                        union[k++] = ur;
                    }
                }
                else if (ur != int.MinValue)
                    union[k++] = ur;
                if (l != 0)
                {
                    if (intersection[l - 1] != ir && ir != int.MinValue)
                    {
                        intersection[l++] = ir;
                    }
                }
                else if (ir != int.MinValue)
                    intersection[l++] = ir;
            }

            while(i< len1)
            {
                union[k++] = a[i++];
            }
            while(j < len2)
            {
                union[k++] = b[j++];
            }

            Console.WriteLine("Union:");
            for (i = 0; i < k; i++)
            {
                Console.Write(union[i] + "  ");
            }
            Console.WriteLine("\nIntersection:");
            for (i = 0; i < l; i++)
            {
                Console.Write(intersection[i] + "  ");
            }
        }
    }
}
