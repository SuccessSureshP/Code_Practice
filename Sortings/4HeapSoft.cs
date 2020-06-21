using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    public class HeapSoft
    {
        static int[] Heapify(int[] a, int ind, int max)
        {
            int left = ind *2 + 1;
            int right = ind *2 + 2;

            int largest;

           //Check if left node is lager than parent node (which is at index ind), if yes make that left node as largest node
            if (left < max && a[left] > a[ind])
                largest = left;
            else
                largest = ind;

            //Check if right node is larger than the largest node we identified in previous step. If yes make that node as largest, otherwise, keep the largest as is
            if (right < max && a[right] > a[largest])
                largest = right;

            if (largest != ind) //If parent is not the largest node. that means either right or left is the largest node. 
                //If either left or right node is larger than parent then largest will not be same as parent index
            {
                //Swap the largest as parent and smaller one to its child
                int temp = a[ind];
                a[ind] = a[largest];
                a[largest] = temp;

                //Now check the swapped node is followed the Heap rule by calling the same method

                a = Heapify(a, largest, max);

            }
            PrintArray(a);
            return a;
        }

        //Build initial  heap from given array
        static int[] BuildHeap(int[] a)
        {
            int n = a.Length;
            for (int i = n/2 - 1; i >= 0; i--) 
                a = Heapify(a, i, n);
            PrintArray(a);
            return a;
        }


        public static int[] PerformHeapSort(int[] a)
        {

            a = BuildHeap(a);
            int n = a.Length - 1;
            for (int i = n; i >=1 ; i--)
            {
                //Largest element will be the 1st one in the array as we already built the heap. So just move that to end
                int t = a[0];
                a[0] = a[i];
                a[i] = t;

                //Again heapfy to make sure largest element will come to first of the array again.
                Heapify(a, 0, i);
            }

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
