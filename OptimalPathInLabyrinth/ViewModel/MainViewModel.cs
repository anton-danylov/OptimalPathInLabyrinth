using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using OptimalPathInLabyrinth.Core;
using OptimalPathInLabyrinth.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OptimalPathInLabyrinth.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        ILabyrinthPathFindingStrategy _strategy;

        public string WindowTitle { get; set; } = "Labyrinth window";

        public ManualResetEvent IsExecutingHandle { get; private set; } = new ManualResetEvent(false);

        public RelayCommand StartCommand { get; private set; }


        private LabyrinthMatrixViewModel _backupMatrix = null;
        private LabyrinthMatrixViewModel _labyrinthMatrix = null;
        public LabyrinthMatrixViewModel MatrixVM
        {
            get { return _labyrinthMatrix; }
            set { Set(nameof(MatrixVM), ref _labyrinthMatrix, value); }
        }


        int _currentGeneration;
        public int CurrentGeneration
        {
            get { return _currentGeneration; }
            set { Set(nameof(CurrentGeneration), ref _currentGeneration, value); }
        }


        private bool _isExecuting = false;
        public bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                StartCommand.RaiseCanExecuteChanged();
            }
        }


        public MainViewModel(ILabyrinthMatrixProvider provider
            , IMatrixDataProvider matrixDataProvider
            , ILabyrinthPathFindingStrategy strategy)
        {
            StartCommand = new RelayCommand(OnStart, () => !IsExecuting);

            string matrix = matrixDataProvider.GetMatrixString(new Uri("pack://application:,,,/Resources/LabyrinthMatrix.txt"));
            

            MatrixVM = new LabyrinthMatrixViewModel(provider.GetLabyrinthMatrixFromString(matrix));

            _strategy = strategy;
        }


        private async void OnStart()
        {
            RestoreDefaults();

            TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            IPathStrategyVisitor visitor = new LambdaVisitor((m, g) =>
            {
                Task.Delay(10).ContinueWith((t) => { CurrentGeneration++; }, uiScheduler).Wait();
            }, m => { });

            IsExecutingHandle.Reset();
            IsExecuting = true;

            try
            {
                await Task.Factory.StartNew(() => { _strategy.GetDestinationPoint(MatrixVM, visitor); });
            }
            finally
            {
                IsExecuting = false;
                IsExecutingHandle.Set();
            }
        }

        void RestoreDefaults()
        {
            if (_backupMatrix == null)
            {
                _backupMatrix = new LabyrinthMatrixViewModel(MatrixVM);
            }
            else
            {
                MatrixVM = new LabyrinthMatrixViewModel(_backupMatrix);
            }

            CurrentGeneration = 0;
        }
    }
}