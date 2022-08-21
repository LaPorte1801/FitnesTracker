using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnesTracker.ViewModel
{
    class UserActivityViewModel
    {
        public List<Models.User> Users { get; }

        public UserActivityViewModel()
        {
            Users = new Models.UserCreator().Users;
        }

        public void ExportSelectedData(List<Models.User> selectedUsers, string exportPath)
        {
            Models.UserSerializer.Serialize(selectedUsers, exportPath);
        }
    }
}
