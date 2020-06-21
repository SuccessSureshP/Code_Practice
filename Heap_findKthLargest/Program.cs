using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap_findKthLargest
{
    //Comparison of top down and bottom up approach for heap: https://www.youtube.com/watch?v=JnzoaqITEQY    
    //Bottom Up Heap building is O(n)
    //Topdown Heap Building is O(n logn)
    //Top down Heapify : https://youtu.be/2fA1FdxNqiE?t=11m24s 
    //kth Smallest element : http://www.geeksforgeeks.org/kth-smallestlargest-element-unsorted-array/
    //#1. A Simple Solution for kth smallest is to sort the given array using a O(nlogn) sorting algorithm like Merge Sort, Heap Sort, etc and return the element at index k-1 in the sorted array. Time Complexity of this solution is O(nLogn). Check Sorting programs
    //#2. 2nd solution is Build Min heap and extract elements k times.  Time complexity of this solution is O(n + kLogn).
    //#3. We can also use Max Heap for finding the k’th smallest element. Following is algorithm.
    //1) Build a Max-Heap MH of the first k elements(arr[0] to arr[k - 1]) of the given array.O(k)

    //2) For each element, after the k’th element(arr[k] to arr[n - 1]), compare it with root of MH.
    // ……a) If the element is less than the root then make it root and call heapify for MH
    //……b) Else ignore it.
    //// The step 2 is O((n-k)*logk)

    //3) Finally, root of the MH is the kth smallest element.

    //Time complexity of this solution is O(k + (n-k)* Logk)
    //#4. QuickSelect : This approach returns k'th smallest element in arr[l..r] using
    // QuickSort based method.  ASSUMPTION: ALL ELEMENTS IN ARR[] ARE DISTINCT (check website URL)
    class Program
    {
        static void Main(string[] args)
        {
            var a = new int[] { 9, 8, 2, 1, 3, 4 };

            //#2 approach. Build Min heap and get k times for result
            MinHeap minheap = new MinHeap(a.Length);
            for(int i=0;i<a.Length;i++)
            {
                minheap.Insert(a[i]);
            }            
            minheap.DisplayHeapArray();
            Console.WriteLine();
            // to get min value. just call Get method
            int minvale =  minheap.Get();
            Console.WriteLine("1st Min value = " + minvale);
            Console.WriteLine("\nRemaining Heap :");
            minheap.DisplayHeapArray();
            //Insert min value back to heap 
            minheap.Insert(minvale);
            Console.WriteLine("\nAfter inserting 1 again, Heap is :\n");
            minheap.DisplayHeapArray();
            //to get kth largest element, call get k times now. thats it.
            int k = 4;
            int kthSmallest = 0;
            for (int i = 0; i < k; i++)
            {
                try
                {
                    kthSmallest = minheap.Get();

                }catch(Exception exp)
                {
                    if (exp.Message.Equals("Heap is full"))
                        Console.WriteLine("K is greater than # of elements in the heap");
                    return;
                }
            }
            Console.WriteLine($"\n{k} th smalleset element is : {kthSmallest}");

            Console.WriteLine("\nRemaining Heap :");
            minheap.DisplayHeapArray();

            k = 5;
            var kthsmall =  FindKthMinUsingMaxHeap(a, k);
            Console.WriteLine($"\n{k} th smalleset element is : {kthsmall}");
         

            k = 0;
            kthsmall = FindKthMinUsingMaxHeap(a, k);
            Console.WriteLine($"\n{k} th smalleset element is : {kthsmall}");
            Console.ReadKey();
        }

        //#3 approach
        static int FindKthMinUsingMaxHeap(int[] a, int k)
        {
            if (a.Length == 0)
                throw new Exception("Array is empty");
            if (k > a.Length)
                throw new Exception("Wrong K");
            
            if (k == 0) //If k is zero, we may have to return 1st min. so making k as 1.
                k = 1;
            //1) Build a Max-Heap MH of the first k elements(arr[0] to arr[k - 1]) of the given array.O(k)
            MaxHeap mh = new MaxHeap(k);
            for (int i=0;i<k;i++)
            {
                mh.Insert(a[i]);
            }
            //2) For each element, after the k’th element(arr[k] to arr[n - 1]), compare it with root of MH.

            for(int i = k;i < a.Length;i++)
            {
                if (mh.GetTop() > a[i])
                    mh.ReplaceTop(a[i]);
            }
            //3) Finally, root of the MH is the kth smallest element.
            return mh.GetTop(); 
        }
    }

    public class MinHeap
    {
        int[] a;
        int length = 0;
        int current = 0;
        public MinHeap(int length)
        {
            this.a = new int[length];
            this.length = length;
        }

        public void Insert(int v)
        {
            if (current == length)
                throw new Exception("Heap is full");
            a[current] = v;
            this.BottomUpHeapfy();
            current++;
        }
        
        private void BottomUpHeapfy()
        {
            int i = current;
            while(i > 0)
            {
                int parent = i - 1 / 2;
                if(a[i] < a[parent])
                {
                    swap(i, parent);
                }
                i = parent;   
            }
        }

        void swap(int i,int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public int Get() 
        {
            if (current == 0)
                throw new Exception("Empty heap");
            int i = 0;
            int res = a[i];
            a[i] = a[current-1]; //Move last element to the head of the heap.
            current--; //update index saying next element can insert at current-1 poison (that will replace duplicate value)
            this.TopDownHepify(i);
            return res;
        }

        private void TopDownHepify(int i)
        {
            int leftchild = (2 * i) + 1;
            int rightChild = (2 * i) + 2;

            int min = i;
            if (leftchild <= current && a[leftchild] < a[i])
                min = leftchild;

            if (rightChild <= current && a[rightChild] < a[min])
                min = rightChild;
            
            if(min != i) // means we found small element at index min. either it is its left child or right child. So, swap those elements.
            {
                swap(i, min);
                //Now we need to heapify subtree where its element is moved. now it is in min index.
                TopDownHepify(min);
            }
        }

        public void DisplayHeapArray()
        {
            for (int i = 0; i < current; i++)
                Console.Write(a[i] + " ");
        }
    }


    public class MaxHeap
    {
        int[] a;
        int length;
        int current;
        
        public MaxHeap(int l)
        {
            length = l;
            a = new int[l];
            current = 0;
        }  

        public void Insert(int v)
        {
            if (current >= length)
                throw new Exception("Heap is full");
            a[current] = v;
            BottomUpHeapify();
            current++;
        }

        private void BottomUpHeapify()
        {
            int i = current;
            while(i> 0)
            {
                int parent = i - 1 / 2; 
                if(a[parent] < a[i])
                {
                    swap(parent, i);
                }
                i = parent;
            }
        }

        private void swap(int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public int Get()
        {
            if (a.Length == 0)
                throw new Exception("Heap is empty");

            int r = a[0];
            a[0] = a[current];
            current--;
            TopdownHeapify(0);
            return r;
        }

        private void TopdownHeapify(int parent)
        {
            int leftchihld = parent * 2 + 1;
            int rightChild = parent * 2 + 2;

            int max = parent;
            if (leftchihld < current && a[leftchihld] > a[parent])
                max = leftchihld;

            if (rightChild < current && a[rightChild] > a[max])
                max = rightChild;

            if(parent != max) //If we found max which is not curret root. do a swap with that max number
            {
                swap(parent, max);

                TopdownHeapify(max);
            }
        }

        public int GetTop()
        {
            if (a.Length == 0)
                throw new Exception("Heap is empty");

            return a[0];
        }

        public void ReplaceTop(int value)
        {
            if (a.Length == 0)
                throw new Exception("Heap is empty");

            a[0] = value;
            TopdownHeapify(0);
        }
    }
}
