using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System.Windows.Media;
using RealtimeDrawingApplication.ViewModel.DataTransferProtocol;
using RealtimeDrawingApplication.Model;

namespace RealtimeDrawingApplication.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        UserControl _menuPaneControl;
        CustomCanvas _drawingSheet;
        PropertySharedUsersProjectWindowsDisplay _routedPagesModel;
        private string _userEmail;
        private string _userName;
        private string _activeProject;
        private bool _isVisible;
        private Dictionary<string, FrameworkElement> _routedPages;
        private bool _isOpen;

        public MainWindowViewModel(UserProxy currentUser)
        {
            _userEmail = currentUser.Email;
            _userName = currentUser.FullName;

            _routedPages = new Dictionary<string, FrameworkElement>();
            _routedPages.Add("PropertyWindowControl", CommonUtility.GetPage("PropertyWindowControl") as FrameworkElement);
            _routedPages.Add("ProjectWindow", CommonUtility.GetPage("ProjectWindow") as FrameworkElement);
            _routedPages.Add("ProjectSharedUserWindowControl", CommonUtility.GetPage("ProjectSharedUserWindowControl") as FrameworkElement);

            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();

            _menuPaneControl = CommonUtility.GetPage("MenuPaneControl") as UserControl;
            _menuPaneControl.DataContext = new MenuPaneViewModel(currentUser);

            EventAggregator.GetEvent<CloseMenuPaneEvent>().Subscribe(CloseMenuPaneControl);

            _routedPagesModel = new PropertySharedUsersProjectWindowsDisplay(_routedPages);

            DrawingSheet = new CustomCanvas();

            OpenMenuPaneControlCommand = new DelegateCommand(OpenMenuPaneControl);
            OpenAddShareUserWindowCommand = new DelegateCommand(OpenAddShareUserWindow);
            ExportProjectAsJsonCommand = new DelegateCommand(ExportProjectAsJson);
            ExportProjectAsXmlCommand = new DelegateCommand(ExportProjectAsXml);
            SharePopupIsOpenCommand = new DelegateCommand(SharePopupIsOpen);

            EventAggregator.GetEvent<SaveProjectEvent>().Subscribe(SaveDrawingComponents);

            EventAggregator.GetEvent<FetchProjectsEvent>().Publish(_userEmail);

            EventAggregator.GetEvent<GetProjectInstanceEvent>().Subscribe(SetActiveProject);
            EventAggregator.GetEvent<CloseLoginViewEvent>().Publish();
        }

        public CustomCanvas DrawingSheet { get => _drawingSheet; set { _drawingSheet = value; } }
        public DelegateCommand OpenMenuPaneControlCommand { get; set; }
        public DelegateCommand ExportProjectAsJsonCommand { get; set; }
        public DelegateCommand ExportProjectAsXmlCommand { get; set; }
        public DelegateCommand OpenAddShareUserWindowCommand { get; set; }
        public DelegateCommand SharePopupIsOpenCommand { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public bool IsVisible { get => _isVisible; set { _isVisible = value; RaisePropertyChanged(); } }
        public PropertySharedUsersProjectWindowsDisplay RoutedPagesModel { get => _routedPagesModel; set { _routedPagesModel = value; RaisePropertyChanged(); } }
        public string ActiveProject { get => _activeProject; set { _activeProject = value; RaisePropertyChanged(); } }
        public UserControl MenupaneControl { get => _menuPaneControl; set { _menuPaneControl = value; RaisePropertyChanged(); } }
        public bool IsOpen { get => _isOpen; set { _isOpen = value; RaisePropertyChanged(); } }

        void OpenMenuPaneControl()
        {
           IsVisible = true;
        }

        void CloseMenuPaneControl()
        {
            IsVisible = false;
        }

        private void SaveDrawingComponents(string projectName)
        {
            _drawingSheet.SaveDrawingComponents(projectName);
        }

        private void SetActiveProject(string projectName)
        {
            ActiveProject = projectName;
        }

        void ExportProjectAsJson()
        {
            DataTransferServices.SerialiseObjectToJson(_activeProject);
        }

        void ExportProjectAsXml()
        {
            DataTransferServices.SerializedObjectToXml(_activeProject);
        }

        void OpenAddShareUserWindow()
        {
            var sharedProjectWindow = CommonUtility.GetPage("SharedProjectWindow") as Window;
            sharedProjectWindow.DataContext = new SharedProjectViewModel(sharedProjectWindow, _activeProject);
            sharedProjectWindow.ShowDialog();
        }

        void SharePopupIsOpen()
        {
           IsOpen=!IsOpen;
        }



    }

    public class CloseLoginViewEvent : PubSubEvent { }
}
