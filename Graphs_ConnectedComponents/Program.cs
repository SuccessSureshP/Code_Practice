using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Find how many connected components in the given graph
/*
      A             D                  F     I 
    /  \             \                /     /  
   B --  c             E             G  --  H 
 
 Output is : 3
 
 */
namespace Graphs_ConnectedComponents
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
    class Program
    {
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
            var nH = new Node('H');
            var nI = new Node('I');
            

            nA.NextNodes.Add(nB);
            nA.NextNodes.Add(nC);

            nB.NextNodes.Add(nC);

            nD.NextNodes.Add(nE);


            nF.NextNodes.Add(nG);
            nG.NextNodes.Add(nH);
            nH.NextNodes.Add(nI);



            g.Nodes.Add(nA);
            g.Nodes.Add(nB);
            g.Nodes.Add(nC);
            g.Nodes.Add(nD);
            g.Nodes.Add(nE);
            g.Nodes.Add(nF);
            g.Nodes.Add(nG);
            g.Nodes.Add(nH);
            g.Nodes.Add(nI);

            int connectedComponents = GetConnectedComponent(g);

            Console.WriteLine("Number of connected components: " + connectedComponents);
            Console.ReadKey();
        }

        private static int GetConnectedComponent(Graph g)
        {
            //Take a Hash table 
            Hashtable hashTable = new Hashtable();
            int count = 0;
            foreach (var node in g.Nodes)
            {
                if (!hashTable.ContainsKey(node))
                {
                    count++;
                    hashTable.Add(node,null);
                    //Now process all nodes reachable from this node
                    ProcessPaths(node, hashTable);
                }
            }

            return count;
        }

        //BFS
        private static void ProcessPaths(Node node, Hashtable hashTable)
        {
            Queue<Node> q = new Queue<Node>();

            q.Enqueue(node);
            Node u;
            while (q.Count() != 0)
            {
                u = q.Dequeue();
                if (u != null)
                {
                    foreach (Node v in u.GetAdjacentNodes()) //get all its adjacent nodes
                    {
                        if (!hashTable.ContainsKey(v)) //If this new node is not in HT, add to HT
                        {
                            hashTable.Add(v, null);
                            q.Enqueue(v);
                        }
                    }
                }
            }
        }
    }
}
