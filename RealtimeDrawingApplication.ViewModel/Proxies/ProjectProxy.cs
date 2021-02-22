using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.Proxies
{
    public class ProjectProxy : BindableBase
    {
        private int _id;
        private string _name;
        private UserProxy _projectCreator;
        private IList<DrawingComponentProxy> _drawingComponents;
        private IList<ProjectSharedUserProxy> _projectSharedUsers;

        public int Id { get => _id;  set {_id = value; RaisePropertyChanged(); } }
        public string Name { get => _name; set { _name = value; RaisePropertyChanged(); } }
        public UserProxy ProjectCreator { get => _projectCreator; set { _projectCreator = value; RaisePropertyChanged(); } }
        public IList<DrawingComponentProxy> DrawingComponents { get => _drawingComponents; set { _drawingComponents = value; RaisePropertyChanged(); } }
        public IList<ProjectSharedUserProxy> ProjectSharedUsers { get => _projectSharedUsers; set { _projectSharedUsers = value; RaisePropertyChanged(); } }
    }
}
