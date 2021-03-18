using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;

namespace RealtimeDrawingApplication.ViewModel
{
    public class CreateProjectViewModel:BindableBase
    {
        private ProjectProxy _projectProxy;
        private string _userEmail;
        private string _name;
        private Window _createProjectWindowView;
        private static Repository<ProjectModel> database = Repository<ProjectModel>.GetRepository;

        public CreateProjectViewModel(Window createProjectWindow, string userEmail)
        {
            _createProjectWindowView = createProjectWindow;
            CreateProjectCommand = new DelegateCommand(CreateProject);
            CloseProjectViewCommand = new DelegateCommand(CloseProjectView);
            _userEmail = userEmail;

            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
        }

        public string Name { get => _name; set { _name = value; RaisePropertyChanged(); } }
        public ProjectProxy SelectedProject { get => _projectProxy; set { _projectProxy = value; RaisePropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }
        public string SharedUserEmail { get => _userEmail; set => _userEmail = value; }
        public DelegateCommand CreateProjectCommand { get; set; }
        public DelegateCommand CloseProjectViewCommand { get; set; }

        void CreateProject()
        {
            _projectProxy = new ProjectProxy();
            _projectProxy.Name = _name;
            _projectProxy.ProjectCreator = database.GetUser(_userEmail);

            EventAggregator.GetEvent<GetProjectInstanceEvent>().Publish(_name);

            EventAggregator.GetEvent<LoadProjectListEvent>().Publish(_projectProxy);

            DatabaseServices.ProjectModelService.SaveToDatabase(_projectProxy);
            MessageBox.Show("Project Created Succesfully", "Notification Message", MessageBoxButton.OK, MessageBoxImage.Information);

            _createProjectWindowView.Close();
        }

        void CloseProjectView()
        {
            _createProjectWindowView.Close();   
        }
    }
}
