using Prism.Events;
using RealtimeDrawingApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Ioc;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RealtimeDrawingApplication.ViewModel.DrawingViewModel
{
    public class TextBoxComponent:TextBox, IComponentProperties, INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public TextBoxComponent(string text="Text")
        {
            Text = text;
            FontSize = 14;
            Id = Guid.NewGuid();
        }

        private void ChangeProperty(PropertyWindowEventModel e)
        {
            switch (e.PropertyType)
            {
                case PropertyType.BorderFill:
                    ShapeBorder = (SolidColorBrush)e.Value;
                    break;
                case PropertyType.Fill:
                    Foreground = (SolidColorBrush)e.Value;
                    ShapeFill = (SolidColorBrush)Foreground;
                    break;
                case PropertyType.Width:
                    Width = (double)e.Value;
                    break;
                case PropertyType.Height:
                    Height = (double)e.Value;
                    break;
                case PropertyType.BorderThickness:
                    BorderThickness = (int)e.Value;
                    break;
                case PropertyType.Title:
                    Title = (string)e.Value;
                    break;
                case PropertyType.X:
                    X = (double)e.Value;
                    break;
                case PropertyType.Y:
                    Y = (double)e.Value;
                    break;
                case PropertyType.Angle:
                    Angle = (double)e.Value;
                    break;
                default:
                    break;
            }
        }

        public ComponentEnum ComponentType { get; set; }
        public string Title { get; set; }
        public SolidColorBrush ShapeBorder { get; set; }
        public SolidColorBrush ShapeFill { get; set; }
        public new double BorderThickness { get; set; }
        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }
        public double Angle { get; set; }
        public Guid Id { get; set; }
        public bool ShowBorder { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object GetComponent()
        {
            return this;
        }
    }
}
