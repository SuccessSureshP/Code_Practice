using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//http://www.geeksforgeeks.org/greedy-algorithms-set-6-dijkstras-shortest-path-algorithm/
//Below are the detailed steps used in Dijkstra’s algorithm to find the shortest path from a single source vertex to all other vertices in the given weighted graph.
namespace Graphs_Dijkstras_shortest_path
{
    class Program
    {
        static void Main(string[] args)
        {
            Node n0 = new Node("n0");
            Node n1 = new Node("n1");
            Node n2 = new Node("n2");
            Node n3 = new Node("n3");
            Node n4 = new Node("n4");
            Node n5 = new Node("n5");
            Node n6 = new Node("n6");
            Node n7 = new Node("n7");
            Node n8 = new Node("n8");

            n0.AddArc(n1, 4);
            n0.AddArc(n7, 8);
            n1.AddArc(n2, 8);
            n1.AddArc(n7, 11);
            n2.AddArc(n3, 7);
            n2.AddArc(n5, 4);
            n2.AddArc(n8, 2);
            n3.AddArc(n4, 9);
            n3.AddArc(n5, 14);
            n4.AddArc(n5, 10);
            n5.AddArc(n6, 2);            
            n6.AddArc(n7, 1);
            n6.AddArc(n8, 6);
            n7.AddArc(n8, 7);
            List<Node> graph = new List<Node>();
            graph.Add(n0);
            graph.Add(n1);
            graph.Add(n2);
            graph.Add(n3);
            graph.Add(n4);
            graph.Add(n5);
            graph.Add(n6);
            graph.Add(n7);
            graph.Add(n8);       

            //Find shortest distance of each node from starting node N0 - Using Dijkstras alogrithem
            FindShortDistanceOfEachNodeFromSource(graph, n0);
        }

        private static void FindShortDistanceOfEachNodeFromSource(List<Node> graph,Node startNode)
        {
            //Take Two HashTables. One to maintain distances of each node from source and another one to indicate the node has been processed
            Dictionary<Node,int> nodeDistances = new Dictionary<Node, int>();
            Hashtable processedElements = new Hashtable();

            nodeDistances.Add(startNode, 0);
            while(processedElements.Count != graph.Count)
            {
                Node nearestNode = GetNearestUnvisitedNode(nodeDistances,processedElements);

                processedElements.Add(nearestNode,true);

                foreach(var arc in nearestNode.Arcs)
                {
                    if (nodeDistances.Keys.Contains(arc.Child))
                    {
                        if (nodeDistances[arc.Child] > nodeDistances[nearestNode] + arc.Weight) //If we able to reach a child node with less weight than its previous weight, update its weight
                            nodeDistances[arc.Child] = nodeDistances[nearestNode] + arc.Weight;
                    }
                    else
                        nodeDistances.Add(arc.Child, nodeDistances[nearestNode]+arc.Weight);
                }
            }

            PrintShortestDistancesFromSourceNode(startNode,nodeDistances);
        }

        private static void PrintShortestDistancesFromSourceNode(Node soruceNode, Dictionary<Node, int> nodeDistances)
        {
            foreach (var key in nodeDistances.Keys)
                Console.WriteLine($"Shortest Distance of {key.Name} from {soruceNode.Name} is : {nodeDistances[key]}");
        }

        private static Node GetNearestUnvisitedNode(Dictionary<Node, int> nodeDistances,Hashtable processedElements)
        {
            int min = int.MaxValue;
            Node nearestNode = null;
            foreach(var key in nodeDistances.Keys.Where(node=> !processedElements.Contains(node))) //Iterate only those nodes which are not visited/prcossed
            {
                if(nodeDistances[key] < min)
                {
                    min = nodeDistances[key];
                    nearestNode =key;
                }
            }
            return nearestNode;
        }
    }


    public class Node
    {
        public string Name { get; set; }
        public List<Arc> Arcs = new List<Arc>();

        public Node(string name)
        {
            Name = name;
        }
        //http://stackoverflow.com/questions/15306040/generate-an-adjacency-matrix-for-a-weighted-graph
        public Node AddArc(Node child, int w)
        {
            if (Arcs.Exists(a => a.Child == child)) //If arc already defined.
                return this;
            Arcs.Add(new Arc()
            {
                Parent = this,
                Child = child,
                Weight = w
            });

            //Adding the reverse link/Arc from child to this parent object
            if (!child.Arcs.Exists(a => a.Child == this && a.Parent == child)) //If is not already added
                child.Arcs.Add(new Arc()
                {
                     Parent = child,
                     Child = this,
                     Weight = w
                });

            return this;
        }
    }

    public class Arc
    {
        public Node Parent;
        public Node Child;
        public int Weight;
    }
}
