using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//http://www.geeksforgeeks.org/iterative-postorder-traversal/
//http://www.geeksforgeeks.org/iterative-postorder-traversal-using-stack/
//http://www.geeksforgeeks.org/inorder-tree-traversal-without-recursion/
//http://www.geeksforgeeks.org/iterative-preorder-traversal/
namespace Tree_Non_Recurssive_Traversal
{
    public class Node
    {
        public char Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }

        public bool isLeftProcessed { get; set; }

        public bool isRightProcessed { get; set; }

        public Node(char v)
        {
            Data = v;
        }

       public  void SetLinks(Node left, Node right, Node parent)
        {
            Left = left;
            Right = right;
            Parent = parent;
        }
    }

    public class Class1
    {

        static int[] CommonElements(int[] a, int[] b)
        {
            HashSet<int> h = new HashSet<int>();
            for (int i = 0; i < a.Length; i++)
                h.Add(a[i]);
            List<int> res = new List<int>();
            for(int i=0;i<b.Length;i++)
            {
                if (h.Contains(b[i]))
                    res.Add(b[i]);
            }
            return res.ToArray();
        }

        public static void Main(string[] arg)
        {
            int[] a1 = {1,2,34,5,6,7 };
            int[] b1 = { 1, 2, 8, 9, 10, };
            var res = CommonElements(a1, b1);


            Node a = new Node('A');
            Node b = new Node('B');
            Node c = new Node('C');
            Node d = new Node('D');
            Node e = new Node('E');
            Node f = new Node('F');
            //     A
            //  B     C
            //D     E    F

            a.SetLinks(b, c, null);
            b.SetLinks(d, null, a);
            c.SetLinks(e, f, a);
            d.SetLinks(null, null, b);
            e.SetLinks(null, null, c);
            f.SetLinks(null, null, c);

            PreOrder(a);

        }

        private static void PreOrder(Node a)
        {
           // Node prev = null;
            Node curr = a;

            while(a != null)
            {
                Console.Write(a.Data + "  ");
                if (a.Left != null && !a.Left.isRightProcessed)
                {
                    a = a.Left;
                    continue;
                }
                else
                {
                    a.isLeftProcessed = true;
                    // a = a.Right;
                }

                if (a.Right != null)
                {
                    a = a.Right;
                    continue;
                }
                else
                {
                    a.isRightProcessed = true;
                    // a = a.Right;
                }

                if (a.isLeftProcessed && a.isRightProcessed)
                    a = a.Parent;
                         
            }



            //while(curr != null)
            //{
            //    Console.WriteLine(curr.Data);
            //    //if(prev.Left == curr || prev == null)
            //    //{
            //        prev = curr;
            //        curr = curr.Left;
            //   // }
            //    if(curr == null)
            //    {
            //        curr = prev.Right;
            //    }
            //    if(curr == null)
            //    {                   
            //        curr = prev.Parent;
            //    }
            //    if(prev == curr.Left)
            //    {
            //        prev = curr;
            //        curr = curr.Right;
            //    }

            //    if(prev == curr.Right)
            //    {
            //        break;
            //        //while(prev != null || prev.Right == null)
            //        //{

            //        //}
            //    }

            //}

        }
    }
}
