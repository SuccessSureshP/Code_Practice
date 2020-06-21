using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList_CloneLinkedList
{
    class Node
    {
        public int Data;
        public Node Next;
        public Node Rand;

        public Node(int v)
        {
            this.Data = v;
        }
        public void AddLinks(Node next, Node rand)
        {
            this.Next = next;
            this.Rand = rand;
        }         
    }
    class Program
    {
        static void Main(string[] args)
        {
            Node n1 = new Node(1);
            Node n2 = new Node(2);
            Node n3 = new Node(3);

            n1.AddLinks(n2, n3);
            n2.AddLinks(n3, null);
            n3.AddLinks(null, n1);

            PrintList(n1);
            
            Node head2 = CloneImmutableList(n1);
            Console.WriteLine();
            PrintList(head2);

            Node head3 = ClonemutableList(n1);
            Console.WriteLine();
            PrintList(head3);

            Node reverseListHead = ReverseList(n1);
            Console.WriteLine();
            PrintList(reverseListHead);


            //Swap alternate nodes 
            n1 = new Node(1);
            n2 = new Node(2);
            n3 = new Node(3);
            Node n4 = new Node(4);
            Node n5 = new Node(5);
            Node n6 = new Node(6);
            Node n7 = new Node(7);
            Node n8 = new Node(8);
            Node n9 = new Node(9);          

            Node head4 = ReverseListPairs(n1); //List With only one node
            Console.WriteLine();
            PrintList(head4);

            n1.AddLinks(n2, null);
            n2.AddLinks(n3, null);
            n3.AddLinks(n4, null);
            
            head4 = ReverseListPairs(n1); //List With even # of nodes
            Console.WriteLine();
            PrintList(head4);

            n1.AddLinks(n2, null);
            n2.AddLinks(n3, null);
            n3.AddLinks(n4, null);
            n4.AddLinks(n5, null);

            head4 = ReverseListPairs(n1); //List with odd # of nodes
            Console.WriteLine();
            PrintList(head4);
            
            //-------------------------------------------
            n1.AddLinks(n2, null);
            n2.AddLinks(n3, null);
            n3.AddLinks(n4, null);
            n4.AddLinks(n5, null);

            Node head5 = ReverseListWithBucketOf3(n1);
            Console.WriteLine();
            Console.WriteLine("ReverseListWithBucketOf3");
            PrintList(head5);


            n1.AddLinks(n2, null);
            n2.AddLinks(n3, null);
            n3.AddLinks(n4, null);
            n4.AddLinks(n5, null);
            n5.AddLinks(n6, null);
            n6.AddLinks(n7, null);
            n7.AddLinks(n8, null);
            n8.AddLinks(n9, null);
            n9.AddLinks(null, null);

            head5 = ReverseListWithBucketOf3(n1);
            Console.WriteLine();
            Console.WriteLine("ReverseListWithBucketOf3");
            PrintList(head5);
            Console.ReadKey();

        }

        static void PrintList(Node n)
        {
            while (n != null)
            {
                Console.Write(n.Data + "  ");
                n = n.Next;
            }
        }

        //Without changing the original node, but you can use extra space
        static Node CloneImmutableList(Node head)
        {
            if (head == null)
                return null;
            Node head2 = null;
            Dictionary<Node, Node> h = new Dictionary<Node, Node>();

            Node t = head;
            while(t != null)
            {
                Node n = new Node(t.Data);

                h.Add(t, n);
                if (head2 == null)
                    head2 = n;

                t = t.Next;
            }

            t = head2;
            Node p = head;
            while(p!= null)
            {
                t.Next = p.Next != null ? h[p.Next] : null;
                Node rn = p.Rand;
                if (rn == null)
                    t.Rand = null;
                else
                    t.Rand = h[rn];

                p = p.Next;
                t = t.Next;
            }

            return head2;
        }

        /// <summary>
        /// You can change existing list. Don't use extra space
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        static Node ClonemutableList(Node head)
        {
            if (head == null)
                return null;

            Node p = head; Node head2 = null;
            //Creating duplicate nodes
            while(p!= null)
            {
                //Node q = new Node(p.Data);
                Node q = new Node(int.Parse(p.Data.ToString()+ p.Data.ToString())); //making 1 as 11, 2 as 22 etc in new list . 
                q.Next = p.Next;
                if (head2 == null)
                    head2 = q;
                p.Next = q;
                p = q.Next;
            }

            p = head;
            Node r = head2;
            //Set Randon pointers
            while(p!= null)
            {
                r.Rand = p.Rand != null ? p.Rand.Next : null;
                p = r.Next;
                if (p != null)
                    r = p.Next;
            }

            p = head; r = head2;
            //Unlink the 1st linked list and 2nd linked list
            while(p != null)
            {
                p.Next = r.Next;
                r.Next = r.Next != null ? r.Next.Next : null;
                p = p.Next;
                r = r.Next;
            }

            return head2;
        }

        //1->2->3 becomes 3->2->1
        static Node ReverseList(Node head)
        {
            Node p1, p2, p3;
            p1 = null;
            p2 = head;

            while(p2 != null)
            {
                p3 = p2.Next;
                p2.Next = p1;
                p1 = p2;
                p2 = p3;
            }

            return p1;
        }

        //Change 1->2->3->4->5 to 2->1->4->3->5
        static Node ReverseListPairs(Node head)
        {
            if (head == null)
                return null;

            Node n = head;
            Node prev = null;
            Node head2 = null;
            while(n != null && n.Next != null)
            {
                n = ReversePairs(n, prev);
                if (head2 == null)
                    head2 = n;
                if (n.Next != null)
                {
                    prev = n.Next;
                    n = n.Next.Next;
                }
            }
            if (head2 == null)
                return head;
            return head2;
        }

        private static Node ReversePairs(Node firstNode, Node prev)
        {
            if (firstNode == null)
                return null;
            if (firstNode.Next == null)
                return firstNode;

            Node secndNode = firstNode.Next;
            Node thirddNode = secndNode.Next;

            secndNode.Next = firstNode;
            firstNode.Next = thirddNode;
            if (prev != null)
                prev.Next = secndNode;
            return secndNode;

        }


        //Reverse the list in Bucket of 3 
        private static Node ReverseListWithBucketOf3(Node head)
        {
            if (head == null)
                return null;
            if (head.Next ==null || head.Next.Next == null) //If total nodes are less than 3, return as is.
                return head;

            Node head2 = null;
            Node prev = null;
            Node n = head;

            while(n != null && n.Next != null && n.Next.Next != null)
            {
                Node headOfReversedTriplet = ReverseTriplet(n, prev);
                if (head2 == null)
                    head2 = headOfReversedTriplet;

                prev = n;
                n = n.Next;
            }

            if (head2 == null)
                return head;
            else
                return head2;
        }


        private static Node ReverseTriplet(Node startNode, Node prevNodeBeforeTriplet)
        {
            if (startNode == null)
                return null;
            if (startNode.Next == null || startNode.Next.Next == null) //if we have only 1 or 2 nodes from startNode, don't rotate
                return startNode;

            Node prev = null;
            Node n = startNode;
            Node next = null;
                int i = 0;
            while(i<3)
            {
                next = n.Next;
                n.Next = prev;
                prev = n;
                n = next;
                i++;
            }
            //now n has the reference to 4th node. So , set that as next node of 1st node in this triplet
            startNode.Next = n;
            //Prev has the last node of the triplet, that should be linked to prevNodeBeforeTriplet
            if (prevNodeBeforeTriplet != null)
                prevNodeBeforeTriplet.Next = prev;

            return prev; //This is new head of the triplet after rotation

        }
    }
}
