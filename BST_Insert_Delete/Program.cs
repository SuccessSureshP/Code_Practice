using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Insert_Delete
{
    class Node
    {
        public Node Right;
        public Node Left;
        public int value;

        public Node(int v)
        {
            value = v;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Node n15 = new Node(15);

            Node n3 = new Node(3);
            Node n17 = new Node(17);

            Node n2 = new Node(2);
            Node n14 = new Node(14);
            Node n16 = new Node(16);
            Node n18 = new Node(18);

            n15.Left = n3;
            n15.Right = n17;

            n3.Left = n2;
            n3.Right = n14;

            n17.Left = n16;
            n17.Right = n18;

            PrintInSortOrder(n15);
            Console.WriteLine();
            Console.WriteLine("\nFound 16: "+ FindNode(n15, 16));
            Console.WriteLine("Found 9: " + FindNode(n15, 9));
            Console.WriteLine("Is Tree a BST?:" + isTreeBST(n15));
            n15 = InsertNode(n15, 13);
            Console.WriteLine("After Insersion");
            PrintInSortOrder(n15);
            Console.WriteLine();

            n15 = DeleteNode(n15, 3); //Node with both left and right
            Console.WriteLine("After Deletion of 3");
            PrintInSortOrder(n15);
            Console.WriteLine();


            n15 = DeleteNode(n15, 2); //Nede without any childs
            Console.WriteLine("After Deletion of 2");
            PrintInSortOrder(n15);
            Console.WriteLine("\n\n");

            n15 = InsertNode(n15, 20);
            n15 = InsertNode(n15, 19);
            Console.WriteLine("After Insersion of 20 and 19");
            PrintInSortOrder(n15);
            Console.WriteLine("\n\n");

            n15 = DeleteNode(n15, 20); //Nede with LEFT Child only (19)
            Console.WriteLine("After Deletion of 20");
            PrintInSortOrder(n15);

            n15 = DeleteNode(n15, 18); //Nede with Rigiht Child only (19 after above deletion)
            Console.WriteLine("\nAfter Deletion of 18");
            PrintInSortOrder(n15);
            Console.WriteLine();


            Console.ReadKey();
        }
        static void PrintInSortOrder(Node root) //In-Order Traversal 
        {
            if (root == null)
                return;
            PrintInSortOrder(root.Left);
            Console.Write(root.value +"  ");
            PrintInSortOrder(root.Right);
        }

        static bool FindNode(Node root,int v)
        {
            if (root == null)
                return false;

            if (root.value == v)
                return true;

            if (v < root.value)
                return FindNode(root.Left, v);
            else
                return FindNode(root.Right, v);
        }

        static Node InsertNode(Node root, int v)
        {   
            if (root == null)
            {
                Node newNode;
                newNode = new Node(v);
                return newNode;
            }
            else if (v < root.value)
                root.Left = InsertNode(root.Left, v);
            else
                root.Right = InsertNode(root.Right, v);

            return root;
        }

        static Node DeleteNode(Node root, int v)
        {
            if (root == null)
                return null;
            if (v < root.value)
            {
                root.Left = DeleteNode(root.Left, v);
            }
            else if (v > root.value)
            {
                root.Right = DeleteNode(root.Right, v);
            }
            else //We found the node to be deleted
            {
                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;
                else // Means node to be deleted has both Left and Right nodes. Then we have to find In-Order Successor  of its Right Sub Tree
                {
                    Node inOrderSucessor = FindInOrderSucessor(root.Right); //****** IMP ******************
                    root.value = inOrderSucessor.value; //****** IMP ****************** Copy the contents of In order Successor 
                    root.Right = DeleteNode(root.Right, root.value); //****** IMP ****************** Now, we have to delete the InOrder Successor. So initiate that call
                }
            }
            return root;
        }

        private static Node FindInOrderSucessor(Node node)
        {
            if (node.Left == null)
                return node;
            return FindInOrderSucessor(node.Left);
        }

        //this is Wrong because we are just checking root value with its immidiate  childrens, but we have to compare with all of its children. So, it is wrong*********************************************************************************** 
        static bool isTreeBSTWrong(Node root)
        {
            if (root == null)
                return true;

            bool isLeftSubTreeBST = true;
            bool isRightSubTreeBST = true;
            if (root.Left != null)
            {
                if (root.value > root.Left.value)
                    isLeftSubTreeBST = isTreeBST(root.Left);
                else
                    isLeftSubTreeBST = false;
            }
            if(root.Right != null)
            {
                if (root.value <= root.Right.value)
                    isRightSubTreeBST = isTreeBST(root.Right);
                else
                    isRightSubTreeBST = false;
            }

            return isLeftSubTreeBST && isRightSubTreeBST;
        }

        static bool isTreeBST(Node root)
        {
            return isTreeBST(root, int.MinValue, int.MaxValue);
        }

        static bool isTreeBST(Node root, int min, int max)
        {
            if (root == null)
                return true;

            if (root.value < min || root.value > max)
                return false;

            //  /* otherwise check the subtrees recursively
          //  tightening the min / max constraints */
        // Allow only distinct values
            return isTreeBST(root.Left, min, root.value - 1) &&
                isTreeBST(root.Right, root.value + 1, max);
        }
    }
}
