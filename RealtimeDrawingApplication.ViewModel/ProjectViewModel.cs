using Prism.Events;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel
{
    public class ProjectViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<ProjectProxy> _projectList;
        private ProjectProxy _selectedProject;

        public ProjectViewModel()
        {
            _projectList = new ObservableCollection<ProjectProxy>();
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<LoadProjectListEvent>().Subscribe(LoadProjectList);
            EventAggregator.GetEvent<FetchProjectsEvent>().Subscribe(FetchProjects);
        }

        public ObservableCollection<ProjectProxy> ProjectList { get => _projectList; set => _projectList = value; }
        public IEventAggregator EventAggregator { get; set; }
        public ProjectProxy SelectedProject { get => _selectedProject; set { _selectedProject = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged()
        {
            var drawingComponents = DatabaseServices.DrawingComponentModelService.DeserializeToProxy(SelectedProject.Name); 

            if (drawingComponents != null)
            {
                EventAggregator.GetEvent<GetProjectInstanceEvent>().Publish(SelectedProject.Name);
                EventAggregator.GetEvent<FetchDrawingComponentsEvent>().Publish(drawingComponents);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProject"));
        }

        void LoadProjectList(ProjectProxy projectProxy)
        {
            _projectList.Add(projectProxy);
        }

        void FetchProjects(string userEmail)
        {
            var projects = DatabaseServices.UserModelService.GetProjectList(userEmail);
            
            if (projects != null)
            {
                foreach (var project in projects)
                {
                    LoadProjectList(DatabaseServices.ProjectModelService.DeserializeToProxy(project));
                }
            }
        }
    }

    public class LoadProjectListEvent : PubSubEvent<ProjectProxy> { }
    public class FetchProjectsEvent : PubSubEvent<string> { }
    public class FetchDrawingComponentsEvent : PubSubEvent<List<DrawingComponentProxy>> { }
}
