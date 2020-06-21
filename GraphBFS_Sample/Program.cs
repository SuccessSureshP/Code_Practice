using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//CCI page 221
//BFS runs in O(V + E) time and takes O(V) space.
/*
             5
           / |  \
          /  7   8
         /        \
        6 -------> 9
        \        /
            10
  
 BFS : 5,6,7,8,9,10
  
 */
namespace GraphBFS_Sample
{
    //We can use this or if Graph node already with out this NodeState field, then we can use HashSet/HashTable instead. 
    enum State
    {
        UnVisited,
        Visited,
        Visiting
    }

    class Node
    {
        public int Data { get; set; }
        public List<Node> NextNodes = new List<Node>();

        public Node(int d)
        {
            Data = d;
        }

        public State NodeState;
        public List<Node> GetAdjacentNodes()
        {
            return NextNodes;
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

    class Program
    {
        static void Main(string[] args)
        {
            var g = new Graph();
            g.Nodes = new List<Node>();
            var n5 = new Node(5);
            var n7 = new Node(7);
            var n8 = new Node(8);
            var n6 = new Node(6);
            var n9 = new Node(9);
            var n10 = new Node(10);
            
        
            n5.NextNodes.Add(n6);
            n5.NextNodes.Add(n7);
            n5.NextNodes.Add(n8);

            n8.NextNodes.Add(n9);

            n6.NextNodes.Add(n9);
            n6.NextNodes.Add(n10);

            n9.NextNodes.Add(n10);
         
            g.Nodes.Add(n5);
            g.Nodes.Add(n6);
            g.Nodes.Add(n7);
            g.Nodes.Add(n8);
            g.Nodes.Add(n9);
            g.Nodes.Add(n10);

            //var result = IsPathExists(g, n8, n10);
           var result = IsPathExists_With_HasSet(g, n8, n10);
            
            
            Console.WriteLine("Path exists from n8 to n10? : "+result.ToString());

            //result = IsPathExists(g, n7, n6);
            result = IsPathExists_With_HasSet(g, n7, n6);
            
            Console.WriteLine("Path exists from n7 to n6? : " + result.ToString());

            Console.WriteLine("\n\n BFS Traversal of all Nodes:");
            Console.WriteLine();
            BFSTraversalOfAllNodes(g);
            Console.ReadKey();
        }

        /// <summary>
        /// This method find the path, but this does not give the shortest path to reach from start node to end node. Check other program "Graphs_ShortestPath_With_BFS" 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
       static bool IsPathExists(Graph g, Node start, Node end)
        {
            Queue<Node> q = new Queue<Node>();

            foreach (var node in g.GetNodes())
            {
                node.NodeState = State.UnVisited;
            }

            start.NodeState = State.Visiting;
            q.Enqueue(start);
            Node u;
            while (q.Count() != 0)
            {
                u = q.Dequeue();
                if (u != null)
                {
                    foreach (Node v in u.GetAdjacentNodes())
                    {
                        if (v.NodeState == State.UnVisited)
                        {
                            if (v == end)
                                return true;
                            
                            v.NodeState = State.Visiting;
                            q.Enqueue(v);
                        }
                    }
                    u.NodeState = State.Visited;
                }

            }
            return false;

        }

        static bool IsPathExists_With_HasSet(Graph g, Node start, Node end)
        {
           HashSet<Node> hash = new HashSet<Node>();
            hash.Add(start);

            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                var n = queue.Dequeue();
                foreach (var adjacentNode in n.GetAdjacentNodes())
                {
                    if (!hash.Contains(adjacentNode))
                    {
                        if (adjacentNode == end)
                            return true;
                        hash.Add(adjacentNode);
                        queue.Enqueue(adjacentNode);
                    }
                }
            }


            return false;
        }


        static void BFSTraversalOfAllNodes(Graph g)
        {
            HashSet<Node> hash = new HashSet<Node>();
            Queue<Node> queue = new Queue<Node>();

            foreach (var node in g.GetNodes())
            {
                if (!hash.Contains(node))
                {
                    hash.Add(node);
                    queue.Enqueue(node);
                    while (queue.Count != 0)
                    {
                        var n = queue.Dequeue();
                        Console.Write(n.Data + "  ");
                        foreach (var adjacentNode in n.GetAdjacentNodes())
                        {
                            if (!hash.Contains(adjacentNode))
                            {
                                hash.Add(adjacentNode);
                                queue.Enqueue(adjacentNode);
                            }
                        }

                    }
                }
            }
        }

    }
}

