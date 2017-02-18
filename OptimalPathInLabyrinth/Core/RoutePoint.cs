using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPathInLabyrinth.Core
{
    public class RoutePoint
    {
        public RoutePoint(int x, int y, RoutePoint prev)
        {
            X = x;
            Y = y;
            Prev = prev;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public RoutePoint Prev { get; private set; }
    }
}
