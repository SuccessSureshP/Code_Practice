using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://www.youtube.com/watch?v=U4yPae3GEO0&list=PLamzFoFxwoNjtJZoNNAlYQ_Ixmm2s-CGX 
//Sub Sequences is set of all combinaitons of all sizes but by keeping the givne sequence of charactrers.
//In combidnaton, order of character doesn't count speraterly. AB and BA is a single combination ,but they are 2 permutations. 
//In permutations, changing of order of characters gives different permutation
//
namespace Strings_All_Subsequences
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "BBABCBCAB";//"ABC"; 
            var res = GetAllSubsequences(str.ToCharArray(), 0);
            int len = 0;
            foreach(var s in res)
            {
                if (isPallendrom(s) && s.Length > len)
                    len = s.Length;
                    
            }
            Console.WriteLine(isPallendrom(str.ToString()));
            Console.WriteLine("Length of Longest Palindromic Subsequence is :"+len);
            //DP approach
            var res2 = LengthOfLongestPallendromicSubSequence("BBABCBCAB"); //==> check in the Order #2
            Console.WriteLine("LengthOfLongestPallendromicSubSequence(BBABCBCAB)=" + res2);          

            res2 = LengthOfLongestPallendromicSubSequence("LPASPAL"); //==> check in the Order #2
            Console.WriteLine("LengthOfLongestPallendromicSubSequence(LPASPAL)=" + res2);

            var res1 = LongestPalindromicSubstring("Bananas");  // ==> check in the Order #1
            Console.WriteLine("LongestPalindromicSubstring(Bananas): " + res1);

            var res3 = LongestCommonSubsequenceofTwoStrings("ACEBA", "ADCA"); //==> check in the Order #3
            Console.WriteLine("LongestCommonSubsequenceofTwoString: " + res3);

            var res4 = LongestCommonSubStringofTwoStrings("LCLC", "CLCL"); //==> check in the Order #4
            Console.WriteLine("LongestCommonSubStringofTwoStrings:");
            foreach (var s in res4)
                Console.WriteLine(s);

            res4 = LongestCommonSubStringofTwoStrings("Mondays", "Monumnetdays"); //==> check in the Order #4
            Console.WriteLine("LongestCommonSubStringofTwoStrings:");
            foreach (var s in res4)
                Console.WriteLine(s);

            Console.ReadKey();

        }
        //This below method will give all combinaitons  of a given string. 
    // If n is the number of things to choose from, and we choose r of them. (order doesn't matter) => formula is nCr => n!/ (n-r)! r!. For all combinations we have to chose all combinations of size 1, 2, 3, ... n. So, r runs from 1 , 2, ,3....n
        //For a string of length n, we will get n C n + N C (n-1) +.......Nc1 combinations. if N = 3,we wil get 7. If n = 4, we will get 15. 
        static List<string> GetAllSubsequences(char[] s,int i)
        {
            List<string> subseqs = new List<string>();
            subseqs.Add(s[i].ToString());

            if (s.Length-1 == i)
                return subseqs;
           
            var temp_result = GetAllSubsequences(s, i + 1);
            
            for (int j = 0; j < temp_result.Count(); j++)
            {
                subseqs.Add(s[i].ToString() + temp_result[j]);
            }
            subseqs.AddRange(temp_result);
            return subseqs;
           
        }

        static bool isPallendrom(string str)
        {
            int i = 0;
            int j = str.Length-1;

            while(i<j)
            {
                if (str[i] != str[j])
                    return false;
                i++;
                j--;
            }
            return true;
        }
       
        //#1
        //Longest Palindromic "Substring" : https://www.youtube.com/watch?annotation_id=annotation_1075415315&feature=iv&src_vid=U4yPae3GEO0&v=obBdxeCx_Qs
        //Ex., LongestPalindromicSubstring("Bananas") => anana
        static string LongestPalindromicSubstring(string str)
        {
            int len = str.Length;
            char[] s = str.ToCharArray();
            bool[,] m = new bool[len, len];

            //Rules 
            //1. Fill 1s diognally
            //2. noe if we found match , chekc lower left diognal (m[i + 1, j - 1])is also true, then manke it true and note i and current size
            //3. If not matched, just put false      
            //4. Extract paliondromic substring using starting point value and max length value      

            //Fill all diognal vales to T. Represent single letter palindroms 
            for (int i = 0; i < len; i++)
                m[i, i] = true;
            int lengthOfLongestPalindromicSubstring = 1;
            int startingPoint = 0;
            
            for(int curr_size = 2;curr_size <= len;curr_size++)
            {
                for(int i = 0;i< len-curr_size+1;i++)
                {
                    int j = i + curr_size - 1;
                    if(s[i] == s[j]) //Both charcters match
                    {
                        //trim both the charaters and check is that resulting string is pallindrom. 
                        if (m[i + 1, j - 1] == true)
                        {
                            m[i, j] = true;
                            lengthOfLongestPalindromicSubstring = curr_size;
                            startingPoint = i;
                        }
                        else
                            m[i, j] = false;
                    }
                    else
                    {
                        m[i, j] = false;
                    }
                }
            }
            
            return str.Substring(startingPoint, lengthOfLongestPalindromicSubstring);
                
        }

        //#2.Longest Palindromic Subsequence
        //https://www.youtube.com/watch?v=U4yPae3GEO0&list=PLamzFoFxwoNjtJZoNNAlYQ_Ixmm2s-CGX
        //Have similar aproach as LongestPalindromicSubstring
        //LengthOfLongestPallendromicSubSequence("LPASPAL") => 5 (which is LPAPL) 
        static int LengthOfLongestPallendromicSubSequence(string str)
        {
            int len = str.Length;
            int[,] m = new int[len, len];
            char[] s = str.ToCharArray();
            //Rules
            //1. Fill 1s on the diognal
            //2. If match found , Take value from lower left diognall (m[i + 1, j - 1]) and add 2 and store in m[i,j]
            //3. If not match, take max value of left square m[i,j-1] or bottom square m[i+1,j] of current square
            //4.Return m[0,len-1] value

            //initialize diagnols - 1 character pallaendroms
            for (int i = 0; i < len; i++)
                m[i, i] = 1;

            for (int curr_len = 2; curr_len <= len; curr_len++)
            {
                for (int i = 0; i < len - curr_len + 1; i++)
                {
                    int j = i + curr_len - 1;
                    if (s[i] == s[j]) // If charcaters matched. Tructane both the characters and get the length of longest pallendromic sub sequence of rest of the string
                    {
                        m[i, j] = m[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        m[i, j] = Math.Max(m[i, j - 1], m[i + 1, j]);
                    }
                }
            }
            return m[0, len - 1];
        }
               
        //#3.Longest Common Subsequence of given two strings : https://www.youtube.com/watch?annotation_id=annotation_1481975453&feature=iv&src_vid=U4yPae3GEO0&v=7KcR7fN4-CA
        //LongestCommonSubsequenceofTwoString("ACEBA","ADCA") => ACA
        //Dynamic programing appraoch
        static string LongestCommonSubsequenceofTwoStrings(string str1,string str2)
        {
            int s1_len = str1.Length;
            int s2_len = str2.Length;
            int pick_s1_or_s2 = 0; 
            int pick_s1 = 1;
            int pick_s2 = 2;

            int[,] match = new int[s1_len, s2_len];
            int[,] pointer = new int[s1_len, s2_len]; // this is reqired because if we take the charcaters as soon as you find match, that may not be part of longest commmon sub sequence. it may be part of another common sub sequence, may not be part of longest common sub sequence
            int i;
            int j;
            //Rules:
            //1. Here we don't fill diognally. and no 1ns on the diognal. We wll fill row by row. 
            //2. If we found match, take top left corner (m[i-1,j-1]) value and add 1
            //3. If not match, Take max of top cell (m[i-1,j]) or left cell (m[i,j-1]). If you picked from Top cell, take S2 and if you picked from left cell, pick s1
            //4. Start from lower right corner of pointer table to track the string back
            for ( i=0;i< s1_len;i++)
            {
                for( j=0;j<s2_len;j++)
                {
                    if(str1[i] == str2[j]) // Last characters matched. 
                    {
                        if (i == 0 || j == 0) //First row or first column                       
                            match[i, j] = 1;                        
                        else
                            match[i, j] = match[i - 1, j - 1] + 1; // Trimming both the characters and taking the LCS of remaining

                        pointer[i, j] = pick_s1_or_s2;
                    }
                    else
                    {
                        if(i > 0 && j> 0) //neither the 1st row , nor the first column
                        {
                            if(match[i-1,j] > match[i,j-1])
                            {
                                match[i, j] = match[i - 1, j];
                                pointer[i, j] = pick_s2; //Since we went to i-1, means we reduced the charcater in the s1. Taking S2 only
                            }
                            else
                            {
                                match[i, j] = match[i, j - 1];
                                pointer[i, j] = pick_s1; //since we went to j-1, means we rediced the charcter in top string s2. So take whole s1
                            }
                        }
                        else if(i == 0 & j > 0) //first row 
                        {
                            match[i, j] = match[i, j - 1];
                            pointer[i, j] = pick_s1;
                        }
                        else if(i > 0 && j ==0) //First column
                        {
                            match[i, j] = match[i - 1, j];
                            pointer[i, j] = pick_s2;
                        }
                    }
                }
            }

            //Now navigate pointer tabele to get the string. Rules are
            //0. Strat from the end of the pointer table. 
            //1. If it is "pick_s1_s2" go diognally and pick the charactor at the locaiton either from S1 or S2. 
            //2. If it is "pick_s2" go top 
            //3. If it is "Pick_s1" go Left 
             i = s1_len-1;
             j = s2_len-1;
            StringBuilder sb = new StringBuilder();
            while(i>=0 && j>=0)
            {
                switch(pointer[i,j])
                {
                    case 0:
                        {
                            sb.Append(str1[i]);
                            // go diagnally
                            i--;
                            j--;
                            break;
                        }
                    case 1:
                        {
                            //Go left;
                            j--;
                            break;
                        }
                    case 2:
                        {
                            //Go top
                            i--;
                            break;
                        }
                }
            }

            var chars = sb.ToString().ToCharArray();
            Array.Reverse(chars);
            return new string (chars);
        }

        //#.4 LongestCommonSubStringofTwoStrings https://www.youtube.com/watch?v=tABtJbLOQho&list=PLamzFoFxwoNjtJZoNNAlYQ_Ixmm2s-CGX&index=4
        //Normal approach of creating all substring from two strings is 2 power n. DP is better   
        //LongestCommonSubStringofTwoStrings("LCLC","CLCL") =>   {"LCL","CLC"} 
        //LongestCommonSubStringofTwoStrings("Mondays","Monumnetdays") =>   {"days"} 
        static List<string> LongestCommonSubStringofTwoStrings(string str1, string str2)
        {
            int s1_len = str1.Length;
            int s2_len = str2.Length;
            char[] s1 = str1.ToCharArray();
            char[] s2 = str2.ToCharArray();

            int[,] m = new int[s1_len, s2_len];
            //Rules
            //1. Compare ith character of s1 with each charactor of s2. 
            //2. If matched, get value from top left diognal and 1. 
            //3. Update max and result set accordingly
            int max = int.MinValue;
            List<string> result = new List<string>();

            for(int i =0;i< s1_len;i++)
            {
                for(int j =0;j < s2_len; j++)
                {
                    if (s1[i] == s2[j])
                    {
                        if (i == 0 || j == 0) //First row or firt column 
                        {
                            m[i, j] = 1;
                        }
                        else
                        {
                            m[i, j] = m[i - 1, j - 1] + 1;
                        }

                        if (m[i, j] > max)
                        {
                            max = m[i, j];
                            result = new List<string>(); //clear the old resutls since we found another maximum common substring
                            result.Add(str1.Substring(i - max + 1, max));
                        }
                        else
                            result.Add(str1.Substring(i - max + 1, max));
                    }
                    else
                        m[i, j] = 0;
                }
            }
            return result;
        }

    }
}
