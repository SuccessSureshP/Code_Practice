using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//Eugune session problem. Check book for diagrams
// Matrix (eg., 4X4) of bools where True represent / and False represent \. Then find unique regions (areas) separated in that matrix. 
/*

 lest take 4 X 4 
 
 \  \  /  /
 \  /  /  \
 /  \  \  /
 \  /  \  \
 
 \ => false
 / => true
 Answer is => 10 unique regions
 
 */
using System.Xml;

namespace Graphs_UniqueRegions_InBooleanMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[,] matrix = new bool[4, 4]
            {
                {false, false, true, true},
                {false, true, true, false},
                {true, false, false, true},
                {false, true, false, false}
            };

            int uniqueRegions = FindUniqueRegions(matrix);

            Console.WriteLine("Unique Regions : "+uniqueRegions);
            Console.ReadKey();
        }

        private static int FindUniqueRegions(bool[,] matrix)
        {
            int uniqueRegions = 0;
            int matrixRows = matrix.GetLength(0);
            int matrixColumns = matrix.GetLength(1);

            //We have to image each triangle as a node in the graph and we have to connect then based on true / or false \ edges.
            //Lets build the graph with 3-dimensional array 
            Node[, ,] graph = new Node[matrixRows, matrixColumns, 2];
            //Lets create all the nodes first.
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixColumns; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        graph[i,j,k] = new Node();
                    }
                }
            }

            //If two triangles of 1str cell if A1 and A2 and second cell is B1 and B2, then A2 and 1 will always connected. Lets do similar connections across the graph
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixColumns-1; j++)
                {
                    var firstTriangle = graph[i, j, 1]; //Right side triangle in left side cell
                    var secondTriangle = graph[i, j + 1, 0]; // Left side triangle of the right side cell
                    //Connect above nodes both ways 
                    firstTriangle.AdjacentNodes.Add(secondTriangle); 
                    secondTriangle.AdjacentNodes.Add(firstTriangle);
                }
            }
            //Now connect nodes/triangles with their below triangles based on current cell value.
            for (int i = 0; i < matrixRows-1; i++)
            {
                for (int j = 0; j < matrixColumns; j++)
                {
                    Node topTriangle = null;
                    Node bottomTriangle = null;
                    if(matrix[i, j]) //Top cell
                        topTriangle = graph[i, j, 1];  //Take A2 if cell value is true
                    else
                        topTriangle = graph[i, j, 0]; //Take A1 if cell value is false

                    if(matrix[i + 1, j]) //Bottom cell
                        bottomTriangle = graph[i + 1, j, 0]; //Take B1 if cell value if true
                    else
                        bottomTriangle = graph[i + 1, j, 1]; //Take B2 if cell value if false

                    //Now connect both of those nodes

                    topTriangle.AdjacentNodes.Add(bottomTriangle);
                    bottomTriangle.AdjacentNodes.Add(topTriangle);
                }
            }

            //Now we have to find number of unique regions (connected components)
            HashSet<Node> h = new HashSet<Node>();
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixColumns; j++)
                {
                    for (int k = 0; k <= 1; k++)
                    {
                        if (!h.Contains(graph[i, j, k]))
                        {
                            uniqueRegions++;
                            h.Add(graph[i,j,k]);
                            ProcessPath(graph[i, j, k],h);
                        }
                    }
                }
            }
            return uniqueRegions;
        }

        //BFS
        private static void ProcessPath(Node node, HashSet<Node> h)
        {
           Queue<Node> q = new Queue<Node>();
            q.Enqueue(node);

            while (q.Count() !=0)
            {
                var n = q.Dequeue();
                foreach (var adjacentNode in n.AdjacentNodes)
                {
                    if (!h.Contains(adjacentNode))
                    {
                        h.Add(adjacentNode);
                        q.Enqueue(adjacentNode);
                    }
                }
            }
        }
    }

    public class  Node
    {
        public List<Node> AdjacentNodes = new List<Node>();
    }
   
}
