using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays_Stocks_MaxProfits
{
    public class Class1
    {
        public static void Main()
        {
            int[] stockPrices = { 2, 5, 1, 6, 8, 9, 7, 2 };
            int maxProfit = GetMaxProfit(stockPrices);
            Console.WriteLine($"Max Profit {maxProfit}");

            MaxProfit_WithSellAndBuyIndices(stockPrices);

            int[] stockPrices1 = { 1, 2, 3, 2, 5, 7, 8 };
            N_MaxProfit(stockPrices1);

            Max2Profits(stockPrices); //==>11 (BUY at first 2 , SELL at 5. then Buy at 1 and sell at 9=> 3+8=11)
            Max2Profits(stockPrices1);//==> 8 (BUY at 1 and SELL at 3, then BUY at 2 and SELL at 8=> 2+6=8 profit)
            Console.ReadKey();
        }
        static int GetMaxProfit(int[] a)
        {
            if (a.Length < 2)
                throw new Exception("Insufficient data");

            int min = a[0];
            int profit = int.MinValue;
            for(int i=1;i< a.Length;i++)
            {
                if (a[i] < min)
                    min = a[i];
                int p = a[i] - min;
                if(p > profit)                
                    profit = p;                
            }

            if (profit == int.MinValue)
                profit = 0;
            return profit;
        }

        static void MaxProfit_WithSellAndBuyIndices(int[] a)
        {
            if (a.Length < 2)
                throw new Exception("Insufficient Data");

            int min = a[0];
            int min_index = 0;
            int profit = int.MinValue;
            int buy_Index = 0, sell_Index =0;
            for(int i=1;i< a.Length;i++)
            {
                if(a[i] < min)
                {
                    min = a[i];
                    min_index = i;
                }
                int p = a[i] - min;
                if(p > profit)
                {
                    profit = p;
                    buy_Index = min_index;
                    sell_Index = i;
                }
            }

            if(profit<=0)
            {
                Console.WriteLine("No Profit");
            }else
            {
                Console.WriteLine($"Profit = {profit}");
                Console.WriteLine($"Buying Price = {a[buy_Index]}");
                Console.WriteLine($"Selling Price = {a[sell_Index]}");
            }
        }


        //Do BUY-SELL-BUY-SELL in this order where we should not SELL and BUY on same day
        static void Max2Profits(int[] a)
        {
            //1. store min from front
            int n = a.Length;

            int[] mins_from_Front = new int[n];
            int[] profit_against_min = new int[n];
            int[] profit_if_sell_before_current = new int[n];
            int[] max_from_end = new int[n];
            int[] profits_against_2nd_sell = new int[n];
            int[] final_profits_of_both_transacitons = new int[n];
            int max_profit =0;

            //1. Setting the mins from front of the array
            int min = a[0];
            for (int i = 0; i < n; i++)
                if (a[i] >= min)
                    mins_from_Front[i] = min;
                else
                {
                    min = a[i];
                    mins_from_Front[i] = min;
                }

            //2. setting profits if we sell at each locaiton (by assuming we bought for min price befor that locaiton)
            for (int i = 0; i < n; i++)
                profit_against_min[i] = a[i] - mins_from_Front[i];

            //3. Max Profit if we Buy and also Sell before current element. This can be achieved by keep tracking max so far from left and keep that in this new array
            int max = profit_against_min[0];
            for (int i = 0; i < n; i++)
                if (profit_against_min[i] <= max)
                    profit_if_sell_before_current[i] = max;
                else
                {
                    profit_if_sell_before_current[i] = max; //First put the max so far inthis this locaitron, then only update the max.
                    max = profit_against_min[i];                    
                }

            //4.Setting the maximums from the back of the array
            max = a[n - 1];
            for (int i = n-1;i>=0;i--)
            {
                if (a[i] <= max)
                    max_from_end[i] = max;
                else
                {
                    max = a[i];
                    max_from_end[i] = max;
                }
            }

            //5. Calculate for 2nd sell event with max_from_end values. this is profit if we buy at ith locaiton and sold for max profit
            for (int i = 0; i < n; i++)
                profits_against_2nd_sell[i] = max_from_end[i] - a[i];

            max_profit = 0;
            for(int i=0;i<n;i++)
            {
                final_profits_of_both_transacitons[i] = profit_if_sell_before_current[i] + profits_against_2nd_sell[i];
                if (max_profit <= final_profits_of_both_transacitons[i])
                    max_profit = final_profits_of_both_transacitons[i];
            }

            Console.WriteLine($"MAx profit of 2 full BUY-SELL transactions is:{max_profit}");

        }

        //http://www.geeksforgeeks.org/stock-buy-sell/ 
        //Buy and Sell as many time as possible to get maximum profit
        static void N_MaxProfit(int[] prices)
        {
            int n = prices.Length;

            interval[] sell_buy_Pairs = new interval[(n/2) + 1];
            int c = 0;
            int i = 0;
            while(i<n-1)
            {
                //Find local minima from i. We can go up to max n-2 becuase we are checking with next element
                while (i < n - 1 && prices[i + 1] <= prices[i])
                    i++;

                if (i == n - 1) //We reached end of the array. So, no chance of buying 
                    break;

                sell_buy_Pairs[c].Buy = i;
                i++;

                //Find next local maxima from i. We can go up to max n-1 becuase we are checking with previous element 
                while (i < n && prices[i - 1] <= prices[i])
                    i++;

                sell_buy_Pairs[c].Sell = i - 1;
                c++;
            }
            Console.WriteLine("Here are buy and sell indices");
            int totalProfit = 0;
            for(int k=0;k<c;k++)
            {
                Console.WriteLine($"({k + 1}) BUY at : {prices[sell_buy_Pairs[k].Buy]} and SELL at :{prices[sell_buy_Pairs[k].Sell]} " );
                totalProfit += prices[sell_buy_Pairs[k].Sell] - prices[sell_buy_Pairs[k].Buy];
            }
            Console.WriteLine($"Total Profit:{totalProfit}");
        }

        struct interval
        {
            public int Sell;
            public int Buy;
        }

    }
}
