﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
