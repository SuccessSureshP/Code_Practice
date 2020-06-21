using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
It represent dependency among the nodes. A-> B means B depends on A. A has to be finished(traversed) before finishing (traversing) B. Topological sort exists only if Graph is ADG (Acyclic Directed Graph)
 * http://www.geeksforgeeks.org/topological-sorting/
 Input : 
        A ------|           F
      /  \      |            \
     D<--B      |             G
     |    \     |
     |    C     |
     \   /      |
       E  <-----|
 
 Assume arrows from top element to bottom element. means, A-->D and A-->B , D-->E, B->D, B-->C, C->E. 

 Output  : A, B,C,D,E  OR 
           A, B,D,C,E 
 * 
 * It is neither DFS nor BFS. 
 */
namespace Graphs_TopologicalSort_With_DFS
{
    class Program
    {

        public class Node
        {
            public char Data { set; get; }
            public List<Node> AdjacentNodes { get; set; }

            public Node(char data)
            {
                Data = data;
                AdjacentNodes = new List<Node>();
            }
        }

        public class Graph
        {
            public List<Node> Nodes { set; get; }

            public static void ConnectNodes(Node start, Node end)
            {
                start.AdjacentNodes.Add(end);
            }

            public Graph()
            {
                Nodes = new List<Node>();
            }
        }
        static void Main(string[] args)
        {
            Node aN = new Node('A');
            Node bN = new Node('B');
            Node cN = new Node('C');
            Node dN = new Node('D');
            Node eN = new Node('E');

            Node fN = new Node('F');
            Node gN = new Node('G');

            Graph g = new Graph();
            g.Nodes.Add(aN);
            g.Nodes.Add(bN);
            g.Nodes.Add(cN);
            g.Nodes.Add(dN);
            g.Nodes.Add(eN);

            g.Nodes.Add(fN);
            g.Nodes.Add(gN);


            Graph.ConnectNodes(aN, dN);
            Graph.ConnectNodes(aN, bN);
            Graph.ConnectNodes(aN, eN);

            Graph.ConnectNodes(bN, dN);
            Graph.ConnectNodes(bN, cN);

            Graph.ConnectNodes(cN, eN);

            //Graph.ConnectNodes(dN, bN); //Cyclic arrow  not shown in the above graph. Enable to see the output 

            Graph.ConnectNodes(dN, eN);

            //Graph.ConnectNodes(dN, cN); //Extra arrow from D to C to see different outputs 


            //Another component in the tree

            Graph.ConnectNodes(fN,gN);

            //Graph.ConnectNodes(gN, fN);  //Cyclic arrow  not shown in the above graph.Enable to see the output 

            #region Calling functions in Scenario #1 and #2. Comment this region to check only Scenario #3
            var graphHasCycle = DetectCyclesExists(g);
            if (graphHasCycle)
            {
                Console.WriteLine("Graph has Cycle. Topological sort not possible");
            }
            else
            {
                Console.WriteLine("Graph does not have Cycle");
                Console.WriteLine("Topological Order: \n");
                PrintToplogicalOrder(g);
            }
            #endregion

            #region Calling function in Scenario #3
            Console.WriteLine("\nTopological Order if no cycles found on creating Topological Order\n");
            PrintToplogicalOrderBydetectingCycle(g);
            #endregion
            Console.ReadKey();
        }

       #region #1 Detecting Cycle
        static bool DetectCyclesExists(Graph g)
        {
            Hashtable hash = new Hashtable();
            foreach (var node in g.Nodes)
            {
                hash.Add(node, "UnProcessed");
            }

            foreach (var node in g.Nodes) // This loop will cover graph with disconnected components as well
            {
                if (hash[node].Equals("UnProcessed"))
                {
                    hash[node] = "Processing";
                    var cycleFound = DFS_CycleCheck(node, hash);
                    if (cycleFound)
                        return true;
                }
            }
            return false;
        }

        static bool DFS_CycleCheck(Node node, Hashtable hash)
        {
            bool result = false;

            foreach (var adjacentNode in node.AdjacentNodes)
            {
                if (hash[adjacentNode].Equals("Processing"))
                    return true;
                if (hash[adjacentNode].Equals("UnProcessed"))
                {
                    hash[adjacentNode] = "Processing";
                    result = result || DFS_CycleCheck(adjacentNode, hash);
                }
            }
            hash[node] = "Processed";

            return result;
        }

        #endregion

       #region #2 Topological order/sort without detecting cycle
        static void PrintToplogicalOrder(Graph g)
        {
            Stack<Node> stack = new Stack<Node>();
            HashSet<Node> hash = new HashSet<Node>();
            foreach (var graphNode in g.Nodes)
            {
                if (!hash.Contains(graphNode))
                {
                    hash.Add(graphNode);
                    ToplogicalOrder(graphNode, stack, hash);
                }
            }

            while (stack.Count !=0)
            {
                Console.Write(stack.Pop().Data + "   ");
            }
        }

       static  void ToplogicalOrder(Node startNode, Stack<Node> stack,HashSet<Node> hash)
        {
            foreach (var adjacentNode in startNode.AdjacentNodes)
            {
                if (!hash.Contains(adjacentNode))
                {
                    hash.Add(adjacentNode);
                    ToplogicalOrder(adjacentNode, stack,hash);
                }
            }
            stack.Push(startNode);
        }

        #endregion
        
       #region #3 Topological Sort along with finding the cycle at the same time
        static void PrintToplogicalOrderBydetectingCycle(Graph g)
        {
            Stack<Node> stack = new Stack<Node>();
            Hashtable hash = new Hashtable();
            foreach (var graphNode in g.Nodes)
            {
                if (!hash.Contains(graphNode))
                {
                    hash.Add(graphNode,"Processing");
                   var isCycleFound = ToplogicalOrderIfNoCycle(graphNode, stack, hash);
                    if (isCycleFound)
                    {
                        Console.WriteLine("Graph has Cycle. Topological sort not possible");
                        return;
                    }
                }
            }

            while (stack.Count != 0)
            {
                Console.Write(stack.Pop().Data + "   ");
            }
        }

        static bool ToplogicalOrderIfNoCycle(Node startNode, Stack<Node> stack, Hashtable hash)
        {
            bool result = false;
            foreach (var adjacentNode in startNode.AdjacentNodes)
            {
                if (hash.Contains(adjacentNode))
                {
                    if (hash[adjacentNode].Equals("Processing"))
                        return true;
                }
                else
                {
                    hash.Add(adjacentNode, "Processing");
                    result = ToplogicalOrderIfNoCycle(adjacentNode, stack, hash);
                }
            }
            hash[startNode] = "Processed";
            stack.Push(startNode);
            return result;
        }
        #endregion
    }
}
