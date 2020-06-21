using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://www.pramp.com/question/gKQ5zA52mySBOA5GALj9
//4 SUM : Given an array of numbers arr and a number S, find 4 different numbers in arr that sum up to S.
//3 SUM : Given an array of numbers arr and a number S, find 3 different numbers in arr that sum up to S.
namespace Array_3Sum_4Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { -5, 2, 4, 8, 3, 6 };

            int[] sumIndices =  Find4Sum(arr, 10);

            Console.WriteLine("4 SUM Indices of 10 are:");            
            if(sumIndices == null)
                Console.WriteLine("No elements found to get sum: 10");
            else
                for (int i = 0; i < sumIndices.Length; i++)
                    Console.Write($"{sumIndices[i]} ");


            sumIndices = Find4Sum(arr, 7);
            Console.WriteLine("\n4 SUM Indices of 7 are:");            
            if (sumIndices == null)
                Console.WriteLine("No elements found to get sum: 7");
            else
                for (int i = 0; i < sumIndices.Length; i++)
                    Console.Write($"{sumIndices[i]} ");


            sumIndices = Find4Sum(arr, 124);
            Console.WriteLine("\n4 SUM Indices of 124 are:");            
            if (sumIndices == null)
                Console.WriteLine("No elements found to get sum: 124");
            else
                for (int i = 0; i < sumIndices.Length; i++)
                    Console.Write($"{sumIndices[i]} ");


            sumIndices = Find3Sum(arr, 4);
            Console.WriteLine("\n3 SUM Indices of 4 are:");
            if (sumIndices == null)
                Console.WriteLine("No elements found to get sum: 4");
            else
                for (int i = 0; i < sumIndices.Length; i++)
                    Console.Write($"{sumIndices[i]} ");

            sumIndices = Find2Sum(arr, -2);
            Console.WriteLine("\n2 SUM Indices of -2 are:");
            if (sumIndices == null)
                Console.WriteLine("No elements found to get sum: -2");
            else
                for (int i = 0; i < sumIndices.Length; i++)
                    Console.Write($"{sumIndices[i]} ");

            Console.ReadKey();
        }

        static int[] Find4Sum(int [] arr,int fourSumValue)
        {
            int[] sumIndices = null;
            Dictionary<int, List<Tuple<int, int>>> hash = GetDictionlaryOfTwoSum(arr);

            //Now we have to iterate through the hash and need to search for every key k1, try to find fourSumValue - k1.
            foreach (var key in hash.Keys)
            {
                if(hash.Keys.Contains(fourSumValue - key)) //we found pair of keys whose sum = fourSumValue. Now, need to find if we can get unique Indices from those associated lists
                {
                    sumIndices = FindUniqueIndices(hash[key], hash[fourSumValue - key]);
                    if (sumIndices  != null)
                        break;
                }
            }

            return sumIndices;
        }
        static Dictionary<int, List<Tuple<int, int>>> GetDictionlaryOfTwoSum(int[] arr)
        {
            Dictionary<int, List<Tuple<int, int>>> hash = new Dictionary<int, List<Tuple<int, int>>>();

            int len = arr.Length;

            for (int i = 0; i < len - 1; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    int twosum = arr[i] + arr[j];
                    if (!hash.Keys.Contains(twosum)) //if no such sum key exists, create empty list to add the tuples
                        hash[twosum] = new List<Tuple<int, int>>();

                    hash[twosum].Add(new Tuple<int, int>(i, j)); // add these 2 Indices to the list of associated sum. No need to have 'else' case, bcoz if twosum key is exists in the hash, we no need to create list object, so we just add the new pair of indices. So, this line has to execute in both the cases
                }
            }

            return hash;

        }
        static int[] FindUniqueIndices(List<Tuple<int,int>> list1, List<Tuple<int,int>> list2)
        {
            int[] sumIndices = null; //Create list of these arrays if we want to find all of such 4 elements that sums up to given value

            foreach (var pair1 in list1)
                foreach(var pair2 in list2)
                {
                    if (pair1.Item1 != pair2.Item1 && pair1.Item1 != pair2.Item2
                      && pair1.Item2 != pair2.Item1 && pair1.Item2 != pair2.Item1
                       )

                    {
                        sumIndices = new int[] { pair1.Item1, pair1.Item2, pair2.Item1, pair2.Item2 };
                        return sumIndices; //Don't return add to list of we want to find all of such 4 elements that sums up to given value
                    }
                }

            return sumIndices;
        }
        static int[] Find3Sum(int[] arr, int threeSumValue)
        {   
            var hash = GetDictionlaryOfTwoSum(arr);

            for(int i=0; i< arr.Length;i++)
            {
                int bal = threeSumValue - arr[i];
                if(hash.Keys.Contains(bal))
                {
                    foreach(var pair in hash[bal])
                    {
                        if(i != pair.Item1 && i != pair.Item2)
                        {
                            return new int[] { i, pair.Item1, pair.Item2 };
                        }
                    }
                }
            }

            return new int[3];
        }

        static int[] Find2Sum(int[] arr, int twoSumValue)
        {
            int len = arr.Length;
            for (int i=0;i<len-1; i++)
            {
                for(int j=i+1;j<len;j++)
                {
                    if (arr[i] + arr[j] == twoSumValue)
                        return new int[] { i, j };
                }
            }

            return null;
        }

    }
}
