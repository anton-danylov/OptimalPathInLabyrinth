using OptimalPathInLabyrinth.Core;

namespace OptimalPathInLabyrinth.Services
{
    public interface ILabyrinthMatrixProvider
    {
        ILabyrinthMatrix GetLabyrinthMatrixFromString(string matrix);
    }
}
