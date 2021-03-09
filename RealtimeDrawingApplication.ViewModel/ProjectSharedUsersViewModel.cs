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

namespace RealtimeDrawingApplication.ViewModel
{
    public class ProjectSharedUsersViewModel : BindableBase
    {
        private ProjectProxy _projectProxy;
        private UserProxy _projectSharedUser;
        private bool _isEditable;
        private ObservableCollection<UserProxy> _projectSharedUsersList;

        public ProjectSharedUsersViewModel()
        {
            _projectSharedUsersList = new ObservableCollection<UserProxy>();
            OpenShareProjectWindowCommand = new DelegateCommand(OpenShareProjectWindow);

            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<SetProjectShareUsersEvent>().Subscribe(LoadProjectSharedUser);
        }

        public ProjectProxy Project { get => _projectProxy; set { _projectProxy = value; RaisePropertyChanged(); } }
        public UserProxy ProjectSharedUser { get => _projectSharedUser; set { _projectSharedUser = value; RaisePropertyChanged(); } }
        public bool IsEditable { get => _isEditable; set { _isEditable = value; RaisePropertyChanged(); } }
        public ObservableCollection<UserProxy> ProjectSharedUsersList { get => _projectSharedUsersList; set => _projectSharedUsersList = value; }
        public IEventAggregator EventAggregator { get; set; }
        public DelegateCommand OpenShareProjectWindowCommand{ get; set; }

        void LoadProjectSharedUser(ProjectSharedUsersEventModel projectSharedUser)
        {
            _projectSharedUser = DatabaseServices.UserModelService.DeserializeToProxy(projectSharedUser.SharedUserEmail);
            _projectSharedUsersList.Add(_projectSharedUser);
        }

        void OpenShareProjectWindow()
        {
            EventAggregator.GetEvent<OpenProjectShareUsersEvent>().Publish();  
        }
    }

    public class SetProjectShareUsersEvent : PubSubEvent<ProjectSharedUsersEventModel> { }

    public class OpenProjectShareUsersEvent : PubSubEvent { }

    public class ProjectSharedUsersEventModel
    {
        public string SharedUserEmail { get; set; }
        public ProjectModel ProjectName { get; set; }
    }
}
