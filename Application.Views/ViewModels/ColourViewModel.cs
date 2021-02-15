﻿using Application.Views.ViewModels;
using RealtimeDrawingApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace RealtimeDrawingApplication.ViewModels
{
    class ColourViewModel:INotifyPropertyChanged
    {
        private Colour _selectedItems;

        public IList<Colour> ColourListItems { get; set; }
        public bool IsOpen { get; set; }
        public Colour SelectedItem { get=>_selectedItems; set { _selectedItems = value; OnPropertyChanged(SelectedItem.Name); } }

        public ColourViewModel()
        {
            ColourListItems = LoadColourList();
            PopUpOpenCommand = new RelayCommand(IsPopUpOpenAction, () => true);
        }

        public RelayCommand PopUpOpenCommand { get; set; }

        void IsPopUpOpenAction()
        {
            IsOpen = !IsOpen;
        }

        public List<Colour> LoadColourList()
        {
            List<Colour> colours = new List<Colour> {
                new Colour {Name="Red", Brush=Brushes.Red },
                new Colour {Name="Green", Brush=Brushes.Green },
                new Colour {Name="Yellow", Brush=Brushes.Yellow },
                new Colour {Name="Blue", Brush=Brushes.Blue },
                new Colour {Name="Black", Brush=Brushes.Black },
                new Colour {Name="White", Brush=Brushes.White },
                new Colour {Name="Brown", Brush=Brushes.Brown },
                new Colour {Name="Pink", Brush=Brushes.Pink },
                new Colour {Name="PaleGoldenRod", Brush=Brushes.PaleGoldenrod },
                new Colour {Name="Violet", Brush=Brushes.Violet },
                new Colour {Name="Purple", Brush=Brushes.Purple },
                new Colour {Name="Indigo", Brush=Brushes.Indigo },
                new Colour {Name="IndianRed", Brush=Brushes.IndianRed },
                new Colour {Name="HoneyDew", Brush=Brushes.Honeydew },
                new Colour {Name="Khaki", Brush=Brushes.Khaki },
                new Colour {Name="LightBlue ", Brush=Brushes.LightBlue },
                new Colour {Name="Transparent", Brush=Brushes.Transparent },
                new Colour {Name="Tomato", Brush=Brushes.Tomato },
                new Colour {Name="SpringGreen", Brush=Brushes.SpringGreen },
                new Colour {Name="Gold", Brush=Brushes.Gold },
                new Colour {Name="ForestGreen", Brush=Brushes.ForestGreen },
                new Colour {Name="GhostWhite", Brush=Brushes.GhostWhite },
                new Colour {Name="Gray", Brush=Brushes.Gray },
                new Colour {Name="DimGray", Brush=Brushes.DimGray },
                new Colour {Name="DeepPink", Brush=Brushes.DeepPink },
                new Colour {Name="PaleVioletRed", Brush=Brushes.PaleVioletRed },
                new Colour {Name="Olive", Brush=Brushes.Olive },
                new Colour {Name="Orange", Brush=Brushes.Orange },
                new Colour {Name="Orchid", Brush=Brushes.Orchid },
                new Colour {Name="OldLace", Brush=Brushes.OldLace },
                new Colour {Name="Chocolate", Brush=Brushes.Chocolate },
                new Colour {Name="CornflowerBlue", Brush=Brushes.CornflowerBlue },
                new Colour {Name="Cornsilk", Brush=Brushes.Cornsilk },
                new Colour{Name="Cyan",Brush=Brushes.Cyan}
            };
            return colours;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
