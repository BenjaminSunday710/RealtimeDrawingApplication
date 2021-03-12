using Newtonsoft.Json;
using RealtimeDrawingApplication.Common;
using System.Xml.Serialization;

namespace RealtimeDrawingApplication.Model
{
    public enum ComponentType { Rectangle, Ellipse, Triangle, TextBox, Line}
    public partial class DrawingComponentModel:IModel
    {
        [XmlIgnoreAttribute]
        [JsonIgnore]
        public virtual int Id { get; set; }
        [XmlAttribute]
        public virtual string Title { get; set; }
        [XmlAttribute]
        public virtual double BorderThickness { get; set; }
        [XmlAttribute]
        public virtual double Width { get; set; }
        [XmlAttribute]
        public virtual double Height { get; set; }
        [XmlAttribute]
        public virtual double X { get; set; }
        [XmlAttribute]
        public virtual double Y { get; set; }
        [XmlAttribute]
        public virtual double Angle { get; set; }
        [XmlAttribute]
        public virtual string Fill { get; set; }
        [XmlAttribute]
        public virtual string Border { get; set; }
        [XmlAttribute]
        public virtual string ComponentType { get; set; }
        [XmlIgnoreAttribute]
        [JsonIgnore]
        public virtual ProjectModel Project { get; set; }
    }

    
}