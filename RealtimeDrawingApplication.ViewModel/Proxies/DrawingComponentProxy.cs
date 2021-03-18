using Prism.Mvvm;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;


namespace RealtimeDrawingApplication.ViewModel.Proxies
{
    public enum ComponentType { Rectangle, Ellipse, Triangle, TextBox, Line }

    public class DrawingComponentProxy
    {
        private string _title;
        private double _borderThickness;
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
        Repository<DrawingComponentModel> database = Repository<DrawingComponentModel>.GetRepository;

        public DrawingComponentProxy() { }

        public string Title { get => _title; set { _title = value;} }
        public double BorderThickness { get => _borderThickness; set { _borderThickness = value; } }
        public double Width { get => _width; set { _width = value; } }
        public double Height { get => _height; set { _height = value; } }
        public double X { get => _x; set { _x = value;  } }
        public double Y { get => _y; set { _y = value;  } }
        public string Fill { get => _fill; set { _fill = value; } }
        public string Border { get => _border; set { _border = value;  } }
        public string ComponentType { get => _componentType; set { _componentType = value;  } }
        public virtual ProjectModel Project { get => _project; set { _project = value; } }
        public double Angle { get => _angle; set { _angle = value; } }
        public ComponentEnum ComponentEnum { get => _componentEnum; set { _componentEnum = value;  } }

       
    }
}
