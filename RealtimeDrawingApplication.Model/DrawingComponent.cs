using System.Windows.Media;

namespace RealtimeDrawingApplication.Model
{
    public enum ComponentType { Rectangle, Ellipse, Triangle, TextBox, Line}
    public class DrawingComponent
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual int BorderThickness { get; set; }
        public virtual double Width { get; set; }
        public virtual double Height { get; set; }
        public virtual double X { get; set; }
        public virtual double Y { get; set; }
        public virtual string Fill { get; set; }
        public virtual string Border { get; set; }
        public virtual ComponentType ComponentType { get; set; }
    }
}