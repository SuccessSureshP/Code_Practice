using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Eugene Session question
/*
 Given a matrix with 1s and 0s. Find number of regions of 1s , means adjacent 1s in all 4 directions (up, down, left and right)
 This seems like connected components problem, but we can solve without forming the graph
 
 Input : 
                {1,0,1,1},
                {1,1,0,0},
                {0,1,0,1},
                {0,0,0,1}
 * 
 Output: 3 regions
 */
namespace Grapahs_Regions_In_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[4, 4]
            {
                {1,0,1,1},
                {1,1,0,0},
                {0,1,0,1},
                {0,0,0,1}
            };

            int numberOfRegions = FindRegions(matrix);
            Console.WriteLine("NumberOfRegions =" +numberOfRegions);
            Console.ReadKey();
        }

        private static int FindRegions(int[,] matrix)
        {
            int result = 0;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    if (matrix[i, j] == 1)
                    {
                        result++;
                        matrix[i, j] = 0;
                        ClearAllAdjacant1s(i,j,rows,cols,matrix);
                    }
            
                }
            }
            
            return result;
        }


        static void ClearAllAdjacant1s(int row, int col, int rows, int cols, int[,] matrix)
        {
            //Searching Right
            int k = col + 1;
            while (k < cols && matrix[row, k] == 1)
            {
                matrix[row, k] = 0;
                ClearAllAdjacant1s(row,k,rows,cols,matrix);
                k++;
            }

            //Searching down

            k = row + 1;
            while (k < rows && matrix[k, col] == 1)
            {
                matrix[k, col] = 0;
                ClearAllAdjacant1s(k,col,rows,cols,matrix);
                k++;
            }

            k = col - 1;
            //Searching left
            while (k >= 0 && matrix[row, k] == 1)
            {
                matrix[row, k] = 0;
                ClearAllAdjacant1s(row,k,rows,cols,matrix);
                k--;
            }

            //Searching Up
            k = row - 1;
            while (k >= 0 && matrix[k, col] == 1)
            {
                matrix[k, col] = 0;
                ClearAllAdjacant1s(k,col,rows,cols,matrix);
                k--;
            }
        }
    }
}
