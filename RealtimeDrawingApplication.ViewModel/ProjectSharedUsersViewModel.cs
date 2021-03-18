using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.Model;
using System.Collections.ObjectModel;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System.Windows;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RealtimeDrawingApplication.ViewModel
{
    public class ProjectSharedUsersViewModel : INotifyPropertyChanged
    {
        //private ProjectProxy _projectProxy;
        private UserProxy _projectSharedUser;
        //private ProjectSharedUserProxy _projectSharedUserProxy;
        private bool _isEditable;
        private ObservableCollection<UserProxy> _projectSharedUsersList;
        private UserProxy _selectedSharedUser;
        private static Repository<ProjectSharedUsersModel> database = Repository<ProjectSharedUsersModel>.GetRepository;

        public ProjectSharedUsersViewModel()
        {
            _projectSharedUsersList = new ObservableCollection<UserProxy>();
            OpenShareProjectWindowCommand = new DelegateCommand(OpenShareProjectWindow);
            RemoveSharedUserCommand = new DelegateCommand(RemoveSharedUser);

            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<LoadProjectShareUsersListEvent>().Subscribe(LoadProjectSharedUserList);
        }

        //public ProjectProxy Project { get => _projectProxy; set { _projectProxy = value; RaisePropertyChanged(); } }
        //public UserProxy ProjectSharedUser { get => _projectSharedUser; set =>_projectSharedUser=value; } 
        public bool IsEditable { get => _isEditable; set { _isEditable = value; OnPropertyChanged(); } }
        public UserProxy SelectedSharedUser { get => _selectedSharedUser; set { _selectedSharedUser = value; } }

        public ObservableCollection<UserProxy> ProjectSharedUsersList { get => _projectSharedUsersList; set => _projectSharedUsersList = value; }
        public IEventAggregator EventAggregator { get; set; }
        public DelegateCommand OpenShareProjectWindowCommand{ get; set; }
        public DelegateCommand RemoveSharedUserCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = " ")
        {
            if (IsEditable) Update();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void LoadProjectSharedUserList(LoadProjectSharedUsersEventModel projectSharedUser)
        {
            _projectSharedUser = DatabaseServices.UserModelService.DeserializeToProxy(projectSharedUser.SharedUserEmail);
            _projectSharedUsersList.Add(_projectSharedUser);
        }

        void Update()
        {
            var projectSharedUserModel = database.GetProjectSharedUser(_projectSharedUser.Email);
            projectSharedUserModel.IsEditable = true;
            database.Update(projectSharedUserModel);
        }

        void RemoveSharedUser()
        {
            var response = MessageBox.Show("Do you want to delete the selected shared user?", "Notification", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {
                return;
            }

            var projectSharedUserModel = database.GetProjectSharedUser(_selectedSharedUser.Email);
            _projectSharedUsersList.Remove(_selectedSharedUser);
            database.Delete(projectSharedUserModel);
        }

        void OpenShareProjectWindow()
        {
            EventAggregator.GetEvent<OpenShareProjectWindowEvent>().Publish();  
        }
    }

    public class LoadProjectShareUsersListEvent : PubSubEvent<LoadProjectSharedUsersEventModel> { }

    public class OpenShareProjectWindowEvent : PubSubEvent { }

    public class LoadProjectSharedUsersEventModel
    {
        public string SharedUserEmail { get; set; }
        public ProjectModel ProjectName { get; set; }
    }
}
