using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeEvalChalanges
{
    //https://www.codeeval.com/open_challenges/183/

    public class MatrixDetailsCollision
    {
        public static void FineMinMoves(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName)) //(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    var rows = line.Split(',');
                    int minmoves = int.MaxValue;
                    foreach (var row in rows)
                    {
                        int moves_in_this_row;
                        var indexOfFirstDot = row.IndexOf('.');
                        var indexOfLastdot = row.LastIndexOf('.');
                        
                        if (indexOfFirstDot == -1 && indexOfLastdot == -1)
                            moves_in_this_row = 0;
                        else
                            moves_in_this_row = indexOfLastdot - indexOfFirstDot + 1;
                       
                        if (minmoves > moves_in_this_row)
                            minmoves = moves_in_this_row;
                    }
                    Console.WriteLine(minmoves);
                }

            Console.ReadKey();
        }
    }
}
