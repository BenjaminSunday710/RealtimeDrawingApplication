using Prism.Commands;
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
        private string _verifyPassword;
        private UserProxy _userProxy;
        private Window _mainWindowView;
        private Window _createAccountView;


        public CreateAccountViewModel()
        {
            _mainWindowView = CommonUtility.GetPage("ApplicationStartupWindow") as Window;
            _createAccountView = CommonUtility.GetPage("CreateAccount") as Window;
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            CreateAccountCommand = new DelegateCommand<PasswordBox>(e => CreateAccount(e));
        }

        public string FirstName { get => _firstName; set { _firstName = value; RaisePropertyChanged(); } }
        public string Email { get => _email; set { _email = value; RaisePropertyChanged(); } }
        public string Password { get => _password; set { _password = value; RaisePropertyChanged(); } }
        public string VerifyPassword { get => _verifyPassword; set { _verifyPassword = value; RaisePropertyChanged(); ValidatePassword(); } }
        public DelegateCommand<PasswordBox> CreateAccountCommand { get; set; }
        public DelegateCommand ShowPassword { get; set; }
        public string LastName { get => _lastName; set { _lastName = value; RaisePropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }

        public void ValidatePassword()
        {
            if (!string.Equals(_password, _verifyPassword))
            {
                MessageBox.Show("Password Verification Failed! Verified password do not match the provided password", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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

            bool isSavedSucessful = DatabaseServices.UserModelService.SaveToDatabase(_userProxy);

            if (isSavedSucessful)
            {
                _mainWindowView.DataContext = new MainWindowViewModel();

                if (_mainWindowView.IsInitialized)
                    PublishCurrentUserDetails();
                
                MessageBox.Show("Account Created");
                _mainWindowView.ShowDialog();
                CloseCreateAccountView();
            }
        }

        void PublishCurrentUserDetails()
        {
            EventAggregator.GetEvent<GetUserInstanceEvent>().Publish(_userProxy);
        }

        void CloseCreateAccountView()
        {
            _createAccountView.Close();
        }

    }

    public class GetUserInstanceEvent:PubSubEvent<UserProxy>{}
}
