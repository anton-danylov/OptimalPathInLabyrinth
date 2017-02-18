using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OptimalPathInLabyrinth.ViewModel;
using OptimalPathInLabyrinth.Services;
using Moq;
using OptimalPathInLabyrinth.Core;
using GalaSoft.MvvmLight.Command;
using System.IO.Packaging;
using System.Windows;
using System.Threading;

namespace OptimalPathInLabyrinth.Tests
{
    [TestClass]
    public class ViewModelTests
    {
        string _testMatrix =
@"
s...
***.
f...
";
        private ILabyrinthMatrix PrepareTestMatrix()
        {
            LabyrinthMatrixProvider provider = new LabyrinthMatrixProvider();

            return provider.GetLabyrinthMatrixFromString(_testMatrix);
        }

        [TestInitialize]
        public void Setup()
        {
            PackUriHelper.Create(new Uri("someuri://0"));
            new FrameworkElement();
            System.Windows.Application.ResourceAssembly = typeof(App).Assembly;

            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }


        [TestMethod]
        public void TestPathFoundWithSimpleStrategy()
        {
            // Arrange
            var mockMatrixDataProvider = new Mock<IMatrixDataProvider>();
            mockMatrixDataProvider
                .Setup(x => x.GetMatrixString(It.IsAny<Uri>()))
                .Returns(String.Empty);

            var mockLabMatrixProvider = new Mock<ILabyrinthMatrixProvider>();
            mockLabMatrixProvider
                .Setup(x => x.GetLabyrinthMatrixFromString(It.IsAny<string>()))
                .Returns(PrepareTestMatrix());


            var viewModelLocator = new ViewModelLocator(
                () => mockLabMatrixProvider.Object
                , () => mockMatrixDataProvider.Object
                , () => new SimplePathFindingStrategy());


            MainViewModel vmMain = viewModelLocator.Main;

            // Act
            vmMain.StartCommand.Execute(null);
            vmMain.IsExecutingHandle.WaitOne();

            // Assert
            Assert.AreEqual(vmMain.MatrixVM[0, 2], LabyrinthMatrix.Path);
        }
    }
}
