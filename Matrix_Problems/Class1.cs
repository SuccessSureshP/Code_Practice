using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Problems
{
    public class Class1
    {
        public static void Main()
        {
            int[,] matrix1 = new int[3, 3] {
                                            { 1,2,3},
                                            { 4,5,6},
                                            { 7,8,9}
                                           };


            int[,] matrix2 = new int[3, 2] {
                                            { 1,2},
                                            { 4,5},
                                            { 1,2}
                                           };

            int[,] matrix3 = new int[2, 3] {
                                            { 1,2,3},
                                            { 4,5,6}
                                           };

            //PrintDiagnally(matrix);
            PrintDiagnally_for_NXM(matrix1);
            PrintDiagnally_for_NXM(matrix2);
            PrintDiagnally_for_NXM(matrix3);

            Transpose(matrix1); //only for Square matrix
            Console.WriteLine("Before 90 Degrees");
            PrintMatrix(matrix1);
            Console.WriteLine("After 90 Degrees");
            RotateMatrix90Degrees(matrix1);
            PrintMatrix(matrix1);
            Console.WriteLine("\nsprial print of above matrix is :");
            PrintMatrixSprial(matrix1);

            Console.WriteLine("\nsprial print of above matrix2 is :");
            PrintMatrixSprial(matrix2);


            Console.WriteLine("\nsprial print of above matrix3 is :");
            PrintMatrixSprial(matrix3);


            int[,] matrix4 = new int[3, 4] {
                                            { 1,2,3,11},
                                            { 4,5,6,22},
                                            { 7,8,9,33}
                                           };

            Console.WriteLine("\nsprial print of above matrix4 is :");
            PrintMatrixSprial(matrix4);

            Console.WriteLine();

        }

        //this works only for square matrix
        static void PrintDiagnally(int[,] a)
        {
            int n = a.GetLength(0);

            int i;
            int j;

            for (int k = 0; k < n; k++)
            {
                i = k;
                j = 0;
                while (i >= 0 && j <= k)
                {                    
                    Console.Write(a[i,j] + "  ");
                    i--;
                    j++;
                }
                Console.WriteLine();
            }

            for(int k=1;k<n;k++)
            {
                i = n - 1;
                j = k;
                while(i>=k && j<n)
                {
                    Console.Write(a[i, j] + "  ");
                    i--;
                    j++;
                }
                Console.WriteLine();
            }

        }

        //this works for both square and rectagle matrix
        static void PrintDiagnally_for_NXM(int[,] a)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);


            //int n = a.GetLength(0);

            int i;
            int j;

            for (int k = 0; k < rows; k++)
            {
                i = k;
                j = 0;
                while (i >= 0 && j < cols)
                {
                    Console.Write(a[i, j] + "  ");
                    i--;
                    j++;
                }
                Console.WriteLine();
            }

            for (int k = 1; k < cols; k++)
            {
                i = rows - 1;
                j = k;
                while (i >= 0 && j < cols)
                {
                    Console.Write(a[i, j] + "  ");
                    i--;
                    j++;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Transpose(int[,] a)
        {
            int n = a.GetLength(0);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < i; j++)
                {
                    int t = a[i, j];
                    a[i, j] = a[j, i];
                    a[j, i] = t;
                }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(a[i, j] + "  ");

                Console.WriteLine();

            }


        }

        //Works only for Square matrix, due to memory reference issue for recangle matrix
        static void RotateMatrix90Degrees(int[,] a)
        {
            int n = a.GetLength(0);
            for(int x=0;x <n/2; x++)
                for (int y = x; y< n-x-1;y++)
                {
                    int t = a[x, y];
                    
                    a[x, y] = a[y, n - 1 - x];// Move Right element to Top element
                    a[y, n - 1 - x] = a[n - 1 - x, n - 1 - y]; //Bottom to right
                    a[n - 1 - x, n - 1 - y] = a[n-1-y,x]; //Left to Bottom
                    a[n - 1 - y, x] = t; //Tom to Left
                }
        }

        //this works for both square and rectagle matrix
        static void PrintMatrixSprial(int[,] a)
        {
            int rows = a.GetLength(0); //Remaining rows to process. so , rows-1 represent current layer end row index
            int cols = a.GetLength(1); //Remanining cols to process. So, cols-1 represent currnet layer ene col index 

            int k = 0; //curent layer staring row index
            int l = 0; //current layer staring col index
            int i;

            while (k < rows && l < cols)
            {
                //Printing top edge of the layer
                for (i = l; i < cols; i++)
                    Console.Write(a[k, i] + "  ");
                k++;

                //Printing Right Edge of the layer 
                for (i = k; i < rows; i++)
                    Console.Write(a[i, cols - 1] + "  ");
                cols--;

                if(k < rows) // Check of bottom layer is there to navigate. Sometimes only one top edge exists in the layer. 
                {
                    for (i = cols - 1; i >=l; i--)
                        Console.Write(a[rows - 1, i] + "  ");
                    rows--;
                }

                if(l < cols) //Check if left edge is there for the layer. Sometimes it may not contain left edge 
                {
                    for (i = rows - 1; i >= k; i--)
                        Console.Write(a[i, l] + "  ");
                    l++;
                }
            }
        }


        static void PrintMatrix(int[,] a)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(a[i, j] + "  ");
                Console.WriteLine();
            }

        }

    }
}
