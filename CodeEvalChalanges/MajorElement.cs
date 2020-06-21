using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//https://www.codeeval.com/open_challenges/132/

namespace CodeEvalChalanges
{
    public static class MajorElement 
    {
        //This is one approach that is sing hastable. 
        public static void FindMajorElement(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while(!sr.EndOfStream)
                {
                    string line =  sr.ReadLine();

                    string[] allNumbers = line.Split(new char[] { ',' });
                    Hashtable h = new Hashtable();
                    int l = allNumbers.Count();
                    int moreThanHalf = l / 2;
                    int major = -1; ;
                    for(int i =0;i< allNumbers.Count(); i++)
                    {
                        if(h.Contains(allNumbers[i]))
                        {
                            int val = (int)h[allNumbers[i]];
                            val++;                            
                            if(val > moreThanHalf) //Found Major number
                            {
                                major = int .Parse(allNumbers[i]);
                                break;
                            }
                            h[allNumbers[i]] = val;
                        }
                        else
                        {
                            h[allNumbers[i]] = 1;
                        }
                    }

                    if (major != -1)
                    {
                        Console.WriteLine(major);
                    }
                    else
                        Console.WriteLine("None");
                }
            }
        }

        public static void FindMajorElementWithoutHashtable(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] allNumbers = line.Split(new char[] { ',' });

                    int count = 1;
                    int majorelement = int.Parse(allNumbers[0]);

                    for(int i=1;i<allNumbers.Length;i++)
                    {
                        if (int.Parse(allNumbers[i]) == majorelement)
                            count++;
                        else
                            count--;

                        if (count == 0)
                        {
                            majorelement = int.Parse(allNumbers[i]);
                            count = 1;
                        }
                    }
                    if (isMajor(majorelement,allNumbers))
                        Console.WriteLine(majorelement + "is Major element");
                    else
                        Console.WriteLine("No Major element exsits");
                }
            }
        }

        private static bool isMajor(int majorelement, string[] allNumbers)
        {
            int count = 0;
            for (int i = 0; i < allNumbers.Length; i++)
                if (int.Parse(allNumbers[i]) == majorelement)
                    count++;

            if (count > allNumbers.Length / 2)
                return true;
            else
                return false;
        }
    }
}
