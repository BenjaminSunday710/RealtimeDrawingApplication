using Microsoft.Win32;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

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

        private static DrawingComponentProxy Convert(DrawingComponentModel drawingComponentModel)
        {
            var item = new DrawingComponentProxy();

            item.Angle = drawingComponentModel.Angle;
            item.Border = drawingComponentModel.Border;
            item.ComponentType = drawingComponentModel.ComponentType;
            //item.ComponentEnum = Enum.Parse(ComponentEnum, dcm.Title);
            item.BorderThickness = drawingComponentModel.BorderThickness;
            item.Fill = drawingComponentModel.Fill;
            item.Height = drawingComponentModel.Height;
            item.Width = drawingComponentModel.Width;
            item.X = drawingComponentModel.X;
            item.Y = drawingComponentModel.Y;
            item.Title = drawingComponentModel.Title;

            return item;
        }

        public static List<DrawingComponentProxy> DeserializeToProxy(string projectName)
        {
            List<DrawingComponentModel> drawingComponents = Repository<DrawingComponentModel>.Database.GetDrawingComponents(projectName);
            List<DrawingComponentProxy> drawingComponentProxies = new List<DrawingComponentProxy>(drawingComponents.Count);
           
            foreach (var drawingComponent in drawingComponents)
            {
                var drawingComponentProxy = Convert(drawingComponent);
                drawingComponentProxies.Add(drawingComponentProxy);
            }

            return drawingComponentProxies;
        }

        public static List<DrawingComponentProxy> DeserializeToProxy(List<DrawingComponentModel> drawingComponentModels)
        {
            List<DrawingComponentProxy> drawingComponentProxies = new List<DrawingComponentProxy>(drawingComponentModels.Count);

            foreach (var drawingComponent in drawingComponentModels)
            {
                var drawingComponentProxy = Convert(drawingComponent);
                drawingComponentProxies.Add(drawingComponentProxy);
            }

            return drawingComponentProxies;
        }

    }
}
