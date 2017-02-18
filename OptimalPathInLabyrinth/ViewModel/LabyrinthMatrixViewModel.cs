using GalaSoft.MvvmLight.Threading;
using OptimalPathInLabyrinth.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OptimalPathInLabyrinth.ViewModel
{
    public class LabyrinthMatrixViewModel : INotifyCollectionChanged, ILabyrinthMatrix
    {
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;


        private ILabyrinthMatrix _labyrinthMatrix = null;

        public int SizeX { get { return _labyrinthMatrix.SizeX; } }
        public int SizeY { get { return _labyrinthMatrix.SizeY; } }


        public LabyrinthMatrixViewModel(ILabyrinthMatrix labyrinthMatrix)
        {
            _labyrinthMatrix = new LabyrinthMatrix(labyrinthMatrix);
        }

        public LabyrinthMatrixViewModel(LabyrinthMatrixViewModel vm)
        {
            _labyrinthMatrix = new LabyrinthMatrix(vm._labyrinthMatrix);
        }

        private void RaiseCollectionChanged(object param)
        {
            var eh = CollectionChanged;
            if (eh != null)
            {
                eh(this, (NotifyCollectionChangedEventArgs)param);
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_synchronizationContext == SynchronizationContext.Current)
            {
                RaiseCollectionChanged(e);
            }
            else
            {
                _synchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }

        public char this[int x, int y]
        {
            get
            {
                return _labyrinthMatrix[x, y];
            }

            set
            {
                _labyrinthMatrix[x, y] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
