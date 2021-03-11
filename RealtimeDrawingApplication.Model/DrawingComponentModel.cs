using Newtonsoft.Json;
using RealtimeDrawingApplication.Common;
using System.Windows.Media;

namespace RealtimeDrawingApplication.Model
{
    public enum ComponentType { Rectangle, Ellipse, Triangle, TextBox, Line}
    public class DrawingComponentModel:IModel
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual int BorderThickness { get; set; }
        public virtual double Width { get; set; }
        public virtual double Height { get; set; }
        public virtual double X { get; set; }
        public virtual double Y { get; set; }
        public virtual double Angle { get; set; }
        public virtual string Fill { get; set; }
        public virtual string Border { get; set; }
        public virtual string ComponentType { get; set; }
        [JsonIgnore]
        public virtual ProjectModel Project { get; set; }
    }
}