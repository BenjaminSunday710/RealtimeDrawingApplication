using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class TextBoxComponent:TextBox, IComponentProperties
    {
        public TextBoxComponent(double width=100, double height=50, string text="Text")
        {
            Width = width;
            Height = height;
            Text = text;
            ShapeFill = Brushes.Transparent;
            ShapeBorder = Brushes.WhiteSmoke;
            BorderThickness = new Thickness(1);
        }

        public ComponentEnum ComponentType { get; set; }
        public string Title { get; set; }
        public SolidColorBrush ShapeBorder { get; set; }
        public SolidColorBrush ShapeFill { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
