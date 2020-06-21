using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//https://www.codeeval.com/open_challenges/20/
namespace CodeEvalChalanges
{
    public class Lowercase
    {
        public static void ConvertToLowercase(string fileName)
        {
           using (StreamReader reader = File.OpenText(fileName)) //(args[0]))
               while (!reader.EndOfStream)
               {
                   string line = reader.ReadLine();
                   if (null == line)
                       continue;

                   int indexOfA =  'A';
                   int indexOfa =  'a';

                   int indexOfZ = 'Z';
                   int indexOfz = 'z';

                   StringBuilder sb = new StringBuilder();
                   foreach (var character in line)
                   {
                       int indexOfChar =character;
                       if (indexOfChar >= indexOfA && indexOfChar <= indexOfZ)
                       {
                           var charDifferenceFromA = indexOfChar - indexOfA;
                           sb.Append((char) (indexOfa + charDifferenceFromA));
                       }
                       else
                       {
                           sb.Append(character);
                       }
                   }


                   Console.WriteLine(sb.ToString());


               }
            Console.ReadLine(); //Remove this and put return 0; in program.cs before submitting
        }
    }
}
