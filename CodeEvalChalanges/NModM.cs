using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeEvalChalanges
{
    //https://www.codeeval.com/open_challenges/62
    //Determine the modulus (without the modulus operator).
    public class NModM
    {
        public static void GetNModM(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName)) //(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    var parts = line.Split(';');

                    var numbersList = parts[0].Split(',');

                    int n =  int.Parse(numbersList[0]);
                    int m = int.Parse(numbersList[1]);

                    int d = n/m;

                    int result = d == 0 ? n : n - (d*m);

                    Console.WriteLine(result.ToString());
                }

            Console.ReadKey();
        }
    }
}
