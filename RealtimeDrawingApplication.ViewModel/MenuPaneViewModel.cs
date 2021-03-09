using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RealtimeDrawingApplication.ViewModel
{
    public class MenuPaneViewModel:BindableBase
    {
        private Window _createProjectWindow;
        private Window _sharedProjectWindow;
        private UserControl _menupaneControl;
        private string _userName;
        private string _userEmail;
        private string _projectName;
       

        public MenuPaneViewModel()
        {
            _menupaneControl = CommonUtility.GetPage("MenuPaneControl") as UserControl;
            CreateProjectCommand = new DelegateCommand(CreateProject);
            ShareProjectCommand = new DelegateCommand(ShareProject);
            OpenProjectCommand = new DelegateCommand(OpenProject);
            SaveProjectCommand = new DelegateCommand(SaveProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProject);
            ExportProjectCommand = new DelegateCommand(Export);
            ImportProjectCommand = new DelegateCommand(Import);
            CloseCommand = new DelegateCommand(Close);
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<ShowCurrentUserDetailsEvent>().Subscribe(DisplayCurrentUser);
            EventAggregator.GetEvent<OpenProjectShareUsersEvent>().Subscribe(ShareProject);
            EventAggregator.GetEvent<GetProjectInstanceEvent>().Subscribe(SetCurrentProject);
        }

        public IEventAggregator EventAggregator { get; set; }
        public DelegateCommand CreateProjectCommand { get; set; }
        public DelegateCommand ShareProjectCommand { get; set; }
        public DelegateCommand OpenProjectCommand { get; set; }
        public DelegateCommand SaveProjectCommand { get; set; }
        public DelegateCommand DeleteProjectCommand { get; set; }
        public DelegateCommand ExportProjectCommand { get; set; }
        public DelegateCommand ImportProjectCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public string UserName { get => _userName; set { _userName = value; RaisePropertyChanged(); } }
        public string UserEmail { get => _userEmail; set { _userEmail = value; RaisePropertyChanged(); } }

        void DisplayCurrentUser(UserProxy currentUser)
        {
            UserEmail = currentUser.Email;
            UserName = currentUser.FullName;
        }

        void SetCurrentProject(string currentProject)
        {
            _projectName = currentProject;
        }

        void CreateProject()
        {
            _createProjectWindow = CommonUtility.GetPage("CreateProject") as Window;
            _createProjectWindow.DataContext = new CreateProjectViewModel(_createProjectWindow,_userEmail);
            _createProjectWindow.ShowDialog();
        }

        void ShareProject()
        {
            _sharedProjectWindow = CommonUtility.GetPage("SharedProjectWindow") as Window;
            _sharedProjectWindow.DataContext = new SharedProjectViewModel(_sharedProjectWindow, _projectName);
            _sharedProjectWindow.ShowDialog();
        }

        void OpenProject()
        {
            EventAggregator.GetEvent<FetchProjectsEvent>().Publish(_userEmail);
        }

        void SaveProject()
        {
            EventAggregator.GetEvent<SaveProjectEvent>().Publish(_projectName);
        }

        void DeleteProject()
        {
            EventAggregator.GetEvent<DeleteProjectEvent>().Publish();
        }

        void Export()
        {
            //Convert the Drawing components to string using Json and Xml and write it to a file
        }

        void Import()
        {
            //Read Drawing components from a file and convert it to drawing component model
        }

        private void Close()
        {
            EventAggregator.GetEvent<CloseMenuPaneEvent>().Publish();
        }

    }

    public class SaveProjectEvent : PubSubEvent<string> { }
    //public class CreateProjectEvent : PubSubEvent<CreateProjectViewModel> { }
    public class DeleteProjectEvent : PubSubEvent { }
    public class CloseMenuPaneEvent : PubSubEvent { }
}
