using RealtimeDrawingApplication.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class LineComponent:Shape,IComponentProperties,INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        public LineComponent(Geometry geometry)
        {
            Id = Guid.NewGuid();
        }

        private void ChangeProperty(PropertyWindowEventModel e)
        {
            switch (e.PropertyType)
            {
                case PropertyType.Border:
                    ShapeBorder = ((Colour)e.Value).BrushValue;
                    break;
                case PropertyType.Fill:
                    Stroke = ((Colour)e.Value).BrushValue;
                    ShapeFill = (SolidColorBrush)Stroke;
                    break;
                case PropertyType.Width:
                    Width = (double)e.Value;
                    break;
                case PropertyType.Height:
                    Height = (double)e.Value;
                    break;
                case PropertyType.BorderThickness:
                    StrokeThickness = (double)e.Value;
                    BorderThickness = StrokeThickness;
                    break;
                case PropertyType.Title:
                    Title = (string)e.Value;
                    break;
                case PropertyType.X:
                    X = (double)e.Value;
                    break;
                case PropertyType.Y:
                    Y = (double)e.Value;
                    break;
                default:
                    break;
            }
        }

        public ComponentEnum ComponentType { get; set; }
        public string Title { get; set; }
        public SolidColorBrush ShapeBorder { get; set; }
        public SolidColorBrush ShapeFill { get; set; }
        public double BorderThickness { get; set; }
        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }
        public Guid Id { get; set; }
        public bool ShowBorder { get; set; }
        public new double Width { get; set; }
        public new double Height { get; set; }
        Geometry Geometry { get; set; }
        protected override Geometry DefiningGeometry => Geometry;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object GetComponent()
        {
            return this;
        }
    }
}
