using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RealtimeDrawingApplication.Common
{
    public class ColourService
    {
        public static ObservableCollection<Colour> LoadColourList()
        {
            ObservableCollection<Colour> colours = new ObservableCollection<Colour>{
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
            return colours;
        }
    }
}
