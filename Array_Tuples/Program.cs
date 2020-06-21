using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Array_Tuples
{
    //For a given array A[] of integers and an integer X, Find all distinct tuples <p,q> such that p+q = x.
    //http://www.geeksforgeeks.org/write-a-c-program-that-given-a-set-a-of-n-numbers-and-another-number-x-determines-whether-or-not-there-exist-two-elements-in-s-whose-sum-is-exactly-x/
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] input_arr =  {12, 3, 10, 5, 7, 6, 8, -1, 4};
            int x = 11;

            //Another approach with o(n) complxity is : using hash table. Scan array and put key = -1 and value = 12(since we need -1 to take 12 in our result pair). Now for each element check if it is in Hashtable and if that is exists the we got pair (element, H[element]). remove the element from the has immediately since that value considered. this is to get distinct tuples
            //First we have to sort the elements by using any sort algorithm. Here is result
            int[] arr = { -1, 3, 4, 5, 6, 7, 8, 10, 12};


            List<Tuple<int, int>> result = GetTuples(arr, x);

            if(result.Count ==0)
                Console.WriteLine("No Tuples found!");

            foreach (var tuple in result)
            {
                Console.Write("<{0},{1}>  ",tuple.Item1,tuple.Item2);
            }
            Console.Read();
        }

        private static List<Tuple<int, int>> GetTuples(int[] arr, int x)
        {
            var result = new List<Tuple<int, int>>();
            var lenght = arr.Length;

            int i = 0;
            int j = lenght - 1;

            while (i<j)
            {
                var sum = arr[i] + arr[j];
                if (sum == x)
                {
                    result.Add(new Tuple<int, int>(arr[i],arr[j]));
                    i++;
                    j--;
                }

                if (sum < x) // If sum less than our expected , we can ignore smaller number
                i++; 
                else if (sum > x) //else if Sum greater than our expected value, we can ignore larger number
                    j--;
            }

            return result;
        }
    }
}
