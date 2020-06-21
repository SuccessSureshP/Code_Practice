using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeEvalChalanges
{
    class Program
    {
        public static int Main1(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;
                    // do something with line
                    //********************************************************






                    //********************************************************
                }

            Console.ReadKey();
            return 0;
        }

    }
}
