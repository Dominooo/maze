using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeTest.Code
{
    public class MazeCoordinate
    {
        private int x;
        private int y;
        private int status;

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

        /// <summary>
        /// sets the appropriate status 0-start, 1-path, 2-end, 3-branch
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public MazeCoordinate()
        {

        }

        public MazeCoordinate(int xCoord, int yCoord)
        {
            x = xCoord;
            y = yCoord;
            status = 1;
        }
    }
}