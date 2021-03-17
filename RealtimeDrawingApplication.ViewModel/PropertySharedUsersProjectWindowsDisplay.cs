using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using RealtimeDrawingApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealtimeDrawingApplication.ViewModel
{
    public class PropertySharedUsersProjectWindowsDisplay : BindableBase
    {
        private FrameworkElement currentContent;
        private bool _isPropertyWindow;
        private bool _isProjectSharedUserWindow;
        private bool _isProjectsWindow;
        private string _windowTitle = "Property Window";
        private Dictionary<string, FrameworkElement> _routedPages;

        public PropertySharedUsersProjectWindowsDisplay( Dictionary<string,FrameworkElement> routedPages)
        {
            _routedPages = routedPages;
            currentContent = _routedPages["PropertyWindowControl"];
            WindowTitle = "Property Window";

            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<OpenProjectWindowEvent>().Subscribe(OpenProjectWindow);
            EventAggregator.GetEvent<OpenShareProjectWindowEvent>().Subscribe(OpenProjectSharedUserWindow);
        }

        //public Dictionary<string, FrameworkElement> RoutedPages { get; }
        public bool IsPropertyWindow { get => _isPropertyWindow; set { _isPropertyWindow = value; UpdateLayoutControl(); RaisePropertyChanged(); } }
        public bool IsProjectSharedUserWindow { get => _isProjectSharedUserWindow; set { _isProjectSharedUserWindow = value; UpdateLayoutControl(); RaisePropertyChanged(); } }
        public bool IsProjectsWindow { get => _isProjectsWindow; set { _isProjectsWindow = value; UpdateLayoutControl(); RaisePropertyChanged(); } }
        public FrameworkElement CurrentContent{get => currentContent; set { currentContent = value; RaisePropertyChanged(); } }
        public string WindowTitle{ get => _windowTitle; set { _windowTitle = value; RaisePropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }

        private void UpdateLayoutControl()
        {
            if (_isProjectsWindow)
            {
                OpenProjectWindow();
            }

            else if (_isProjectSharedUserWindow)
            {
                OpenProjectSharedUserWindow();
            }

            else
            {
                OpenPropertyWindow();  
            }
        }

        void OpenProjectWindow()
        {
            if (_routedPages.ContainsKey("ProjectWindow"))
            {
                CurrentContent = _routedPages["ProjectWindow"];
                WindowTitle = "Project Window";
            }
        }

        void OpenProjectSharedUserWindow()
        {
            if (_routedPages.ContainsKey("ProjectSharedUserWindowControl"))
            {
                CurrentContent = _routedPages["ProjectSharedUserWindowControl"];
                WindowTitle = "Shared User Window";
            }
        }

        void OpenPropertyWindow()
        {
            if (_routedPages.ContainsKey("PropertyWindowControl"))
            {
                CurrentContent = _routedPages["PropertyWindowControl"];
                WindowTitle = "Property Window";
            }
        }
    }
}
      

      
