using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//https://www.codeeval.com/open_challenges/238/
//You need to calculate and print a number that will tell how many times you can make the word ‘code’ from the letters in the matrix, using a 2x2 submatrix. 
namespace CodeEvalChalanges
{
    public class Code_Combinations
    {
        public static void GetCode_Combinations(string fileName)
        {
              using (StreamReader reader = File.OpenText(fileName)) //(args[0]))
                  while (!reader.EndOfStream)
                  {
                      string matrix_in_line = reader.ReadLine();

                      var rows = matrix_in_line.Split('|');
                      int codeCombinationCounts = 0;

                      char[] formedLetters = new char[4];
                      int k = 0;

                      for (int i = 0; i <= rows.Length - 2; i++)
                          //Run all strings except last one (rows) , that will be picked by previous row
                          for (int j = 0; j <= rows[0].Length - 2; j++)
                              //Run with each character of the string except last one, that will be picked by calculation
                          {
                              k = 0;
                              formedLetters[k++] = rows[i][j]; //top_Left
                              formedLetters[k++] = rows[i][j + 1]; //top_Right 

                              formedLetters[k++] = rows[i + 1][j]; //bottom_Left
                              formedLetters[k] = rows[i + 1][j + 1];//bottom_right

                              //string formed_String = string.Concat(top_Left, top_Right, bottom_Left, bottm_Right);
                              Array.Sort(formedLetters);

                              var sortedFormedString = new string(formedLetters);

                              if (sortedFormedString.Equals("cdeo")) //sorted order of 'code'
                                  codeCombinationCounts++;
                          }
                      Console.WriteLine(codeCombinationCounts);
                  }

            
            Console.ReadKey();
        }

    }
}
