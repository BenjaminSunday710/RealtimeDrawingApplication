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

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class CustomCanvas : Canvas
    {
        private double xValue;
        private double yValue;
        //private double height;
        //private double width;

        public IEventAggregator EventAggregator { get; set; }

        public CustomCanvas()
        {
            Background = Brushes.LightGoldenrodYellow;
            AllowDrop = true;
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<ResetPropertyEvent>().Subscribe(SetupViewProperties);
        }

        public ObservableCollection<Colour> ColourListItems => new ObservableCollection<Colour>{
                new Colour {Name="Red", BrushValue=Brushes.Red },
                new Colour {Name="Green", BrushValue=Brushes.Green },
                new Colour {Name="Yellow", BrushValue=Brushes.Yellow },
                new Colour {Name="Blue", BrushValue=Brushes.Blue },
                new Colour {Name="Black", BrushValue=Brushes.Black },
                new Colour {Name="White", BrushValue=Brushes.White },
                new Colour {Name="Brown", BrushValue=Brushes.Brown },
                new Colour {Name="Pink", BrushValue=Brushes.Pink },
                new Colour {Name="PaleGoldenRod", BrushValue=Brushes.PaleGoldenrod },
                new Colour {Name="Violet", BrushValue=Brushes.Violet },
                new Colour {Name="Purple", BrushValue=Brushes.Purple },
                new Colour {Name="Indigo", BrushValue=Brushes.Indigo },
                new Colour {Name="IndianRed", BrushValue=Brushes.IndianRed },
                new Colour {Name="HoneyDew", BrushValue=Brushes.Honeydew },
                new Colour {Name="Khaki", BrushValue=Brushes.Khaki },
                new Colour {Name="LightBlue ", BrushValue=Brushes.LightBlue },
                new Colour {Name="Transparent", BrushValue=Brushes.Transparent },
                new Colour {Name="Tomato", BrushValue=Brushes.Tomato },
                new Colour {Name="SpringGreen", BrushValue=Brushes.SpringGreen },
                new Colour {Name="Gold", BrushValue=Brushes.Gold },
                new Colour {Name="ForestGreen", BrushValue=Brushes.ForestGreen },
                new Colour {Name="GhostWhite", BrushValue=Brushes.GhostWhite },
                new Colour {Name="Gray", BrushValue=Brushes.Gray },
                new Colour {Name="DimGray", BrushValue=Brushes.DimGray },
                new Colour {Name="DeepPink", BrushValue=Brushes.DeepPink },
                new Colour {Name="PaleVioletRed", BrushValue=Brushes.PaleVioletRed },
                new Colour {Name="Olive", BrushValue=Brushes.Olive },
                new Colour {Name="Orange", BrushValue=Brushes.Orange },
                new Colour {Name="Orchid", BrushValue=Brushes.Orchid },
                new Colour {Name="OldLace", BrushValue=Brushes.OldLace },
                new Colour {Name="Chocolate", BrushValue=Brushes.Chocolate },
                new Colour {Name="CornflowerBlue", BrushValue=Brushes.CornflowerBlue },
                new Colour {Name="Cornsilk", BrushValue=Brushes.Cornsilk },
                new Colour{Name="Cyan",BrushValue=Brushes.Cyan},
                new Colour{Name="Chartreuse",BrushValue=Brushes.Chartreuse},
                new Colour{Name="DarkMagenta",BrushValue=Brushes.DarkMagenta}
            };
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
                            case PropertyType.Border:
                                _item.ShapeBorder = ColourListItems.FirstOrDefault(x => x.BrushValue == (propertyWindowEventModel.Value as Brush)).BrushValue;
                                break;
                            case PropertyType.Fill:
                                _item.ShapeFill = ColourListItems.FirstOrDefault(x => x.BrushValue == (propertyWindowEventModel.Value as Brush)).BrushValue;
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
                                _item.BorderThickness = (int)propertyWindowEventModel.Value;
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
                if (_component != null)
                {
                    var __component = (_component as IComponentProperties);
                    _component = __component.GetComponent() as FrameworkElement;
                }
            }
        }

        //private void AddItem(ProjectProxy obj)
        //{
        //    var fetchDBComponents = new List<IComponentProperties>();
        //    Children.Clear();
        //    foreach (var item in fetchDBComponents)
        //    {
        //        var Component = DrawingComponentService.GetDefaultComponent(item.ComponentType);
        //        Component.Width = item.Width;
        //        Component.Height = item.Height;
        //        SetLeft(Component, item.X);
        //        SetTop(Component, item.Y);
        //        Children.Add(Component);
        //    }
        //}

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
                CurrentlySelectedItem.PropertyId =component.Id;
                CurrentlySelectedItem.ComponentType = component.ComponentType;
                CurrentlySelectedItem.PropertyType = PropertyType.X;
                CurrentlySelectedItem.Value =xValue;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);
                CurrentlySelectedItem.PropertyType = PropertyType.Y;
                CurrentlySelectedItem.Value = yValue;
                EventAggregator.GetEvent<SetPropertyEvent>().Publish(CurrentlySelectedItem);
            }
        }

        public FrameworkElement SelectedComponent { get; set; }
        public IComponentProperties ClickedComponent { get; set; }

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

        //void SetX()
        //{
        //    EventAggregator.GetEvent<DefaultPropertyWindowEvent>().Publish(new PropertyWindowEventModel
        //    {
        //        PropertyType = PropertyType.X,
        //        Value = xValue
        //    });
        //}

        //void SetY()
        //{
        //    EventAggregator.GetEvent<DefaultPropertyWindowEvent>().Publish(new PropertyWindowEventModel
        //    {
        //        PropertyType = PropertyType.Y,
        //        Value = yValue
        //    });
        //}

        //void SetWidth()
        //{
        //    EventAggregator.GetEvent<DefaultPropertyWindowEvent>().Publish(new PropertyWindowEventModel
        //    {
        //        PropertyType = PropertyType.Width,
        //        Value = width
        //    });
        //}

        //void SetHeight()
        //{
        //    EventAggregator.GetEvent<DefaultPropertyWindowEvent>().Publish(new PropertyWindowEventModel
        //    {
        //        PropertyType = PropertyType.Height,
        //        Value = height
        //    });
        //}

        //public void UpdateComponent(PropertyWindowEventModel e)
        //{
        //    if (SelectedComponent is IComponentProperties component)
        //    {
        //        switch(e.PropertyType)
        //        {
        //            case PropertyType.Height:
        //                component.Height = (double)e.Value;
        //                break;
        //            case PropertyType.Width:
        //                component.Width = (double)e.Value;
        //                break;
        //            case PropertyType.Title:
        //                component.Title = (string)e.Value;
        //                break;
        //            case PropertyType.Fill:
        //                component.ShapeFill = ((Colour)e.Value).BrushValue;
        //                break;
        //            case PropertyType.Border:
        //                component.ShapeBorder=((Colour)e.Value).BrushValue;
        //                break;
        //            case PropertyType.BorderThickness:
        //                component.BorderThickness = (int)e.Value;
        //                break;
        //        }
        //    }
        //}

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        void ClickedComponentResetProperties(PropertyWindowEventModel e)
        {
            switch (e.PropertyType)
            {
                case PropertyType.Height:
                    ClickedComponent.Height = (double)e.Value;
                    break;
                case PropertyType.Width:
                    ClickedComponent.Width = (double)e.Value;
                    break;
                case PropertyType.Title:
                    ClickedComponent.Title = (string)e.Value;
                    break;
                case PropertyType.Fill:
                    ClickedComponent.ShapeFill = ((Colour)e.Value).BrushValue;
                    break;
                case PropertyType.Border:
                    ClickedComponent.ShapeBorder = ((Colour)e.Value).BrushValue;
                    break;
                case PropertyType.BorderThickness:
                    ClickedComponent.BorderThickness = (int)e.Value;
                    break;
                case PropertyType.X:
                    ClickedComponent.ShapeBorder = ((Colour)e.Value).BrushValue;
                    break;
                case PropertyType.Y:
                    ClickedComponent.BorderThickness = (int)e.Value;
                    break;
                default:
                    break;
            }
        }
    }

    public class DefaultPropertyWindowEvent : PubSubEvent<List<PropertyWindowEventModel>> { }
}
