using Prism.Mvvm;
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
        private bool _isSharedUserWindow;
        private bool _isProjectsWindow;
        private string _windowTitle = "Property Window";
        private Dictionary<string, FrameworkElement> _routedPages;

        public PropertySharedUsersProjectWindowsDisplay( Dictionary<string,FrameworkElement> routedPages)
        {
            _routedPages = routedPages;
            currentContent = _routedPages["PropertyWindowControl"];
            WindowTitle = "Property Window";
        }

        //public Dictionary<string, FrameworkElement> RoutedPages { get; }
        public bool IsPropertyWindow { get => _isPropertyWindow; set { _isPropertyWindow = value; UpdateLayoutControl(); RaisePropertyChanged(); } }
        public bool IsSharedUserWindow { get => _isSharedUserWindow; set { _isSharedUserWindow = value; UpdateLayoutControl(); RaisePropertyChanged(); } }
        public bool IsProjectsWindow { get => _isProjectsWindow; set { _isProjectsWindow = value; UpdateLayoutControl(); RaisePropertyChanged(); } }
        public FrameworkElement CurrentContent
        {
            get { return currentContent; }
            set { currentContent = value; RaisePropertyChanged(); }
        }

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; RaisePropertyChanged(); }
        }

        private void UpdateLayoutControl()
        {
            if (_isProjectsWindow)
            {
                if (_routedPages.ContainsKey("ProjectWindow"))
                {
                    CurrentContent = _routedPages["ProjectWindow"];
                    WindowTitle = "Project Window";
                }
            }

            else if (_isSharedUserWindow)
            {
                if (_routedPages.ContainsKey("SharedUserWindowControl"))
                {
                    CurrentContent = _routedPages["SharedUserWindowControl"];
                    WindowTitle = "Shared User Window";
                }
            }

            else
            {
                if (_routedPages.ContainsKey("PropertyWindowControl"))
                {
                    CurrentContent = _routedPages["PropertyWindowControl"];
                    WindowTitle = "Property Window";
                }
            }
        }
    }
}
      

      
