using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeEvalChalanges
{
    public  static  class StringPermutations
    {
        public static void GetStringPermutations(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName)) 
                while (!reader.EndOfStream)
                {
                    string givenString = reader.ReadLine();
                    
                    _allStrings.Clear();

                    permute(givenString,0,givenString.Length-1);
                    _allStrings.Clear();
                    GetAllPremutes(givenString); //Simple way
                    Console.WriteLine("All Permutations:");
                    PrintValues(_allStrings);
                    _allStrings.Clear();


                    PermutationsWithRepetitions(givenString.ToCharArray(), new char[givenString.Length], givenString.Length, 0, givenString.Length - 1);
                    Console.WriteLine();
                    Console.WriteLine("All Permutations With Repetitions:");
                    PrintValues(_allStrings);
                    _allStrings.Clear();


                    _allStrings =  GetAllCombinations(givenString.ToCharArray(),0);
                    Console.WriteLine();
                    Console.WriteLine("All Combinations:");
                    PrintValues(_allStrings);
                    _allStrings.Clear();

                    Console.WriteLine();
                }
        }

        static void PrintValues(List<string> result)
        {
            int counter = 0;
            foreach (var str in result)
            {
                if (!string.IsNullOrEmpty(str))
                    Console.Write(str);
                counter++;
                if (counter < result.Count)
                    Console.Write(',');
            }
        }
        //Check here http://www.mathsisfun.com/combinatorics/combinations-permutations.html
        private static List<string> _allStrings = new List<string>(); //we will get n! items if n is size of given string.  Actual formula for permutation is N C r = N ! / (n-r) ! => here r is # of items to chose to create a permutation. In our example , we are choosing all n characters for all permutations. So, r = n here. so, n!/(n-n)! => n!/0! => n!/1 => n!
        //this is complex way. check the next method for simple way
        //http://www.geeksforgeeks.org/write-a-c-program-to-print-all-permutations-of-a-given-string/
        static void permute(string modifiedString, int l, int r)
        {
            //Take all characters  from r-l+1 to the end and soft them first. i.e., we are sorting remaining string before permuting with it. 0 to l characters are constant. So, just add them as it is with strin.Contcat 
            char[] remaining = new char[r-l+1];
            modifiedString.CopyTo(l, remaining, 0, r - l + 1);
             Array.Sort(remaining);
             modifiedString = string.Concat(modifiedString.Substring(0, l), new string(remaining));  //0 to l characters are constant. So, just add them as it is with strin.Contcat 

            int i;
            if (l == r)
                _allStrings.Add(modifiedString);
            else
            {
                for (i = l; i <= r; i++)
                {
                    //Check :http://www.geeksforgeeks.org/write-a-c-program-to-print-all-permutations-of-a-given-string/
                    modifiedString = Swap(modifiedString.ToCharArray(), l, i);
                    permute(modifiedString,l+1,r);
                    modifiedString = Swap(modifiedString.ToCharArray(), l, i); //Backtrack
                }
            }
        }

        private static string  Swap(Char[] modifiedStringArray, int l, int i)
        {
            var c = modifiedStringArray[l];
            modifiedStringArray[l] = modifiedStringArray[i];
            modifiedStringArray[i] = c;

            return new string(modifiedStringArray);
        }

        //Simple Approach which will give stings in lexicographic order (http://www.geeksforgeeks.org/lexicographic-permutations-of-string/) 
        private static void GetAllPremutes(string str) //we will get n! items if n is size of given string
        {
            char[] input = str.ToCharArray();
            Array.Sort(input); // sort the given string to get lexicographic order 
            bool[] used = new bool[input.Count()];
            StringBuilder sb = new StringBuilder();
            int len = input.Length;

            DoPermute(input, used, sb, 0, len);
        }

        private static void DoPermute(char[] input, bool[] used, StringBuilder sb, int st, int level)
        {

            if (st == level)
            {
                _allStrings.Add(sb.ToString());
                return;
            }

            
            for(int i=0;i< level;i++)
            {
                if (used[i] == true)
                    continue;

                sb.Append(input[i]);
                used[i] = true;               

                DoPermute(input, used, sb, st + 1, level);
                used[i] = false;
                sb.Remove(sb.Length - 1, 1);

            }
            
        }


        //This below method will give all combinations  of a given string. 
        // If n is the number of things to choose from, and we choose r of them. (order doesn't matter) => formula is nCr => n!/ (n-r)! r!. For all combinations we have to chose all combinations of size 1, 2, 3, ... n. So, r runs from 1 , 2, ,3....n
        //For a string of length n, we will get n C n + N C (n-1) +.......Nc1 combinations. if N = 3,we will get 7. If n = 4, we will get 15. 
        //If N = 3 => we need all combinations of 3 characters (3C3) + we need all combinations of 2 characters (3C2) + we need all combinations of 1 characters (3C1)
       //nCr = n!/((n-r)! * r!)      => 3!/(0! * 3!) + 3!/(1!* 2!) + 3!/(2!* 1!) = 1 (ABC)+3(AB,BC,AC)+3(A,B,C) = 7
        static List<string> GetAllCombinations(char[] s, int i)
        {
            List<string> subseqs = new List<string>();
            subseqs.Add(s[i].ToString());

            if (s.Length - 1 == i)
                return  ;

            var temp_result = GetAllCombinations(s, i + 1);

            for (int j = 0; j < temp_result.Count(); j++)
            {
                subseqs.Add(s[i].ToString() + temp_result[j]);
            }
            subseqs.AddRange(temp_result);
            return subseqs;

        }


        //http://www.geeksforgeeks.org/print-all-permutations-with-repetition-of-characters/
        static void PermutationsWithRepetitions(char[] input,char[] data, int length, int index, int lastIndex)
        {
            for(int i = 0;i< length;i++)
            {
                data[index] = input[i];

                if (index == lastIndex)
                    _allStrings.Add(new string(data));
                else
                    PermutationsWithRepetitions(input, data, length, index + 1, lastIndex);
            }
        }

    }
}
