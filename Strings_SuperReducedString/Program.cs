using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://www.hackerrank.com/challenges/reduced-string
//aabbbcc => b (two aa can be deleted etc)
namespace Strings_SuperReducedString
{
    class Program
    {
        static void Main(String[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
            string givenString = Console.ReadLine();
            String res = ReduceString(givenString);
            Console.WriteLine(res);
            Console.ReadKey();
        }
        static string ReduceString(string str)
        {
            int len = str.Length;

            if (len == 0)
                return "Empty String";


            if (len == 1)
                return str;

            int current = 0;
            int next = 1;
            Stack<char> st = new Stack<char>();

            while (next < len)
            {
                if (str[current] == str[next])
                {
                    current = next + 1;
                    next = current + 1;
                }
                else
                {
                    if (st.Count > 0)
                    {
                        if (st.Peek() == str[current])
                            st.Pop();
                        else
                            st.Push(str[current]);
                    }
                    else
                        st.Push(str[current]);

                    current = next;
                    next = next + 1;
                }
            }
            if (current == len - 1)
            {
                if (st.Count > 0)
                {
                    if (st.Peek() == str[current])
                        st.Pop();
                    else
                        st.Push(str[current]);
                }

            }

            if (st.Count == 0)
                return "Empty String";
            else
            {
                char[] result = new char[st.Count];
                int i = st.Count-1;
                while (st.Count > 0)
                {
                    result[i--] = st.Pop();
                }

                return new string(result);
            }
        }
    }
}
