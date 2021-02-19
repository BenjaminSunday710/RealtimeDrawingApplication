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
        private FrameworkElement currentContent =new PropertySharedUsersProjectWindowsDisplay().RoutedPages["PropertyWindowControl"];
        private bool _isPropertyWindow;
        private bool _isSharedUserWindow;
        private bool _isProjectsWindow;
        private string _windowTitle = "Property Window";
        //private Dictionary<string, FrameworkElement> _routedPages;

        public PropertySharedUsersProjectWindowsDisplay()
        {
            RoutedPages = new Dictionary<string, FrameworkElement>();
        }

        public Dictionary<string, FrameworkElement> RoutedPages { get; }
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
                if (RoutedPages.ContainsKey("ProjectWindow"))
                {
                    CurrentContent = RoutedPages["ProjectWindow"];
                    WindowTitle = "Project Window";
                }
            }

            else if (_isSharedUserWindow)
            {
                if (RoutedPages.ContainsKey("SharedUsersWindow"))
                {
                    CurrentContent = RoutedPages["SharedUsersWindow"];
                    WindowTitle = "Shared User Window";
                }
            }

            else
            {
                if (RoutedPages.ContainsKey("PropertyWindowControl"))
                {
                    CurrentContent = RoutedPages["PropertyWindowControl"];
                    WindowTitle = "Property Window";
                }
            }
        }
    }
}
      

      
