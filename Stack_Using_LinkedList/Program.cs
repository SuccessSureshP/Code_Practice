using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Using_LinkedList
{
    public class Node
    {
         public int Data { get; set; }
        public Node Next { get; set; }
                

        public Node(int v)
        {
            Data = v;
            Next = null;
        }
    }

    public class Stack
    {
        Node head = null;
        public int Size { get; set; }

        public Stack()
        {
            Size = 0;
        }
        public void Push(int v)
        {
            Node n = new Node(v);
            if (head == null)
            {
                head = n;
            }
            else
            {
                n.Next = head;
                head = n;
            }
            Size++;
        }
        public int Pop()
        {
            if (head == null)
                throw new Exception("Stack is Empty");
            int v = head.Data;
            head = head.Next;
            Size--;
            return v;
        }

        public int Top()
        {
            if (head == null)
                throw new Exception("Stack is empty");

            return head.Data;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stack s = new Stack();

            s.Push(1);
            s.Push(2);
            s.Push(3);

            Console.WriteLine("Top element:" + s.Top());
            Console.WriteLine("Size:" + s.Size);
            s.Pop();
            Console.WriteLine("Top element:" + s.Top());
            Console.WriteLine("Size:" + s.Size);
            s.Push(4);
            s.Push(5);
            Console.WriteLine("Top element:" + s.Top());
            Console.WriteLine("Size:" + s.Size);
            Console.ReadKey();
        }
    }
}
