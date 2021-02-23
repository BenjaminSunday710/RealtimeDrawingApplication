using Prism.Events;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class ShapeComponent : Shape, IComponentProperties
    {
        public ShapeComponent(Geometry geometry)
        {
            Geometry = geometry;
            Fill = Brushes.Red;
            Width = 50;
            Height = 50;
            var eventAggregator=GenericServiceLocator.Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ResetPropertyEvent>().Subscribe(e => ChangeProperty(e));
        }

        private void ChangeProperty(PropertyWindowEventModel e)
        {
            switch (e.PropertyType)
            {
                case PropertyType.Border:
                    ShapeBorder = ((Colour)e.Value).BrushValue;
                    break;
                case PropertyType.Fill:
                    Fill = ((Colour)e.Value).BrushValue;
                    break;
                case PropertyType.Width:
                    break;
                case PropertyType.Height:
                    break;
                default:
                    break;
            }
        }

        protected override Geometry DefiningGeometry => Geometry;
        public Geometry Geometry { get; }
        public string Title { get; set; }
        public ComponentEnum ComponentType{ get; set; }
        public SolidColorBrush ShapeBorder{ get; set; }
        public SolidColorBrush ShapeFill{ get; set; }
        public double X{ get; set; }
        public double Y{ get; set; }
    }
}
