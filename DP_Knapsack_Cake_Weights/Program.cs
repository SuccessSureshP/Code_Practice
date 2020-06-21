using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_Knapsack_Cake_Weights
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> cakes = new Dictionary<int, int>();
            cakes.Add(2, 15);
            cakes.Add(4, 8);
            cakes.Add(5, 10);
            cakes.Add(3, 20);
            cakes.Add(6, 6); //Make this cake value to 66 and check value for 7 KG Bag. You will see only 6 KG Cake in the bag

            GetCakesWithMaxValue(cakes, 7);
            
            GetCakesWithMaxValue(cakes, 6);

            GetCakesWithMaxValue(cakes, 9);

            GetCakesWithMaxValue(cakes, 8);

            GetCakesWithMaxValue(cakes, 17);

            GetCakesWithMaxValue(cakes, 20);
            GetCakesWithMaxValue(cakes, 23);
            Console.ReadKey();
        }

        static void GetCakesWithMaxValue(Dictionary<int,int> cakes,int weight)
        {
            Dictionary<int, int> maxweights = new Dictionary<int, int>();
            Dictionary<int, int> cakeIndexes = new Dictionary<int, int>();
            
            for(int i=0;i<=weight;i++)
            {
                maxweights[i] = 0;
                int maxVlaue = 0;
                cakeIndexes[i] = 0;
                if (cakes.Keys.Contains(i))
                {
                    maxVlaue = cakes[i];
                    cakeIndexes[i] = 0;
                }
                for(int j = i;j>= i/2; j--)                
                {
                    int max1 = Math.Max(maxVlaue, maxweights[j] + maxweights[i - j]);
                    if(max1 > maxVlaue)
                    {
                        cakeIndexes[i] = j;
                        maxVlaue = max1;
                    }
                }

                maxweights[i] = maxVlaue;
               
            }
            Console.WriteLine($"Total Value for Bag of weight:{weight} is:");
            Console.WriteLine(maxweights[weight]);

            int backTrackIndex = weight;

            Console.WriteLine("Cakes :");
            //Back Tracking
            while(backTrackIndex > 0)
            {                
                int k = backTrackIndex - cakeIndexes[backTrackIndex];
                if(cakes.Keys.Contains(k))
                    Console.WriteLine(k);
                backTrackIndex = cakeIndexes[backTrackIndex];
            }

        }
    }
}

