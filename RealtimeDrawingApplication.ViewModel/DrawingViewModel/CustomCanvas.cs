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

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class CustomCanvas:Canvas
    {
        public CustomCanvas()
        {
            Background = Brushes.LightGoldenrodYellow;
            AllowDrop = true;
            //IEventAggregator eventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            //eventAggregator.GetEvent<AddItemsEvent>().Subscribe(AddItem);
        }

        private void AddItem(ProjectProxy obj)
        {
            var fetchDBComponents = new List<IComponentProperties>();
            Children.Clear();
            foreach (var item in fetchDBComponents)
            {
                var Component = DrawingComponentService.GetDefaultComponent(item.ComponentType);
                Component.Width = item.Width;
                Component.Height = item.Height;
                SetLeft(Component, item.X);
                SetTop(Component, item.Y);
                Children.Add(Component);
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            var item = e.Data.GetData("toolboxitem") as FrameworkElement;
            var currentMousePosition = e.GetPosition(this);
            var xValue = currentMousePosition.X;
            var yValue = currentMousePosition.Y;
            //var xValue = currentMousePosition.X - item.Width / 2;
            //var yValue = currentMousePosition.Y - item.Height / 2;
            Canvas.SetLeft(item, xValue);
            Canvas.SetTop(item, yValue);
            Children.Add(item);
            //var allChildren = Children;
            //foreach (var _item in allChildren)
            //{
            //    var model = _item as IPropertyWindow;

            //}
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
                    model.X = position.X;
                    model.Y = position.Y;
                    SetLeft(SelectedComponent, position.X);
                    SetTop(SelectedComponent, position.Y);
                }
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            SelectedComponent = e.Source as FrameworkElement;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            SelectedComponent = null;
            base.OnMouseLeftButtonUp(e);
        }
    }
}
