using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesTracker.Models
{
    internal class User
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
    }
}
