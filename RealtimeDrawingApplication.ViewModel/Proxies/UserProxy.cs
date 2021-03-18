using Prism.Events;
using Prism.Mvvm;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.Proxies
{
    public class UserProxy : BindableBase
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private IList<ProjectSharedUsersModel> _projectSharedUsers;
        private string _fullName;

        public UserProxy() { }
        
        public string FirstName { get => _firstName; set { _firstName = value; RaisePropertyChanged(); } }
        public string Email { get => _email; set { _email = value; RaisePropertyChanged(); } }
        public string Password { get => _password; set { _password = value; RaisePropertyChanged(); } }
        public IList<ProjectSharedUsersModel> ProjectSharedUsers { get => _projectSharedUsers; set { _projectSharedUsers = value; RaisePropertyChanged(); } }
        public string LastName { get => _lastName; set { _lastName = value; RaisePropertyChanged(); } }
        public string FullName { get => _fullName; set { _fullName = value; RaisePropertyChanged(); } }
    }

}
