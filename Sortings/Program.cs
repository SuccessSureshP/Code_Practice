using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    //6=log (64) => it is binary logarithm (base 2). means 2^6 = 24. logb(x), is the unique real number y such that b^y = x. https://en.wikipedia.org/wiki/Logarithm
    //(log n) Vs. (n log n) , which is smaller? obviously log n. 
    //O(log n) where n = 64 => log 64 = 6 => Ideal case
    //O(nlog n) where n = 64 => 64 * log 64 = 64 * 6 = 384 => Average case
    //O(n^2) => 64^2 => 4096 => Worse base
    //O(n) => o(64) => 64 = Best case
    class Program
    {
        static void Main(string[] args)
        {
            //int[] a = new[] {5, 6, 2, 1, 8};
            //int[] a = new[] { 5, 6, 2, -1, -8 };
            //int[] a = new[] { 5, -8, 2, 6, -1 };

            //int[] a = new[] { 5, 1, 4, 2, 8 }; // Input to explain difference between DoBubbleSort and DoOptimizedBubbleSort

            //int[] a = new[] {3,0, 4, 2,0, 1, 3, 2, 4,0, 1, 2, 1, 3, 2, 3,3,2,2,0};  //Input for Count Sort. here k is 4. All values are range from 0 to 4.

            int[] a = new[] {225, 11, 12, 155, 12};

            Console.WriteLine("Input Array");
            for (int i = 0; i <= a.Length-1; i++)
            {
                Console.Write(a[i] + "   ");
            }
            
            Console.WriteLine("\n-----------------------------------");

            //a  =  InsertionSort.PerformInsertionSort(a);
            //a = SelectionSort.PerformSelectionSort(a);
            //a = HeapSoft.PerformHeapSort(a);
            a = QuickSort.PerformQuickSort(a);
           //a = BubbleSort.DoBubbleSort(a);
            //a = BubbleSort.DoOptimizedBubbleSort(a);
           //a = CountSort.DoCountSort(a, 4);
            //a = BucketSort.DoBucketSort(a);
           // a = MergeSort.DoMergeSort(a);
            //a = RadixSort.DoRadixSort(a);

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Output Array");
            for (int i = 0; i <= a.Length - 1; i++)
            {
                Console.Write(a[i] + "   ");
            }

            Console.ReadKey();

        }
    }
}
