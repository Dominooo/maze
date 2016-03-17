using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MazeTest.Code
{
    public class GenerateMazeCoordinates
    {
        MazeCoordinate CurrentLocation = new MazeCoordinate();
        int CurrentDirection;
        bool endReached = false;
        List<MazeCoordinate> MazeCoordinates = new List<MazeCoordinate>();
        int MazeSize;
        int MaxSpaces;
        int MinDistance;
        Direction NewDirection = new Direction();
        int NewDistance;
        Direction direction1;
        Direction direction2;

        private enum Direction
        {
            nowhere = 0,
            up = 1,
            right = 2,
            down = 3,
            left = 4
        }

        public List<MazeCoordinate> GetMazeCoordinates()
        {
            GetStartPosition();
            GenerateNextSteps();
            return MazeCoordinates;
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

        private bool GetRandomDirection()
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
            if (direction1 == direction2 && (Direction)possibleDirection == direction1)
            {
                consecutiveTurns = true;
            }
            if (possibleDirection != oppositeDirection && GetNextCoordinate(true, CurrentLocation, possibleDirection, distance) != null && consecutiveTurns == false)
            {

                if (direction1 != Direction.nowhere)
                {
                    direction2 = direction1;
                }
                direction1 = (Direction)CurrentDirection;
                NewDirection = (Direction)possibleDirection;
                NewDistance = distance;
                return true;
            }
            else
            {
                return false;
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
    }
}