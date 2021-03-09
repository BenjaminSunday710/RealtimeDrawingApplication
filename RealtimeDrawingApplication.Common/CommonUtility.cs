using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealtimeDrawingApplication.Common
{
    public class CommonUtility
    {
        public static Dictionary<string, Type> RoutedPages { get; private set; } = new Dictionary<string, Type>();
        public static object GetPage(string name)
        {
            if (RoutedPages.ContainsKey(name))
            {
                var type = RoutedPages[name];
                var page = Activator.CreateInstance(type);
                return page;
            }
            return null;
        }

    }
}
