using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://www.youtube.com/watch?v=Cv0ft2dFz80&t=610s
//With Priority Queue: https://www.youtube.com/watch?v=GSBLe8cKu0s
//http://www.geeksforgeeks.org/divide-and-conquer-set-7-the-skyline-problem/
namespace Skyline_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            //new int[8,3]; //8 buildins total 
            int[,] buildings = {

                //{x1,x2,heigh}
                { 1,5,11},
                { 2,7,6},
                { 3,9,13},
                { 12,16,7},
                { 14,25,3},
                { 19,22,18},
                { 23,29,13},
                { 24,28,4}
            };
            var skyline = GetSkyline_Rec(0, 7, buildings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low">low is the lower boundary of the list of bildings</param>
        /// <param name="high">high is the higher boundary of the list of buildings</param>
        /// <param name="buildings">All list of buildings</param>
        /// <returns></returns>
        public static  List<int[]> GetSkyline_Rec(int low, int high, int[,] buildings)
        {
            List<int[]> skyLineList = new List<int[]>();

            if (low > high)
                return skyLineList;
            else if (low == high) // means only one building exists
            {
                int x1 = buildings[low,0];//starng  point
                int x2 = buildings[low,1]; //ending point
                int h = buildings[low,2];//Height

                int[] element1 = { x1, h };
                int[] element2 = { x2, 0 };
                skyLineList.Add(element1);
                skyLineList.Add(element2);
                return skyLineList;
            }
            else
            {
                int mid = (low + high) / 2;  // mid building index 
                List<int[]> Skylinelist_lower = GetSkyline_Rec(low, mid, buildings);
                List<int[]> Skylinelist_higher = GetSkyline_Rec(mid + 1, high, buildings);

                return MergeSkylines(Skylinelist_lower, Skylinelist_higher);
            }

        }

        /// <summary>
        /// This will merge two skylines into onle skyline
        /// </summary>
        /// <param name="skylinelist_lower"></param>
        /// <param name="skylinelist_higher"></param>
        /// <returns></returns>
        private static List<int[]> MergeSkylines(List<int[]> skylinelist_lower, List<int[]> skylinelist_higher)
        {
            int h1 = 0;//last seen height of the skyline 1/skylinelist_lower 
            int h2 = 0;//last seen height of the skyline 2/skylinelist_heigher             

            List<int[]> skyline_merged = new List<int[]>();
            while (true)
            {
                if (skylinelist_lower.Count == 0 || skylinelist_higher.Count == 0)
                    break;
                var strip1 = skylinelist_lower.First();
                var strip2 = skylinelist_higher.First();
                int[] mergedStrip = new int[2]; //0: x co-ordinate and 1: Height
                if (strip1[0] < strip2[0]) //Comparing X-cordinates of two dots of two skylines. Strip 1 dot came before Strip 2 dot
                {
                    mergedStrip[0] = strip1[0];
                    mergedStrip[1] = strip1[1];
                    //if 'Y' of choosen point is less than the last seen height of the other skyline 
                    if (strip1[1] < h2)
                    {
                        mergedStrip[1] = h2; //Updating the height of the resultent strip 
                    }
                    h1 = strip1[1]; // storing the height of current seen building in Skyline 1
                    skylinelist_lower.RemoveAt(0); // Removing the 1st strip since it has been processed
                }
                else if (strip2[0] < strip1[0]) //Strip 2 dot came before Strip 1 dot
                {
                    mergedStrip[0] = strip2[0];
                    mergedStrip[1] = strip2[1];
                    //if  Y of choosen point is less than the last seen height of othe other skyline
                    if (strip2[1] < h1)
                    {
                        mergedStrip[1] = h1;
                    }

                    h2 = strip2[1];
                    skylinelist_higher.RemoveAt(0);
                }
                else // meeans both dots are on same  X-axis
                {
                    mergedStrip[0] = strip1[0];
                    mergedStrip[1] = Math.Max(strip1[1], strip2[1]); // get the max height of the both the points;
                    h1 = strip1[1];
                    h2 = strip2[1];
                    skylinelist_lower.RemoveAt(0);
                    skylinelist_higher.RemoveAt(0);
                }

                //Add the merged strip point to the final merged Skyline
                skyline_merged.Add(mergedStrip);
            }
            while (skylinelist_lower.Count > 0) //Add if any extra pint of Skyline 1 remain exists
            {
                skyline_merged.Add(skylinelist_lower.First());
                skylinelist_lower.RemoveAt(0);
            }
            while (skylinelist_higher.Count > 0) //Add if any extra pint of Skyline 2 remain exists
            {
                skyline_merged.Add(skylinelist_higher.First());
                skylinelist_higher.RemoveAt(0);
            }
            //Now we have to remove redundant strip points
            int current = 0;
            while (current < skyline_merged.Count)
            {
                bool dupFound = true;
                int i = current + 1;
                while (i < skyline_merged.Count && dupFound)
                {
                    if (skyline_merged[i][1] == skyline_merged[current][1]) // if the height of the point at i has same height as of current dot. we can remove this ith dot
                    {
                        dupFound = true;
                        skyline_merged.RemoveAt(i); // After removeal of ith element, next elemnt will becomes ith element. So, above conditon can work again after the loop
                    }
                    else
                        dupFound = false;
                }
                //we are done deleting of ducpliate point which has same eight as current dot's height. Lets examinie the next one. 
                current++;
            }
            return skyline_merged;
        }
    }
}
