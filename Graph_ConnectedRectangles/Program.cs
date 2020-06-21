using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Given set of rectables with (4 points). Find wehther all are connected or not. (Meetup Question by Eguine on 7/16)
//1. Easy : Find given two rectangles are connected 
//2. Medium : Find given N rectangles are connected
//3. Hard: Support points of each rectagle are relatively small  integers, then find. (this is by considering whole area as 1X1 matrix and fill it with true/false and then do similar to "Graphs_ConnectedComponents") 
namespace Graph_ConnectedRectangles
{
    public class Program
    {
        public class Rectangle
        {
            public int p1x;
            public int p1y;

            public int p2x;
            public int p2y;

            public int p3x;
            public int p3y;

            public int p4x;
            public int p4y;

            public Rectangle(int _p1x, int _p1y, int _p2x, int _p2y, int _p3x, int _p3y, int _p4x, int _p4y)
            {
                p1x = _p1x;
                p1y = _p1y;

                p2x = _p2x;
                p2y = _p2y;

                p3x = _p3x;
                p3y = _p3y;

                p4x = _p4x;
                p4y = _p4y;
            }

        }
        public static void Main(string[] args)
        {
            Rectangle r1 = new Rectangle(1, 1, 3, 1, 3, 3, 1, 3);
            Rectangle r2 = new Rectangle(2, 2, 4, 2, 4, 4, 2, 4);

            //Rectangle r3 = new Rectangle(5, 4, 7, 4, 7, 7, 5, 7); //With this , all are not connected
              Rectangle r3 = new Rectangle(4, 4, 7, 4, 7, 7, 4, 7); //With this, all are connected. 

            Rectangle[] rectangles = new Rectangle[] { r1, r2,r3 };


            //var result = AreConnected(r1, r2);
            var result = AreConnected(rectangles);
            Console.WriteLine(result);
            Console.ReadKey();



        }

        public static bool AreConnected(Rectangle[] rectangles)
        {
            HashSet<LinkedList<Rectangle>> connectedSets = new HashSet<LinkedList<Rectangle>>();

            foreach (Rectangle r in rectangles)
                if (!(IsValidRectangle(r)))
                    return false;

            bool foundConnection = false;
            foreach(Rectangle r in rectangles)
            {              

                foreach (LinkedList<Rectangle> set in connectedSets)
                {
                    foreach(Rectangle scannedRect in set)
                    {
                        if(AreConnected(r,scannedRect))
                        {
                            set.AddLast(r);
                            foundConnection = true;
                            break;
                        }
                    }
                }

                LinkedList<Rectangle> newconnectedSet = new LinkedList<Rectangle>();
                if (connectedSets.Count == 0 || !foundConnection)
                    newconnectedSet.AddLast(r);

                if(foundConnection)
                    foreach (LinkedList<Rectangle> set in connectedSets)
                    {
                        if(set.Contains(r))
                        {
                            foreach (Rectangle rect in set)
                                if (!(newconnectedSet.Contains(rect)))
                                    newconnectedSet.AddLast(rect);                            
                        }
                    }
                connectedSets.RemoveWhere(set => set.Contains(r));

                connectedSets.Add(newconnectedSet);
            }

            if (connectedSets.Count == 1)
                return true;

            return false;
        }

        public static bool AreConnected(Rectangle r1, Rectangle r2)
        {
            if (!IsValidRectangle(r1) || !IsValidRectangle(r2))
                return false;
            //Draw diagram to get more clarity
            if (((r1.p1x <= r2.p1x && r2.p1x <= r1.p2x) ||
                 (r1.p1x <= r2.p2x && r2.p2x <= r1.p2x))
                &&
                (
                     (r1.p1y <= r2.p1y && r2.p1y <= r1.p4y) ||
                     (r1.p1y <= r2.p3y && r2.p3y <= r1.p4y)
                ))
                return true;

            return false;
        }

        public static bool IsValidRectangle(Rectangle r)
        {
            //TODO : 
            if (r.p1y == r.p2y && r.p2x == r.p3x && r.p3y == r.p4y && r.p4x == r.p1x)
                return true;
            return false;
        }
    }
}
