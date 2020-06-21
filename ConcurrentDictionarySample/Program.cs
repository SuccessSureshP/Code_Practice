using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//MS Interview question
namespace ConcurrentDictionarySample
{

    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;



    public struct Person
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Person(string name, string phoneNumber) : this()
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }

    public class Phonebook
    {

        private readonly object _lock = new object();

        private readonly ConcurrentDictionary<string, Person> _indexByName;

        private readonly ConcurrentDictionary<string, Person> _indexByPhoneNumber;

        public Phonebook(IEnumerable<Person> people)
        {

            _indexByName = new ConcurrentDictionary<string, Person>();
            _indexByPhoneNumber = new ConcurrentDictionary<string, Person>();

            foreach (var p in people)
            {
                _indexByName.AddOrUpdate(p.Name,p, (s, pers) => pers);
                _indexByPhoneNumber.AddOrUpdate(p.PhoneNumber, p, (s, pers) => pers);
            }
        }

        public Person? LookupByName(string name)
        {

            Person pers;
            var res = _indexByName.TryGetValue(name, out pers);
            return res ? (Person?)pers : null;
        }

        public Person? LookupByPhoneNumber(string phoneNumber)
        {

            Person pers;
            var res = _indexByPhoneNumber.TryGetValue(phoneNumber, out pers);
            return res ? (Person?)pers : null;
        }

        public void AddPerson(Person person)
        {

            Monitor.Enter(_lock);

            var res1 = LookupByName(person.Name);
            var res2 = LookupByPhoneNumber(person.PhoneNumber);

            if (res1 != null && res2 != null)
            {
                Monitor.Exit(_lock);
                return;
            }

            Person p;

            if (res1 != null)
            {

                _indexByPhoneNumber.TryRemove(((Person)res1).PhoneNumber, out p);
                _indexByPhoneNumber.TryAdd(person.PhoneNumber, person);
            }

            if (res2 != null)
            {

                _indexByName.TryRemove(((Person)res2).Name, out p);
                _indexByName.TryAdd(person.Name, person);
            }

            _indexByName.AddOrUpdate(person.Name, person, (s, pers) => person);
            _indexByPhoneNumber.AddOrUpdate(person.PhoneNumber, person, (s, pers) => person);

            Monitor.Exit(_lock);
        }
    }


    class Program
    {
        static Phonebook pb;
        static void Main(string[] args)
        {
            List<Person>  persons = new List<Person>();
            persons.Add(new Person("Suresh","425-638-9319"));
            persons.Add(new Person("Guru", "425-638-6646"));
            persons.Add(new Person("Sairam", "425-638-4444"));

            pb = new Phonebook(persons);

            ThreadStart ts = new ThreadStart(Thread1);
            Thread thred = new Thread(ts);
            thred.Start();

            ThreadStart ts2 = new ThreadStart(Thread2);
            Thread thred2 = new Thread(ts2);
            thred2.Start();

            Console.ReadKey();
        }

        public static void Thread1()
        {
            Console.WriteLine("Thread1");
            var result = pb.LookupByName("Suresh");
            if (result != null)
                Console.WriteLine("Person Object : " + ((Person)result).Name + " Phone number : " + ((Person)result).PhoneNumber);
            else
                Console.WriteLine("Person Suresh Not found");

            Console.WriteLine("Thread1:Adding Pavani");
            pb.AddPerson(new Person("Pavani", "435-818-4898"));
            Console.WriteLine("Thread1:Adding Pavani Completed");
        }

        public static void Thread2()
        {
            Console.WriteLine("Thread2");
            var result1 = pb.LookupByName("Guru");
            if (result1 != null)
                Console.WriteLine("Person Object : " + ((Person)result1).Name + " Phone number : " + ((Person)result1).PhoneNumber);
            else
                Console.WriteLine("Person Guru1 Not found");

            Console.WriteLine("Thread2:Finding Pavani");

            var result2 = pb.LookupByName("Pavani");
            if (result2 != null)
                Console.WriteLine("Person Object : " + ((Person)result2).Name + " Phone number : " + ((Person)result2).PhoneNumber);
            else
                Console.WriteLine("Person Pavani Not found");

            Console.WriteLine("Thread2:Finding Pavani Completed");
        }
         
    }




}

