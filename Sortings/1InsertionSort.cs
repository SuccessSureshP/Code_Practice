using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    //Find each element position by comparing and shifting all elements before that element starting from previous element. If we find right place put it there.
    public class InsertionSort
    {
        public static int[] PerformInsertionSort(int [] a)
        {
            if(a.Length == 0)
                Console.WriteLine("No elements to Sort");
            if(a.Length ==1)
                Console.WriteLine(a[0]);

            for (int i = 1; i <= a.Length-1; i++)
            {
                int value = a[i];
                int j = i - 1;
                while (j>=0 && a[j] > value)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = value;
            }

            return a;
        }

    }
}
