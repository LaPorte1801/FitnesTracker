using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FitnesTracker.Models
{
    static class UserDeserializer
    {
        public static List<User> DeserializeData()
        {
            List<User> Users = new();

            string path;
            string[] files;

            List<UserActivity[]> userActivity = new();

            try
            {
                path = Environment.CurrentDirectory + "/TestData";
                files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    string jsonData = File.ReadAllText(file);
                    userActivity.Add(JsonSerializer.Deserialize<UserActivity[]>(jsonData));
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Папка TestData не найдена. Поместите папку TestData в одну папку с исполняемым файлом",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            catch (JsonException)
            {
                MessageBox.Show("Данные в папке TestData повреждены либо введены некорректно", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

            for (int i = 0; i < userActivity[20].Length; i++)
            {
                Users.Add(new User(userActivity[20][i].User));
            }

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

            return Users;
        }
    }
}
