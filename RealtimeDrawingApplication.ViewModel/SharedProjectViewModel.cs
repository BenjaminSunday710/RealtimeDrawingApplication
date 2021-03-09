﻿using Prism.Commands;
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
using System.Windows.Controls;

namespace RealtimeDrawingApplication.ViewModel
{
    public class SharedProjectViewModel:BindableBase
    {
        private bool _isAllowed;
        private string _sharedUserEmail;
        private string _projectName;
        private ProjectModel _sharedProject;
        private UserModel _sharedUser;
        private Window _sharedProjectWindowView;
        private ProjectSharedUserProxy _projectSharedUserProxy;

        public SharedProjectViewModel(Window sharedProjectWindow, string projectName)
        {
            _sharedProjectWindowView = sharedProjectWindow;
            
            _projectName = projectName;

            ShareCommand = new DelegateCommand(Share);
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
        }

        public IEventAggregator EventAggregator { get; set; }
        public string SharedUserEmail { get => _sharedUserEmail; set { _sharedUserEmail = value; RaisePropertyChanged(); } }
        public DelegateCommand ShareCommand { get; set; }
        public bool IsAllowed { get => _isAllowed; set { _isAllowed = value; RaisePropertyChanged(); } }
        public ProjectSharedUsersEventModel ProjectSharedUsers { get; set; } = new ProjectSharedUsersEventModel();

        void GetProjectInstance()
        {
            _sharedProject = DatabaseServices.Repository<ProjectModel>.Database.GetProject(_projectName);

            if (_sharedProject == null)
            {
                MessageBox.Show("Error Occurred during the sharing process \nSave Project before sharing", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Share()
        {
            _projectSharedUserProxy = new ProjectSharedUserProxy();
            _sharedUser = DatabaseServices.Repository<UserModel>.Database.GetUser(_sharedUserEmail);

            if (_sharedUser == null)
            {
                MessageBox.Show("Unable to share!! Provided Email is not a registered Account", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _isAllowed = true;
            GetProjectInstance();
            PublishSharedProject();
            SaveSharedUser();
            CloseSharedProjectWindow();
        }

        void PublishSharedProject()
        {
            ProjectSharedUsers.SharedUserEmail = _sharedUserEmail;
            ProjectSharedUsers.ProjectName = _sharedProject;
            EventAggregator.GetEvent<SetProjectShareUsersEvent>().Publish(ProjectSharedUsers);
        }

        void SaveSharedUser()
        {
            _projectSharedUserProxy.ProjectSharedUser = _sharedUser;
            _projectSharedUserProxy.Project = _sharedProject;
            _projectSharedUserProxy.IsAllowed = _isAllowed;

            DatabaseServices.ProjectSharedUsersModelService.SaveToDatabase(_projectSharedUserProxy);

            MessageBox.Show("Project has been shared Sucessfuly", "Notification Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void CloseSharedProjectWindow()
        {
            _sharedProjectWindowView.Close();
        }
    }
}
