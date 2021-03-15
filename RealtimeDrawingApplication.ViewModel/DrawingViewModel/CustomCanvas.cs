using Prism.Events;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using System.Collections.ObjectModel;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.DataTransferProtocol;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class CustomCanvas : Canvas
    {
        private double xValue;
        private double yValue;
        private DrawingComponentProxy _drawingComponentProxy;
        private ProjectModel _currentProject;
        private static Repository<ProjectModel> database = Repository<ProjectModel>.GetRepository;

        public IEventAggregator EventAggregator { get; set; }

        public CustomCanvas() : base()
        {
            Background = Brushes.WhiteSmoke;
            AllowDrop = true;
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<ResetPropertyEvent>().Subscribe(SetupViewProperties);
            EventAggregator.GetEvent<GetProjectInstanceEvent>().Subscribe(GetProjectInstance);
            EventAggregator.GetEvent<FetchDrawingComponentsEvent>().Subscribe(DrawComponents);
            EventAggregator.GetEvent<ImportFileEvent>().Subscribe(DisplayImportedDrawings);
            EventAggregator.GetEvent<UpdateProjectEvent>().Subscribe(UpdateDrawingComponents);

        }

        public ObservableCollection<string> ColourList = new Colour().ColourList;
        //public ObservableCollection<Colour> ColourListItems => new ObservableCollection<Colour>{
        //        new Colour {Name="Red", BrushValue=Brushes.Red },
        //        new Colour {Name="Green", BrushValue=Brushes.Green },
        //        new Colour {Name="Yellow", BrushValue=Brushes.Yellow },
        //        new Colour {Name="Blue", BrushValue=Brushes.Blue },
        //        new Colour {Name="Black", BrushValue=Brushes.Black },
        //        new Colour {Name="White", BrushValue=Brushes.White },
        //        new Colour {Name="Brown", BrushValue=Brushes.Brown },
        //        new Colour {Name="Pink", BrushValue=Brushes.Pink },
        //        new Colour {Name="PaleGoldenRod", BrushValue=Brushes.PaleGoldenrod },
        //        new Colour {Name="Violet", BrushValue=Brushes.Violet },
        //        new Colour {Name="Purple", BrushValue=Brushes.Purple },
        //        new Colour {Name="Indigo", BrushValue=Brushes.Indigo },
        //        new Colour {Name="IndianRed", BrushValue=Brushes.IndianRed },
        //        new Colour {Name="HoneyDew", BrushValue=Brushes.Honeydew },
        //        new Colour {Name="Khaki", BrushValue=Brushes.Khaki },
        //        new Colour {Name="LightBlue ", BrushValue=Brushes.LightBlue },
        //        new Colour {Name="Transparent", BrushValue=Brushes.Transparent },
        //        new Colour {Name="Tomato", BrushValue=Brushes.Tomato },
        //        new Colour {Name="SpringGreen", BrushValue=Brushes.SpringGreen },
        //        new Colour {Name="Gold", BrushValue=Brushes.Gold },
        //        new Colour {Name="ForestGreen", BrushValue=Brushes.ForestGreen },
        //        new Colour {Name="GhostWhite", BrushValue=Brushes.GhostWhite },
        //        new Colour {Name="Gray", BrushValue=Brushes.Gray },
        //        new Colour {Name="DimGray", BrushValue=Brushes.DimGray },
        //        new Colour {Name="DeepPink", BrushValue=Brushes.DeepPink },
        //        new Colour {Name="PaleVioletRed", BrushValue=Brushes.PaleVioletRed },
        //        new Colour {Name="Olive", BrushValue=Brushes.Olive },
        //        new Colour {Name="Orange", BrushValue=Brushes.Orange },
        //        new Colour {Name="Orchid", BrushValue=Brushes.Orchid },
        //        new Colour {Name="OldLace", BrushValue=Brushes.OldLace },
        //        new Colour {Name="Chocolate", BrushValue=Brushes.Chocolate },
        //        new Colour {Name="CornflowerBlue", BrushValue=Brushes.CornflowerBlue },
        //        new Colour {Name="Cornsilk", BrushValue=Brushes.Cornsilk },
        //        new Colour{Name="Cyan",BrushValue=Brushes.Cyan},
        //        new Colour{Name="Chartreuse",BrushValue=Brushes.Chartreuse},
        //        new Colour{Name="DarkMagenta",BrushValue=Brushes.DarkMagenta}
        //    };
        void SetupViewProperties(PropertyWindowEventModel propertyWindowEventModel)
        {

            if (propertyWindowEventModel != null)
            {
                FrameworkElement _component = null;

                foreach (FrameworkElement item in Children)
                {
                    var _item = (IComponentProperties)item;

                    if (_item!=null && _item.Id == propertyWindowEventModel.PropertyId)
                    {
                        switch (propertyWindowEventModel.PropertyType)
                        {
                            case PropertyType.BorderFill:
                                _item.ShapeBorder = (SolidColorBrush)propertyWindowEventModel.Value;
                                break;
                            case PropertyType.Fill:
                                _item.ShapeFill = (SolidColorBrush)propertyWindowEventModel.Value;
                                break;
                            case PropertyType.Width:
                                _item.Width = (double)propertyWindowEventModel.Value;
                                break;
                            case PropertyType.Height:
                                _item.Height = (double)propertyWindowEventModel.Value;
                                break;
                            case PropertyType.Title:
                                _item.Title = (string)propertyWindowEventModel.Value;
                                break;
                            case PropertyType.BorderThickness:
                                _item.BorderThickness = (double)propertyWindowEventModel.Value;
                                break;
                            case PropertyType.X:
                                _item.X = (double)propertyWindowEventModel.Value;
                                SetLeft(item, _item.X);
                                break;
                            case PropertyType.Y:
                                _item.Y = (double)propertyWindowEventModel.Value;
                                SetTop(item, _item.Y);
                                break;
                            default:
                                break;
                        }
                        _component = _item as FrameworkElement;
                    }
                }

                //while converting to FrameWorkElement and later Reconvert to IComponentProperties
                //Also how do GetComponent() works
                if (_component != null)
                {
                    var __component = _component as IComponentProperties;
                    _component = __component.GetComponent() as FrameworkElement;
                }
            }
        }

        void GetProjectInstance(string projectName)
        {
            _currentProject = database.GetProject(projectName);
           this.Children.Clear();
        }

        public void SaveDrawingComponents(string projectName)
        {
            _currentProject =database.GetProject(projectName);

            if (_currentProject == null)
            {
                MessageBox.Show("Project cannot be saved! Create Project","Notification",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }

            foreach (var item in this.Children)
            {
                _drawingComponentProxy = new DrawingComponentProxy();
                var getItem = (IComponentProperties)item;
                _drawingComponentProxy.X = getItem.X;
                _drawingComponentProxy.Y = getItem.Y;
                _drawingComponentProxy.Angle = getItem.Angle;
                _drawingComponentProxy.Title = getItem.Title;
                _drawingComponentProxy.Height = getItem.Height;
                _drawingComponentProxy.Width = getItem.Width;
                _drawingComponentProxy.Border = getItem.ShapeBorder.ToString();
                _drawingComponentProxy.Fill = getItem.ShapeFill.ToString();
                _drawingComponentProxy.ComponentType = getItem.ComponentType.ToString();
                _drawingComponentProxy.Project = _currentProject;

                DatabaseServices.DrawingComponentModelService.SaveToDatabase(_drawingComponentProxy);
            }
            MessageBox.Show("Project Saved", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void UpdateDrawingComponents(string projectName )
        {
            _currentProject = database.GetProject(projectName);

            if (_currentProject == null)
            {
                MessageBox.Show("Project cannot be saved! Create Project", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            List<DrawingComponentProxy> drawingComponentProxies = new List<DrawingComponentProxy>();

            foreach (var item in this.Children)
            {
                _drawingComponentProxy = new DrawingComponentProxy();
                var getItem = (IComponentProperties)item;
                _drawingComponentProxy.X = getItem.X;
                _drawingComponentProxy.Y = getItem.Y;
                _drawingComponentProxy.Angle = getItem.Angle;
                _drawingComponentProxy.Title = getItem.Title;
                _drawingComponentProxy.Height = getItem.Height;
                _drawingComponentProxy.Width = getItem.Width;
                _drawingComponentProxy.Border = getItem.ShapeBorder.ToString();
                _drawingComponentProxy.Fill = getItem.ShapeFill.ToString();
                _drawingComponentProxy.ComponentType = getItem.ComponentType.ToString();
                _drawingComponentProxy.Project = _currentProject;

                drawingComponentProxies.Add(_drawingComponentProxy);
            }
            DrawingComponentModelService.UpdateDrawingComponentModel(projectName, drawingComponentProxies);
        }

        public void DrawComponents(List<DrawingComponentProxy> drawingComponents)
        {
            ComponentEnum componentType = ComponentEnum.Ellipse;
            this.Children.Clear();
            
            foreach (var drawingComponent in drawingComponents)
            {
                switch(drawingComponent.Title)
                {
                    case "Triangle":
                        componentType = ComponentEnum.Triangle;
                        break;
                    case "Ellipse":
                        componentType = ComponentEnum.Ellipse;
                        break;
                    case "Rectangle":
                        componentType = ComponentEnum.Rectangle;
                        break;
                    case "Line":
                        componentType = ComponentEnum.Line;
                        break;
                    case "TextBlock":
                        componentType = ComponentEnum.TextBox;
                        break;
                }
                var component = DrawingComponentService.GetDefaultShapeGeometry(componentType);
                var fetchedDBComponent = new ShapeComponent(component, componentType);
                //var fetchedDBComponent = component as IComponentProperties;
                fetchedDBComponent.X = drawingComponent.X;
                fetchedDBComponent.Y = drawingComponent.Y;
                fetchedDBComponent.Angle = drawingComponent.Angle;
                fetchedDBComponent.Title = drawingComponent.Title;
                fetchedDBComponent.Width = drawingComponent.Width;
                fetchedDBComponent.Height = drawingComponent.Height;
                fetchedDBComponent.ShapeFill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(drawingComponent.Fill));
                fetchedDBComponent.ShapeBorder = new SolidColorBrush((Color)ColorConverter.ConvertFromString(drawingComponent.Border));
                fetchedDBComponent.BorderThickness = drawingComponent.BorderThickness;

                var fetchedComponent = fetchedDBComponent.GetComponent() as FrameworkElement;
                
                SetLeft(fetchedComponent, fetchedDBComponent.X);
                SetTop(fetchedComponent, fetchedDBComponent.Y);
                Children.Add(fetchedComponent);
                
            }
        }

        void DisplayImportedDrawings(string FileType)
        {
            List<DrawingComponentProxy> importedDrawingComponents = new List<DrawingComponentProxy>();

            switch(FileType)
            {
                case "Json":
                    importedDrawingComponents = DataTransferServices.DeserialiseObjectFromJson();
                    break;
                case "Xml":
                    importedDrawingComponents = DataTransferServices.DeserializeObjectFromXml();
                    break;
                default:
                    importedDrawingComponents = null;
                    break;
            }

            DrawComponents(importedDrawingComponents);
        }

        protected override void OnDrop(DragEventArgs e)
        {
            var item = e.Data.GetData("toolboxitem") as FrameworkElement;
            var currentMousePosition = e.GetPosition(this);
            xValue = currentMousePosition.X;
            yValue = currentMousePosition.Y;
            Canvas.SetLeft(item, xValue);
            Canvas.SetTop(item, yValue);
            Children.Add(item);

            if (item is IComponentProperties component)
            {
                CurrentlySelectedItem.PropertyId = component.Id;
                CurrentlySelectedItem.ComponentType = component.ComponentType;
                CurrentlySelectedItem.PropertyType = PropertyType.X;
                CurrentlySelectedItem.Value = xValue;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.Y;
                CurrentlySelectedItem.Value = yValue;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.Angle;
                CurrentlySelectedItem.Value = component.Angle;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.Height;
                CurrentlySelectedItem.Value = component.Height;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.Width;
                CurrentlySelectedItem.Value = component.Width;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.Fill;
                CurrentlySelectedItem.Value = component.ShapeFill;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.BorderFill;
                CurrentlySelectedItem.Value = component.ShapeBorder;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.BorderThickness;
                CurrentlySelectedItem.Value = component.BorderThickness;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);

                CurrentlySelectedItem.PropertyType = PropertyType.Title;
                CurrentlySelectedItem.Value = component.Title;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);
            }
        }

        public FrameworkElement SelectedComponent { get; set; }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (SelectedComponent != null)
            {
                var position = e.GetPosition(this);
                if (position.X < ActualWidth - SelectedComponent.ActualWidth &&
                    position.Y < ActualHeight - SelectedComponent.ActualHeight)
                {
                    var model = SelectedComponent as IComponentProperties;
                    if (model!=null)
                    {
                        model.X = position.X;
                        model.Y = position.Y;
                        SetLeft(SelectedComponent, position.X);
                        SetTop(SelectedComponent, position.Y);
                        CurrentlySelectedItem.Value = position.X;
                        CurrentlySelectedItem.PropertyType = PropertyType.X;
                        EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);
                        CurrentlySelectedItem.Value = position.Y;
                        CurrentlySelectedItem.PropertyType = PropertyType.Y;
                        EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem); 
                    }
                }
            }
        }

        public PropertyWindowEventModel CurrentlySelectedItem { get; set; } = new PropertyWindowEventModel();

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            SelectedComponent = e.Source as FrameworkElement;
            SelectedComponent.MouseLeftButtonUp += SelectedComponent_MouseLeftButtonUp;   
            if (GetParent(SelectedComponent) is IComponentProperties component)
            {
                CurrentlySelectedItem = new PropertyWindowEventModel()
                {
                    PropertyId = component.Id,
                    ComponentType = component.ComponentType
                };
                ResetPreviousComponent();
                component.ShowBorder = true;
                SelectedComponent = component.GetComponent() as FrameworkElement;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);
            }
            base.OnMouseLeftButtonDown(e);

        }

        private void SelectedComponent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectedComponent = null;
        }

        private FrameworkElement GetParent(FrameworkElement frameworkElement)
        {
            if (frameworkElement is ShapeComponent)
            {
                return frameworkElement;
            }
            else
            {
                if (frameworkElement.Parent==null)
                {
                    return null;
                }
                return GetParent(frameworkElement.Parent as FrameworkElement);
            }
        }

        private void ResetPreviousComponent()
        {
            FrameworkElement _component=null;
            foreach (FrameworkElement item in Children)
            {
                if (item is IComponentProperties component && component.ShowBorder)
                {
                    _component = item;
                    break;
                }
            }
            if (_component!=null)
            {
                var __component = (_component as IComponentProperties);
                __component.ShowBorder=false;
                _component = __component.GetComponent() as FrameworkElement; 
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            SelectedComponent = null;
            //throw EventAggregator to set current
            base.OnMouseLeftButtonUp(e);
        }

    }

    public class ResetPropertyEvent : PubSubEvent<PropertyWindowEventModel> { }
   

    public class SetPropertyEvent : PubSubEvent<PropertyWindowEventModel> { }
    
}
