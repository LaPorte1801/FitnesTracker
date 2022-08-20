using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnesTracker.ViewModel
{
    class UserActivityViewModel : DependencyObject
    {
        readonly List<Models.User> _users;

        public static readonly DependencyProperty StepsProperty;

        static UserActivityViewModel()
        {
            StepsProperty = DependencyProperty.Register("UserSteps", typeof(List<int>), typeof(UserActivityViewModel));
        }
        public UserActivityViewModel()
        {
            _users = new Models.UserCreator().Users;
        }

        public List<int> UserSteps
        {
            get { return (List<int>)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }        
    }
}
