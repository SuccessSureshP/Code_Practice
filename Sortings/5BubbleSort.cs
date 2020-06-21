using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    public class BubbleSort
    {
        public static int[] DoBubbleSort(int[] a)
        {
            bool swapped;
            int n = a.Length;
            do
            {
                swapped = false;
                for (int i = 1; i < n; i++) //On every loop, we are placing the largest element to the end. So, we really no need to compare till end on every time. check optimized solution below 
                {
                    if (a[i - 1] > a[i])
                    {
                        int t = a[i];
                        a[i] = a[i - 1];
                        a[i - 1] = t;
                        swapped = true;
                    }
                    Console.Write("i=" + i + " =>   ");
                    PrintArray(a);
                }
                Console.WriteLine();
            }while(swapped); //If no swapping done, no need to iterate the loop. 

            return a;
        }

        public static int[] DoOptimizedBubbleSort(int[] a)
        {
            int upperBound;
            int n = a.Length;
            do
            {
                upperBound = 0; //Reseting the variable.
                for (int i = 1; i < n; i++) //Here n Array upper bond only first time. Later it will be updated at end of this do while loop.
                {
                    if (a[i - 1] > a[i])
                    {
                        int t = a[i];
                        a[i] = a[i - 1];
                        a[i - 1] = t;
                        upperBound = i;
                    }
                    Console.Write("i="+i + " =>   ");
                    PrintArray(a);
                }
                Console.WriteLine();
                n = upperBound; //Replacing the n with last swapped i value. That means numbers after that were not swapped i.e., they are greater last swapped ith value and they were already in sorted order. So, no need to check with them. So reducing the upper bound for next for loop.

            } while (n>0);

            return a;
        }

        static void PrintArray(int[] a)
        {
            for (int i = 0; i <= a.Length - 1; i++)
            {
                Console.Write(a[i] + "   ");
            }
            Console.WriteLine();
        }
    }
}
