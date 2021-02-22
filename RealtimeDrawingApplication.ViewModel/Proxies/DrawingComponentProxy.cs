using Prism.Mvvm;
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
        private int _id;
        private string _title;
        private int _fontSize;
        private double _width;
        private double _height;
        private double _x;
        private double _y;
        private string _fill;
        private string _border;
        private ComponentType _componentType;

        public int Id { get => _id; set { _id = value; RaisePropertyChanged(); } }
        public string Title { get => _title; set { _title = value; RaisePropertyChanged(); } }
        public int FontSize { get => _fontSize; set { _fontSize = value; RaisePropertyChanged(); } }
        public double Width { get => _width; set { _width = value; RaisePropertyChanged(); } }
        public double Height { get => _height; set { _height = value; RaisePropertyChanged(); } }
        public double X { get => _x; set { _x = value; RaisePropertyChanged(); } }
        public double Y { get => _y; set { _y = value; RaisePropertyChanged(); } }
        public string Fill { get => _fill; set { _fill = value; RaisePropertyChanged(); } }
        public string Border { get => _border; set { _border = value; RaisePropertyChanged(); } }
        public ComponentType ComponentType { get => _componentType; set { _componentType = value; RaisePropertyChanged(); } }
    }
}
