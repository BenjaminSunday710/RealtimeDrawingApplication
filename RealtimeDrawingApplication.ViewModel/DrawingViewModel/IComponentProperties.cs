using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public enum ControlEnum { Triangle, Rectangle, Circle, TextBox}

    public interface IComponentProperties
    {
        ControlEnum ControlType { get; set; }
        string Title { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        Brushes ShapeBorder { get; set; }
        Brushes ShapeFill { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
