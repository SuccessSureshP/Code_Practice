using System;
using System.IO;
using System.Text;


namespace CodeEvalChalanges
{
 /// <summary>
    /// https://www.codeeval.com/open_challenges/167/submit/
 /// You are given a text. Write a program which outputs its lines according to the following rules: 
//1.If line length is ≤ 55 characters, print it without any changes.
//2.If the line length is > 55 characters, change it as follows:
//1.Trim the line to 40 characters.
//2.If there are spaces ‘ ’ in the resulting string, trim it once again to the last space (the space should be trimmed too).
//3.Add a string ‘... <Read More>’ to the end of the resulting string and print it.
    //Inputs
    //Tom's mouth watered for the apple, but he stuck to his work.
    //Two thousand verses is a great many - very, very great many.
    //Output
    //Tom's mouth watered for the apple, but... <Read More>
    //Two thousand verses is a great many -... <Read More>



    /// </summary>
    class TextTrimming
    {
        public static int PerformTextTrimming(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName)) //(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    if (line.Length <= 55)
                    {
                        Console.WriteLine(line);
                        continue;
                    }

                    //trim the line to 40 characters 
                    line = line.Substring(0, 40);
                    int spaceIndex = line.LastIndexOf(' ');
                    StringBuilder sb = new StringBuilder();
                    if (spaceIndex != -1)
                    {
                        sb.Append(line.Substring(0, spaceIndex));
                    }
                    else
                    {
                        sb.Append(line);
                    }
                    sb.Append("... <Read More>");
                    Console.WriteLine(sb.ToString());
                }

            Console.ReadKey();
            return 0;
        }
    }
}
