using FluentNHibernate.Mapping;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure.Mappings
{
    public class DrawingComponentMap:ClassMap<DrawingComponent>
    {
        public DrawingComponentMap()
        {
            Id(x => x.Id);
            Map(x => x.X);
            Map(x => x.Y);
            Map(x => x.Width);
            Map(x => x.Height);
            Map(x => x.Title);
            Map(x => x.BorderThickness);
            Map(x => x.ComponentType);
            Map(x => x.Fill);
            Map(x => x.Border);
        }
    }
}
