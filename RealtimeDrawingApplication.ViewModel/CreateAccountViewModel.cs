﻿using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace RealtimeDrawingApplication.ViewModel
{
    public class CreateAccountViewModel : BindableBase
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private UserProxy _userProxy;
        private Window _mainWindowView;
        private Window _createAccountView;
        private bool _isPasswordVisible;
        private bool _isConfirmedPasswordVisible;


        public CreateAccountViewModel()
        {
            _mainWindowView = CommonUtility.GetPage("ApplicationStartupWindow") as Window;
            _createAccountView = CommonUtility.GetPage("CreateAccount") as Window;
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            CreateAccountCommand = new DelegateCommand<PasswordBox>(e => CreateAccount(e));
            ShowPasswordCommand = new DelegateCommand<PasswordBox>(e => ShowPassword(e));
            ShowConfirmPasswordCommand = new DelegateCommand<PasswordBox>(e => ShowConfirmPassword(e));
            CloseCommand = new DelegateCommand(CloseCreateAccountView);
            //EventAggregator.GetEvent<CloseLoginViewEvent>().Publish();
        }

        public string FirstName { get => _firstName; set { _firstName = value; RaisePropertyChanged(); } }
        public string Email { get => _email; set { _email = value; RaisePropertyChanged(); } }
        public string Password { get => _password; set { _password = value; RaisePropertyChanged(); } }
        public string ConfirmPassword { get => _confirmPassword; set { _confirmPassword = value; RaisePropertyChanged(); ValidatePassword(); } }
        public DelegateCommand<PasswordBox> CreateAccountCommand { get; set; }
        public DelegateCommand<PasswordBox> ShowPasswordCommand { get; set; }
        public DelegateCommand<PasswordBox> ShowConfirmPasswordCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public string LastName { get => _lastName; set { _lastName = value; RaisePropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }
        public bool IsPasswordVisible { get => _isPasswordVisible; set { _isPasswordVisible = value; RaisePropertyChanged(); } }
        public bool IsConfirmedPasswordVisible { get => _isConfirmedPasswordVisible; set { _isConfirmedPasswordVisible = value; RaisePropertyChanged(); } }

        public void ValidatePassword()
        {
            if (!string.Equals(_password, _confirmPassword))
            {
                MessageBox.Show("Password Verification Failed! Verified password do not match the provided password", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public void ShowPassword(PasswordBox passwordBox)
        {
            if (IsPasswordVisible)
            {
                IsPasswordVisible = false;
                return;
            }
            IsPasswordVisible = true;
            Password = passwordBox.Password;
        }
       
        public void ShowConfirmPassword(PasswordBox confirmPasswordBox)
        {
            if (IsConfirmedPasswordVisible)
            {
                IsConfirmedPasswordVisible = false;
                return;
            }
            IsConfirmedPasswordVisible = true;
            ConfirmPassword = confirmPasswordBox.Password;
        }

        public void CreateAccount(PasswordBox passwordBox)
        {
            _userProxy = new UserProxy();
            _password = passwordBox.Password;
            _userProxy.Email = _email;
            _userProxy.FirstName = _firstName;
            _userProxy.LastName = _lastName;
            _userProxy.FullName = _firstName + " " + _lastName;
            _userProxy.Password = _password;

            if (_email==null || _firstName==null || _lastName==null || _password==null)
            {
                MessageBox.Show("Incomplete Details Provided! Complete the Form", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isSavedSucessful = DatabaseServices.UserModelService.SaveToDatabase(_userProxy);

            if (isSavedSucessful)
            {
                _mainWindowView.DataContext = new MainWindowViewModel(_userProxy);

                MessageBox.Show("Account Created");
                CloseCreateAccountView();
                _mainWindowView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Provided Email has been registered!, Sign in or use another Email to create an account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        void CloseCreateAccountView()
        {
            EventAggregator.GetEvent<CloseCreateAccountViewEvent>().Publish();
        }

    }
}
