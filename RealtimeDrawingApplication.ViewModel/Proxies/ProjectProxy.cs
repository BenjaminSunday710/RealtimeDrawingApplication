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
    public class ProjectProxy : BindableBase
    {
        private string _name;
        private UserModel _projectCreator;
        private IList<DrawingComponentModel> _drawingComponents;
        private IList<ProjectSharedUsersModel> _projectSharedUsers;
        //private static Repository<ProjectModel> database = new Repository<ProjectModel>();

        public ProjectProxy()
        {
           
        }

        public string Name { get => _name; set { _name = value; RaisePropertyChanged(); } }
        public UserModel ProjectCreator { get => _projectCreator; set { _projectCreator = value; RaisePropertyChanged(); } }
        public IList<DrawingComponentModel> DrawingComponents { get => _drawingComponents; set { _drawingComponents = value; RaisePropertyChanged(); } }
        public IList<ProjectSharedUsersModel> ProjectSharedUsers { get => _projectSharedUsers; set { _projectSharedUsers = value; RaisePropertyChanged(); } }

    }

}
