using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPathInLabyrinth.Core
{
    public class LambdaVisitor : IPathStrategyVisitor
    {
        Action<ILabyrinthMatrix, int> _onNextGeneration;
        Action<ILabyrinthMatrix> _onFinish;

        public LambdaVisitor(Action<ILabyrinthMatrix, int> onNextGeneration, Action<ILabyrinthMatrix> onFinish)
        {
            _onNextGeneration = onNextGeneration;
            _onFinish = onFinish;
        }

        public void OnFinish(ILabyrinthMatrix matrix)
        {
            _onFinish(matrix);
        }

        public void OnNextGeneration(ILabyrinthMatrix matrix, int generation)
        {
            _onNextGeneration(matrix, generation);
        }
    }
}
