using Prism.Events;
using Prism.Mvvm;
using RealtimeDrawingApplication.Common;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Prism.Commands;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace RealtimeDrawingApplication.ViewModel
{
    public enum PropertyType { BorderFill, Fill, Width, Height, Title, BorderThickness, X, Y, BorderFillString, FillColourString, Angle }

    public class PropertyWindowControlViewModel:INotifyPropertyChanged
    {
        private double _width;
        private double _height;
        private double _x;
        private string _title;
        private double _y;
        private double _angle;
        private double _borderThickness;
        private SolidColorBrush _fill;
        private SolidColorBrush _borderFill;
        private SolidColorBrush _selectedFill;
        private SolidColorBrush _selectedBorderFill;
        
        public PropertyWindowControlViewModel()
        {
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            Colours = new Dictionary<string, SolidColorBrush>();
            LoadColours();
            EventAggregator.GetEvent<SetPropertyEvent>().Subscribe(SetupViewProperties);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double Width { get => _width; set { _width = value; OnPropertyChanged(); }  }
        public double Height { get => _height; set { _height = value; OnPropertyChanged(); } }
        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public double Angle { get => _angle; set { _angle = value; OnPropertyChanged(); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged();  } }
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }
        public double BorderThickness { get => _borderThickness; set { _borderThickness = value; OnPropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }
        public PropertyWindowEventModel CurrentlySelectedItem { get; set; }
        public SolidColorBrush Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }
        public SolidColorBrush BorderFill { get => _borderFill; set { _borderFill = value; OnPropertyChanged(); } }
        public static Dictionary<string, SolidColorBrush> Colours { get; set; }
        public SolidColorBrush SelectedFillColour { get => _selectedFill; set { _selectedFill = value; OnColourPropertyChanged(); } }
        public SolidColorBrush SelectedBorderFill { get => _selectedBorderFill; set { _selectedBorderFill = value; OnColourPropertyChanged(); } }

        public event PropertyChangedEventHandler ColourPropertyChanged;
        void OnColourPropertyChanged([CallerMemberName] string propertyName = "")
        {
            switch(propertyName)
            {
                case nameof(SelectedFillColour):
                    Fill = SelectedFillColour;
                    break;
                case nameof(SelectedBorderFill):
                    BorderFill = SelectedBorderFill;
                    break;
            }
            ColourPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static void LoadColours()
        {
            Type colorType = typeof(Colors);
            foreach (PropertyInfo property in colorType.GetProperties())
            {
                Colours.Add(property.Name, (new SolidColorBrush((Color)ColorConverter.ConvertFromString(property.Name))));
            }
        }

        public Dictionary<string, PropertyType> PropertyKeyValuePair = new Dictionary<string, PropertyType>
        {
            { nameof(Width),PropertyType.Width},
            { nameof(Height),PropertyType.Height},
            { nameof(Y),PropertyType.Y},
            { nameof(X),PropertyType.X},
            { nameof(Angle),PropertyType.Angle},
            { nameof(Title),PropertyType.Title},
            { nameof(Fill),PropertyType.Fill},
            { nameof(BorderFill),PropertyType.BorderFill},
            { nameof(BorderThickness),PropertyType.BorderThickness},
        };

        void SetupViewProperties(PropertyWindowEventModel propertyWindowEventModel)
        {
            if (propertyWindowEventModel!=null && propertyWindowEventModel.Value!=null)
            {
                CurrentlySelectedItem = propertyWindowEventModel;
                switch (propertyWindowEventModel.PropertyType)
                {
                    case PropertyType.BorderFill:
                        BorderFill = (SolidColorBrush)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.Fill:
                        Fill = (SolidColorBrush)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.Width:
                        Width = (double)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.Height:
                        Height = (double)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.Title:
                        Title = (string)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.BorderThickness:
                        BorderThickness = (double)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.X:
                        X = (double)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.Y:
                        Y = (double)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.Angle:
                        Angle = (double)propertyWindowEventModel.Value;
                        break;
                    default:
                        break;
                } 
            }
        }

        public object GetValue(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Width):
                    return Width;
                case nameof(Height):
                    return Height;
                case nameof(Y):
                    return Y;
                case nameof(X):
                    return X;
                case nameof(Angle):
                    return Angle;
                case nameof(Title):
                    return Title;
                case nameof(Fill):
                    return Fill;
                case nameof(BorderFill):
                    return BorderFill;
                case nameof(BorderThickness):
                    return BorderThickness;
                default:
                    return null;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var value = GetValue(propertyName);
           
            if (value != null)
            {
                if (CurrentlySelectedItem == null) return;
                CurrentlySelectedItem.PropertyType = PropertyKeyValuePair[propertyName];

                CurrentlySelectedItem.Value = value;
                EventAggregator.GetEvent<ResetPropertyEvent>().Publish(CurrentlySelectedItem);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
