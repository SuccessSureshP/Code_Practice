using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//https://www.codeeval.com/open_challenges/96/

namespace CodeEvalChalanges
{
    public class SwapCase
    {
        public static void PerformSwapCase(string fileName)
        {
            using(var streamReader =  File.OpenText(fileName))
            while(!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
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
                     if (indexOfChar >= indexOfA && indexOfChar <= indexOfZ) //If it is capital letter
                       {
                           var charDifferenceFromA = indexOfChar - indexOfA;
                           sb.Append((char) (indexOfa + charDifferenceFromA)); //Add small letter to SB
                       }
                     else if (indexOfChar >= indexOfa && indexOfChar <= indexOfz) //If it is small letter
                     {
                         var charDifferenceFroma = indexOfChar - indexOfa;
                         sb.Append((char) (indexOfA + charDifferenceFroma)); //Add captal letter to SB
                     }
                     else
                         sb.Append(character);

                }
                Console.WriteLine(sb.ToString());
            }

            Console.ReadKey();
        }
    }
}
