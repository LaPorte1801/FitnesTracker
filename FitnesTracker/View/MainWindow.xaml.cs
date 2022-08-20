using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace FitnesTracker.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Line _lineSteps;
        Line _lineDays;
        Polygon _triangleSteps;
        Polygon _triangleDays;

        public MainWindow()
        {
            InitializeComponent();

            _lineSteps = new Line();
            _lineDays = new Line();
            _triangleSteps = new Polygon();
            _triangleDays = new Polygon();
            graphGrid.Children.Add(_lineSteps);
            graphGrid.Children.Add(_lineDays);
            graphGrid.Children.Add(_triangleSteps);
            graphGrid.Children.Add(_triangleDays);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModel.UserActivityViewModel();
        }

        private void graphGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _lineSteps.X1 = 10;
            _lineSteps.Y1 = 10;
            _lineSteps.X2 = 10;
            _lineSteps.Y2 = graphGrid.ActualHeight - 10;
            _lineSteps.StrokeThickness = 3;
            _lineSteps.Stroke = Brushes.Black;

            _triangleSteps.Points = new PointCollection { new Point(10, 10), new Point(8, 16), new Point(12, 16) };
            _triangleSteps.StrokeThickness = 3;
            _triangleSteps.Stroke = Brushes.Black;

            _lineDays.X1 = 10;
            _lineDays.Y1 = graphGrid.ActualHeight - 10;
            _lineDays.X2 = graphGrid.ActualWidth - 10;
            _lineDays.Y2 = graphGrid.ActualHeight - 10;
            _lineDays.StrokeThickness = 3;
            _lineDays.Stroke = Brushes.Black;

            _triangleDays.Points = new PointCollection { new Point(graphGrid.ActualWidth - 10, graphGrid.ActualHeight - 10), new Point(graphGrid.ActualWidth - 16, graphGrid.ActualHeight - 8), new Point(graphGrid.ActualWidth - 16, graphGrid.ActualHeight - 12) };
            _triangleDays.StrokeThickness = 3;
            _triangleDays.Stroke = Brushes.Black;
        }
    }
}
