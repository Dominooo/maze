using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeTest.Code
{
    public class GenerateMazeSolution
    {
        private enum Direction
        {
            nowhere = 0,
            up = 1,
            right = 2,
            down = 3,
            left = 4
        }
        
        private SolutionCoordinate CurrentCoordinate = new SolutionCoordinate();
        private Direction CurrentOrientation = Direction.nowhere;
        private bool EndReached = false;
        private List<MazeCoordinate> Maze = new List<MazeCoordinate>();
        private List<SolutionCoordinate> MazeSolution = new List<SolutionCoordinate>();

        private int Up;
        private int Right;
        private int Down;
        private int Left;
        
        

        private void GetOrientedDirections()
        {
            switch (CurrentOrientation)
            {
                case Direction.up:
                    Up = (int)Direction.up;
                    Right = (int)Direction.right;
                    Down = (int)Direction.down;
                    Left = (int)Direction.left;
                    break;
                case Direction.right:
                    Up = (int)Direction.right;
                    Right = (int)Direction.down;
                    Down = (int)Direction.left;
                    Left = (int)Direction.up;
                    break;
                case Direction.down:
                    Up = (int)Direction.down;
                    Right = (int)Direction.left;
                    Down = (int)Direction.up;
                    Left = (int)Direction.right;
                    break;
                case Direction.left:
                    Up = (int)Direction.left;
                    Right = (int)Direction.up;
                    Down = (int)Direction.right;
                    Left = (int)Direction.down;
                    break;
            }
        }

        public List<SolutionCoordinate> GetSolution(List<MazeCoordinate> maze, MazeCoordinate startCoordinate, int mazeSize, bool useEasyMode = true)
        {
            Maze = maze;
            Direction startDirection = 0;
            if (startCoordinate.XCoordinate == 0)
            {
                startDirection = Direction.right;
                CurrentOrientation = Direction.right;
            }
            else if (startCoordinate.XCoordinate == mazeSize -1) 
            {
                startDirection = Direction.left;
                CurrentOrientation = Direction.left;
            }
            else if (startCoordinate.YCoordinate == 0)
            {
                startDirection = Direction.down;
                CurrentOrientation = Direction.down;
            }
            else
            {
                startDirection = Direction.up;
                CurrentOrientation = Direction.up;
            }

            CurrentCoordinate.XCoordinate = startCoordinate.XCoordinate;
            CurrentCoordinate.YCoordinate = startCoordinate.YCoordinate;
            CurrentCoordinate.Orientation = (int)CurrentOrientation;
            //CurrentCoordinate.DirectionTraveled = (int)startDirection;

            MazeSolution.Add(new SolutionCoordinate(startCoordinate.XCoordinate, startCoordinate.YCoordinate, (int)CurrentOrientation/*, (int)startDirection*/));

            if (useEasyMode)
            {
                //Solve puzzle just trying to go right
                bool mazeSolved = false;
                while (mazeSolved == false)
                {
                    mazeSolved = SolvePuzzleEasyMode();
                }
                
            }
            else
            {
                //Solve puzzle more inteligently
                SolvePuzzleHardMode();
            }

            return MazeSolution;
        }

        private bool CheckForwardSpace()
        {

            GetOrientedDirections();
            int x = CurrentCoordinate.XCoordinate;
            int y = CurrentCoordinate.YCoordinate;
            switch (Up)
            {
                case 1:
                    y -= 1;
                    break;
                case 2:
                    x += 1;
                    break;
                case 3:
                    y += 1;
                    break;
                case 4:
                    x -= 1;
                    break;
            }
            if (Maze.Exists(m => (m.XCoordinate == x) && (m.YCoordinate == y)))
            {
                MazeCoordinate forwardSpace = Maze.Find(m => (m.XCoordinate == x) && (m.YCoordinate == y));
                if (forwardSpace.Status == 2)
                {
                    EndReached = true;
                }
                CurrentOrientation = (Direction)Up;
                CurrentCoordinate = new SolutionCoordinate(forwardSpace.XCoordinate, forwardSpace.YCoordinate, Up);
                MazeSolution.Add(new SolutionCoordinate(forwardSpace.XCoordinate, forwardSpace.YCoordinate, Up));
                return true;
            }

            return false;
        }

        private bool CheckLeftSpace()
        {

            GetOrientedDirections();
            int x = CurrentCoordinate.XCoordinate;
            int y = CurrentCoordinate.YCoordinate;
            switch (Left)
            {
                case 1:
                    y -= 1;
                    break;
                case 2:
                    x += 1;
                    break;
                case 3:
                    y += 1;
                    break;
                case 4:
                    x -= 1;
                    break;
            }
            if (Maze.Exists(m => (m.XCoordinate == x) && (m.YCoordinate == y)))
            {
                MazeCoordinate leftSpace = Maze.Find(m => (m.XCoordinate == x) && (m.YCoordinate == y));
                if (leftSpace.Status == 2)
                {
                    EndReached = true;
                }
                CurrentOrientation = (Direction)Left;
                CurrentCoordinate = new SolutionCoordinate(leftSpace.XCoordinate, leftSpace.YCoordinate, Left);
                MazeSolution.Add(new SolutionCoordinate(leftSpace.XCoordinate, leftSpace.YCoordinate, Left));
                return true;
            }

            return false;
        }

        private bool CheckRightSpace()
        {
            GetOrientedDirections();
            int x = CurrentCoordinate.XCoordinate;
            int y = CurrentCoordinate.YCoordinate;
            switch (Right)
            {
                case 1:
                    y -= 1;
                    break;
                case 2:
                    x += 1;
                    break;
                case 3:
                    y += 1;
                    break;
                case 4:
                    x -= 1;
                    break;
            }
            if (Maze.Exists(m => (m.XCoordinate == x) && (m.YCoordinate == y)))
            {
                MazeCoordinate rightSpace = Maze.Find(m => (m.XCoordinate == x) && (m.YCoordinate == y));
                if (rightSpace.Status == 2)
                {
                    EndReached = true;
                }
                CurrentOrientation = (Direction)Right;
                CurrentCoordinate = new SolutionCoordinate(rightSpace.XCoordinate, rightSpace.YCoordinate, Right);
                MazeSolution.Add(new SolutionCoordinate(rightSpace.XCoordinate, rightSpace.YCoordinate, Right));
                return true;
            }

            return false;
        }

        private void GoBackwards()
        {
            //dead end has been reached, turn around and start again.
            GetOrientedDirections();
            
            int x = CurrentCoordinate.XCoordinate;
            int y = CurrentCoordinate.YCoordinate;
            switch (Down)
            {
                case 1:
                    y -= 1;
                    break;
                case 2:
                    x += 1;
                    break;
                case 3:
                    y += 1;
                    break;
                case 4:
                    x -= 1;
                    break;
            }

            CurrentOrientation = (Direction)Down;
            CurrentCoordinate = new SolutionCoordinate(x, y, Down);
            MazeSolution.Add(new SolutionCoordinate(x, y, Down));
            
        }

        private bool SolvePuzzleEasyMode()
        {
            //check if there is a move to the right, if not go forward
            //if can't go right or forward go left
            //follow until the end is reached
            //track total number of moves

            if (CheckRightSpace())
            {
                if (EndReached)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (CheckForwardSpace())
            {
                if (EndReached)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(CheckLeftSpace())
            {
                if (EndReached)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {

                GoBackwards();
                return false;
            }
        }

        private void SolvePuzzleHardMode()
        {

        }
    }
}