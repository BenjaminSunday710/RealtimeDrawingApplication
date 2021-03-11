using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using RealtimeDrawingApplication.ViewModel.Proxies;

namespace RealtimeDrawingApplication.ViewModel
{
    public class LoginViewModel:BindableBase
    {
        private string _email;
        private string _password;
        private Window _createAccountView;
        private Window _loginView;
        Visibility _isVisible;
        Window _mainWindowView;
        UserProxy _user;

        public LoginViewModel()
        {
            _loginView = CommonUtility.GetPage("LogInPageWindow") as Window;
            LoginCommand = new DelegateCommand<PasswordBox>(Login);
            ShowPasswordCommand = new DelegateCommand<PasswordBox>(ShowPassword);
            OpenCreateAccountViewCommand = new DelegateCommand(OpenCreateAccountView);
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
        }

        public DelegateCommand<PasswordBox> LoginCommand { get; set; }
        public DelegateCommand<PasswordBox> ShowPasswordCommand { get; set; }
        public DelegateCommand OpenCreateAccountViewCommand { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public string Email { get => _email; set{_email = value; RaisePropertyChanged(); } }
        public string Password { get => _password; set { _password = value; RaisePropertyChanged(); } }
        public Visibility IsVisible { get => _isVisible; set { _isVisible = value; RaisePropertyChanged(); } }

        void Login(PasswordBox passwordBox)
        {
            _password = passwordBox.Password;
            _user = DatabaseServices.UserModelService.DeserializeToProxy(_email);
           
            if (_user == null)
            {
                MessageBox.Show("Error! Couldn't find account with the provided information! Create an account","Error Message",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            else if (_user.Password != _password)
            {
                MessageBox.Show("Provided Password is Incorrect", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _user.FullName = _user.FirstName + " " + _user.LastName;
            _mainWindowView = CommonUtility.GetPage("ApplicationStartupWindow") as Window;
            _mainWindowView.DataContext = new MainWindowViewModel();

            if (_mainWindowView.IsInitialized)
            {
                EventAggregator.GetEvent<GetUserInstanceEvent>().Publish(_user);

                _mainWindowView.ShowDialog();
            }
        }

        void ShowPassword(PasswordBox passwordBox)
        {
            if(IsVisible == Visibility.Visible)
            {
                IsVisible = Visibility.Collapsed;
                return;
            }
            IsVisible = Visibility.Visible;
            Password = passwordBox.Password;
        }

        void OpenCreateAccountView()
        {
            _createAccountView = CommonUtility.GetPage("CreateAccount") as Window;
            _createAccountView.DataContext = new CreateAccountViewModel();

            if (_createAccountView.IsInitialized)
            {
                //_loginView.Close();
                _createAccountView.ShowDialog();
            }
        }
    }

}
