using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

namespace FitnesTracker.Models
{
    static class UserSerializer
    {
        public static void Serialize(List<User> selectedUsers, string directoryPath)
        {
            try
            {
                for (int i = 0; i < selectedUsers.Count; i++)
                {
                    File.WriteAllText(directoryPath.Split('.')[0] + $"{i + 1}." + directoryPath.Split('.')[1], JsonConvert.SerializeObject(selectedUsers[i]), Encoding.UTF8);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }
    }
}
