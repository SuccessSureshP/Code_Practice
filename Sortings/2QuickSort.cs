using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    public class QuickSort
    {
        public static int[] PerformQuickSort(int[] a)
        {
            return DoQuickSort(a, 0, a.Length - 1);
        }

        private static int[] DoQuickSort(int[] a, int left, int right)
        {
            int pivot = Partition(a, left, right);
            if(left < pivot-1)
                DoQuickSort(a,left,pivot-1); //sort left half
            if(pivot < right)
                DoQuickSort(a,pivot, right); // Sort Right half

            return a;
        }

        private static int Partition(int[] a, int left, int right)
        {
            int pivot = (left + right)/2;

            while (left <= right)
            {
                //Keep all elements which are less than our pivot element. find next element which is greater than pivot. Then below loop will be stopped.
                while (a[left] < a[pivot])
                    left++;

                //Keep all elements which are greater than pivot element. Find next element which is less than pivot element. Then below loop will be stopped. 
                while (a[right] > a[pivot])
                    right--;

                if (left <= right) //left points to bigger element than center pivot element. Right points to smaller element than the center pivot element. Swap them
                {
                    int t = a[left];
                    a[left] = a[right];
                    a[right] = t;

                    left++;
                    right--;
                }
            }
            return left;
        }

    }
}
