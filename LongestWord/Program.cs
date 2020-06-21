using System;
using System.IO;

namespace LongestWord
{
 //Sample code to read in test cases:
/// <summary>
/// In this challenge you need to find the longest word in a sentence. If the sentence has more than one word of the same length you should pick the first one. 
/// </summary>
    class Program
    {
      public static int Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (null == line)
                    continue;

                int lengthOflongestWord = 0;
                string longestString = line; //Take entire line as longest string initially. This is because if user given single string in a line, that will be the longest one
                var strings =  line.Split(' ');
                
                foreach (var s in strings)
                {
                    if (s.Length > lengthOflongestWord)
                    {
                        lengthOflongestWord = s.Length;
                        longestString = s;
                    }
                }
                Console.WriteLine(longestString);
            }
            Console.ReadKey();
            return 0;
        }
    }
}
