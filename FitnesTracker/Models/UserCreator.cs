using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitnesTracker.Models
{
    internal class UserCreator
    {
        public List<User> Users { get; }
        public UserCreator()
        {
            string path = Environment.CurrentDirectory + "/TestData";
            string[] files = Directory.GetFiles(path);
            
            Users = new List<User>();

            List<UserActivity[]> userActivity = new();

            foreach (var file in files)
            {
                string jsonData = File.ReadAllText(file);

                try
                {
                    userActivity.Add(JsonSerializer.Deserialize<UserActivity[]>(jsonData));
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            for (int i = 0; i < userActivity[20].Length; i++)
            {
                Users.Add(new User(userActivity[20][i].User));
            }
            Debug.WriteLine(Users.Count);

            for (int i = 0; i < userActivity.Count; i++)
            {
                for (int j = 0; j < userActivity[i].Length; j++)
                {
                    for (int k = 0; k < userActivity[i].Length; k++)
                    {
                        if (userActivity[i][k].User == Users[j].UserName)
                        {
                            Users[j].AddData(userActivity[i][k].Rank, userActivity[i][k].Status, userActivity[i][k].Steps);
                        }
                    }
                }
            }

            Debug.WriteLine(Users[0].UserName + " " + Users[0].Status[0] + " " + Users[0].Steps[0] + " " + Users[0].AverageSteps);
        }
    }
}
