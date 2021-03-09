using Application.Views;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.ViewModel;
using RealtimeDrawingApplication.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RealtimeDrawingApplication.Views2
{
    /// <summary>
    /// Interaction logic for LogInPageWindow.xaml
    /// </summary>
    public partial class LogInPageWindow : Window
    {
        //private Dictionary<string, Type> _routedPagesByType;

        public LogInPageWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            LoadAllWindows();
            
        }

        void LoadAllWindows()
        {
            CommonUtility.RoutedPages.Add(nameof(PropertyWindowControl),typeof(PropertyWindowControl));
            CommonUtility.RoutedPages.Add(nameof(ProjectWindow), typeof(ProjectWindow));
            CommonUtility.RoutedPages.Add(nameof(ProjectSharedUserWindowControl), typeof(ProjectSharedUserWindowControl));
            CommonUtility.RoutedPages.Add(nameof(SharedProjectWindow), typeof(SharedProjectWindow));
            CommonUtility.RoutedPages.Add(nameof(CreateAccount), typeof(CreateAccount));
            CommonUtility.RoutedPages.Add(nameof(CreateProject), typeof(CreateProject));
            CommonUtility.RoutedPages.Add(nameof(LogInPageWindow), typeof(LogInPageWindow));
            CommonUtility.RoutedPages.Add(nameof(MenuPaneControl), typeof(MenuPaneControl));
            CommonUtility.RoutedPages.Add(nameof(ApplicationStartupWindow), typeof(ApplicationStartupWindow));
           
        }
    }
}
