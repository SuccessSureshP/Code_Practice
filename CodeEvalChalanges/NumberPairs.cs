using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeEvalChalanges
{
    public static class NumberPairs
    {
        public static void GetNumberPairs(string fileName)
        {
              using (StreamReader reader = File.OpenText(fileName)) //(args[0]))
                  while (!reader.EndOfStream)
                  {
                      string line = reader.ReadLine();

                      var parts = line.Split(';');

                      var numbersList =  parts[0].Split(',');

                      int[] numbers = new int[numbersList.Count()];
                      int i = 0;
                      foreach (var n in numbersList)
                      {
                          numbers[i++] = int.Parse(n);
                      }

                      int finalSum = int.Parse(parts[1]);


                      var upperBound = numbers.Length - 1;
                      int index1 = 0;
                      bool pairsFound = false;

                      while (index1 <= upperBound)
                      {
                          if ((numbers[index1] + numbers[upperBound]) < finalSum) //If we don't get final sum by adding biggest upper bound number, we don't get that final sum by adding lower numbers. So, we should go next
                          {
                              index1++;
                              continue;
                          }
                          int index2 = index1 + 1;
                          while (index2 <= upperBound)
                          {
                              var currentsum = numbers[index1] + numbers[index2];
                              if (currentsum == finalSum)
                              {
                                  if (pairsFound)
                                      Console.Write(';');
                                  Console.Write(numbers[index1].ToString() + ',' + numbers[index2].ToString());
                                  upperBound = index2-1;
                                  pairsFound = true;
                                  break;
                              }

                              if(currentsum > finalSum)
                                  break;

                              index2++;
                          }
                          index1++;
                      }

                      if (!pairsFound)
                          Console.WriteLine("NULL");
                      else
                          Console.Write("\n");

                  }

            Console.ReadKey();
        }

    }
}
