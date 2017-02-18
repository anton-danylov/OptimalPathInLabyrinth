using System;

namespace OptimalPathInLabyrinth.Services
{
    public interface IMatrixDataProvider
    {
        string GetMatrixString(Uri uri);
    }
}
