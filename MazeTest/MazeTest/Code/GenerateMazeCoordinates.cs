using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeTest.Code
{
    public class GenerateMazeCoordinates
    {
        bool BranchEndReached = false;
        MazeCoordinate BranchStartLocation = new MazeCoordinate();
        private MazeCoordinate CurrentLocation = new MazeCoordinate();
        private int CurrentDirection;
        private bool endReached = false;
        private List<MazeCoordinate> MazeCoordinates = new List<MazeCoordinate>();
        private int MazeSize;
        private int MaxSpaces;
        private int MinDistance;
        private Direction NewDirection = new Direction();
        private int NewDistance;
        private Direction Direction1;
        private Direction Direction2;

        private enum Direction
        {
            nowhere = 0,
            up = 1,
            right = 2,
            down = 3,
            left = 4
        }

        public List<MazeCoordinate> GetMazeCoordinates(int mazeSize)
        {
            MazeSize = mazeSize;
            switch (MazeSize)
            {
                case 20:
                    MaxSpaces = 3;
                    MinDistance = 2;
                    break;
                case 30:
                    MaxSpaces = 4;
                    MinDistance = 2;
                    break;
                case 40:
                    MaxSpaces = 4;
                    MinDistance = 3;
                    break;

            }

            GetStartPosition();
            GenerateNextSteps();
            for (int i = 0; i < 3; i++)
            {
                AddRandomBranches();
            }
            return MazeCoordinates;
        }

        private void AddNextBranchSpaces(Direction newDirection, int distance)
        {
            for (int i = 0; i < distance; i++)
            {
                MazeCoordinate nextStep = GetNextBranchCoordinate(CurrentLocation, (int)newDirection);
                nextStep.Status = 3;
                MazeCoordinates.Add(nextStep);
                CurrentLocation = nextStep;
                if (BranchEndReached)
                {
                    break;
                }
            }
        }

        private void AddNextSpaces(Direction newDirection, int distance)
        {
            for (int i = 0; i < distance; i++)
            {
                MazeCoordinate nextStep = GetNextCoordinate(true, CurrentLocation, (int)newDirection);
                nextStep.Status = 1;
                MazeCoordinates.Add(nextStep);
                CurrentLocation = nextStep;
                if (endReached)
                {
                    MazeCoordinates[MazeCoordinates.Count - 1].Status = 2;
                    break;
                }
            }
        }

        private void AddRandomBranches()
        {
            BranchEndReached = false;
            bool validBranchStart = false;
            while (validBranchStart == false)
            {
                validBranchStart = GetValidBranchStartPoint();
            }

            GenerateNextBranchSteps();

        }

        private void GenerateNextBranchSteps()
        {
            bool validDirection = false;
            while (validDirection == false)
            {
                validDirection = GetRandomDirection(true);
            }
            AddNextBranchSpaces(NewDirection, NewDistance);
            CurrentDirection = (int)NewDirection;
            if (BranchEndReached == false)
            {
                GenerateNextBranchSteps();
            }
        }

        private void GenerateNextSteps()
        {
            bool validDirection = false;
            while (validDirection == false)
            {
                validDirection = GetRandomDirection();
            }
            //GetRandomDirection();
            AddNextSpaces(NewDirection, NewDistance);
            CurrentDirection = (int)NewDirection;
            if (endReached == false)
            {
                GenerateNextSteps();
            }
        }

        private MazeCoordinate GetNextBranchCoordinate(MazeCoordinate location, int newDirection, int moveDistance = 1)
        {
            bool validMove = true;
            int x = location.XCoordinate;
            int y = location.YCoordinate;
            switch (newDirection)
            {
                case 1:
                    y -= moveDistance;
                    if (y <= 0)
                    {
                        validMove = false;
                    }
                    else
                    {
                        if (moveDistance == 1)
                        {
                            if (MazeCoordinates.Exists(m => (m.XCoordinate == x) && (m.YCoordinate == y) && (m.Status == 1)))
                            {
                                BranchEndReached = true;
                            }
                        }

                    }
                    break;
                case 2:
                    x += moveDistance;
                    if (x >= MazeSize - 1)
                    {
                        validMove = false;
                    }
                    else
                    {
                        if (moveDistance == 1)
                        {
                            if (MazeCoordinates.Exists(m => (m.XCoordinate == x) && (m.YCoordinate == y) && (m.Status == 1)))
                            {
                                BranchEndReached = true;
                            }
                        }
                    }
                    break;
                case 3:
                    y += moveDistance;
                    if (y >= MazeSize - 1)
                    {
                        validMove = false;
                    }
                    else
                    {
                        if (moveDistance == 1)
                        {
                            if (MazeCoordinates.Exists(m => (m.XCoordinate == x) && (m.YCoordinate == y) && (m.Status == 1)))
                            {
                                BranchEndReached = true;
                            }
                        }
                    }

                    break;
                case 4:
                    x -= moveDistance;
                    if (x <= 0)
                    {
                        validMove = false;
                    }
                    else
                    {
                        if (moveDistance == 1)
                        {
                            if (MazeCoordinates.Exists(m => (m.XCoordinate == x) && (m.YCoordinate == y) && (m.Status == 1)))
                            {
                                BranchEndReached = true;
                            }
                        }
                    }

                    break;


            }

            if (!validMove)
            {
                return null;
            }
            else
            {
                MazeCoordinate nextMove = new MazeCoordinate(x, y);
                return nextMove;
            }
        }

        private MazeCoordinate GetNextCoordinate(bool validate, MazeCoordinate location, int newDirection, int distance = 1)
        {
            bool validMove = true;
            int x = location.XCoordinate;
            int y = location.YCoordinate;
            switch (newDirection)
            {
                case 1:
                    y -= distance;
                    if (validate)
                    {
                        if (y <= 0)
                        {
                            if (MazeCoordinates.Count < MazeSize * 7)
                            {
                                validMove = false;
                            }
                            else
                            {
                                if (distance == 1)
                                {
                                    endReached = true;
                                }
                            }
                        }
                    }

                    break;
                case 2:
                    x += distance;
                    if (validate)
                    {
                        if (x >= MazeSize - 1)
                        {
                            if (MazeCoordinates.Count < MazeSize * 7)
                            {
                                validMove = false;
                            }
                            else
                            {
                                if (distance == 1)
                                {
                                    endReached = true;
                                }
                            }
                        }
                    }

                    break;
                case 3:
                    y += distance;
                    if (validate)
                    {
                        if (y >= MazeSize - 1)
                        {
                            if (MazeCoordinates.Count < MazeSize * 7)
                            {
                                validMove = false;
                            }
                            else
                            {
                                if (distance == 1)
                                {
                                    endReached = true;
                                }
                            }
                        }
                    }

                    break;
                case 4:
                    x -= distance;
                    if (validate)
                    {
                        if (x <= 0)
                        {
                            if (MazeCoordinates.Count < MazeSize * 7)
                            {
                                validMove = false;
                            }
                            else
                            {
                                if (distance == 1)
                                {
                                    endReached = true;
                                }
                            }
                        }
                    }

                    break;


            }

            if (!validMove)
            {
                return null;
            }
            else
            {
                MazeCoordinate nextMove = new MazeCoordinate(x, y);
                return nextMove;
            }


        }



        private bool GetRandomDirection(bool isBranch = false)
        {
            int oppositeDirection = 0;
            switch (CurrentDirection)
            {
                case 1:
                    oppositeDirection = 3;
                    break;
                case 2:
                    oppositeDirection = 4;
                    break;
                case 3:
                    oppositeDirection = 1;
                    break;
                case 4:
                    oppositeDirection = 2;
                    break;
            }
            int distance = GetRandomDistance();
            Random r = new Random();
            int possibleDirection = r.Next(1, 5);
            bool consecutiveTurns = false;
            int check = MazeCoordinates.Count();
            if (Direction1 == Direction2 && (Direction)possibleDirection == Direction1)
            {
                consecutiveTurns = true;
            }
            if (isBranch)
            {
                if (possibleDirection != oppositeDirection && GetNextBranchCoordinate(CurrentLocation, possibleDirection, distance) != null && consecutiveTurns == false)
                {

                    if (Direction1 != Direction.nowhere)
                    {
                        Direction2 = Direction1;
                    }
                    Direction1 = (Direction)CurrentDirection;
                    NewDirection = (Direction)possibleDirection;
                    NewDistance = distance;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (possibleDirection != oppositeDirection && GetNextCoordinate(true, CurrentLocation, possibleDirection, distance) != null && consecutiveTurns == false)
                {

                    if (Direction1 != Direction.nowhere)
                    {
                        Direction2 = Direction1;
                    }
                    Direction1 = (Direction)CurrentDirection;
                    NewDirection = (Direction)possibleDirection;
                    NewDistance = distance;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        private int GetRandomDistance()
        {
            Random r = new Random();
            return r.Next(MinDistance, MaxSpaces);
        }

        private void GetStartPosition()
        {
            int x;
            int y;

            Random r = new Random();
            //determine what side the start of the maze will be on
            int xOrY = r.Next(0, 2);
            int minOrMax = r.Next(0, 2);

            int startingLocation = r.Next(1, MazeSize - 2);

            //set the initial direction based on starting side 0 = x, 1 = y
            if (xOrY == 0)
            {
                if (minOrMax == 0)
                {
                    x = 0;
                    CurrentDirection = (int)Direction.right;
                }
                else
                {
                    x = MazeSize - 1;
                    CurrentDirection = (int)Direction.left;
                }
                y = startingLocation;
            }
            else
            {
                if (minOrMax == 0)
                {
                    y = 0;
                    CurrentDirection = (int)Direction.down;
                }
                else
                {
                    y = MazeSize - 1;
                    CurrentDirection = (int)Direction.up;
                }
                x = startingLocation;
            }

            MazeCoordinate startingPoint = new MazeCoordinate();
            startingPoint.XCoordinate = x;
            startingPoint.YCoordinate = y;
            startingPoint.Status = 0;

            MazeCoordinates.Add(startingPoint);

            CurrentLocation = startingPoint;
            int distance = GetRandomDistance();
            for (int i = 0; i < distance; i++)
            {
                MazeCoordinate nextStep = new MazeCoordinate();
                nextStep = GetNextCoordinate(false, CurrentLocation, CurrentDirection, 1);
                nextStep.Status = 1;
                MazeCoordinates.Add(nextStep);
                CurrentLocation = nextStep;
            }
        }

        private bool GetValidBranchStartPoint()
        {
            Random r = new Random();
            int endPointX = r.Next(2, MazeSize - 2);
            int endPointY = r.Next(2, MazeSize - 2);
            CurrentDirection = 0;
            if (MazeCoordinates.Exists(m => (m.XCoordinate == endPointX) && (m.YCoordinate == endPointY)))
            {
                return false;
            }
            else
            {
                BranchStartLocation = new MazeCoordinate(endPointX, endPointY);
                BranchStartLocation.Status = 3;
                CurrentLocation = new MazeCoordinate(endPointX, endPointY);
                return true;
            }
        }
    }
}