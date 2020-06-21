using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sortings
{
    //Find min and put it in first place and then find next min and put it in next place and so on.
    public class SelectionSort
    {
        public static int[] PerformSelectionSort(int[] a)
        {
            for (int i = 0; i < a.Length-1; i++)
            {
                int j = FindMinPosition(a, i, a.Length - 1);
                int t = a[i];
                a[i] = a[j];
                a[j] = t;
            }

            return a;
        }

        static int FindMinPosition(int[] a, int start, int end)
        {
            if(start+1 == end)
                if(a[start] < a[end])
                    return start;
                else
                    return end;

            int rest = FindMinPosition(a, start + 1, end); //Recursive way to find smaller item's position
            if (a[start] < a[rest])
                return start;
            else
                return rest;

        }
    }
}
