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
        private string _fillColourString;
        private string _borderFillString;
        private SolidColorBrush _fill;
        private SolidColorBrush _borderFill;
        private bool _isOpen;
        private bool _isOpenFill;
        private Colour _colour;
        private ObservableCollection<string> _colourListItems;

        public PropertyWindowControlViewModel()
        {
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            _colour = new Colour();
            _colourListItems = new ObservableCollection<string>();
            _colourListItems = _colour.ColourList;
            PopUpOpenCommand = new DelegateCommand(IsPopUpOpenAction);
            PopUpOpenFillCommand = new DelegateCommand(IsPopUpOpenFillAction);
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
        public ObservableCollection<string> ColourListItems { get=>_colourListItems; set=>_colourListItems=value; }
        public bool IsOpen { get => _isOpen; set { _isOpen = value; OnPropertyChanged(); } }
        public bool IsOpenFill { get => _isOpenFill; set { _isOpenFill = value; OnPropertyChanged(); } }
        public string FillColourString { get => _fillColourString; set { _fillColourString = value; SetColour(); OnPropertyChanged();  } }
        public string BorderFillString { get => _borderFillString; set { _borderFillString = value; SetColour(); OnPropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }
        public DelegateCommand PopUpOpenCommand { get; set; }
        public DelegateCommand PopUpOpenFillCommand { get; set; }
        public PropertyWindowEventModel CurrentlySelectedItem { get; set; }
        public SolidColorBrush Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }
        public SolidColorBrush BorderFill { get => _borderFill; set { _borderFill = value; OnPropertyChanged(); } }

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
            {nameof(BorderFillString),PropertyType.BorderFillString },
            {nameof(FillColourString),PropertyType.FillColourString },
        };


        public void SetColour([CallerMemberName] string propertyName="")
        {
            if (propertyName == nameof(FillColourString))
            {
                Fill = _colour.GetSolidColour(FillColourString);
            }

            else if (propertyName == nameof(BorderFillString))
            {
                BorderFill = _colour.GetSolidColour(BorderFillString);
            }
        }

        public void GetColourString(SolidColorBrush brushValue, string propertyName = " " )
        {
            if (propertyName == nameof(Fill))
            {
                _fillColourString = _colour.GetColourString(Fill);
            }

            else
            {
                _borderFillString = _colour.GetColourString(BorderFill);
            }
        }


        void SetupViewProperties(PropertyWindowEventModel propertyWindowEventModel)
        {
            if (propertyWindowEventModel!=null && propertyWindowEventModel.Value!=null)
            {
                CurrentlySelectedItem = propertyWindowEventModel;
                switch (propertyWindowEventModel.PropertyType)
                {
                    case PropertyType.BorderFill:
                        BorderFill = (SolidColorBrush)propertyWindowEventModel.Value;
                        GetColourString(BorderFill, nameof(BorderFill));
                        break;
                    case PropertyType.Fill:
                        Fill = (SolidColorBrush)propertyWindowEventModel.Value;
                        GetColourString(Fill, nameof(Fill));
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
                case nameof(FillColourString):
                    return FillColourString;
                case nameof(BorderFillString):
                    return BorderFillString;
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
                CurrentlySelectedItem.PropertyType = PropertyKeyValuePair[propertyName];

                if (CurrentlySelectedItem.PropertyType == PropertyType.FillColourString)
                {
                    CurrentlySelectedItem.PropertyType = PropertyType.Fill;
                    SetColour(propertyName);
                    value = GetValue(nameof(Fill));
                }

                else if (CurrentlySelectedItem.PropertyType == PropertyType.BorderFillString)
                {
                    CurrentlySelectedItem.PropertyType = PropertyType.BorderFill;
                    SetColour(propertyName);
                    value = GetValue(nameof(BorderFill));
                }

                CurrentlySelectedItem.Value = value;
                EventAggregator.GetEvent<ResetPropertyEvent>().Publish(CurrentlySelectedItem);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void IsPopUpOpenAction()
        {
            IsOpen = !IsOpen;
        }

        void IsPopUpOpenFillAction()
        {
            IsOpenFill = !IsOpenFill;
        }
    }
}
