using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Eugene HW question. Find Shortest path between 2 nodes using BFS. This is for unweighted Graph. for Weighted Graph, we have to use Dijkstra’s Algorithem
//http://www.eecs.yorku.ca/course_archive/2006-07/W/2011/Notes/BFS_part2.pdf
/*
            E
           /  \
          D -- F
         /    / \ 
        C -- B  /  
       /       / 
      A -----G    
  
 Find Shortest path between A and F. 
 Answer is : A-G-F. Another path A-C-B-F is longer.  
 */
namespace Graphs_ShortestPath_With_BFS
{
    class Program
    {
        class Node
        {
            private char data;
            public List<Node> NextNodes = new List<Node>();

            public Node(char d)
            {
                data = d;
            }

            public List<Node> GetAdjacentNodes()
            {
                return NextNodes;
            }

            public char Data
            {
                get { return data; }
            }
        }

        class Graph
        {
            public List<Node> Nodes;

            public List<Node> GetNodes()
            {
                return Nodes;
            }
        }
        static void Main(string[] args)
        {
            var g = new Graph();
            g.Nodes = new List<Node>();
            var nA = new Node('A');
            var nB = new Node('B');
            var nC = new Node('C');
            var nD = new Node('D');
            var nE = new Node('E');
            var nF = new Node('F');
            var nG = new Node('G');
            
            ConnectNodes(nA,nC);
            ConnectNodes(nA, nG);
             
            ConnectNodes(nC,nB);
            ConnectNodes(nC,nD);
            
            ConnectNodes(nB,nF);

            ConnectNodes(nG,nF);

            ConnectNodes(nD,nF);

            ConnectNodes(nD,nE);

            ConnectNodes(nE,nF);

            g.Nodes.Add(nA);
            g.Nodes.Add(nB);
            g.Nodes.Add(nC);
            g.Nodes.Add(nD);
            g.Nodes.Add(nE);
            g.Nodes.Add(nF);

            DisplayShortestPath(g, nA, nF);

            DisplayShortestPath(g, nG, nD);
        }
        //This will add double directional connection between 2 nodes. un-directed graph is equal to double directed graph.
        static void ConnectNodes(Node firstNode, Node secondNode)
        {
            firstNode.NextNodes.Add(secondNode);
            secondNode.NextNodes.Add(firstNode);
        }


        private static void DisplayShortestPath(Graph g, Node startNode, Node endNode)
        {
            //lets have 2 hash tables. one for keeping depth of each path as we going from A to each nodes
            //Second to keep track the previous node for each node 

            Hashtable hashDepth = new Hashtable();
            Hashtable hashPath = new Hashtable();

            //Now lets navigate starting from node A with BFS
            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(startNode);

            hashDepth.Add(startNode,0);
            hashPath.Add(startNode,null);

            while (queue.Count!= 0)
            {
                var node = queue.Dequeue();
                if(node == endNode)
                    break;
                
                foreach (var nextNode in node.NextNodes)
                {
                    if (!hashPath.ContainsKey(nextNode))
                    {
                        hashPath.Add(nextNode,node);
                        queue.Enqueue(nextNode); 
                    }

                    if (!hashDepth.ContainsKey(nextNode))
                    {
                        hashDepth.Add(nextNode, (int)hashDepth[node]+1); //Add 1 to the depth of parent
                    }
                }
            }

            //now result 

            if (!hashPath.ContainsKey(endNode))
            {
              Console.WriteLine(endNode.Data + " Node is not connected to the given start node " + startNode.Data);
                return;
            }

            PrintPath(endNode,hashPath);

            Console.WriteLine();
            Console.WriteLine("Depth of this path is :" + hashDepth[endNode]);
            Console.ReadKey();
        }

        static void PrintPath(Node n,Hashtable hashPath)
        {
            if(n == null)
                return;
            
            PrintPath((Node)hashPath[n],hashPath);
            Console.Write(n.Data +"  ");
        }


    }
}
