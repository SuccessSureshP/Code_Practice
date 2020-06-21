using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Normal DFS Algorithm. 
 * With DFS , path between two nodes may not be Shortest. Cycle detection is possible with DFS.Below graphs are UnDirected (Double directed) 
 http://www.geeksforgeeks.org/detect-cycle-undirected-graph/ 
 * http://www.geeksforgeeks.org/detect-cycle-in-a-graph/
 
              A         F
            /   \
            D   B
            |    \
            |    C 
            \   /
              E
 
   
 DFS : A,D,E,C,B,F OR A,B,C,E,D,F
 
 
              A 
            /   \
            D   B
            |    \
            |    C 
            \   
              E
  
 DFS : A,D,E,B,C  OR A,B,C,D,E
  
 Complexity : O(V+E) 
 * 
 */
namespace Graphs_DFS
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

            public static void ConnectNodes(Node n1, Node n2)
            {
                n1.AdjacentNodes.Add(n2);
                n2.AdjacentNodes.Add(n1);
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
            
            Graph g = new  Graph();
            g.Nodes.Add(aN);
            g.Nodes.Add(bN);
            g.Nodes.Add(cN);
            g.Nodes.Add(dN);
            g.Nodes.Add(eN);
            
            g.Nodes.Add(fN);

            Graph.ConnectNodes(aN,dN);
            Graph.ConnectNodes(aN, bN);

            Graph.ConnectNodes(bN, cN);

            Graph.ConnectNodes(cN, eN);

            Graph.ConnectNodes(dN, eN);


            DFS(g);
           
            Console.WriteLine("\n\n Is Cycle Exists: " + HasCycleExists(g));

            Graph.ConnectNodes(bN, dN);

            PrintAllPaths(aN, eN);
            Console.ReadKey();

        }

        static void DFS(Graph g)
        {
            Stack<Node> stack = new Stack<Node>();
            HashSet<Node> hash = new HashSet<Node>();

            foreach (var node in g.Nodes)
            {
                if(!hash.Contains(node))
                    stack.Push(node);

                while (stack.Count != 0)
                {
                    var n = stack.Pop();
                    hash.Add(n);
                    foreach (var adjacentNode in n.AdjacentNodes)
                    {
                        if(!hash.Contains(adjacentNode))
                            stack.Push(adjacentNode);
                    }
                }
            }

            foreach (var node in hash)
            {
                Console.Write(node.Data + "   ");
            }
        }

        // Undirected means, double-directed graph. That means , there will be two edges between two vertex on each direction. So there will always a cycle between any two vertices's, but we don't consider that as a cycle. So, we need to find if any cycle involves more than 2 nodes
        //In order to not to count cycle with parent, we need to know/store the parent as well. 
        static bool HasCycleExists(Graph g)
        {
            HashSet<Node> hash = new HashSet<Node>();
            Stack<Tuple<Node,Node>> stack = new Stack<Tuple<Node, Node>>(); // Lets consider 1st value for node and 2nd value for its parent

            foreach (var graphnode in g.Nodes)
            {
                if (!hash.Contains(graphnode))
                {
                    stack.Push(new Tuple<Node, Node>(graphnode, null));
                    hash.Add(graphnode);
                }

                while (stack.Count != 0)
                {
                    var value = stack.Pop();
                    var node = (Node) value.Item1;
                    var parentNode = (Node)value.Item2;
                    foreach (var adjacentNode in node.AdjacentNodes)
                    {
                        if (adjacentNode != parentNode) 
                        {
                            if (hash.Contains(adjacentNode)) //adjacentNode is not parent of node, but already exists in he hash, means, it is a cycle
                                return true;
                            hash.Add(adjacentNode);
                            stack.Push(new Tuple<Node, Node>(adjacentNode,node));
                        }
                    }
                }
            }
            return false;
        }

        static void PrintAllPaths(Node source, Node Dest)
        {
            Hashtable visited = new Hashtable();
            List<Node> path = new List<Node>();

            PrintallPathsCore(source, Dest, visited, path);
        }

        private static void PrintallPathsCore(Node source, Node dest, Hashtable visited, List<Node> path)
        {
            visited[source] = true;
            path.Add(source);

            if (source == dest)
                PrintPath(path);
            else
            {
                foreach(var child in source.AdjacentNodes)
                {
                    if (visited[child] != null && (bool)visited[child] == true)
                        continue;
                    PrintallPathsCore(child, dest, visited, path);
                    
                }
            }
            path.RemoveAt(path.Count - 1);
            visited[source] = false;
        }

        private static void PrintPath(List<Node> path)
        {
            foreach (var n in path)
                Console.Write(n.Data);
            Console.WriteLine();
        }
    }
}
