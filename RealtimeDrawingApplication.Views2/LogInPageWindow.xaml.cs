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
        public LogInPageWindow()
        {
            InitializeComponent();
        }

        private void BtnCreateAccountDisplay_Click(object sender, RoutedEventArgs e)
        {
            var window = new Views.CreateAccount();
            this.Visibility = Visibility.Collapsed;
            window.ShowDialog();
           
        }
    }
}
