/*
  In App.xaml:

  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using OptimalPathInLabyrinth.Core;
using OptimalPathInLabyrinth.Services;
using System;

namespace OptimalPathInLabyrinth.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator() 
            : this(() => new LabyrinthMatrixProvider()
            , () => new ResourceMatrixDataProvider()
            , () => new SimplePathFindingStrategy())
        {

        }

        public ViewModelLocator(Func<ILabyrinthMatrixProvider> matrixProviderFactory,
            Func<IMatrixDataProvider> matrixDataProviderFactory,
            Func<ILabyrinthPathFindingStrategy> pathFindingStrategyFactory)
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ILabyrinthMatrixProvider>(matrixProviderFactory);
            SimpleIoc.Default.Register<IMatrixDataProvider>(matrixDataProviderFactory);
            SimpleIoc.Default.Register<ILabyrinthPathFindingStrategy>(pathFindingStrategyFactory);


            SimpleIoc.Default.Register<MainViewModel>();
        }


        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().MatrixVM = null;
        }
    }
}