using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://github.com/kaihsyn/leetcode/blob/master/Solution124.java - 2nd problem - Max path sum
//http://www.geeksforgeeks.org/find-maximum-path-sum-in-a-binary-tree/
namespace Binary_tree_Node_Relation
{

    public class Node
    {
        public int value;
        public Node Right { get; set; }
        public Node Left { get; set; }

        public Node(int v)
        {
            value = v;
        }
    }

    public class LinkedListNode
    {
        public Node CurrentNode { get; set; }
        public LinkedListNode Next { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {

            Node n1 = new Node(1);

            Node n2 = new Node(2);
            Node n3 = new Node(3);

            Node n4 = new Node(4);
            Node n5 = new Node(5);

            Node n6 = new Node(6);
            Node n7 = new Node(7);

            Node n8 = new Node(8);
            Node n9 = new Node(9);
            Node n10 = new Node(10);

            n1.Left = n2;
            n1.Right = n3;

            n2.Left = n4;
            n2.Right = n5;

            n3.Left = n6;
            n3.Right = n7;

            n6.Left = n8;

            n7.Right = n9;

            n4.Left = n10;

            Console.WriteLine($"{9} is {GetLevel(n1, 9, 0)}");
            Console.WriteLine($"{1} is {GetLevel(n1, 1, 0)}");
            Console.WriteLine($"{20} is {GetLevel(n1, 20, 0)}");

            Console.WriteLine($"LCA of {n8.value} and {n9.value} is :{FindLCA(n1, n8.value, n9.value).value}");
            var res = FindLCA(n1, 20, 30);
            
            Console.WriteLine($"LCA of {n8.value} and {n9.value} is :{res?.value}");
            Console.WriteLine($"LCA of {n4.value} and {n9.value} is :{FindLCA(n1, n4.value, n9.value).value}");

           // Console.WriteLine($"LCA of {n6.value} {n8.value} and {n9.value} is :{FindLCA(n1, new List<int> { 6,8,9 }).value}");
            Console.WriteLine($"LCA of {n6.value} {n8.value} and {n9.value} is :{FindLCAOfListOfNodes(n1, new List<Node> { n6,n8,n9}).value}");
            Console.WriteLine($"LCA of {n4.value} {n6.value} and {n9.value} is :{FindLCAOfListOfNodes(n1, new List<Node> { n4, n6, n9 }).value}");
            Console.WriteLine($"LCA of {n6.value} {n8.value} and {n3.value} is :{FindLCAOfListOfNodes(n1, new List<Node> { n6, n8 ,n3 }).value}");
            //Console.WriteLine($"LCA of {n3.value} and {n9.value} is :{FindLCA(n1, n3.value, n9.value).value}");
            //Console.WriteLine($"LCA of {n3.value} and {n9.value} is :{FindLCA(n1, n3.value, n9.value).value}");
            //Console.WriteLine($"LCA of {n3.value} and {n9.value} is :{FindLCA(n1, n3.value, n9.value).value}");
            //PrintRelation(n1, 8, 9);

            PrintRelation(n1, 6, 7);

            PrintRelation(n1, 3, 7);
            PrintRelation(n1, 7, 3);

            PrintRelation(n1, 3, 9);
            PrintRelation(n1, 9, 3);

            PrintRelation(n1, 1, 9);
            PrintRelation(n1, 9, 1);

            PrintRelation(n1, 4, 9);

            PrintRelation(n1, 10, 9);

            PrintRelation(n1, 20, 30);

            MaxPath mp = new MaxPath();
            MaxPathSum(n1, mp);
            Console.WriteLine($"Max Path Sum = {mp.Value }");


            Node n_10 = new Node(10);

            Node n_2 = new Node(2);
            Node n2nd_10 = new Node(10);

            Node n_20 = new Node(20);
            Node n_1 = new Node(1);

            Node n_25 = new Node(-25);
            Node n_3 = new Node(3);
            Node n_4 = new Node(4);

            //http://www.geeksforgeeks.org/find-maximum-path-sum-in-a-binary-tree/

            n_10.Left = n_2;
            n_10.Right = n2nd_10;

            n_2.Left = n_20;
            n_2.Right = n_1;


            n2nd_10.Right = n_25;

            n_25.Left = n_3;
            n_25.Right = n_4;


            mp = new MaxPath();
            MaxPathSum(n_10, mp);
            Console.WriteLine($"Max Path Sum = {mp.Value }");

            Console.ReadKey();
        }

        static Node FindLCA(Node n,int v1,int v2)
        {
            if (n == null)
            {
                return null;
            }
            if (n.value == v1 || n.value == v2)
            {
                return n;
            }

            Node rightLCA = FindLCA(n.Right, v1, v2);
            Node leftLCA = FindLCA(n.Left, v1, v2);

            if (rightLCA != null && leftLCA != null)
                return n;
            else if (leftLCA != null)
                return leftLCA;
            else 
                return rightLCA;
        }
        static Node FindLCA(Node n,IEnumerable<int> values)
        {
            Dictionary<int, bool> lookupNode = new Dictionary<int, bool>();
            foreach(var va in values)
            {
                lookupNode.Add(va, false);
            }

            return FindLCAofList(n, values, lookupNode);
        }

        //Not giving right answer
        static Node FindLCAofList(Node n, IEnumerable<int> values, Dictionary<int,bool> lookupNodes)
        {
            if (n == null)
                return null;
            foreach (int v in values)
                if (n.value == v)
                {
                    lookupNodes[v] = true;
                }
            Node left_LCA = null;
            Node right_LCA = null;
            foreach (int v in values)
               left_LCA = FindLCAofList(n.Left,values,lookupNodes);

            if(left_LCA != null)
            {
                if (!lookupNodes.Any(lookupNode => lookupNode.Value == false))
                    return left_LCA;
            }

            foreach (int v in values)
               right_LCA = FindLCAofList(n.Right, values, lookupNodes);

            if (left_LCA != null)
            {
                if (!lookupNodes.Any(lookupNode => lookupNode.Value == false))
                    return right_LCA;
            }

            return n;
        }

        static Node FindLCAOfListOfNodes(Node root, List<Node> listOfNodes)
        {
            List<LinkedListNode> pathsToTargets = new List<LinkedListNode>();
            foreach(var target in listOfNodes)
            {
                pathsToTargets.Add(GetPathLinkedListToTarget(root, target));
            }

            //Put elements from 1st path to a hashtable
            Dictionary<Node,int> lookupPath = new Dictionary<Node, int>();
            //Add the 1st path to the lookupPath
            var head = pathsToTargets[0];
            while(head != null)
            {
                lookupPath.Add(head.CurrentNode, 1);
                head = head.Next;
            }
            for(int i=1;i< pathsToTargets.Count;i++)
            {
                var listhead = pathsToTargets[i];
                while (listhead != null) //navigate thoru each path and increment the counts
                {
                    if (lookupPath.Keys.Contains(listhead.CurrentNode))
                        lookupPath[listhead.CurrentNode]++;
                    listhead = listhead.Next;
                }               
            }
            //Now find the common Ancestor 
            int noOftargetNodes = listOfNodes.Count;
            var lookupEnumerator = lookupPath.GetEnumerator();          
            Node commonAncestor = null;
            while (lookupEnumerator.MoveNext())
            {
                if (lookupEnumerator.Current.Value == noOftargetNodes)
                    commonAncestor = lookupEnumerator.Current.Key;
            }
            return commonAncestor;
        }
       static LinkedListNode GetPathLinkedListToTarget(Node root, Node target)
        {
            Hashtable path = new Hashtable();
            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(root);
            path.Add(root, null);

            while(queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node == target)
                    break;
                
                if(node.Left != null && !path.Contains(node.Left))
                {
                    path.Add(node.Left, node); //Adding child as key and its parent as vlaue to make look up easy
                    queue.Enqueue(node.Left);
                }
                if (node.Right != null && !path.Contains(node.Right))
                {
                    path.Add(node.Right, node); //Adding child as key and its parent as vlaue to make look up easy
                    queue.Enqueue(node.Right);
                }
            }

            if (!path.Contains(target))
                throw new Exception("Target Node noe exists in the given tree");
            
            //Now Create linked list for the path 
            LinkedListNode head = null;
         
            var end = target;
            while(end != null)
            {
                LinkedListNode listNode = new LinkedListNode();
                listNode.CurrentNode = end;

                if (head == null)
                    head = listNode;
                else
                {
                    listNode.Next = head;
                    head = listNode;
                }
                end = (Node)path[end];
            }
            return head;
        }

        static void PrintRelation(Node n,int v1,int v2)
        {
            Node lcaNode = FindLCA(n, v1, v2);
            if (lcaNode == null)
            {
                Console.WriteLine($"{v1}, {v2} are not related");
            }else if(lcaNode.value == v1)
            {
                if((lcaNode.Left != null && lcaNode.Left.value == v2) || 
                    (lcaNode.Right != null && lcaNode.Right.value == v2))
                {
                    Console.WriteLine($"{v1} is parent of {v2}");
                }
                else
                {
                    int l = GetLevel(lcaNode, v2, 0);
                    StringBuilder str = new StringBuilder();
                    for(int i =0; i< l -2; i++)
                    {
                        str.Append(" Grand");
                    }
                    Console.WriteLine($"{v1} is {str.ToString()} parent of {v2}");
                }
            }else if (lcaNode.value == v2)
            {
                if ((lcaNode.Left != null && lcaNode.Left.value == v1) ||
                    (lcaNode.Right != null && lcaNode.Right.value == v1))
                {
                    Console.WriteLine($"{v1} is child of {v2}");
                }
                else
                {
                    int l = GetLevel(lcaNode, v1, 0);
                    StringBuilder str = new StringBuilder();
                    for (int i = 0; i < l -2 ; i++)
                    {
                        str.Append(" Grand");
                    }
                    Console.WriteLine($"{v1} is {str.ToString()} child of {v2}");
                }
            }
            else if ((lcaNode.Left != null && lcaNode.Right != null) && (lcaNode.Left.value == v1 && lcaNode.Right.value == v2)
                || (lcaNode.Left.value == v2 && lcaNode.Right.value == v1))
            {
                Console.WriteLine($"{v1} is Direct Sibling of {v2}");
            }else
            {
                int v1Level = GetLevel(lcaNode, v1, 0);
                int v2Level = GetLevel(lcaNode, v2, 0);
                int removal = Math.Abs(v1Level - v2Level);
                int degree = Math.Min(v1Level, v2Level);
                Console.WriteLine($"{v1} is cousin of {v2} with degree={degree -2} and removal={removal} ");
            }

        }
        static int GetLevel(Node node, int value, int level)
        {
            if(node == null)
            {
                return 0;
            }
            if(node.value == value)
            {
                return level + 1 ;
            }
            return Math.Max(GetLevel(node.Left, value, level + 1), GetLevel(node.Right, value, level + 1));
        }


        //Max path sum . A path can start at any leafe node and can end at any node. 

        public class MaxPath
        {
            public int Value { get; set; }
        }

        static int MaxPathSum(Node root, MaxPath mp)
        {
            if (root == null)
                return 0;

            int lsum = MaxPathSum(root.Left, mp);
            int rsum = MaxPathSum(root.Right, mp);

            int maxOfChilds = Math.Max(lsum, rsum);

            int maxwithRoot = Math.Max(root.value, root.value + maxOfChilds);

            int sumOfallthree = lsum + rsum + root.value;

            int maxpathSumFortheRoot = Math.Max(maxwithRoot, sumOfallthree);

            //Now compare with what we have in the reference object
            if (mp.Value < maxpathSumFortheRoot)
                mp.Value = maxpathSumFortheRoot;

            //now return the max path so far 
            return maxwithRoot;

        }
    }
}
