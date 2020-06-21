using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Page 8 of CTG : Chapter 4 : Algorithms
//Find min number in rotated numbers of increasingly sorted array (partially sorted array)
//Since we used binary search here. So, it is O(log n). If we find min element sequentially from start to end , it is O(n), which is not good/not small as o(log n). 
namespace PartiallySortedArrayBinarySearch
{
    class Program
    {
        static int GetMin(int[] a)
        {
            int left = 0;
            int right = a.Length - 1;
            int mid = left;
            while (a[left] >= a[right])
            {
                if (left + 1 == right) //If left reached end of 1st sub array of increasing numbers and right reached starting of 2nd sub array of increasing numbers. Then that element at right is the minimum number
                    return a[right];

                mid = (left + right)/2;

                if (a[left] == a[mid] && a[mid] == a[right])
                    return SearchMinSequantially(a, left, right);

                if (a[mid] >= a[left])
                    //mid element is in part of 1st sub array of increasing numbers. So ,min will be 2nd sub array
                    left = mid;
                else if (a[mid] <= a[right]) //mid element is not greater than left element , that means it is somewhere in 2nd sub array. So, we need to search all elements to the left of the mid element to find the minimum number
                    right = mid;
            }

            return a[mid]; //it will come here if given array is already sorted initially. 
        }

        private static int SearchMinSequantially(int[] a, int left, int right)
        {
            int min = a[left];
            for(int i= left;i<right;i++)
                if (a[i] < min)
                    min = a[i];

            return min;
        }

        static void Main(string[] args)
        {
            //Test cases
            int[] a = new[] {3, 4, 5, 1, 2}; //Partially sorted
            //int[] a = new[] {4,4,4,5,4,4,4,4,4}; //Partially sorted
            //int[] a = new[] {1,0,1,1,1,1}; // go to SearchMinSequantially
            //int[] a = new[] { 1,2,3,4 }; //Fully sorted
            //int[] a = new[] { 1 }; //Single element 
            //int[] a = new[] { 1,1 }; //all elements of same value
            //int[] a = new[] { 3, 4, 5, 6, 7, 8, 9,10,11, 1, 2 }; //Partially sorted

            Console.WriteLine("Min number is :{0}",GetMin(a));
            Console.WriteLine("MAX number is :{0}", GetMax(a));
            Console.WriteLine("Index of {0} is :{1}", 4, FindIndexOf(a, 4));
            a = new int[] { 5, 1, 2, 3, 4 };
            Console.WriteLine("Index of {0} is :{1}", 1, FindIndexOf(a, 1));

            a = new int[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 };
            Console.WriteLine("Index of {0} is :{1}", 9, FindIndexOf(a, 9));
            Console.WriteLine("Index of {0} is :{1}", 5, FindIndexOf(a, 5));
            a = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            Console.WriteLine("Index of {0} is :{1}", 6, FindIndexOf(a, 6));
            Console.WriteLine("Index of {0} is :{1}", 2, FindIndexOf(a, 2));

            Console.ReadKey();

        }

        static private int GetMax(int [] a)
        {
            int left = 0;
            int right = a.Length - 1;
            int mid = right; //Setting default value to last element so that if in case list already sorted, return last elemenent
            while(a[left] >= a[right])
            {
                if(left+1 == right) //we reached end of fist increasing list in left and begenning of the decreasing 2nd sub list in the right. 
                {
                    return a[left];  // so, left holds the max number
                }

                mid = (left + right) / 2;

                if (a[left] == a[mid] && a[right] == a[mid])
                   return  SearchMaxSequantially(a,left,right);

                if (a[mid] >= a[left]) //mid element is somewhere in the 1st sub array
                {
                    left = mid;
                }
                else
                    if (a[mid] <= a[right]) //mid element is somewhere in the 2nd sub array. So Max element wil be on left side.
                    right = mid;
            }
            return a[mid]; 
        }
       static int SearchMaxSequantially(int[] a, int left, int right)
        {
            int max = a[left];
            while (left <= right)
            {
                if (max < a[left])
                    max = a[left];
                left++;
            }
            return max;
        }
        //Find the index of a given element in the array?
        static int FindIndexOf(int [] a, int value) //Binary Search on Partially sorted Array
        {
            int left = 0;
            int right = a.Length - 1;
            while (left < right)
            {
                if (a[left] == value)
                    return left;
                if (a[right] == value)
                    return right;

                int mid = (left + right) / 2;

                if (a[mid] == value)
                    return mid;
                if(a[left] <= a[mid]) //Array from left to mid is increasing order. Ex., 3,4,5,6,'7',8,9,1,2. here 3 <=7. 
                {
                    if (a[left] <= value && value <= a[mid]) // if value to be searched is in the first increasing subset (between left and mid). For ex., if we looking for 5
                        right = mid - 1;
                    else //No, it is not in the increasing subset. It is somewhere else. So, check the rest of the array by moving left to mid+1. For ex., if we looking for 1
                        left = mid + 1; 
                }
                else // This means, Array from mid to right are in increasing order . Ex., is 8,9,1,2,'3',4,5,6,7. Here 8 <= 3, which is false
                {
                    if (a[mid] <= value && value <= a[right]) // If value to be searched is in the 2nd increasing subset (between mid and right). Ex., if we looking for 5
                        left = mid + 1;
                    else //No, it is not in the increasing subset after mid. So, change right to mid-1 and do search. for ex., if we looking for 1
                        right = mid - 1;
                }   
            }
            return -1;
        }
    }
}
