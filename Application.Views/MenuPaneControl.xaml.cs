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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealtimeDrawingApplication.Views
{
    /// <summary>
    /// Interaction logic for MenuPane.xaml
    /// </summary>
    public partial class MenuPaneControl : UserControl
    {
        public MenuPaneControl()
        {
            InitializeComponent();
        }

        private void TblDisplayCreateAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CreateAccount createAccountDialog = new CreateAccount();
            createAccountDialog.ShowDialog();
        }

        private void TblDisplayCreateProject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CreateProject createProjectDialog = new CreateProject();
            createProjectDialog.ShowDialog();
        }

        private void TblDisplayShareProject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SharedProjectWindow sharedProjectDialog = new SharedProjectWindow();
            sharedProjectDialog.ShowDialog();
        }

        private void TblDisplayClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
