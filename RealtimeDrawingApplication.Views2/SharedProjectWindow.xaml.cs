﻿using RealtimeDrawingApplication.ViewModel;
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

namespace RealtimeDrawingApplication.Views
{
    /// <summary>
    /// Interaction logic for SharedProjectWindow.xaml
    /// </summary>
    public partial class SharedProjectWindow : Window
    {
        public SharedProjectWindow()
        {
            InitializeComponent();
            //DataContext = new ProjectSharedUsersViewModel();
        }
    }
}
