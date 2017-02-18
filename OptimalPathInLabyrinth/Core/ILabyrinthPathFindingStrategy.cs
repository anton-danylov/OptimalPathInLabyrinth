using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPathInLabyrinth.Core
{
    public interface ILabyrinthPathFindingStrategy
    {
        RoutePoint GetDestinationPoint(ILabyrinthMatrix matrix, IPathStrategyVisitor visitor);
    }
}
