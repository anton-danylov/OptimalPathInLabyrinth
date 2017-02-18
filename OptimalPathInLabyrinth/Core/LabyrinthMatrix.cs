using System;

namespace OptimalPathInLabyrinth.Core
{
    public class LabyrinthMatrix : ILabyrinthMatrix
    {
        public const char EmptyCell = '.';
        public const char FillGen0 = 'x';
        public const char FillGen1 = 'X';
        public const char Path = '+';
        public const char Wall = '*';
        public const char Start = 's';
        public const char Finish = 'f';

        private char[,] _labyrinthMatrix = null;

        public int SizeX { get { return _labyrinthMatrix?.GetLength(0) ?? 0; } }
        public int SizeY { get { return _labyrinthMatrix?.GetLength(1) ?? 0; } }


        public LabyrinthMatrix(char[,] labyrinthMatrix)
        {
            _labyrinthMatrix = labyrinthMatrix;
        }

        public LabyrinthMatrix(int sizeX, int sizeY)
        {
            if (sizeX <= 0)
                throw new ArgumentException(nameof(sizeX));

            if (sizeY <= 0)
                throw new ArgumentException(nameof(sizeY));

            _labyrinthMatrix = new char[sizeX, sizeY];
        }

        public LabyrinthMatrix(ILabyrinthMatrix copyFrom)
        {
            _labyrinthMatrix = new char[copyFrom.SizeX, copyFrom.SizeY];
            //Buffer.BlockCopy(copyFrom._labyrinthMatrix, 0, _labyrinthMatrix, 0, copyFrom._labyrinthMatrix.Length * sizeof(char));

            for (int x = 0; x < copyFrom.SizeX; x++)
            {
                for (int y = 0; y < copyFrom.SizeY; y++)
                {
                    _labyrinthMatrix[x, y] = copyFrom[x, y];
                }
            }
        }

        public char this[int x, int y]
        {
            get
            {
                CheckIndexer(x, y);
                return _labyrinthMatrix[x, y];
            }

            set
            {
                CheckIndexer(x, y);
                _labyrinthMatrix[x, y] = value;
            }
        }

        protected void CheckIndexer(int x, int y)
        {
            if (_labyrinthMatrix == null)
                throw new NullReferenceException("LabyrinthMatrix");

            if (x < 0 || x >= SizeX)
                throw new ArgumentException(nameof(x));

            if (y < 0 || y >= SizeY)
                throw new ArgumentException(nameof(y));
        }

    }

}
