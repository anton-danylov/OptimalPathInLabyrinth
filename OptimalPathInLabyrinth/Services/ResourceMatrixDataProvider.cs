using System;
using System.Windows;

namespace OptimalPathInLabyrinth.Services
{
    public class ResourceMatrixDataProvider : IMatrixDataProvider
    {
        public string GetMatrixString(Uri uri)
        {
            var resourceStream = Application.GetResourceStream(uri);

            string matrix = null;
            using (var reader = new System.IO.StreamReader(resourceStream.Stream))
            {
                matrix = reader.ReadToEnd();
            }

            return matrix;
        }
    }
}
