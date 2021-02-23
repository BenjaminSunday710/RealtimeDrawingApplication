using Application.Views;
using RealtimeDrawingApplication.ViewModel;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using RealtimeDrawingApplication.Views2;
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
    public partial class MainWindow : Window/*,INotifyPropertyChanged*/
    {
        private Dictionary<string, FrameworkElement> _routedPages;

        public MainWindow()
        {
            InitializeComponent();
            PopulateRoutedPages();
            DataContext = new PropertySharedUsersProjectWindowsDisplay(_routedPages);
        }

        void PopulateRoutedPages()
        {
            _routedPages = new Dictionary<string, FrameworkElement>();
            _routedPages.Add(nameof(PropertyWindowControl), new PropertyWindowControl());
            _routedPages.Add(nameof(ProjectWindow), new ProjectWindow());
            _routedPages.Add(nameof(SharedUserWindowControl), new SharedUserWindowControl());
        }

        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement component = null;
            if (sender is Rectangle rectangle)
            {
                //component =new ShapeComponent(ShapeService.GetDefaultShapeGeometry(ControlEnum.Rectangle));
                component = DrawingComponentService.GetDefaultComponent(ComponentEnum.Rectangle);
            }
            else if (sender is Ellipse ellipse)
            {
                //component = new ShapeComponent(ShapeService.GetDefaultShapeGeometry(ControlEnum.Ellipse));
                component = DrawingComponentService.GetDefaultComponent(ComponentEnum.Ellipse);
            }
            else if (sender is Path path)
            {
                //component = new ShapeComponent(ShapeService.GetDefaultShapeGeometry(ControlEnum.Triangle));
                component = DrawingComponentService.GetDefaultComponent(ComponentEnum.Triangle);
            }
            else if (sender is TextBlock textBlock)
            {
                //component = new ShapeComponent(ShapeService.GetDefaultShapeGeometry(ControlEnum.Triangle));
                component = DrawingComponentService.GetDefaultComponent(ComponentEnum.TextBox);
            }

            if (component != null)
            {
                //e.MouseDevice.OverrideCursor = Cursors.Hand;
                DataObject dataObject = new DataObject("toolboxitem", component);
                DragDrop.DoDragDrop(component as FrameworkElement, dataObject, DragDropEffects.Copy);
            }
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

        private void DisplayPropertyWindowMenu_Click(object sender, RoutedEventArgs e)
        {
            PropertyWindowPopUp.IsOpen = !PropertyWindowPopUp.IsOpen;
        }

        private void PropertyWindowPopUp_LostFocus(object sender, RoutedEventArgs e)
        {
            PropertyWindowPopUp.StaysOpen = false;
        }

        private void LoadLoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            LogInPageWindow logInPageWindow = new LogInPageWindow();
            logInPageWindow.ShowDialog();
        }
    }
}
