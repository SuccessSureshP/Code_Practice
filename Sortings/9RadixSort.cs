using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    public class RadixSort
    {
        public static int[] DoRadixSort(int[] a)
        {
            List<int>[] buckets = new List<int>[10];

            //We need to find max digit length out of given input
            int maxlength = 0;
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                var currentNumberLength = a[i].ToString().Length;
                if (currentNumberLength > maxlength)
                    maxlength = currentNumberLength;
            }

            for (int i = 0; i < maxlength; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int digit = (int)((a[j] % Math.Pow(10, i + 1)) / Math.Pow(10, i)); //This will pick digit of each number starting from LSB (least significant bit - right most digit )and proceeding to MSB (Most significant bit - left most digit ) on each value of i.

                    if(buckets[digit] == null)
                        buckets[digit] = new List<int>();

                    buckets[digit].Add(a[j]);
                }
                //Overwrite a from numbers from buckets
                int k = 0;
                for (int j = 0; j < buckets.Length; j++)
                {
                    if (buckets[j] != null)
                    {
                        foreach (var integer in buckets[j])
                        {
                            a[k++] = integer;
                        }
                    }
                }

                //Clear the buckets before iterating with next value of i (next digit)
                buckets = new List<int>[10];

            }

            return a;
        }
    }
}
