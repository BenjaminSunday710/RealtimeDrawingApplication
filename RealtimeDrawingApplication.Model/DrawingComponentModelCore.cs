using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Model
{
    [Serializable]
    public partial class DrawingComponentModel : ISerializable
    {
        public DrawingComponentModel()
        {

        }

        public DrawingComponentModel(SerializationInfo info, StreamingContext context)
        {
            Title = (string)info.GetValue(nameof(Title), typeof(string));
            BorderThickness = (double)info.GetValue(nameof(BorderThickness), typeof(double));
            Border = (string)info.GetValue(nameof(Border), typeof(string));
            X = (double)info.GetValue(nameof(X), typeof(double));
            Y = (double)info.GetValue(nameof(Y), typeof(double));
            Width = (double)info.GetValue(nameof(Width), typeof(double));
            Height = (double)info.GetValue(nameof(Height), typeof(double));
            Angle = (double)info.GetValue(nameof(Angle), typeof(double));
            Fill = (string)info.GetValue(nameof(Fill), typeof(string));
            Border = (string)info.GetValue(nameof(Border), typeof(string));
            ComponentType = (string)info.GetValue(nameof(ComponentType), typeof(string));
            //ComponentEnum = (string)info.GetValue(nameof(ComponentEnum), typeof(string));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(BorderThickness), BorderThickness);
            info.AddValue(nameof(Border), Border);
            info.AddValue(nameof(Width), Width);
            info.AddValue(nameof(Height), Height);
            info.AddValue(nameof(X), X);
            info.AddValue(nameof(Y), Y);
            info.AddValue(nameof(Fill), Fill);
            info.AddValue(nameof(ComponentType), ComponentType);
            //info.AddValue(nameof(ComponentEnum), ComponentEnum);
            info.AddValue(nameof(Angle), Angle);
        }
    }
}
