using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//CTG : Chapter 4 , page 14. DFS implementation under the cover of recursive function
namespace StringPathInMatrix
{
    class Program
    {

        static bool HasPath(char[,] matrix, string inputString)
        {
            //Boundary cases
            if(matrix == null || matrix.Length == 0 || string.IsNullOrEmpty(inputString))
                return false;

            int rows = matrix.GetLength(0); //IMP length of first dimension 
            int cols = matrix.GetLength(1); //IMP length of 2nd dimension

            //Take another matrix of same size with boolean flags 
            var visited = new bool[rows,cols];
            
            for(int i=0;i<rows;i++)
                for (int j = 0; j < cols; j++)
                {
                    visited[i,j] = false;
                }

            for(int i=0;i<rows;i++)
                for (int j = 0; j < cols; j++)
                {
                    if (HasPathCore(matrix, i, j, rows, cols, inputString, visited, 0))
                        return true;
                }


            return false;
        }

        private static bool HasPathCore(char[,] matrix, int row, int col, int rows, int cols, string inputString, bool[,] visited, int pathLength)
        {
            if (pathLength == inputString.Length) //Means we navigated all the characters of the input string. Means, we found the full path
                return true;

            bool hasPath = false;
            if (row >= 0 && row < rows && col >= 0 && col < cols
                && matrix[row, col] == inputString[pathLength]
                && !visited[row, col])
            {

                pathLength++;
                visited[row, col] = true;
                //Checking all next character by going through all 4 directions from present character
                hasPath = HasPathCore(matrix, row - 1, col, rows, cols, inputString, visited, pathLength)
                          || HasPathCore(matrix, row, col - 1, rows, cols, inputString, visited, pathLength)
                          || HasPathCore(matrix, row + 1, col, rows, cols, inputString, visited, pathLength)
                          || HasPathCore(matrix, row, col + 1, rows, cols, inputString, visited, pathLength);

                if (!hasPath) //If we don't find next character in any direction, we don't take even this charcator
                {
                    --pathLength;
                    visited[row, col] = false;
                }
            }

            return hasPath;
        }

        static void Main(string[] args)
        {
            char[,] matrix = new char[3, 4]
            {
                {'a', 'b', 'c', 'e'},
                {'s', 'f', 'c', 's'},
                {'a', 'd', 'e', 'e'}
            };


            //Functional cases
            string inputString = "abcb";
            bool hasPath = HasPath(matrix, inputString);
            Console.WriteLine("Path for {0} found?{1}",inputString,hasPath);

            inputString = "bcced";
            hasPath = HasPath(matrix, inputString);
            Console.WriteLine("Path for {0} found?{1}", inputString, hasPath);

            //Boundary cases
            inputString = "a";
            hasPath = HasPath(matrix, inputString);
            Console.WriteLine("Path for {0} found?{1}", inputString, hasPath);

            inputString = "abcescfsadee"; //All chars from matricx
            hasPath = HasPath(matrix, inputString);
            Console.WriteLine("Path for {0} found?{1}", inputString, hasPath);
            
            Console.ReadKey();
        }
    }
}
