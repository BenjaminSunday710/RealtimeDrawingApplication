﻿using RealtimeDrawingApplication.ViewModel.Proxies;
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
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : Window
    {
        public CreateProject()
        {
            InitializeComponent();
            var createProjectDataContext = new ProjectProxy();
            DataContext = createProjectDataContext;
        }

        private void BtnCreateProject_Click(object sender, RoutedEventArgs e)
        {
            var project = new ProjectProxy();
            project.Name = txtProjectName.Text;
        }
    }
}