using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//CCI Page 220
namespace IsTreeBalanced
{

    public class Node
    {
        public char Data { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(char c)
        {
            Data = c;
        }
    }



    class Program
    {
        static int HeightOfNode(Node node)
        {
            if (node == null)
                return 0;


            //Check Height of left node/tree
            int leftSubTreeHeight = HeightOfNode((node.Left));
            if (leftSubTreeHeight == -1)
                return -1;

            //Check Height of right Node/Subtree
            int rightSubTreeHeight = HeightOfNode(node.Right);
            if (rightSubTreeHeight == -1)
                return -1;

            //Check Difference in heights of Left and Right Nodes/SubTrees
            int diff = leftSubTreeHeight - rightSubTreeHeight;
            if (Math.Abs(diff) > 1) //Means left sub tree and right sub tree have heights of more than 1. It is not balanced starting from this parent to the root.
                return -1;

            return Math.Max(leftSubTreeHeight ,rightSubTreeHeight) + 1;//This is height of this node. Take maximum height from left or right and add 1 to it.

        }

        static bool IsTreeBalanced(Node root)
        {
            if (HeightOfNode(root) == -1)
                return false;
            return true;
        }

        static void Main(string[] args)
        {
            //Success case 
            Node a = new Node('A');
            Node b = new Node('B');
            Node c = new Node('C');
            Node d = new Node('D');
            Node e = new Node('E');

            a.Left = b;
            a.Right = c;

            b.Left = d;
            b.Right = e;

            Console.WriteLine("1. Is Balanced:"+IsTreeBalanced(a));

            //Failure case
            Node f = new Node('F'); //Added extra node to e

            
            e.Right = f;

            Console.WriteLine("2. Is Balanced:" + IsTreeBalanced(a));
            Console.ReadKey();

        }


        
    }
}
