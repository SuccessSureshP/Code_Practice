using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Problems
{
    
    //Below program not correct.
    class Program
    {
        static void Main(string[] args)
        {
            //JumoGame();
            //GasStations();           
            Minimum_Arrows_To_shoot_Balloons();
            //MeetingRoomSchedulingProblem();
            // REMELM();
            //RemoveDup2();
            Console.ReadKey();
        }

        //https://leetcode.com/problems/jump-game/
        static void JumoGame()
        {
            int[] a1 = { 2, 3, 1, 1, 4 };
            int[] a2 = { 3, 2, 1, 0, 4 };
            int[] a3 = { 0 };
            int[] a4 = { 6 };
            int[] a5 = { 3, 2, 2, 0, 4 }; // It should return true. By taking 2 steps from 0st position, we can reach 4
            int[] a6 = { 1, 1, 2, 1, 0, 2 };
            
            Console.WriteLine(CanJump(a1).ToString());
            Console.WriteLine(CanJump(a2).ToString());
            Console.WriteLine(CanJump(a3).ToString());
            Console.WriteLine(CanJump(a4).ToString());
            Console.WriteLine(CanJump(a5).ToString());
            Console.WriteLine(CanJump(a6).ToString());

            Console.WriteLine("Actual problem results:");
            Console.WriteLine(CanJumpWith_A_of_i_max_Jumps(a1).ToString());
            Console.WriteLine(CanJumpWith_A_of_i_max_Jumps(a2).ToString());
            Console.WriteLine(CanJumpWith_A_of_i_max_Jumps(a3).ToString());
            Console.WriteLine(CanJumpWith_A_of_i_max_Jumps(a4).ToString());
            Console.WriteLine(CanJumpWith_A_of_i_max_Jumps(a5).ToString());
            Console.WriteLine(CanJumpWith_A_of_i_max_Jumps(a6).ToString());
        }

        //This one is basic solution with the rule that you can jump from ith location to a[i] location. In the actual problem, it represent maximum length of jumps it can take.
        static public bool CanJump(int[] nums)
        {
            int last_index = nums.Length-1;
            int i = 0;
            while(i<=last_index)
            {
                if (i == last_index)
                    return true;
                if (nums[i] == 0)
                    break;
                i += nums[i];
            }
            return false;
        }
        //Idea is to work backwards from the last index. Keep track of the smallest index that can "jump" to the last index. Check whether the current index can jump to this smallest index.
        static public bool CanJumpWith_A_of_i_max_Jumps(int[] nums)
        {
            int last = nums.Length-1;
            for(int i=nums.Length-2;i>=0;i--)
            {
                if (i + nums[i] >= last) // if we can reach last element from ith element.
                    last = i; //now, lets make ith as current last element. 
            }

            if (last <= 0)
                return true;
            else
                return false;
        }

        static void GasStations()
        {
            //Positive case
            int[] gasoline = { 8, 6, 30, 9, 15, 21, 2, 18 };
            int[] stations = { 15, 8, 2, 6, 18, 9, 21, 30 };

            //var r = CanCompleteCircuit(gasoline, stations);
            var r = CanCompleteCircuit_Opitimized(gasoline, stations);
            Console.WriteLine(r);

            //Negative case
            gasoline = new int[] { 8, 5, 30, 9, 15, 21, 2, 18 };
            stations = new int[] { 15, 8, 2, 6, 18, 9, 21, 30 };

            //var r2 = CanCompleteCircuit(gasoline, stations);
            var r2 = CanCompleteCircuit_Opitimized(gasoline, stations);
            Console.WriteLine(r2);
        }

        //o(n^2) SOLUTION
        static public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int len = gas.Length;
            int len2 = cost.Length;

            if (len != len2)
                return -1;
            int start = 0; //lets start by setting start positon as 0
            while(start< len)
            {
                int position = start;
                int remgas = gas[position];
                int dist = cost[position];             
                while (remgas >= dist)
                {
                    //move to next station. update the position
                    position = (position + 1) % len;
                    //update the remaining gas and distance to travel
                    remgas = remgas - dist + gas[position];
                    dist = cost[position];
                    if (position == start)
                        return start;
                }
                //Controls came here means, we are out of gas but we haven't reached where we started. So lets increment the start petition and try
                start++;
            }
            return -1;
        }

        //O(N) solution : https://discuss.leetcode.com/topic/5088/my-ac-is-o-1-space-o-n-running-time-solution-does-anybody-have-posted-this-solution/2
        static public int CanCompleteCircuit_Opitimized(int[] gas,int[] cost)
        {
            int stationsCount = gas.Length;
            int costCount = cost.Length;

            if (stationsCount == 0 || costCount == 0 || stationsCount != costCount)
                return -1;

            int start = stationsCount - 1; //lets make last station as our starting point
            int end = 0; // set end station as 0

            int remgas = gas[start] - cost[start]; // reduce the cost to reach 0th station from last station. 

            while(start > end) //Go as far as possible until start and end collide
            {
                if(remgas >=0) // if still some gas exists
                {
                    remgas += gas[end] - cost[end]; //update remaining gas
                    end++; //move to next station
                }
                else // remaining gas becomes 0, means we can't make a loop. in this situation, lets consider station before current assumed start station as new start station
                {
                    --start; // make start point backward 
                    remgas+= gas[start] - cost[start]; //update the overall remaining gas. Notice, we are not travelling again from start to end. We are updating the remaining gas by considering new start station. this step helps to make complexity to O(n). 
                }
            }
            //Control comes here after start and end collide. Now, we need to check whether we have empty or non-empty or negative volume of gas remaining in the car.
            return remgas >= 0 ? start : -1; //Negative means, no solution exists. Means, we can't make a circle with given gasoline and costs
        }

        //Discussed this balloons in Saturday class 
        static void Minimum_Arrows_To_shoot_Balloons()
        {
            List<Point> balloons = new List<Point>();
            balloons.Add(new Point(1, 4));
            balloons.Add(new Point(3, 6));
            balloons.Add(new Point(5, 7));
            balloons.Add(new Point(7, 8));
            balloons.Add(new Point(9, 10));

            Console.WriteLine("Minimum Arrows needed to shoot all balloons:" + Minimum_Arrows_To_shoot_Balloons(balloons));

            balloons.Clear();
            balloons.Add(new Point(1, 6));
            balloons.Add(new Point(2, 8));
            balloons.Add(new Point(7, 12));
            balloons.Add(new Point(10, 16));


            Console.WriteLine("Minimum Arrows needed to shoot all balloons:" + Minimum_Arrows_To_shoot_Balloons(balloons));


            balloons.Clear();
            balloons.Add(new Point(1, 6));
            balloons.Add(new Point(2, 8));
            balloons.Add(new Point(4, 9));           


            Console.WriteLine("Minimum Arrows needed to shoot all balloons:" + Minimum_Arrows_To_shoot_Balloons(balloons));
        }
        static int Minimum_Arrows_To_shoot_Balloons(List<Point> balloons)
        {
            //Sort balloons based on X1 if they are not sorted. it is O(nlog n). Above one is already sorted
            if (balloons.Count() == 0)
                return 0;

            int left = balloons[0].X1;
            int right = balloons[0].X2;
            int arrowCount = 1;

            for(int i=1;i<balloons.Count();i++)
            {
                if (left <= balloons[i].X1 && balloons[i].X1 <= right)
                {
                    left = balloons[i].X1; //Update left edge
                    right = Math.Min(balloons[i].X2, right); // This is critical one. If ith balloon x1 and x2 is between current left and right, then update right to  ith balloon x2. Check 3rd input to get this
                }
                else //we found a balloon which is not overlapping with before one
                {
                    left = balloons[i].X1;
                    right = balloons[i].X2;
                    arrowCount++;
                }
            }

            return arrowCount;
        }
        class Point
        {
            public int X1;
            public int X2;
            public Point(int x1,int x2)
            {
                X1 = x1;
                X2 = x2;
            }
        }


        //Meeting Room Scheduling Problem (http://blog.gainlo.co/index.php/2016/07/12/meeting-room-scheduling-problem/)
        public static void MeetingRoomSchedulingProblem()
        {
            List<Meeting> meetings = new List<Meeting>();
            meetings.Add(new Meeting(1, 4));
            meetings.Add(new Meeting(3, 6));
            meetings.Add(new Meeting(6, 7));            
            //Sort by StartTime if not already sorted (O(nlogN).
            Console.WriteLine("Is there any conflicts" + MeetingConflictsExists(meetings));

            Console.WriteLine("Minimum rooms needed to schedule all meetings:" + Find_MinimumRoomsNeededToScheduleAllMeetings(meetings));

            meetings.Clear();
            meetings.Add(new Meeting(1, 4));
            meetings.Add(new Meeting(5, 6));
            meetings.Add(new Meeting(6, 7));
            //Sort by StartTime if not already sorted (O(nlogN).
            Console.WriteLine("Is there any conflicts: " + MeetingConflictsExists(meetings));
            //Follow-up
            var numOfRooms =  Find_MinimumRoomsNeededToScheduleAllMeetings(meetings);        
            
            Console.WriteLine("Minimum rooms needed to schedule all meetings:" + numOfRooms);
        }

        

        private static bool MeetingConflictsExists(List<Meeting> meetings)
        {
            for(int i=0;i< meetings.Count()-1;i++)
            {
                var thisMeeting = meetings[i];
                var nextMeeting = meetings[i + 1];
                if (thisMeeting.StartTime < nextMeeting.StartTime && nextMeeting.StartTime < thisMeeting.EndTime) //If next meeting is starting before current meeting ends, means there is a conflict
                    return true;
            }
            return false;
        }

        private static int Find_MinimumRoomsNeededToScheduleAllMeetings(List<Meeting> meetings)
        {
            HashSet<Meeting> finished = new HashSet<Meeting>();
            int roomCount = 0;
            for (int i=0;i<meetings.Count()-1; i++)
            {
                var thisMeeting = meetings[i];
                if (finished.Contains(thisMeeting))
                    continue;
                roomCount++;
                finished.Add(thisMeeting);
                //find al exclusive meeting that we can schedule in this room without any conflicts
                for (int j=i+1;j<meetings.Count()-1;j++)
                {
                    var anotherMeeting = meetings[j];
                    if (finished.Contains(anotherMeeting))
                        break;
                    if (anotherMeeting.StartTime >= thisMeeting.EndTime)
                    {
                        thisMeeting = anotherMeeting;
                        finished.Add(anotherMeeting);
                    }
                }                
            }
            return roomCount;
        }
        class Meeting
        {
            public int StartTime;
            public int EndTime;
            public Meeting(int x1, int x2)
            {
                StartTime = x1;
                EndTime = x2;
            }
        }



        //Remove Element

        //Given an array and a value, remove all the instances of that value in the array.
        //Also return the number of elements left in the array after the operation.
        //It does not matter what is left beyond the expected length.

        //Example:
        //If array A is [4, 1, 1, 2, 1, 3]
        //and value elem is 1,
        //then new length is 3, and A is now[4, 2, 3]
        //Try to do it in less than linear additional space complexity.
        private static void REMELM()
        {
            //int[] a = { 4, 1, 1, 2, 1, 3 };
            //int v = 1;
            int[] a = { 2, 0, 1, 2, 0, 3, 2, 2, 2, 1, 0, 0, 0, 1, 0, 0, 2, 2, 2, 3, 2, 3, 1, 2, 1, 2, 2, 3, 2, 3, 0, 3, 0, 2, 1, 2, 0, 0, 3, 2, 3, 0, 3, 0, 2, 3, 2, 2, 3, 1, 3, 3, 0, 3, 3, 0, 3, 3, 2, 0, 0, 0, 0, 1, 3, 0, 3, 1, 3, 1, 0, 2, 3, 3, 3, 2, 3, 3, 2, 2, 3, 3, 3, 1, 3, 2, 1, 0, 0, 0, 1, 0, 3, 2, 1, 0, 2, 3, 0, 2, 1, 1, 3, 2, 0, 1, 1, 3, 3, 0, 1, 2, 1, 2, 2, 3, 1, 1, 3, 0, 2, 2, 2, 2, 1, 0, 2, 2, 2, 1, 3, 1, 3, 1, 1, 0, 2, 2, 0, 2, 3, 0, 1, 2, 1, 1, 3, 0, 2, 3, 2, 3, 2, 0, 2, 2, 3, 2, 2, 0, 2, 1, 3, 0, 2, 0, 2, 1, 3, 1, 1, 0, 0, 3, 0, 1, 2, 2, 1, 2, 0, 1, 0, 0, 0, 1, 1, 0, 3, 2, 3, 0, 1, 3, 0, 0, 1, 0, 1, 0, 0, 0, 0, 3, 2, 2, 0, 0, 1, 2, 0, 3, 0, 3, 3, 3, 0, 3, 3, 1, 0, 1, 2, 1, 0, 0, 2, 3, 1, 1, 3, 2 };
            int v = 2;
            Console.WriteLine("New length = " + RemoveElement1(a,v));
        }
        private static int RemoveElement(int[] a,int v)
        {
            //two pointers from either side
            if (a.Length == 0)
                return 0;

            int left = 0;
            int right = a.Length - 1;
            while(left<right)
            {
                while (a[left] != v)
                    left++;
                while (a[right] == v)
                    right--;
                if(left<right)
                {
                    int t = a[left];
                    a[left] = a[right];
                    a[right] = t;
                    left++;
                    right--;
                }
            }
            return left;
        }
        private static int RemoveElement1(int[] a, int v)
        {
            //two pointers from same side
            if (a.Length == 0)
                return 0;

            int slow = 0;
            int fast = 0;
            while (fast < a.Length)
            {
                if (a[fast] != v)
                    a[slow++] = a[fast];

                fast++;
            }
            return slow;
        }

//Remove Duplicates from Sorted Array

//Given a sorted array, remove the duplicates in place such that each element can appear atmost twice and return the new length.

//Do not allocate extra space for another array, you must do this in place with constant memory.

//Note that even though we want you to return the new length, make sure to change the original array as well in place

//For example,
//Given input array A = [1, 1, 1, 2],

//Your function should return length = 3, and A is now[1, 1, 2].

        private static void RemoveDup2()
        {
            int[] a = { 1, 1, 1, 2 };
            int newSize = RemoveDup2(a);
            Console.WriteLine($"New Length {newSize}");
        }
       private static int RemoveDup2(int[] a)
        {
            int len = a.Length;
            if (len == 0)
                return 0;

            int slow = 0;
            int fast = 0;
            int numberCount = 0;
            int currentNum = a[slow];
            while(fast < len)
            {
                if(numberCount < 2 || a[fast] != currentNum)
                {
                    if (a[slow] == a[fast])
                        numberCount++;
                    else
                    {
                        numberCount = 0;
                        currentNum = a[fast];
                    }

                    a[slow++] = a[fast];
                }
                fast++;
            }

            return slow; //This is new length of the array
        }


        //Reverse bits of an 32 bit unsigned integer (https://codelab.interviewbit.com/problems/revbits/)
        //Example 2:

        //x = 3,

        //          00000000000000000000000000000011 
        //=>        11000000000000000000000000000000
        //return 3221225472
        public long reverse(long A)
        {
            long rev = 0;

            for (int i = 0; i < 32; i++)
            {
                rev <<= 1;
                if ((A & (1 << i)) != 0)
                    rev |= 1; //XOR 
            }
            return rev;
        }

    }
}
