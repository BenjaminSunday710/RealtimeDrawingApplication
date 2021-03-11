using Prism.Mvvm;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.Proxies
{
    public enum ComponentType { Rectangle, Ellipse, Triangle, TextBox, Line }
    public class DrawingComponentProxy:BindableBase
    {
        private string _title;
        private int _borderThickness;
        private double _width;
        private double _height;
        private double _x;
        private double _y;
        private double _angle;
        private string _fill;
        private string _border;
        private string _componentType;
        private ProjectModel _project;
        private ComponentEnum _componentEnum;
        private static Repository<DrawingComponentModel> database = new Repository<DrawingComponentModel>();

        public DrawingComponentProxy()
        {
           
        }

        public string Title { get => _title; set { _title = value; RaisePropertyChanged(); } }
        public int BorderThickness { get => _borderThickness; set { _borderThickness = value; RaisePropertyChanged(); } }
        public double Width { get => _width; set { _width = value; RaisePropertyChanged(); } }
        public double Height { get => _height; set { _height = value; RaisePropertyChanged(); } }
        public double X { get => _x; set { _x = value; RaisePropertyChanged(); } }
        public double Y { get => _y; set { _y = value; RaisePropertyChanged(); } }
        public string Fill { get => _fill; set { _fill = value; RaisePropertyChanged(); } }
        public string Border { get => _border; set { _border = value; RaisePropertyChanged(); } }
        public string ComponentType { get => _componentType; set { _componentType = value; RaisePropertyChanged(); } }
        public ProjectModel Project { get => _project; set { _project = value; RaisePropertyChanged(); } }
        public double Angle { get => _angle; set { _angle = value; RaisePropertyChanged(); } }

        public ComponentEnum ComponentEnum { get => _componentEnum; set { _componentEnum = value; RaisePropertyChanged(); } }
    }
}
