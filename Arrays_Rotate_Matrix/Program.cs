using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Rotate the matrix 90 degrees in clock wise direction
namespace Arrays_Rotate_Matrix
{
    class Program
    {
        public static void Main(string[] args)
        {
            int n = 5;
            int[,] m = new int[5,5]
            {
                {01, 02, 03, 04, 05},
                {11, 12, 13, 14, 15},
                {21, 22, 23, 24, 25},
                {31, 32, 33, 34, 35},
                {41, 42, 43, 44, 45}
            };
            //Clock wise direction. Left to Top, Top to right, Right to Bottom and bottom to left
            for (int layer = 0; layer < n/2; layer++)
            {
                int first = layer;
                int last = n - layer - 1;

                for (int i = first; i < last; i++)
                {
                    //For top elements and right elements, indexes has to move in forward direction. But for Bottom and Left, indexes has start from last element and has to move to first element. So we use offset for those elements. 
                    //Also, in each layer : 
                    // => for top row ,  1st/Row index is constant = first 
                    // => for left row,  2nd/column index is constant => first
                    // => for bottom row, 1st/row index is constant => last
                    // => for right now , 2nd/column index is constant = > last  

                    int offset = i - first;

                    //Top element 
                    int top = m[first, i];

                    //top <= Left. Left to Top
                    m[first, i] = m[last - offset,first];

                    //Left  <= Bottom. Bottom to left 
                    m[last - offset, first] = m[last, last - offset];

                    // Bottom<= right . Right to Bottom     
                    m[last, last - offset] = m[i, last];

                    //Top to Right
                    m[i, last] = top;
                }
            }

            //Printing
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(m[i,j] +" ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
