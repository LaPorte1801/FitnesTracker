using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace FitnesTracker.Models
{
    static class UserSerializer
    {
        public static void Serialize(List<User> selectedUsers, string directoryPath)
        {
            //for (int i = 0; i < selectedUsers[0].Steps.Count; i++)
            //{
            //    File.Create(directoryPath + $"{i + 1}.json");
            //}

            for (int i = 0; i < selectedUsers.Count; i++)
            {
                File.WriteAllText(directoryPath + $"{i + 1}.json", JsonConvert.SerializeObject(selectedUsers[i]), Encoding.UTF8);
            }
        }
    }
}
