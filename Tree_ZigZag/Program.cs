using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
//Given a tree. Print level by level elements in zig-Zag fashion. One level left to right and next level right to left. 
namespace Tree_ZigZag
{
    class Node
    {
        public  char Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(char a)
        {
            Data = a;
        }

    }
    public class Program
    {
        static void Main(string[] args)
        {

            //INPUT: Tree is like this 
            /*      A
                   /  \
                 B      C
               /  \    /  \
              D    E  F    G
             / \   /\         
            H  I  J  K     
             
            
           OUTPUT: 
            A
            C B
            D E F G
            K J I H          
            */

            Node a = new Node('A');

            Node b = new Node('B');
            Node c = new Node('C');
            a.Left = b;
            a.Right = c;

            Node d = new Node('D');
            Node e = new Node('E');
            b.Left = d;
            b.Right = e;
            
            Node f = new Node('F');
            Node g = new Node('G');
            c.Left = f;
            c.Right = g;

            Node h = new Node('H');
            Node i = new Node('I');
            d.Left = h;
            d.Right = i;


            Node j = new Node('J');
            Node k = new Node('K');
            e.Left = j;
            e.Right = k;
         

            PrintTree_ZigZag(a);
            Console.Read();
        }

        static void PrintTree_ZigZag(Node root)
        {
            if(root == null)
                return;

            Stack<Node> ltr_stack  = new Stack<Node>();
            Stack<Node> rtl_stack = new Stack<Node>();
            

            ltr_stack.Push(root);

            while (ltr_stack.Count != 0 || ltr_stack.Count != 0)
            {
                while (ltr_stack.Count != 0)
                {
                    var ele = ltr_stack.Pop();
                    Console.Write(ele.Data + "  ");
                    //We have to push the left child first and then right child
                    if (ele.Left != null)
                        rtl_stack.Push(ele.Left);

                    if(ele.Right != null)
                        rtl_stack.Push(ele.Right);
                }
                Console.WriteLine();

                while (rtl_stack.Count != 0)
                {
                    var ele = rtl_stack.Pop();
                    Console.Write(ele.Data + "  ");
                    //We have to push right node first and then left node.
                    if(ele.Right != null)
                        ltr_stack.Push(ele.Right);

                    if(ele.Left != null)
                        ltr_stack.Push(ele.Left);
                }
                Console.WriteLine();

            }

        }
    }
}
