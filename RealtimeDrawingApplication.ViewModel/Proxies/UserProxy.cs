using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.Proxies
{
    public class UserProxy:BindableBase
    {
        private int _id;
        private string _fullName;
        private string _email;
        private string _password;
        private IList<ProjectSharedUserProxy> _projectSharedUsers;

        public int Id { get => _id; set{_id = value; RaisePropertyChanged(); } }
        public string FullName { get => _fullName; set{_fullName = value; RaisePropertyChanged(); } }
        public string Email { get => _email; set{_email = value; RaisePropertyChanged(); } }
        public string Password { get => _password; set { _password = value; RaisePropertyChanged(); } }
        public IList<ProjectSharedUserProxy> ProjectSharedUsers { get => _projectSharedUsers; set{ _projectSharedUsers = value; RaisePropertyChanged(); } }
    }
}
