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
        public List<int> Steps 
        {
            get { return (List<int>)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }
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

        static User()
        {
            StepsProperty = DependencyProperty.Register("Steps", typeof(List<int>), typeof(User));
        }

        public void AddData(int rank, string status, int steps)
        {
            Rank.Add(rank);
            Status.Add(status);
            Steps.Add(steps);
        }

        public static readonly DependencyProperty StepsProperty;

    }
}
