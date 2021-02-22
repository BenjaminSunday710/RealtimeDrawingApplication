using Application.Views;
using RealtimeDrawingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealtimeDrawingApplication.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        private FrameworkElement currentContent = new PropertyWindowControl();
        private bool _isPropertyWindow;
        private bool _isSharedUserWindow;
        private bool _isProjectsWindow;
        private string _windowTitle = "Property Window";

        public FrameworkElement CurrentContent
        {
            get { return currentContent; }
            set { currentContent = value; OnPropertyChanged(); }
        }

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; OnPropertyChanged(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            //ViewModelExtract();
            DataContext = this;
        }

        public void ViewModelExtract()
        {
            var viewModel = new PropertySharedUsersProjectWindowsDisplay();
            viewModel.RoutedPages.Add(nameof(PropertyWindowControl), new PropertyWindowControl());
            viewModel.RoutedPages.Add(nameof(ProjectWindow), new ProjectWindow());
            viewModel.RoutedPages.Add(nameof(SharedUserWindowControl), new SharedUserWindowControl());
        }

        public bool IsPropertyWindow { get => _isPropertyWindow; set { _isPropertyWindow = value; UpdateLayoutControl(); OnPropertyChanged(); } }
        public bool IsSharedUserWindow { get => _isSharedUserWindow; set { _isSharedUserWindow = value; UpdateLayoutControl(); OnPropertyChanged(); } }
        public bool IsProjectsWindow { get => _isProjectsWindow; set { _isProjectsWindow = value; UpdateLayoutControl(); OnPropertyChanged(); } }

        private void UpdateLayoutControl()
        {
            if (_isProjectsWindow)
            {
                CurrentContent = new ProjectWindow();
                WindowTitle = "Project Window";
            }

            else if (_isSharedUserWindow)
            {
                CurrentContent = new SharedUserWindowControl();
                WindowTitle = "Shared User Window";
            }

            else
            {
                CurrentContent = new PropertyWindowControl();
                WindowTitle = "Property Window";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuPane.Visibility = Visibility.Visible;
        }

        private void AddProjectIconBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = new Border();
            border.Background = Brushes.Gray;
        }

        private void ShareProjectIconBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = new Border();
            border.Background = Brushes.Gray;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void DisplayPropertyWindowMenu_Click(object sender, RoutedEventArgs e)
        {
            PropertyWindowPopUp.IsOpen = !PropertyWindowPopUp.IsOpen;
        }

        private void PropertyWindowPopUp_LostFocus(object sender, RoutedEventArgs e)
        {
            PropertyWindowPopUp.StaysOpen = false;
        }
    }
}
