using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.Proxies
{
    public class ProjectSharedUserProxy:BindableBase
    {
        private int _id;
        private ProjectProxy _project;
        private UserProxy _projectCreator;
        private bool _isAllowed;
        private bool _isEditable;

        public int Id { get => _id; set { _id = value; RaisePropertyChanged(); } }
        public ProjectProxy Project { get => _project; set {_project = value; RaisePropertyChanged(); } }
        public UserProxy ProjectCreator { get => _projectCreator; set {_projectCreator = value; RaisePropertyChanged(); } }
        public bool IsAllowed { get => _isAllowed; set {_isAllowed = value; RaisePropertyChanged(); } }
        public bool IsEditable { get => _isEditable; set {_isEditable = value; RaisePropertyChanged(); } }
    }
}
