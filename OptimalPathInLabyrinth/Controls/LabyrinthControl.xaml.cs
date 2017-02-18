using OptimalPathInLabyrinth.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Specialized;
using OptimalPathInLabyrinth.Core;

namespace OptimalPathInLabyrinth.Controls
{
    /// <summary>
    /// Interaction logic for LabyrinthControl.xaml
    /// </summary>
    public partial class LabyrinthControl : Control
    {
        Canvas _canvas = null;


        #region Brush Dependency Properties
        public Brush WallBrush
        {
            get { return (Brush)GetValue(WallBrushProperty); }
            set { SetValue(WallBrushProperty, value); }
        }

        public static readonly DependencyProperty WallBrushProperty =
            DependencyProperty.Register("WallBrush", typeof(Brush), typeof(LabyrinthControl), new PropertyMetadata(null));


        public Brush EmptyCellBrush
        {
            get { return (Brush)GetValue(EmptyCellBrushProperty); }
            set { SetValue(EmptyCellBrushProperty, value); }
        }

        public static readonly DependencyProperty EmptyCellBrushProperty =
            DependencyProperty.Register("EmptyCellBrush", typeof(Brush), typeof(LabyrinthControl), new PropertyMetadata(null));


        public Brush StartPointBrush
        {
            get { return (Brush)GetValue(StartPointBrushProperty); }
            set { SetValue(StartPointBrushProperty, value); }
        }

        public static readonly DependencyProperty StartPointBrushProperty =
            DependencyProperty.Register("StartPointBrush", typeof(Brush), typeof(LabyrinthControl), new PropertyMetadata(null));


        public Brush FinishPointBrush
        {
            get { return (Brush)GetValue(FinishPointBrushProperty); }
            set { SetValue(FinishPointBrushProperty, value); }
        }

        public static readonly DependencyProperty FinishPointBrushProperty =
            DependencyProperty.Register("FinishPointBrush", typeof(Brush), typeof(LabyrinthControl), new PropertyMetadata(null));


        public Brush FirstGenerationBrush
        {
            get { return (Brush)GetValue(FirstGenerationBrushProperty); }
            set { SetValue(FirstGenerationBrushProperty, value); }
        }

        public static readonly DependencyProperty FirstGenerationBrushProperty =
            DependencyProperty.Register("FirstGenerationBrush", typeof(Brush), typeof(LabyrinthControl), new PropertyMetadata(null));


        public Brush SecondGenerationBrush
        {
            get { return (Brush)GetValue(SecondGenerationBrushProperty); }
            set { SetValue(SecondGenerationBrushProperty, value); }
        }

        public static readonly DependencyProperty SecondGenerationBrushProperty =
            DependencyProperty.Register("SecondGenerationBrush", typeof(Brush), typeof(LabyrinthControl), new PropertyMetadata(null));


        public Brush FoundPathBrush
        {
            get { return (Brush)GetValue(FoundPathBrushProperty); }
            set { SetValue(FoundPathBrushProperty, value); }
        }

        public static readonly DependencyProperty FoundPathBrushProperty =
            DependencyProperty.Register("FoundPathBrush", typeof(Brush), typeof(LabyrinthControl), new PropertyMetadata(null));

        #endregion


        private readonly Dictionary<char, Func<Brush>> _colorTable = new Dictionary<char, Func<Brush>>();

        public static readonly DependencyProperty MatrixProperty =
            DependencyProperty.Register("Matrix", typeof(LabyrinthMatrixViewModel), typeof(LabyrinthControl)
                , new FrameworkPropertyMetadata(null, OnMatrixPropertyChanged));


        private static void OnMatrixPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabyrinthControl me = d as LabyrinthControl;

            var oldMatrix = e.OldValue as LabyrinthMatrixViewModel;

            if (oldMatrix != null)
            {
                oldMatrix.CollectionChanged -= me.OnMatrixChanged;
            }

            var newMatrix = e.NewValue as LabyrinthMatrixViewModel;

            if (newMatrix != null)
            {
                newMatrix.CollectionChanged += me.OnMatrixChanged;
            }
        }

        private void OnMatrixChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshCanvas(Matrix);
        }

        public LabyrinthMatrixViewModel Matrix
        {
            get { return (LabyrinthMatrixViewModel)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public LabyrinthControl()
        {
            InitializeComponent();

            _colorTable[LabyrinthMatrix.Wall] = () => WallBrush;
            _colorTable[LabyrinthMatrix.EmptyCell] = () => EmptyCellBrush;
            _colorTable[LabyrinthMatrix.Start] = () => StartPointBrush;
            _colorTable[LabyrinthMatrix.Finish] = () => FinishPointBrush;
            _colorTable[LabyrinthMatrix.FillGen0] = () => FirstGenerationBrush;
            _colorTable[LabyrinthMatrix.FillGen1] = () => SecondGenerationBrush;
            _colorTable[LabyrinthMatrix.Path] = () => FoundPathBrush;


            this.SizeChanged += (s, e) => RefreshCanvas(Matrix); 
        }

        Brush GetFillColor(char cell)
        {
            return _colorTable[cell]();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = (Canvas)GetTemplateChild("PART_Canvas");
        }

        private void RefreshCanvas(LabyrinthMatrixViewModel matrix)
        {
            if (matrix == null)
                return;

            if (_canvas == null)
                return;

            _canvas.Children.Clear();

            int maxX = matrix.SizeX;
            int maxY = matrix.SizeY;

            double cellSize = Math.Min(Math.Round(_canvas.ActualWidth / maxX), Math.Round(_canvas.ActualHeight / maxY));

            if (double.IsNaN(cellSize) || cellSize < 5.0)
            {
                cellSize = 5.0;
            }

            for (int x = 0; x < maxX; x++)
                for (int y = 0; y < maxY; y++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    rect.StrokeThickness = 0.1;

                    rect.Fill = GetFillColor(matrix[x, y]);
                    rect.Width = cellSize - 1;
                    rect.Height = cellSize - 1;
                    Canvas.SetLeft(rect, x * cellSize);
                    Canvas.SetTop(rect, y * cellSize);
                    _canvas.Children.Add(rect);
                }
        }
    }
}
