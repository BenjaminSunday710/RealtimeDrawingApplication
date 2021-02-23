using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public enum ComponentEnum { Triangle, Rectangle, Ellipse, Line, TextBox}

    public interface IComponentProperties
    {
        ComponentEnum ComponentType { get; set; }
        string Title { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        SolidColorBrush ShapeBorder { get; set; }
        SolidColorBrush ShapeFill { get; set; }
        double X { get; set; }
        double Y { get; set; }
    }
}
