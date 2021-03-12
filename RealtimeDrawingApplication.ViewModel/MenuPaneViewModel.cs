using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.ViewModel.DataTransferProtocol;
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
    public class MenuPaneViewModel : BindableBase
    {
        private Window _createProjectWindow;
        private Window _sharedProjectWindow;
        private UserControl _menupaneControl;
        private string _userName;
        private string _userEmail;
        private string _projectName;
        private bool _exportPopUpIsOpen;
        private bool _importPopUpIsOpen;

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
            ImportAsJsonCommand = new DelegateCommand(ImportAsJson);
            ExportAsJsonCommand = new DelegateCommand(ExportAsJson);
            ImportAsXmlCommand = new DelegateCommand(ImportAsXml);
            ExportAsXmlCommand = new DelegateCommand(ExportAsXml);
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
        public DelegateCommand ExportAsJsonCommand { get; set; }
        public DelegateCommand ImportAsJsonCommand { get; set; }
        public DelegateCommand ExportAsXmlCommand { get; set; }
        public DelegateCommand ImportAsXmlCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public string UserName { get => _userName; set { _userName = value; RaisePropertyChanged(); } }
        public string UserEmail { get => _userEmail; set { _userEmail = value; RaisePropertyChanged(); } }
        public bool ExportPopUpIsOpen { get => _exportPopUpIsOpen; set { _exportPopUpIsOpen = value; RaisePropertyChanged(); } }
        public bool ImportPopUpIsOpen { get => _importPopUpIsOpen; set { _importPopUpIsOpen = value; RaisePropertyChanged(); } }

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
            _createProjectWindow.DataContext = new CreateProjectViewModel(_createProjectWindow, _userEmail);
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
            ExportPopUpIsOpen = !ExportPopUpIsOpen;
        }

        void Import()
        {
            ImportPopUpIsOpen = !_importPopUpIsOpen;
        }

        private void Close()
        {
            EventAggregator.GetEvent<CloseMenuPaneEvent>().Publish();
        }

        private void ImportAsJson()
        {
            EventAggregator.GetEvent<ImportFileEvent>().Publish("Json");
        }

        private void ExportAsJson()
        {
            DataTransferServices.SerialiseObjectToJson(_projectName);
        }

        private void ImportAsXml()
        {
            EventAggregator.GetEvent<ImportFileEvent>().Publish("Xml");
        }

        private void ExportAsXml()
        {
            DataTransferServices.SerializedObjectToXml(_projectName);
        }
    }

    public class SaveProjectEvent : PubSubEvent<string> { }
    public class ImportFileEvent : PubSubEvent<string> { }
    public class DeleteProjectEvent : PubSubEvent { }
    public class CloseMenuPaneEvent : PubSubEvent { }
}
