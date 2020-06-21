using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays_Pivot_based_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 2, 4, 5, 2, 1, 2, 1, 3 };
            Shuffle(a, 0); // 0 is index of the pivot here
           // int[] b = { 1, 2, 3, 1, 2, 6, 2, 1, 5 };
            int[] b = { 1, 2, 0, 3, 5, 1, 2, 6, 7, 2, 1, 5 };
            Print(b);
            QuickSortIndex(b, 5); // 5 is pivot value , not index of pivot
            Print(b);

            int[] c = { 1, 2, 0, 3, 5, 1, 2, 6, 7, 2, 1, 5 };
            QuickSortIndex(c, 2); // 2 is pivot value , not index of pivot
            //Shuffle(b, 4);- Wrong
            Print(c);
            Console.ReadKey();
        }
        static void Print(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");
            Console.WriteLine();
        }
        static int QuickSortIndex(int[] a, int pivot)
        {
            int operationCount = 0;
            int i = 0;
            int j = a.Length - 1;
            // first iteration
            while (i < j)
            {
                if (a[i] < pivot)
                {
                    operationCount++;
                    i++;
                }
                else if (a[j] >= pivot)
                {
                    operationCount++;
                    j--;
                }
                else if (a[i] > a[j])
                {
                    operationCount++;
                    Swap(a, i, j);
                    i++;
                    j--;
                }
            }
            Console.WriteLine($"Middle Output: {string.Join(",", a).ToString()}");
            // second iteration
            j = a.Length - 1;
            while (i < j)
            {
                if (a[i] <= pivot)
                {
                    operationCount++;
                    i++;
                }
                else if (a[j] != pivot)
                {
                    operationCount++;
                    j--;
                }
                else if (a[j] == pivot)
                {
                    operationCount++;
                    Swap(a, i, j);
                    i++;
                    j--;
                }
            }

            return operationCount;
        }

        static void Swap(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
        //Wrong code. Infinite loop for array b
        static void Shuffle(int[] a, int pivotIndex)
        {
            if (a.Length == 0)
                throw new Exception("No elements");
            if (pivotIndex < 0 || pivotIndex > a.Length - 1)
                throw new Exception("Wrong Pivot index");
            int i = 0;
            int j = a.Length - 1;
            int pivotValue = a[pivotIndex];
            while (i < j)
            {
                if (a[i] > pivotValue && a[j] < pivotValue)
                {
                    int t = a[i];
                    a[i] = a[j];
                    a[j] = t;
                    i++;
                    j--;
                }
                else if (a[i] == pivotValue)
                {
                    if (a[j] < pivotValue)
                    {
                        a[i] = a[j];
                        a[j] = int.MaxValue;
                        i++;
                        j--;
                    }
                    else
                        j--;

                }
                else if (a[j] == pivotValue)
                {
                    if (a[i] > pivotValue)
                    {
                        a[j] = a[i];
                        a[i] = int.MinValue;
                        i++;
                        j--;
                    }
                    else
                        i++;
                }
                else if (a[i] < pivotValue && a[j] > pivotValue)
                {
                    i++;
                    j--;
                }
            }

            i = 0;
            while (a[i] < pivotValue && i < a.Length)
            {
                if (a[i] == int.MinValue)
                {
                    //find last element which is less than pivot value and swap values 
                    j = i;
                    while (j < a.Length && a[j] < pivotValue)
                        j++;
                    if (j == a.Length || j - 1 == i)
                        a[i] = pivotValue;
                    else
                    {
                        a[i] = a[j - 1];
                        a[j - 1] = pivotValue;
                    }
                }
                i++;
            }
            i = a.Length - 1;
            while (a[i] > pivotValue && i > 0)
            {
                if (a[i] == int.MaxValue)
                {
                    //find first element which is great than pivot value and swap values 
                    j = i;
                    while (j >= 0 && a[j] > pivotValue)
                        j--;
                    if (j == 0 || j + 1 == i)
                        a[i] = pivotValue;
                    else
                    {
                        a[i] = a[j + 1];
                        a[j + 1] = pivotValue;
                    }
                }
                i--;
            }


        }
    }
}
