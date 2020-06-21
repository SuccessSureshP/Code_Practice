using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings_SubStringIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            //string inputText = "babbaaabaaaabbababaaabaabbbbabaaaabbabbabaaaababbabbbaaabbbaaabbbaabaabaaaaababbaaaaaabababbbbba";
            //string searchText = "abaaaabbabbabaaaababbab";
            string inputText = "ABC ABCDAB ABCDABCDABDE";
            string searchText = "ABCDABD";

            int index =  GetSubStringIndex(inputText, searchText);
            int index1 = KMP_Search(inputText, searchText);

            Console.WriteLine($"Index is :{index}");
            Console.WriteLine($"Index with KMP sarch :{index1}");
            Console.ReadKey();
        }
        public static int GetSubStringIndex(string haystack, string needle)
        {

            if (needle == "" || haystack == "")
                return -1;

            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                int k = 1;
                int stIndex = -1;
                if (haystack[i] == needle[0])
                {
                    stIndex = i;
                    for (int j = i + 1; j < haystack.Length; j++)
                    {
                        if (k < needle.Length)
                        {
                            if (haystack[j] == needle[k])
                            {
                                k++;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                }

                if (k == needle.Length) //We found all characters of needle in haystack. So we found 
                    return stIndex;
            }

            return -1;

        }

        //Using KMP Algorithm (Knuth–Morris–Pratt algorithm) https://en.wikipedia.org/wiki/Knuth–Morris–Pratt_algorithm#.22Partial_match.22_table_.28also_known_as_.22failure_function.22.29
        static int [] GetPartialMatchTable(string w)
        {
            int[] t = new int[w.Length];
            int pos = 2;
            int cnd = 0;

            t[0] = -1;
            t[1] = 0;
            while(pos < w.Length)
            {
                if(w[pos-1] == w[cnd]) //We found a sufix character of W till pos-1 which is prefix of w from cnd.
                {
                    t[pos] = cnd + 1;
                    cnd = cnd + 1;
                    pos = pos + 1;
                }else if(cnd > 0)
                {
                    cnd = t[cnd];
                }else
                {
                    t[pos] = 0;
                    pos = pos + 1;
                }
            }
            return t;
        }

        static int KMP_Search(string s, string w) //s =the text to be searched and w = word sought
        {
            int m = 0;
            int i = 0;
            var t = GetPartialMatchTable(w);
            while(m+i < s.Length)
            {
                if(w[i] == s[m+i])
                {
                    if (i == w.Length - 1) //We matched all characters of w. So found the string. Send starting point
                        return m;
                    else
                    {
                        i = i + 1;
                    }
                }
                else if(t[i] > -1)
                {
                    m = m + i - t[i];
                    i = t[i];
                }else
                {
                    m = m + 1;
                    i = 0;
                }
            }

            return -1;

        }

    }
}
