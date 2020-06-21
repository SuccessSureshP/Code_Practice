using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class Node
    {
        public char value;
        public Dictionary<char,Node> childs;
        public bool IsTerminating;

        public Node(char v, bool isTerminating)
        {
            value = v;
            childs = new Dictionary<char, Node>();
            IsTerminating = isTerminating;
        }
     }

    public class Trie
    {
        public Node root = new Node(' ', false);
        public bool Search(string str)
        {
            return Search(str, root, 0);
        }

        private bool Search(string str,Node node,int index)
        {
            if (index == str.Length)
            {
                if (node.IsTerminating == true)
                    return true;
            }
            else
            {
                if (node.childs.Keys.Contains(str[index]))
                    return Search(str, node.childs[str[index]], index + 1);

            }
            return false;
        }

        private void Add(string str,Node n, int index)
        {
            if(n.childs.Keys.Contains(str[index]))
            {
                if (str.Length - 1 == index)
                {
                    n.childs[str[index]].IsTerminating = true;
                }
                else
                {
                    Add(str, n.childs[str[index]], index + 1);
                }
            }
            else
            {
                if(str.Length -1 == index)
                {
                    n.childs.Add(str[index], new Node(str[index], true));
                }
                else
                {
                    n.childs.Add(str[index], new Node(str[index], false));
                    Add(str, n.childs[str[index]], index + 1);
                }
            }
        }

        public void Add(string str)
        {
            Add(str, root, 0);
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {

            Trie t = new Trie();

            t.Add("apple");
            t.Add("anar");

            t.Add("Mango");

            Console.WriteLine("is 'anar' there? " + t.Search("anar"));
            Console.WriteLine("is 'App' there? " + t.Search("App"));
            Console.ReadKey();
        }
    }
}
