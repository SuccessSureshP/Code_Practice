using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// With Arrays : http://quiz.geeksforgeeks.org/merge-sort/
namespace Sortings
{
   public class MergeSort
    {
       public static int[] DoMergeSort(int[] a)
       {
           List<int> input = new List<int>();
           for (int i = 0; i < a.Length; i++)
           {
               input.Add(a[i]);
           }

           var outPut = MergeSorting(input);
           int k = 0;
           foreach (var integer in outPut)
           {
               a[k++] = integer;
           }

           return a;
       }

       private static List<int> MergeSorting(List<int> input)
       {
          List<int> output = new List<int>();
           List<int> left = new List<int>();
           List<int> right = new List<int>();
           int n = input.Count;
           //Base case
           if (n <= 1)
               return input;


           int middle = n/2;
           for (int i = 0; i < middle; i++)
           {
               left.Add(input[i]);
           }
           for (int i = middle; i < n; i++)
           {
               right.Add(input[i]); 
           }

           //Recursive call
           left = MergeSorting(left); 
           right = MergeSorting(right);

           if (left[left.Count - 1] <= right[0]) //comparing last element of Left and first element of Right. True means all elements of Left are smaller than all elements of Right.  And, both Left and Right already sorted.
               return Append(left, right); // So, just append them.

           output = Merge(left, right); //Above IF condition gives False, So, merge all elements carefully into single array.

           return output;
       }

       private static List<int> Append(List<int> left, List<int> right)
       {
           foreach (var integer in right)
           {
               left.Add(integer);
           }

           return left; //Added all elements of right to the end of left.
       }

       private static List<int> Merge(List<int> left, List<int> right)
       {
           List<int> r = new List<int>();

           while (left.Count > 0 && right.Count > 0)
           {
               if (left[0] < right[0])
               {
                   r.Add(left[0]);
                   left.RemoveAt(0);
               }
               else
               {
                   r.Add(right[0]);
                   right.RemoveAt(0);
               }
           }

           while (left.Count > 0) //If Right list becomes empty before left list in above loop, just add remaining elements into the result
           {
               r.Add(left[0]);
               left.RemoveAt(0);
           }
           while (right.Count > 0) //If Left list becomes empty before Right list in above loop, just add remaining elements into the result
           {
               r.Add(right[0]);
               right.RemoveAt(0);
           }

           return r;
       }
    }
}
