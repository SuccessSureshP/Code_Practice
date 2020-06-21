using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    public class BucketSort
    {
        public static int[] DoBucketSort(int[] a)
        {
            //Verify Input
            if (a == null || a.Length <= 1)
                return a;

            //Finding maximum and minimum
            int max = a[0];
            int min = a[0];
            int n = a.Length;
            for (int i = 1; i < n; i++)
            {
                if (min > a[i])
                    min = a[i];

                if (max < a[i])
                    max = a[i];
            }

            //Now Take buckets of size max-min. Array of buckets. Each bucket is a Linked List
            //i.e, Create a temporary "bucket" to store the values in order
            //each value will be stored in its corresponding index
            //scooting everything over to the left as much as possible (minValue)
            //e.g. 34 => index at 34 - minValue
            LinkedList<int>[] buckets = new LinkedList<int>[max - min + 1]; //LinkedList is more efficient than normal List

            /*when a single element is added to a List<> class, the internal array is expanded to 4 cells. (Basically when the internal array of a List<> is full, 
            all the elements are copied over to a new array double the size of the original array).

            For the purpose of bucket sort, it would be more efficient to have a list that does not constantly copy elements between arrays and does not use any extra space. 
            Turns out the LinkedList<> class is perfect for this. The only difference with LinkedList is using an AddLast call instead of Add, (AddLast is used instead of 
            AddFirst to keep the sorting algorithm stable, ie. keep equal elements in the same order). Also the iteration is a little different*/

            //Now allocate elements of given array  into appropriate buckets
            for (int i = 0; i < n; i++)
            {
                var bucketIndex = a[i] - min; //If max value is 60 and min value is 50,lets say a[i] = 52 , bucketIndex = 52-50 = 2, it will be allocated to 3rd bucket.
                if(buckets[bucketIndex] == null)
                    buckets[bucketIndex] = new LinkedList<int>();

                buckets[bucketIndex].AddLast(a[i]);
            }

            //Now scan each bucket and put numbers from each bucket into final array
            int k = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                {
                    LinkedListNode<int> node = buckets[i].First; //start add head of linked list
                    while (node != null)
                    {
                        a[k++] = node.Value; //get value of current linked node
                        node = node.Next; //move to next linked node
                    }
                }
            }

            return a;
        }


    }
}
