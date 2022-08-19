using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnesTracker.Models
{
    internal class User : DependencyObject
    {
        public List<int> Rank { get; }
        public string? UserName { get; }
        public List<string> Status { get; }
        public List<int> Steps { get; }
        public int AverageSteps 
        {
            get
            {
                return (int)Steps.Average();
            }
        }
        public int BestResult
        {
            get
            {
                return Steps.Max();
            }
        }
        public int WorstResult 
        {
            get
            {
                return Steps.Min();
            }
        }

        public User(string userName)
        {
            UserName = userName;
            Rank = new List<int>();
            Status = new List<string>();
            Steps = new List<int>();
        }

        public void AddData(int rank, string status, int steps)
        {
            Rank.Add(rank);
            Status.Add(status);
            Steps.Add(steps);
        }

        public int StepsProperty
        {
            get { return (int)GetValue(StepsPropertyProperty); }
            set { SetValue(StepsPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StepsProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepsPropertyProperty =
            DependencyProperty.Register("StepsProperty", typeof(int), typeof(User), new PropertyMetadata(0));


    }
}
