using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FitnesTracker.Extensions;
using System.Windows.Shapes;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace FitnesTracker.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    //TODO: Ебануть рацсветку строчек
    public partial class MainWindow : Window
    {
        ViewModel.UserActivityViewModel viewModel = new();
        int margin = 20;
        List<Line> daysGridLines = new();
        List<Line> stepsGridLines = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = viewModel;
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



        private void dGUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGraphics();
        }

        private void graphGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            lineStepsAxis.X1 = margin;
            lineStepsAxis.Y1 = margin;
            lineStepsAxis.X2 = margin;
            lineStepsAxis.Y2 = graphCanvas.ActualHeight - margin;

            stepsAxisArrow.Points = new PointCollection { new Point(margin, margin), new Point(margin - 2, margin + 6), new Point(margin + 2, margin + 6) };

            lineDaysAxis.X1 = margin;
            lineDaysAxis.Y1 = graphCanvas.ActualHeight - margin;
            lineDaysAxis.X2 = graphCanvas.ActualWidth - margin;
            lineDaysAxis.Y2 = graphCanvas.ActualHeight - margin;

            daysAxisArrow.Points = new PointCollection
            {
                new Point(graphCanvas.ActualWidth - margin,
                graphCanvas.ActualHeight - margin), new Point(graphCanvas.ActualWidth - (margin + 6), graphCanvas.ActualHeight - (margin - 2)),
                new Point(graphCanvas.ActualWidth - (margin + 6), graphCanvas.ActualHeight - (margin + 2))
            };

            Canvas.SetTop(tbDaysAxisTitle, graphCanvas.ActualHeight - (margin + 25));
            Canvas.SetLeft(tbDaysAxisTitle, graphCanvas.ActualWidth - (margin + 25));
            Canvas.SetTop(tbZero, graphCanvas.ActualHeight - margin);
            Canvas.SetLeft(tbZero, margin - 5);

            pLStepsGraph.Points = new PointCollection();

            if (dGUsers.SelectedItem != null)
            {
                maximumDot.Visibility = Visibility.Visible;
                minimumDot.Visibility = Visibility.Visible;

                double scale = graphCanvas.ActualHeight / (((Models.User)dGUsers.SelectedItem).Steps.Max() * 1.25);

                for (int i = 0; i < ((Models.User)dGUsers.SelectedItem).Steps.Count; i++)
                {
                    pLStepsGraph.Points.Add(new Point(i * ((graphCanvas.ActualWidth - margin - 20) /
                        ((Models.User)dGUsers.SelectedItem).Steps.Count) + margin,
                        graphCanvas.ActualHeight - ((Models.User)dGUsers.SelectedItem).Steps[i] * scale - margin));

                    if (((Models.User)dGUsers.SelectedItem).Steps[i] == ((Models.User)dGUsers.SelectedItem).BestResult)
                    {
                        maximumDot.SetCenter(new Point((i * ((graphCanvas.ActualWidth - margin - 20) /
                        ((Models.User)dGUsers.SelectedItem).Steps.Count) + margin),
                        graphCanvas.ActualHeight - ((Models.User)dGUsers.SelectedItem).Steps[i] * scale - margin));
                    }

                    if (((Models.User)dGUsers.SelectedItem).Steps[i] == ((Models.User)dGUsers.SelectedItem).WorstResult)
                    {
                        minimumDot.SetCenter(new Point((i * ((graphCanvas.ActualWidth - margin - 20) /
                        ((Models.User)dGUsers.SelectedItem).Steps.Count) + margin),
                        graphCanvas.ActualHeight - ((Models.User)dGUsers.SelectedItem).Steps[i] * scale - margin));
                    }
                }
            }


            if (dGUsers.SelectedItem != null)
            {
                for (int i = 0; i < daysGridLines.Count; i++)
                {
                    graphCanvas.Children.Remove(daysGridLines[i]);
                }

                for (int i = 0; i < stepsGridLines.Count; i++)
                {
                    graphCanvas.Children.Remove(stepsGridLines[i]);
                }

                for (int i = 0; i < ((Models.User)dGUsers.SelectedItem).Steps.Count; i++)
                {
                    if (i < ((Models.User)dGUsers.SelectedItem).Steps.Count - 1)
                    {
                        daysGridLines.Add(new Line());
                        daysGridLines[i].X1 = margin + (i + 1) * ((graphCanvas.ActualWidth - margin - 20) / ((Models.User)dGUsers.SelectedItem).Steps.Count);
                        daysGridLines[i].Y1 = margin;
                        daysGridLines[i].X2 = margin + (i + 1) * ((graphCanvas.ActualWidth - margin - 20) / ((Models.User)dGUsers.SelectedItem).Steps.Count);
                        daysGridLines[i].Y2 = graphCanvas.ActualHeight - margin;
                        daysGridLines[i].StrokeThickness = 1;
                        daysGridLines[i].Stroke = Brushes.LightGray;
                        graphCanvas.Children.Add(daysGridLines[i]);
                    }
                }
            }
        }

        private void dGUsers_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (((Models.User)dGUsers.Items[e.Row.GetIndex()]).BestResult > 
                ((Models.User)dGUsers.Items[e.Row.GetIndex()]).AverageSteps * 1.2 ||
                ((Models.User)dGUsers.Items[e.Row.GetIndex()]).WorstResult < 
                ((Models.User)dGUsers.Items[e.Row.GetIndex()]).AverageSteps * 0.8)
            {
                if (((Models.User)dGUsers.Items[e.Row.GetIndex()]).BestResult >
                ((Models.User)dGUsers.Items[e.Row.GetIndex()]).AverageSteps * 1.2)
                {
                    e.Row.Background = Brushes.LightSkyBlue;
                }
                if (((Models.User)dGUsers.Items[e.Row.GetIndex()]).WorstResult <
                ((Models.User)dGUsers.Items[e.Row.GetIndex()]).AverageSteps * 0.8)
                {
                    e.Row.Background = Brushes.PaleVioletRed;
                }
            }
            else
            {
                e.Row.Background = Brushes.White;
            }
        }
    }
}
