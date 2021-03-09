using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.DatabaseServices
{
    public class DrawingComponentModelService
    {
        public static void SaveToDatabase(DrawingComponentProxy drawingComponentProxy)
        {
            DrawingComponentModel drawingComponent = new DrawingComponentModel();
            drawingComponent.X = drawingComponentProxy.X;
            drawingComponent.Y = drawingComponentProxy.Y;
            drawingComponent.Width = drawingComponentProxy.Width;
            drawingComponent.Height = drawingComponentProxy.Height;
            drawingComponent.Fill = drawingComponentProxy.Fill;
            drawingComponent.Border = drawingComponentProxy.Border;
            drawingComponent.BorderThickness = drawingComponentProxy.BorderThickness;
            drawingComponent.ComponentType = drawingComponentProxy.ComponentType;
            drawingComponent.Angle = drawingComponentProxy.Angle;
            drawingComponent.Project = drawingComponentProxy.Project;
            drawingComponent.Title = drawingComponentProxy.Title;

            Repository<DrawingComponentModel>.Database.Create(drawingComponent);
        }

        public static List<DrawingComponentProxy> DeserializeToProxy(string projectName)
        {
            List<DrawingComponentProxy> drawingComponentProxies = new List<DrawingComponentProxy>();
            List<DrawingComponentModel> drawingComponents = Repository<DrawingComponentModel>.Database.GetDrawingComponents(projectName);
            DrawingComponentProxy drawingComponentProxy = new DrawingComponentProxy();

            foreach (var drawingComponent in drawingComponents)
            {
                drawingComponentProxy.Angle = drawingComponent.Angle;
                drawingComponentProxy.Border = drawingComponent.Border;
                drawingComponentProxy.BorderThickness = drawingComponent.BorderThickness;
                drawingComponentProxy.ComponentType = drawingComponent.ComponentType;
                drawingComponentProxy.Fill = drawingComponent.Fill;
                drawingComponentProxy.Height = drawingComponent.Height;
                drawingComponentProxy.Project = drawingComponent.Project;
                drawingComponentProxy.Title = drawingComponent.Title;
                drawingComponentProxy.Width = drawingComponent.Width;
                drawingComponentProxy.X = drawingComponent.X;
                drawingComponentProxy.Y = drawingComponent.Y;

                drawingComponentProxies.Add(drawingComponentProxy);
            }

            return drawingComponentProxies;
        }

        public static List<DrawingComponentProxy> DeserializeToProxy(List<DrawingComponentModel> drawingComponentModels)
        {
            List<DrawingComponentProxy> drawingComponentProxies = new List<DrawingComponentProxy>();
            DrawingComponentProxy drawingComponentProxy = new DrawingComponentProxy();

            foreach (var drawingComponent in drawingComponentModels)
            {
                drawingComponentProxy.Angle = drawingComponent.Angle;
                drawingComponentProxy.Border = drawingComponent.Border;
                drawingComponentProxy.BorderThickness = drawingComponent.BorderThickness;
                drawingComponentProxy.ComponentType = drawingComponent.ComponentType;
                drawingComponentProxy.Fill = drawingComponent.Fill;
                drawingComponentProxy.Height = drawingComponent.Height;
                drawingComponentProxy.Project = drawingComponent.Project;
                drawingComponentProxy.Title = drawingComponent.Title;
                drawingComponentProxy.Width = drawingComponent.Width;
                drawingComponentProxy.X = drawingComponent.X;
                drawingComponentProxy.Y = drawingComponent.Y;

                drawingComponentProxies.Add(drawingComponentProxy);
            }

            return drawingComponentProxies;
        }
    }
}
