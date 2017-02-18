using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPathInLabyrinth.Core
{
    public interface ILabyrinthMatrix
    {
        int SizeX { get; }
        int SizeY { get; }

        char this[int x, int y] { get; set; }
    }
}
