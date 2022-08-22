using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FitnesTracker.Extensions
{
    public static class EllipseExtensions
    {
        public static void SetCenter(this Ellipse ellipse, Point point)
        {
            Canvas.SetLeft(ellipse, point.X - ellipse.Width / 2);
            Canvas.SetTop(ellipse, point.Y - ellipse.Height / 2);
        }
    }
}
