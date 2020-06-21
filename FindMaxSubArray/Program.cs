using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMaxSubArray
{
    class Program
    {
        public static void GetMaxSubArray(int[] a )
        {
            if(a.Length == 0)
                throw  new Exception("No elements in the array");

            int startIndex= 0;
            int endIndex = 0; 
            int tempStarIndex = 0;
            int currentSum = a[0];
            int prevSum = a[0];

            for (int i = 1; i < a.Length; i++)
            {
                if (currentSum < 0) //so, take ith element into currentsum as previous sum is negative.
                {
                    currentSum = a[i];
                    tempStarIndex = i;
                }
                else
                {
                    currentSum += a[i];
                }

                if (prevSum <= currentSum) //We found sum so far (prevsum) is less than currentSum. So, track it and store start and end indices
                {
                    prevSum = currentSum;
                    startIndex = tempStarIndex;
                    endIndex = i;
                }

            }
            
            Console.WriteLine("Maximum Sub array , Start Index : {0}", startIndex);
            Console.WriteLine("Maximum Sub array , End Index : " + endIndex);
            Console.WriteLine("Maximum Sub array , Sum of elements : " + prevSum);


        }
        static void Main(string[] args)
        {

            int[] input1 = new[] {4, -2, 6, -7, -3, -5, 2};
            int[] input2 = new[] { 1, 1, -5, 2, 3, 2, -2 };

            GetMaxSubArray(input1);
            GetMaxSubArray(input2);

            Console.ReadKey();
        }
    }
}
