using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPathInLabyrinth.Core
{
    public interface IPathStrategyVisitor
    {
        void OnNextGeneration(ILabyrinthMatrix matrix, int generation);
        void OnFinish(ILabyrinthMatrix matrix);
    }
}
