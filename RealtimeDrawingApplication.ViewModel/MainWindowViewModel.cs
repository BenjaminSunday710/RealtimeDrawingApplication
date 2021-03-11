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
        private Visibility _isVisible = Visibility.Collapsed;
        private Dictionary<string, FrameworkElement> _routedPages;

        public MainWindowViewModel()
        {
            _routedPages = new Dictionary<string, FrameworkElement>();
            _routedPages.Add("PropertyWindowControl", CommonUtility.GetPage("PropertyWindowControl") as FrameworkElement);
            _routedPages.Add("ProjectWindow", CommonUtility.GetPage("ProjectWindow") as FrameworkElement);
            _routedPages.Add("ProjectSharedUserWindowControl", CommonUtility.GetPage("ProjectSharedUserWindowControl") as FrameworkElement);

            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<GetUserInstanceEvent>().Subscribe(SetCurrentUser);

            _menuPaneControl = CommonUtility.GetPage("MenuPaneControl") as UserControl;
            _menuPaneControl.DataContext = new MenuPaneViewModel();

            EventAggregator.GetEvent<CloseMenuPaneEvent>().Subscribe(CloseMenuPaneControl);

            _routedPagesModel = new PropertySharedUsersProjectWindowsDisplay(_routedPages);

            DrawingSheet = new CustomCanvas();

            OpenMenuPaneControlCommand = new DelegateCommand(OpenMenuPaneControl);

            EventAggregator.GetEvent<SaveProjectEvent>().Subscribe(SaveDrawingComponents);
            EventAggregator.GetEvent<ExportFileEvent>().Subscribe(ExportDrawingComponents);

        }

        private void SaveDrawingComponents(string projectName)
        {
            _drawingSheet.SaveDrawingComponents(projectName);
        }

        public CustomCanvas DrawingSheet { get => _drawingSheet; set { _drawingSheet = value; } }
        public DelegateCommand OpenMenuPaneControlCommand { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public Visibility IsVisible { get => _isVisible; set { _isVisible = value; RaisePropertyChanged(); } }
        public UserControl MenuPaneControl { get => _menuPaneControl; set { _menuPaneControl = value; RaisePropertyChanged(); } }

        public PropertySharedUsersProjectWindowsDisplay RoutedPagesModel { get => _routedPagesModel; set { _routedPagesModel = value; RaisePropertyChanged(); } }
        public UserProxy CurrentUser { get; set; } = new UserProxy();

        void OpenMenuPaneControl()
        {
            CurrentUser.Email = _userEmail;
            CurrentUser.FullName = _userName;
            EventAggregator.GetEvent<ShowCurrentUserDetailsEvent>().Publish(CurrentUser);

            IsVisible = Visibility.Visible;
        }

        void SetCurrentUser(UserProxy currentUser)
        {
            _userEmail = currentUser.Email;
            _userName = currentUser.FullName;
        }

        void CloseMenuPaneControl()
        {
            IsVisible = Visibility.Collapsed;
        }

       

        //void DrawComponents(List<DrawingComponentProxy> drawingComponents)
        //{
        //    _drawingSheet.DrawComponents(drawingComponents);  
        //}

        void ExportDrawingComponents(string projectName)
        {
            var drawingComponents = DatabaseServices.Repository<DrawingComponentModel>.Database.GetDrawingComponents(projectName);
            var exportFile = Json<DrawingComponentModel>.SerialisedObject(drawingComponents);
            FileHandlingServices.SaveFile(exportFile);
        }
    }

    public class ShowCurrentUserDetailsEvent : PubSubEvent<UserProxy> { }
}
