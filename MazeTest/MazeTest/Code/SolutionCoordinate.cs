using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeTest.Code
{
    public class SolutionCoordinate
    {
        private int x;
        private int y;
        //private int directionTraveled;
        private int orientation;
        
        
        public int XCoordinate
        {
            get { return x; }
            set { x = value; }
        }

        public int YCoordinate
        {
            get { return y; }
            set { y = value; }
        }

            //nowhere = 0,
            //up = 1,
            //right = 2,
            //down = 3,
            //left = 4

        public int Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }
        
        //public int DirectionTraveled
        //{
        //    get { return directionTraveled; }
        //    set { directionTraveled = value; }
        //}

        public SolutionCoordinate()
        {

        }
        

        public SolutionCoordinate(int xCoord, int yCoord, int currentOrientation/*, int dirTraveled*/)
        {
            x = xCoord;
            y = yCoord;
            orientation = currentOrientation;
            //directionTraveled = dirTraveled;
        }

        
    }
}