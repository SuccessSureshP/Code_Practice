using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//CCI. Page : 229 Fine next ndoe in IN-order Traversal 
namespace NextNode_In_BST
{
    //Draw UML class diagram instead in the Board
    public class Node
    {
        public int Data { get; set; }

        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node Parent { get; set; }

        public Node(int d)
        {
            Data = d;
        }
    }

    public static class MainClass
    {
        /// <summary>
        /// In Order Sucessor in BST. Without using parent pointer. Time Complexity: O(h) where h is height of tree.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Node InOrderSucessor(Node root, Node node)
        {
            if (node == null)
                return null;
             
            if(node.Right != null)
                return FindLeftMostNode(node.Right);                
            else
            {
                Node succ = null;
                while(root != null)
                {
                    if (root.Data > node.Data)
                    {
                        succ = root;
                        root = root.Left;
                    }
                    else if (root.Data < node.Data)
                        root = root.Right;
                    else
                        break;
                }
                if (succ == null)
                    throw new Exception($"No Successor found for {node.Data}");
                                
                return succ;
            }

            
        }

        /// <summary>
        /// In Order Sucessor in BST. This is using Parent Pointer
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public  static Node   GetNextNode(Node node)
        {
            if (node == null) //If given node is null
                return null;
                     
            //if (node.Parent == null) //Means node is root
            //    return node.Right; //This is wrong thought..........................**************** bcoz, We have to traverse right subtree and return left most leag node on that sub tree. 


            if (node.Right != null) //Node has right  Sub tree
                return FindLeftMostNode(node.Right);
            else // This will cover both scenarios 1. IF node is right child of its parents 2. if node is left child of its parent
            {
                Node p = node;
                Node q = p.Parent;
                while (q != null && q.Left != p) //****** IMP ******************
                {
                    p = q;
                    q = q.Parent;
                }
                return q;
            }
        }

        private static Node FindLeftMostNode(Node node)
        {
            Node n = node;
            while (n.Left != null)
            {
                n = n.Left;
            }
            return n;
        }

        public static void Main(string[] args)
        {
            Node n5 = new Node(5);

            Node n3 = new Node(3);
            Node n7 = new Node(7);

            Node n2 = new Node(2);
            Node n4 = new Node(4);
            Node n6 = new Node(6);
            Node n8 = new Node(8);

            n5.Left = n3;
            n5.Right = n7;

            n3.Left = n2;
            n3.Right = n4;

            n7.Left = n6;
            n7.Right = n8;


            n3.Parent = n7.Parent = n5;

            n2.Parent = n4.Parent = n3;

            n6.Parent = n8.Parent = n7;

            try
            {

                Console.WriteLine("Next Node of " + n4.Data + "  is  " + GetNextNode((n4)).Data);

                var nextNode = GetNextNode((n8));
                string nextNodeValue = (nextNode == null) ? "NULL" : nextNode.Data.ToString();

                Console.WriteLine("Next Node of " + n8.Data + "  is  " + nextNodeValue);

                Console.WriteLine("Next Node of " + n2.Data + "  is  " + GetNextNode((n2)).Data);

                Console.WriteLine("Next Node of " + n7.Data + "  is  " + GetNextNode((n7)).Data);

             

                Console.WriteLine("Next Node of " + n2.Data + "  is  " + InOrderSucessor(n5, n2).Data);

                Console.WriteLine("Next Node of " + n7.Data + "  is  " + InOrderSucessor(n5, n7).Data);                


                //Find next max value before given value
                Console.WriteLine("Next max before  " + 6 + "  is  " + NextMaxNum(n5, 6));

                Console.WriteLine("Next max before  " + 8 + "  is  " + NextMaxNum(n5, 8));

                Console.WriteLine("Next max before  " + 3 + "  is  " + NextMaxNum(n5, 3));

                Console.WriteLine("Next max before  " + 2 + "  is  " + NextMaxNum(n5, 2));


                InorderPredecessor(n5, 6);
                InorderPredecessor(n5, 2);

                InorderPredecessor(n5, n6);
                InorderPredecessor(n5, n2);
                InorderPredecessor(n5, n5);


                Console.WriteLine("Next Node of " + n8.Data + "  is  " + InOrderSucessor(n5, n8).Data);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
            
        }

        /// <summary>
        /// Iterative : This finds In-order Predecessor. In other words, previous node in In-order sequence (sorting order) in BST
        /// </summary>
        /// <param name="root"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int NextMaxNum(Node root, int v)
        {
            //return NextMaxNum(root, 0, v); Recurssive
            //Below is iterative

            int result = 0;
            while(root != null)
            {
                if(root.Data < v)
                {
                    result = root.Data;
                    root = root.Right;
                }else
                {
                    root = root.Left;
                }
            }
            return result;
        }
        /// <summary>
        /// Recurssive: This finds In-order Predecessor. In other words, previous node in In-order sequence (sorting order) in BST
        /// </summary>
        /// <param name="current"></param>
        /// <param name="maxSofar"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private static int NextMaxNum(Node current, int maxSofar, int v)
        {            
            if (current.Data > maxSofar && current.Data < v)
            {
                maxSofar = current.Data;
                if(current.Right != null)
                    maxSofar = NextMaxNum(current.Right, maxSofar, v);
            }
            else
            {
                if (current.Left != null)
                    maxSofar = NextMaxNum(current.Left, maxSofar, v);
            }

            return maxSofar;

        }
        /// <summary>
        /// Iterative: This finds In-order Predecessor. In other words, previous node in In-order sequence (sorting order) in BST. This works for all values even negative values
        /// </summary>
        /// <param name="root"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private static void InorderPredecessor(Node root,int v)
        {
            int result = int.MinValue;
            while(root != null)
            {
                if (root.Data < v)
                {
                    result = Math.Max(root.Data, result);
                    root = root.Right;
                }
                else
                    root = root.Left;
            }

            if (result == int.MinValue)
                Console.WriteLine("No max value exists which is less than :" + v);
            else
                Console.WriteLine("Next max before  " + v + "  is  " + result);
        }

        /// <summary>
        /// Iterative: This finds In-order Predecessor. In other words, previous node in In-order sequence (sorting order) in BST. In this case we were given with a node
        /// </summary>
        /// <param name="root"></param>
        /// <param name="node"></param>
        private static void InorderPredecessor(Node root,Node node)
        {
            if (node == null)
                return;

            if (node.Left != null)
            {
                //find right most node of node.Left, that will be In order Predecessor
                Node p = node.Left;
                while (p.Right != null)
                    p = p.Right;
                Console.WriteLine($"In Order Predecessor of {node.Data} is {p.Data}");
            }
            else
            {
                InorderPredecessor(root, node.Data);
            }

        }


    }

}
