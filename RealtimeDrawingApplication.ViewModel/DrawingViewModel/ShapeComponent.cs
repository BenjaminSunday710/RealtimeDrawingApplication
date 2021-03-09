using Prism.Events;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class ShapeComponent : Grid,IComponentProperties, INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _angle;
        private bool _showBorder;
        private SolidColorBrush _shapeFill = Brushes.Red;
        private SolidColorBrush _shapeBorder = Brushes.Blue;
        private double _borderThickness = 0.1;

        public ShapeComponent(Geometry geometry, ComponentEnum componentEnum)
        {
            Geometry = geometry;
            ComponentEnum = componentEnum;
            Id = Guid.NewGuid();
            Height = 60;
            Width = 60;
            Shape = this;
            Title = componentEnum.ToString();
        }

        public ShapeComponent Shape { get; set; }

        public object GetComponent()
        {
            Shape.Children.Clear();
            var border = new Border();
            border.Padding = new Thickness(5);
            var shape = new Path() { Data = Geometry };
            shape.Stretch = Stretch.Fill;
            shape.Fill = ShapeFill;
            shape.Stroke = ShapeBorder;
            shape.StrokeThickness = BorderThickness;
            // Features: The height and width is limiting
            //the shape changes to zero;
            shape.Height = Height - 10;
            shape.Width = Width - 10;
            shape.HorizontalAlignment = HorizontalAlignment.Center;
            shape.VerticalAlignment = VerticalAlignment.Center;
            border.Child = shape;
            Shape.Children.Add(border);
            if (ShowBorder)
            {
                border.BorderBrush = Brushes.Red;
                border.BorderThickness = new Thickness(1);
                Shape.Children.Add(new Border
                {
                    HorizontalAlignment=HorizontalAlignment.Left,
                    VerticalAlignment=VerticalAlignment.Top,
                    Width=5,
                    Height=5,
                    Background=Brushes.Purple
                });
                Shape.Children.Add(new Border
                {
                    HorizontalAlignment=HorizontalAlignment.Right,
                    VerticalAlignment=VerticalAlignment.Top,
                    Width=5,
                    Height=5,
                    Background=Brushes.Purple
                });
                Shape.Children.Add(new Border
                {
                    HorizontalAlignment=HorizontalAlignment.Left,
                    VerticalAlignment=VerticalAlignment.Bottom,
                    Width=5,
                    Height=5,
                    Background=Brushes.Purple
                });
                Shape.Children.Add(new Border
                {
                    HorizontalAlignment=HorizontalAlignment.Right,
                    VerticalAlignment=VerticalAlignment.Bottom,
                    Width=5,
                    Height=5,
                    Background=Brushes.Purple
                });
            }
            return Shape;
        }

        public bool ShowBorder { get => _showBorder; set { _showBorder = value; OnPropertyChanged(); } }

        public Geometry Geometry { get; set; }
        public ComponentEnum ComponentEnum { get; }
        public string Title { get; set; }
        //public double Width { get; set; }
        //public double Height { get; set; }
        public ComponentEnum ComponentType { get; set; }
        public SolidColorBrush ShapeBorder { get => _shapeBorder; set => _shapeBorder = value; }
        public SolidColorBrush ShapeFill { get => _shapeFill; set => _shapeFill = value; }
        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }
        public double Angle { get => _angle; set { _angle = value; OnPropertyChanged(); } }
        public double BorderThickness { get => _borderThickness; set => _borderThickness = value; }
        public Guid Id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            UpdateDrawing();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateDrawing()
        {

        }
    }
}
