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
    public enum PropertyType { Border, Fill, Width, Height, Title, BorderThickness, X, Y }

    public class PropertyWindowControlViewModel:INotifyPropertyChanged
    {
        private double _width;
        private double _height;
        private double _x;
        private string _title;
        private double _y;
        private double _borderThickness;
        private Colour _selectedFill = new Colour() { Name = "Red", BrushValue = Brushes.Red };
        private Colour _selectedBorder = new Colour() { Name = "Red", BrushValue = Brushes.Red };
        private bool _isOpen;
        private bool _isOpenFill;

        public PropertyWindowControlViewModel()
        {
            EventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<DefaultPropertyWindowEvent>().Subscribe(e => GetPropertyChange(e));
            ColourListItems = ColourService.LoadColourList();
            PopUpOpenCommand = new DelegateCommand(IsPopUpOpenAction);
            PopUpOpenFillCommand = new DelegateCommand(IsPopUpOpenFillAction);
            EventAggregator.GetEvent<SetPropertyEvent>().Subscribe(SetupViewProperties);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double Width { get => _width; set { _width = value; OnPropertyChanged(); }  }
        public double Height { get => _height; set { _height = value; OnPropertyChanged(); } }
        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged();  } }
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }
        public double BorderThickness { get => _borderThickness; set { _borderThickness = value; OnPropertyChanged(); } }
        public ObservableCollection<Colour> ColourListItems { get; set; }
        public bool IsOpen { get => _isOpen; set { _isOpen = value; OnPropertyChanged(); } }
        public bool IsOpenFill { get => _isOpenFill; set { _isOpenFill = value; OnPropertyChanged(); } }
        public Colour SelectedFill { get => _selectedFill; set { _selectedFill = value; OnPropertyChanged(); } }
        public Colour SelectedBorder { get => _selectedBorder; set { _selectedBorder = value; OnPropertyChanged(); } }
        public IEventAggregator EventAggregator { get; set; }
        public DelegateCommand PopUpOpenCommand { get; set; }
        public DelegateCommand PopUpOpenFillCommand { get; set; }
        public PropertyWindowEventModel CurrentlySelectedItem { get; set; }

        public Dictionary<string, PropertyType> PropertyKeyValuePair = new Dictionary<string, PropertyType>
        {
            { nameof(Width),PropertyType.Width},
            { nameof(Height),PropertyType.Height},
            { nameof(Y),PropertyType.Y},
            { nameof(X),PropertyType.X},
            { nameof(Title),PropertyType.Title},
            { nameof(SelectedFill),PropertyType.Fill},
            { nameof(SelectedBorder),PropertyType.Border},
            { nameof(BorderThickness),PropertyType.BorderThickness},
        };

        void SetupViewProperties(PropertyWindowEventModel propertyWindowEventModel)
        {
            if (propertyWindowEventModel!=null && propertyWindowEventModel.Value!=null)
            {
                CurrentlySelectedItem = propertyWindowEventModel;
                switch (propertyWindowEventModel.PropertyType)
                {
                    case PropertyType.Border:
                        SelectedBorder = ColourListItems.FirstOrDefault(x => x.BrushValue == (propertyWindowEventModel.Value as Brush));
                        break;
                    case PropertyType.Fill:
                        SelectedFill = ColourListItems.FirstOrDefault(x => x.BrushValue == (propertyWindowEventModel.Value as Brush));
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
                        BorderThickness = (int)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.X:
                        X = (double)propertyWindowEventModel.Value;
                        break;
                    case PropertyType.Y:
                        Y = (double)propertyWindowEventModel.Value;
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
                case nameof(Title):
                    return Title;
                case nameof(SelectedFill):
                    return SelectedFill;
                case nameof(SelectedBorder):
                    return SelectedBorder;
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
        
        void GetPropertyChange(List<PropertyWindowEventModel> e)
        {
            foreach (var item in e)
            {
                switch (item.PropertyType)
                {
                    case PropertyType.X:
                        _x = (double)item.Value;
                        break;
                    case PropertyType.Y:
                        _y = (double)item.Value;
                        break;
                    case PropertyType.Width:
                        _width = (double)item.Value;
                        break;
                    case PropertyType.Height:
                        _height = (double)item.Value;
                        break;
                    case PropertyType.Fill:
                        _selectedFill = (Colour)item.Value;
                        break;
                    case PropertyType.Border:
                        _selectedBorder = (Colour)item.Value;
                        break;
                    case PropertyType.BorderThickness:
                        _borderThickness = (int)item.Value;
                        break;
                    case PropertyType.Title:
                        _title = (string)item.Value;
                        break;
                    default:
                        break;
                }
            }
           
        }

    }
}
