using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCelebrityProblem
{
    public class Person
    {
        public char Name { get; set; }

        private List<Person> acquaintances = new List<Person>();

        public Person[] AllAcquaintances
        {
            get { return acquaintances.ToArray(); }
        }
        
        public Person(char name)
        {
            Name = name;
        }  

        public void AddAcquaintances(List<Person>  acq)
        {
            acquaintances = acq;
        }

        public bool Knows(Person p)
        {
            return (acquaintances.Contains(p));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person a = new Person('A');
            Person b = new Person('B');
            Person c = new Person('C');
            Person d = new Person('D');
            Person e = new Person('E');
            Person f = new Person('F');

            a.AddAcquaintances(new List<Person> { b, c, d });
            b.AddAcquaintances(new List<Person> { c });
            d.AddAcquaintances(new List<Person> { b, c , f});
            e.AddAcquaintances(new List<Person> { d, c });
            f.AddAcquaintances(new List<Person> { c }); //Try by commenting this line. 


            FindCelebrity(new List<Person> { a, c, b, d, e, f }); //C is Celebrity. Try by commenting f above
            Console.WriteLine($"\n Is path exists from {a.Name} to {e.Name} : " + HasConnecton(a, e));

            Console.WriteLine($"\n Is path exists from {a.Name} to {f.Name} : " + HasConnecton(a, f));
            Console.ReadKey();
        }

        private static void FindCelebrity(List<Person> list)
        {
            //Put all elements into Stack
            Stack<Person> st = new Stack<Person>();

            foreach (Person p in list)
                st.Push(p);


            Person p1 = st.Peek();
            st.Pop();
            Person p2 = st.Peek();
            st.Pop();

            while(st.Count() > 0)
            {
                if(p1.Knows(p2)) // p1 Knows p2, means P1 is not Celebrity. So replace P1 with next top element
                {
                    p1 = st.Peek();
                    st.Pop();
                }else  // This means, P1 don't know p2, which implies that P2 is not celebrity. So replace P2 with next top element
                {
                    p2 = st.Peek();
                    st.Pop();
                }
            }

            Person celeb = null;            

            if (p1.Knows(p2))
                celeb = p2;
            else if (p2.Knows(p1))
                celeb = p1;


            //makes sure the person hold by  celeb variable is real celebrity. 
            foreach(var p in list)
            {
                if (p != celeb && (!p.Knows(celeb) || celeb.Knows(p)))
                {
                    Console.Write("No celebrity exists");
                    return;
                }
            }

            Console.Write("Celebrity: " + celeb.Name);
        }

        //Find if we can reach from p1 to p2 by using p1's acquaintances graph
        private static bool HasConnecton(Person p1, Person p2)
        {
            if (p1 == null || p2 == null)
                return false;
            //It is a DFS. 
            Stack<Person> stack = new Stack<Person>();
            HashSet<Person> scannedPersons = new HashSet<Person>();

            stack.Push(p1);
            while(stack.Count > 0)
            {
                var person = stack.Pop();
                if (person == p2)
                    return true;
                //add acquaintances of person into stack
                foreach(var acq in person.AllAcquaintances)
                {
                    if(!scannedPersons.Contains(acq))
                        stack.Push(acq);
                }
                scannedPersons.Add(person);
            }
            return false;
        }
    }
}
