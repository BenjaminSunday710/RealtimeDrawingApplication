using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RealtimeDrawingApplication.Common
{
    public class Colour
    {
        private Color _color;
        private SolidColorBrush _brushValue;
        private ObservableCollection<string> _colourList;
        private Dictionary<SolidColorBrush, string> _colourDictionary;
        private string _colourString;

        public Colour()
        {
            _colourDictionary = new Dictionary<SolidColorBrush, string>();
            _colourList = new ObservableCollection<string>();
            LoadColour();
        }

        public ObservableCollection<string> ColourList { get => _colourList; set => _colourList = value; }
        public string ColourString { get => _colourString; set => _colourString = value; }
        public Dictionary<SolidColorBrush,string> ColourDictionary { get => _colourDictionary; set => _colourDictionary = value; }

        public SolidColorBrush GetSolidColour(string colourString)
        {
            _color = (Color)ColorConverter.ConvertFromString(colourString);
            _brushValue = new SolidColorBrush(_color);
            return _brushValue;
        }

        public Color GetColour(string colourString)
        {
            _color = (Color)ColorConverter.ConvertFromString(colourString);
            return _color;
        }

        private ObservableCollection<string> LoadColour()
        {
            Type colorType = typeof(Colors);
            foreach (PropertyInfo property in colorType.GetProperties())
            {
                _colourDictionary[GetSolidColour(property.Name)] = property.Name;
                _colourList.Add(property.Name);
            }
            return _colourList;
        }

        public string GetColourString( SolidColorBrush brushValue)
        {
            string colourString = " ";
            if (ColourDictionary.TryGetValue(brushValue, out string value))
            {
                colourString = value; 
            }
            return colourString;
        }
    }
}
