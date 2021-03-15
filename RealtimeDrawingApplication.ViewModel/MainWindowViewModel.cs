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
        private Visibility _isVisible = Visibility.Collapsed;
        private Dictionary<string, FrameworkElement> _routedPages;

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

            EventAggregator.GetEvent<SaveProjectEvent>().Subscribe(SaveDrawingComponents);

            EventAggregator.GetEvent<FetchProjectsEvent>().Publish(_userEmail);

            EventAggregator.GetEvent<GetProjectInstanceEvent>().Subscribe(SetActiveProject);
        }

        public CustomCanvas DrawingSheet { get => _drawingSheet; set { _drawingSheet = value; } }
        public DelegateCommand OpenMenuPaneControlCommand { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public Visibility IsVisible { get => _isVisible; set { _isVisible = value; RaisePropertyChanged(); } }
        public PropertySharedUsersProjectWindowsDisplay RoutedPagesModel { get => _routedPagesModel; set { _routedPagesModel = value; RaisePropertyChanged(); } }
        public string ActiveProject { get => _activeProject; set { _activeProject = value; RaisePropertyChanged(); } }

        void OpenMenuPaneControl()
        {
            _menuPaneControl.Visibility = Visibility.Visible;
            //IsVisible = Visibility.Visible;
        }

        void CloseMenuPaneControl()
        {
            _menuPaneControl.Visibility = Visibility.Collapsed;
            //IsVisible = Visibility.Collapsed;
        }

        private void SaveDrawingComponents(string projectName)
        {
            _drawingSheet.SaveDrawingComponents(projectName);
        }

        private void SetActiveProject(string projectName)
        {
            _activeProject = projectName;
        }

    }
}
