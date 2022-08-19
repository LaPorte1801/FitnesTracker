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
        public UserCreator()
        {
            string path = Environment.CurrentDirectory + "/TestData";
            string[] files = Directory.GetFiles(path);

            List<UserActivity[]> userActivity = new();

            foreach (var file in files)
            {
                string jsonData = File.ReadAllText(file);

                try
                {
                    userActivity.Add(JsonSerializer.Deserialize<UserActivity[]>(jsonData));
                }
                catch (ArgumentException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            List<User> users = new List<User>();

            for (int i = 0; i < userActivity[20].Length; i++)
            {
                users.Add(new User(userActivity[20][i].User));
            }
            Debug.WriteLine(users.Count);

            for (int i = 0; i < userActivity.Count; i++)
            {
                for (int j = 0; j < userActivity[i].Length; j++)
                {
                    for (int k = 0; k < userActivity[i].Length; k++)
                    {
                        if (userActivity[i][k].User == users[k].UserName)
                        {
                            users[k].AddData(userActivity[i][k].Rank, userActivity[i][k].Status, userActivity[i][k].Steps);
                        }
                    }
                }
            }

            Debug.WriteLine(users[5].UserName + " " + users[5].Status[2] + " " + users[5].Steps[2] + " " + users[5].AverageSteps);
        }
    }
}
