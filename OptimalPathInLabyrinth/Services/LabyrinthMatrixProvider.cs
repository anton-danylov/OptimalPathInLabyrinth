using OptimalPathInLabyrinth.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPathInLabyrinth.Services
{
    public class LabyrinthMatrixProvider : ILabyrinthMatrixProvider
    {
        public ILabyrinthMatrix GetLabyrinthMatrixFromString(string matrix)
        {
            var lines = matrix.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int maxX = lines.Max(l => l.Length);
            int maxY = lines.Length;


            var labirynthMatrix = new LabyrinthMatrix(maxX, maxY);

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    labirynthMatrix[x, y] = lines[y][x];
                }
            }

            return labirynthMatrix;
        }
    }
}
