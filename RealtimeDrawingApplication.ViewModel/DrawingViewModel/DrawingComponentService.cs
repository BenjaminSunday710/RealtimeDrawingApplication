using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class DrawingComponentService
    {
        public static FrameworkElement GetDefaultComponent(ComponentEnum component)
        {
            if (component == ComponentEnum.TextBox)
            {
                return new TextBoxComponent();
            }
            else 
            {
                return (new ShapeComponent(GetDefaultShapeGeometry(component), component).GetComponent()) as FrameworkElement;
            }
        }

        public static Geometry GetDefaultShapeGeometry(ComponentEnum component)
        {
            switch (component)
            {
                case ComponentEnum.Ellipse:
                    return new EllipseGeometry(new Point(25, 25), 25, 25);
                case ComponentEnum.Rectangle:
                    return Geometry.Parse("M0,0 L50,0 L50,50 L0,50Z");
                case ComponentEnum.Triangle:
                    return Geometry.Parse("M25,0 L50,50 L0,50Z");
                case ComponentEnum.Line:
                    return Geometry.Parse("M10,5 L50,50");
                default:
                    return Geometry.Parse("M25,0 L50,50 L0,50Z");
            }
        }
    }
}
