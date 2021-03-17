using Prism.Events;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Windows;

namespace RealtimeDrawingApplication.ViewModel
{
    public class ProjectWindowViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<ProjectProxy> _projectList;
        private ProjectProxy _selectedProject;
        private static Repository<ProjectModel> database = Repository<ProjectModel>.GetRepository;

        public ProjectWindowViewModel()
        {
            _projectList = new ObservableCollection<ProjectProxy>();
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<LoadProjectListEvent>().Subscribe(AddProject);
            EventAggregator.GetEvent<FetchProjectsEvent>().Subscribe(FetchProjects);
            EventAggregator.GetEvent<DeleteProjectEvent>().Subscribe(DeleteProject);
        }

        public ObservableCollection<ProjectProxy> ProjectList { get => _projectList; set { _projectList = value; OnPropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }
        public ProjectProxy SelectedProject { get => _selectedProject; set { _selectedProject = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged()
        {
            if (SelectedProject == null)
                return;

            var drawingComponents = DatabaseServices.DrawingComponentModelService.DeserializeToProxy(SelectedProject.Name);

            if (drawingComponents != null)
            {
                EventAggregator.GetEvent<GetProjectInstanceEvent>().Publish(SelectedProject.Name);
                EventAggregator.GetEvent<FetchDrawingComponentsEvent>().Publish(drawingComponents);
            }
            else return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProject"));
        }

        void AddProject(ProjectProxy projectProxy)
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
                    AddProject(DatabaseServices.ProjectModelService.DeserializeToProxy(project));
                }
            }
        }

        void DeleteProject(string projectName)
        {
            if (projectName == null)
                return;

            var project = database.GetProject(projectName);

            var response = MessageBox.Show("Project will be deleted permanently","Notification",MessageBoxButton.OKCancel,MessageBoxImage.Information);

            if (response == MessageBoxResult.Cancel)
                return;
            
            var item = ProjectList.FirstOrDefault(x => x.Name == project.Name);
            ProjectList.Remove(item);

            database.Delete(project);
            EventAggregator.GetEvent<ClearCanvasEvent>().Publish();
        }
    }

    public class LoadProjectListEvent : PubSubEvent<ProjectProxy> { }
    public class ClearCanvasEvent : PubSubEvent { }
    public class FetchProjectsEvent : PubSubEvent<string> { }
    public class FetchDrawingComponentsEvent : PubSubEvent<List<DrawingComponentProxy>> { }
}
