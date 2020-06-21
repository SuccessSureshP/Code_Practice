using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityArraySorting
{
    /// <summary>
    /// December OD/SP OTS 
    //Products are identified by alphanumeric codes. Each code is stored as a string. We have three types of 
    //products:high priority, medium priority, and low priority. Given an array of product codes, sort the array so that 
    //the highest priority products come at the beginning of the array, the medium priority products come in the 
    //middle, and the low priority customers come at the end. Within a priority group, order does not matter. You are 
    //given a priority function which, given a product code, returns 1 for high, 2 for medium and 3 for low. This array 
    //may contain a large number of product codes, so do your best to minimize additional storage. 
    //You are given this fuction for usage: private int GetPriority(string productCode).You dont need to implement this 
    //function. 
    //Implement: public void OrderProductsByPriority(string[] productCodes) 
    //What is the runtime complexity of your solution? 

    /// </summary>
    class Program
    {
        static string[] SortByPriority(string[] a)
        {
            int st = 0;
            int mid = 0;
            int end = a.Length - 1;

            while (mid <= end)
            {
                var priority = GetPriority(a[mid]);
                if (priority == 2)
                    mid++;
                if (priority == 1)
                {
                    string s = a[st];
                    a[st] = a[mid];
                    a[mid] = s;
                    st++;
                }
                if (priority == 3)
                {
                    string s = a[end];
                    a[end] = a[mid];
                    a[mid] = s;
                    end--;


                }

            }
            return a;
        }


        static void Main(string[] args)
        {
            //string[] products = {"basdf", "casdf", "aasdf", "bsdsafd", "bbbbb", "cccccsdf", "aserw"};
            string[] products = { "basdf", "casdf", "aasdf" };
            foreach (var product in products)
                Console.Write(product + "   ");

            products = SortByPriority(products);
            Console.WriteLine();
            Console.WriteLine("After Sorting");
            foreach (var product in products)
                Console.Write(product + "   ");

            Console.ReadKey();
        }

        static int GetPriority(string productCode)
        {
            char c = productCode[0];
            switch (c)
            {
                case 'a': return 1;
                case 'b': return 2;
                case 'c': return 3;
            }
            return 2;
        }
    }
}
