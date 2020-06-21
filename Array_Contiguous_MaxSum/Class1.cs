using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Contiguous_MaxSum
{
    public class Class1
    {
        public static void Main()
        {
            int[] a = { 4, 1, -2, 5, -8, -6, 7, 6 };
            int[] arr = { 0, 1, 2, -5, 7, 6 };
            int[] arr1 = { 0 };
            int[] arr2 = { -1, -2 };
            int[] arr3 = { -1 };
            int[] arr4 = { 2, 1 };
            int[] arr5 = { 1, 2 };
            int[] arr6 = { 1, 2, -3 };
            int[] arr7 = { 0, 1, 2, -5, -8, 7, 6 };
            ContigousSubArray(a);

            ContigousSubArray(arr);
            ContigousSubArray(arr1);
            ContigousSubArray(arr2);
            ContigousSubArray(arr3);
            ContigousSubArray(arr4);
            ContigousSubArray(arr5);
            ContigousSubArray(arr6);
            ContigousSubArray(arr7);

            Console.ReadKey();
        }

        static void ContigousSubArray(int[] a)
        {
            int currentSum = 0;
            int currentSumStartIndex = 0;
            int resultStartIndex =0;
            int resultEndIndex = 0;
            int resultSum = 0;
            for (int i=0;i<a.Length;i++)
            {
                if (currentSum == 0)
                {
                    if (a[i] > 0)
                    {
                        currentSum = a[i];
                        currentSumStartIndex = i;
                    }
                }
                else
                    currentSum += a[i];

                if (currentSum < 0)
                    currentSum = 0;


                if (currentSum > resultSum)
                {
                    resultSum = currentSum;
                    resultStartIndex = currentSumStartIndex;
                    resultEndIndex = i;
                }
            }

            Console.WriteLine($"Max Sum: {resultSum}");
            Console.WriteLine($"Start Index : {resultStartIndex}");
            Console.WriteLine($"End Index: {resultEndIndex}");
        }

    }
}
