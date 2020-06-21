using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    public class CountSort
    {
        public static int[] DoCountSort(int[] a, int k) //K is the upper bound of the allowed values from 0. 
        {
            int[] b = new int[k+1]; //there are k+1 allowed values from 0 to K+1. So , take an array of k+1  size
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                if (a[i] > k)
                    throw new Exception("Value not with in the range 0 to" + k);
                b[a[i]]++;
            }

            int index = 0;
            for (int j = 0; j <= k; j++)
            {
                while (b[j] > 0)
                {
                    a[index++] = j;
                    b[j]--;
                }
            }

            return a;
        }
    }
}
