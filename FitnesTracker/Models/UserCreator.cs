using System.Collections.Generic;

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
