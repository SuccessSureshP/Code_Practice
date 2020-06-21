using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeEvalChalanges
{
     public class ReverseWords
    {
         public static void PerformReverseWords(string fileName)
         {
             using (StreamReader reader = File.OpenText(fileName)) //(args[0]))
                 while (!reader.EndOfStream)
                 {
                     string line = reader.ReadLine();
                     if(line == string.Empty)
                         continue;
                     var words = line.Split(' ');
                     int wordCount = words.Count();
                     for (int i = wordCount-1; i>0;i--)
                     {
                         Console.Write(words[i]);
                         Console.Write(" ");
                     }
                     Console.Write(words[0]);
                     Console.WriteLine();
                 }

             Console.ReadKey();
         }
    }
}
