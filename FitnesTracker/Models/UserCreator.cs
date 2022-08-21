using FitnesTracker.Models;
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
    internal class UserCreator
    {
        public List<User> Users { get; }
        public UserCreator()
        {
            Users = UserDeserializer.DeserializeData();
        }
    }
}
