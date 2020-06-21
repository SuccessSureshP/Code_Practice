using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BineryTree_to_Linked_Lists
{
    //CCI Page 224 : given a Binary tree. Create Liked List for the nodes at each level. If binary tree of Depth D, we will get D linked lists.
    class Program
    {
        class Node
        {
            public int Value;
            public Node Right;
            public Node Left;

            public Node(int i)
            {
                Value = i;
            }
        }
        
        static void Main(string[] args)
        {
            Node n9 = new Node(9);

            Node n5 = new Node(5);
            Node n11 = new Node(11);
            

            Node n4 = new Node(4);
            Node n6 = new Node(6);
            Node n10 = new Node(10);
            Node n12 = new Node(12);

            n9.Left = n5;
            n9.Right = n11;

            n5.Left = n4;
            n5.Right = n6;

            n11.Left = n10;
            n11.Right = n12;

            var result = LevelwiseLists(n9);

            foreach (var levelList in result)
            {
                foreach (var node in levelList)
                {
                    Console.Write(node.Value + "   ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static List<List<Node>> LevelwiseLists(Node root)
        {
            List<List<Node>> resultLists = new List<List<Node>>();

            List<Node> prevLevelList = new List<Node>();
            prevLevelList.Add(root);

            while (prevLevelList.Count>0)
            {
                resultLists .Add(prevLevelList);
                List<Node> newList = new List<Node>();
                foreach (var element in prevLevelList)
                {
                    if(element.Left!=  null)
                        newList.Add(element.Left);
                    if(element.Right !=  null)
                        newList.Add(element.Right);
                }
                prevLevelList = newList;
            }

            return resultLists;
        }

    }
}
