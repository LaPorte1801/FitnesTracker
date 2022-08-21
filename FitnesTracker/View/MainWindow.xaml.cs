using Microsoft.Win32;
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
        ViewModel.UserActivityViewModel viewModel = new();
        Line _lineSteps;
        Line _lineDays;
        Polygon _triangleSteps;
        Polygon _triangleDays;

        public MainWindow()
        {
            InitializeComponent();

            _lineSteps = new Line
            {
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };

            _lineDays = new Line
            {
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };

            _triangleSteps = new Polygon
            {
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };

            _triangleDays = new Polygon
            {
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };

            graphGrid.Children.Add(_lineSteps);
            graphGrid.Children.Add(_lineDays);
            graphGrid.Children.Add(_triangleSteps);
            graphGrid.Children.Add(_triangleDays);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = viewModel;
        }

        private void graphGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGraphics();
        }

        private void mIExportSelectedData_Click(object sender, RoutedEventArgs e)
        {
            if (dGUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Данные не выделены", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string exportPath = "";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON-Файл (*.json)|*.json|Все файлы (*.*)|*.*";
                if (saveFileDialog.ShowDialog(this) == true)
                {
                    exportPath = saveFileDialog.FileName;
                }
               
                viewModel.ExportSelectedData(dGUsers.SelectedItems.Cast<Models.User>().ToList(), exportPath);
            }
        }

        private void UpdateGraphics()
        {
            _lineSteps.X1 = 10;
            _lineSteps.Y1 = 10;
            _lineSteps.X2 = 10;
            _lineSteps.Y2 = graphGrid.ActualHeight - 10;

            _triangleSteps.Points = new PointCollection { new Point(10, 10), new Point(8, 16), new Point(12, 16) };

            _lineDays.X1 = 10;
            _lineDays.Y1 = graphGrid.ActualHeight - 10;
            _lineDays.X2 = graphGrid.ActualWidth - 10;
            _lineDays.Y2 = graphGrid.ActualHeight - 10;

            _triangleDays.Points = new PointCollection 
            { 
                new Point(graphGrid.ActualWidth - 10,
                graphGrid.ActualHeight - 10), new Point(graphGrid.ActualWidth - 16, graphGrid.ActualHeight - 8),
                new Point(graphGrid.ActualWidth - 16, graphGrid.ActualHeight - 12) 
            };

            pLStepsGraph.Points = new PointCollection();

            if (dGUsers.SelectedItem != null)
            {
                double scale = graphGrid.ActualHeight / (((Models.User)dGUsers.SelectedItem).Steps.Max() * 2);

                for (int i = 0; i < ((Models.User)dGUsers.SelectedItem).Steps.Count; i++)
                {
                    pLStepsGraph.Points.Add(new Point((i * (graphGrid.ActualWidth /
                        ((Models.User)dGUsers.SelectedItem).Steps.Count) + 10),
                        graphGrid.ActualHeight - ((Models.User)dGUsers.SelectedItem).Steps[i] * scale - 10));
                }


            }
        }

        private void dGUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGraphics();
        }

        private void dGUsers_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
