using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Chekc this https://youtu.be/rbg7Qf8GkQ4?t=9m50s
//https://simpledevcode.wordpress.com/2014/09/16/avl-tree-in-c/
namespace AVL_Tree_Insert_Delete
{
    public class AVLTree
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

        Node root; 
        
        public void Add(int v)
        {
            Node n = new Node(v);
            if (root == null)
            {
                root = n;
            }
            else
            {
                root = RecursiveInsert(root, n);
            }
        }

        private Node RecursiveInsert(Node current, Node node)
        {
            if (current == null)
                return node;

            if (node.value < current.value)
            {
                current.Left = RecursiveInsert(current.Left, node);
                current = Balance_Tree(current);
            }
            else if(node.value > current.value)
            {
                current.Right = RecursiveInsert(current.Right, node);
                current = Balance_Tree(current);
            }

            return current;
        }

        private int max(int l,int r)
        {
            return l > r ? l : r;
        }

        private int GetHeight(Node current)
        {
            int height = 0;
            if(current != null)
            {
                int l = GetHeight(current.Left);
                int r = GetHeight(current.Right);

                height = max(l,r) + 1;
            }
            return height;
        }

        private int Balance_Factor(Node node)
        {
            int l = GetHeight(node.Left);
            int r = GetHeight(node.Right);
            return l - r;
        }

        //RR situation. So do Rotaton Left
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }

        //LL situatoion. So do a Rotaton Right
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }

        //LF siatikon
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private Node RotateRL(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
            
        }


        private Node Balance_Tree(Node current)
        {
            int balancing_Factor = Balance_Factor(current);
            if(balancing_Factor > 1) // Means Left Sub tree has more children levels than Right Sub Tree. 
            {
                if (Balance_Factor(current.Left) > 0)
                    current = RotateLL(current);
                else
                    current = RotateLR(current);
            }else if(balancing_Factor < -1)
            {
                if (Balance_Factor(current.Right) > 0)
                    current = RotateRL(current);
                else
                    current = RotateRR(current);
            }

            return current;
        }

        public void DisplayTree()
        {
            InOrderDisplayTree(root);
            Console.ReadKey();
        }
        private void InOrderDisplayTree(Node n)
        {
            if (n != null)
            {
                InOrderDisplayTree(n.Left);
                Console.Write(n.value + "  ");
                InOrderDisplayTree(n.Right);
            }
        }


        public void Delete(int k)
        {
            root = Delete(root, k);
        }
        private Node Delete(Node current, int k)
        {
            Node parent;
            if (current == null)
                return null;
            else if (k < current.value)
            {
                //Left Sub tree
                current.Left = Delete(current.Left, k);
                if (Balance_Factor(current) == -2) //After deleting element in the left sub tree, elements on the right became more. So if they became equal to -2, then we have to do rotatation
                {
                    if (Balance_Factor(current.Right) <= 0)
                        current = RotateRR(current);
                    else
                        current = RotateRL(current);
                }
            }
            //Right Sub tree
            else if (k > current.value)
            {
                current.Right = Delete(current.Right, k);
                if(Balance_Factor(current) == 2) //After deleting elememnt in Right Sub tree, elements on the left became more. So if they became equal to 2, then we have to do rotation
                {
                    if (Balance_Factor(current.Left) <= 0)
                        current = RotateLL(current);
                    else
                        current = RotateLR(current);
                }
            }
            else //Target Found
            {
                if (current.Right != null)
                {
                    //Delete its Inorder sucessor
                    parent = current.Right;
                    while (parent.Left != null)
                        parent = parent.Left;
                    //Copy InOrder Sucessor value to deleted element. 
                    current.value = parent.value;
                    //Now delete the InOrder Sucessor Node 
                    current.Right = Delete(current.Right, parent.value);
                    if (Balance_Factor(current) == 2) //After deleting node in the right Sub tree, nodes on the left became more and so do rotation 
                    {
                        if (Balance_Factor(current.Left) <= 0)
                            current = RotateLL(current);
                        else
                            current = RotateLR(current);
                    }
                }
                else
                    return current.Left;
            }

            return current;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            AVLTree tree = new AVLTree();
            tree.Add(2);
            tree.Add(1);
            tree.Add(0);
            tree.Add(-1);
            tree.Add(-2);
            tree.Add(3);
            tree.Add(5);
            tree.Add(4);
            tree.Add(6);

            tree.DisplayTree();

            //tree.Delete(3); // try this one also
            tree.Delete(2); 
            Console.WriteLine();
            tree.DisplayTree();

            Console.ReadKey();
        }        
    }
}
