using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPathInLabyrinth.Core
{
    public class SimplePathFindingStrategy : ILabyrinthPathFindingStrategy
    {
        RoutePoint FindStartPoint(ILabyrinthMatrix tempMatrix)
        {
            int startX = 0, startY = 0;

            int maxX = tempMatrix.SizeX;
            int maxY = tempMatrix.SizeY;

            for (startX = 0; startX < maxX; startX++)
                for (startY = 0; startY < maxY; startY++)
                    if (tempMatrix[startX, startY] == LabyrinthMatrix.Start)
                        return new RoutePoint(startX, startY, null);

            return null;
        }

        public RoutePoint GetDestinationPoint(ILabyrinthMatrix matrix, IPathStrategyVisitor visitor)
        {
            int maxX = matrix.SizeX;
            int maxY = matrix.SizeY;


            RoutePoint[] allowedMoves = { new RoutePoint(0, 1, null), new RoutePoint(0, -1, null), new RoutePoint(1, 0, null), new RoutePoint(-1, 0, null), };

            RoutePoint start = FindStartPoint(matrix);
            if (start == null)
                return null;

            Stack<RoutePoint> stack = new Stack<RoutePoint>();
            stack.Push(start);

            RoutePoint finishPoint = null;
            int generation = 0;

            do
            {
                Stack<RoutePoint> stackNext = new Stack<RoutePoint>();
                while (stack.Count > 0 && finishPoint == null)
                {
                    RoutePoint curr = stack.Pop();

                    for (int i = 0; i < allowedMoves.Length; i++)
                    {
                        int checkX = curr.X + allowedMoves[i].X;
                        int checkY = curr.Y + allowedMoves[i].Y;

                        if (checkX >= 0 && checkX < maxX && checkY >= 0 && checkY < maxY)
                        {
                            if (matrix[checkX, checkY] == LabyrinthMatrix.EmptyCell)
                            {
                                stackNext.Push(new RoutePoint(checkX, checkY, curr));
                                matrix[checkX, checkY] = (generation % 2 == 0) ? LabyrinthMatrix.FillGen0 : LabyrinthMatrix.FillGen1;
                            }
                            else if (matrix[checkX, checkY] == LabyrinthMatrix.Finish)
                            {
                                finishPoint = new RoutePoint(checkX, checkY, curr);
                                break;
                            }
                        }
                    }
                }
                stack = stackNext;
                ++generation;

                visitor.OnNextGeneration(matrix, generation);
            }
            while (stack.Count != 0 && finishPoint == null);



            while (finishPoint != null)
            {
                matrix[finishPoint.X, finishPoint.Y] = LabyrinthMatrix.Path;
                finishPoint = finishPoint.Prev;
            }

            visitor.OnFinish(matrix);

            return finishPoint;
        }
    }
}
