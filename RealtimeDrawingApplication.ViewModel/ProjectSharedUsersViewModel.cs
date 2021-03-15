using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System.Windows;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;

namespace RealtimeDrawingApplication.ViewModel
{
    public class ProjectSharedUsersViewModel : BindableBase
    {
        private ProjectProxy _projectProxy;
        private UserProxy _projectSharedUser;
        private ProjectSharedUserProxy _projectSharedUserProxy;
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

        public ProjectProxy Project { get => _projectProxy; set { _projectProxy = value; RaisePropertyChanged(); } }
        public UserProxy ProjectSharedUser { get => _projectSharedUser; set { _projectSharedUser = value; RaisePropertyChanged(); } }
        public bool IsEditable { get => _isEditable; set { _isEditable = value; Update();RaisePropertyChanged(); } }
        public ObservableCollection<UserProxy> ProjectSharedUsersList { get => _projectSharedUsersList; set => _projectSharedUsersList = value; }
        public IEventAggregator EventAggregator { get; set; }
        public DelegateCommand OpenShareProjectWindowCommand{ get; set; }
        public DelegateCommand RemoveSharedUserCommand { get; set; }
        public UserProxy SelectedSharedUser { get => _selectedSharedUser; set { _selectedSharedUser = value; RaisePropertyChanged(); } }

        void LoadProjectSharedUserList(LoadProjectSharedUsersEventModel projectSharedUser)
        {
            _projectSharedUser = DatabaseServices.UserModelService.DeserializeToProxy(projectSharedUser.SharedUserEmail);
            _projectSharedUsersList.Add(_projectSharedUser);
        }

        void Update()
        {
            var projectSharedUserModel = database.GetProjectSharedUser(_projectSharedUser.Email);
            _projectSharedUserProxy.IsEditable = true;
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
