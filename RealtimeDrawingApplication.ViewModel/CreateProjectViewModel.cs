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

namespace RealtimeDrawingApplication.ViewModel
{
    public class CreateProjectViewModel:BindableBase
    {
        //private static ObservableCollection<ProjectProxy> _projectListItems;
        private ProjectProxy _projectProxy;
        private string _userEmail;
        private string _name;
        private Window _createProjectWindowView;
        //private Window _mainWindowView;

        public CreateProjectViewModel(Window createProjectWindow, string userEmail)
        {
            _createProjectWindowView = createProjectWindow;
            CreateProjectCommand = new DelegateCommand(CreateProject);
            CloseProjectViewCommand = new DelegateCommand(CloseProjectView);
            _userEmail = userEmail;

            //EventAggregator.GetEvent<CreateProjectEvent>().Subscribe(InitialiseProject);

            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            //EventAggregator.GetEvent<ShowCurrentUserDetailsEvent>().Subscribe(GetUserInstance);



            //_mainWindowView = CommonUtility.GetPage("ApplicationStartupWindow") as Window;
        }

        public string Name { get => _name; set { _name = value; RaisePropertyChanged(); } }
        public ProjectProxy SelectedProject { get => _projectProxy; set { _projectProxy = value; RaisePropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }
        public string SharedUserEmail { get => _userEmail; set => _userEmail = value; }
        public DelegateCommand CreateProjectCommand { get; set; }
        public DelegateCommand CloseProjectViewCommand { get; set; }
        //public ProjectInstanceEventModel CurrentProjectInstance { get; set; } = new ProjectInstanceEventModel();

        //private void GetUserInstance(UserInstanceEventModel currentUser)
        //{
        //    _userEmail = currentUser.Email;
        //}

        //void InitialiseProject(CreateProjectViewModel projectInstance)
        //{
        //    //_projectWindowView.DataContext = projectInstance;
           
        //}

        void CreateProject()
        {
            _projectProxy = new ProjectProxy();
            _projectProxy.Name = _name;
            _projectProxy.ProjectCreator = DatabaseServices.Repository<UserModel>.Database.GetUser(_userEmail);

            EventAggregator.GetEvent<GetProjectInstanceEvent>().Publish(_name);

            EventAggregator.GetEvent<LoadProjectListEvent>().Publish(_projectProxy);

            DatabaseServices.ProjectModelService.SaveToDatabase(_projectProxy);
            MessageBox.Show("Project Created Succesfully", "Notification Message", MessageBoxButton.OK, MessageBoxImage.Hand);

            _createProjectWindowView.Close();
        }

        void CloseProjectView()
        {
            _createProjectWindowView.Close();   
        }
    }

    public class GetProjectInstanceEvent : PubSubEvent<string> { }
    public class ProjectInstanceEventModel
    {
        public string Name { get; set; }
    }
}
