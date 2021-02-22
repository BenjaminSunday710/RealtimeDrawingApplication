using Prism.Events;
using Prism.Mvvm;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel
{
    public class ProjectViewModel:BindableBase
    {
        private ObservableCollection<ProjectProxy> _projectListItems;
        private ProjectProxy _selectedProject;

        public ProjectViewModel()
        {

        }

        public ObservableCollection<ProjectProxy> ProjectListItems{ get => _projectListItems; set { _projectListItems = value; RaisePropertyChanged(); } }
        public ProjectProxy SelectedProject { get => _selectedProject; set { _selectedProject = value; RaisePropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }

        void AddProjectItem()
        {
           
        }
    }

    public class AddProjectEvent : PubSubEvent<ProjectProxy> { }
}
