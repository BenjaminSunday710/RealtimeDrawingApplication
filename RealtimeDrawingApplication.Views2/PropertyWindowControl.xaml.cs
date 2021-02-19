using Prism.Events;
using Prism.Ioc;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Application.Views
{
    /// <summary>
    /// Interaction logic for PropertyWindow.xaml
    /// </summary>
    public partial class PropertyWindowControl : UserControl
    {
        public Colour selectedColour { get; set; }
        public PropertyWindowControl()
        {
            InitializeComponent();
            DataContext = new ColourViewModel();
            //var eventAggregator = GenericServiceLocator.Container.Resolve<IEventAggregator>();
            //eventAggregator.GetEvent<ResetColourEvent>().Subscribe(GetFillColour);
        }

        private void GetFillColour(Colour selectedColour)
        {
            Binding binding1 = new Binding("Name");
            binding1.Source = selectedColour;
            FillColourName.SetBinding(TextBlock.TextProperty, binding1);
            Binding binding2 = new Binding("Brush");
            binding2.Source = selectedColour;
            //FillColour.SetBinding(, binding1);
        }

        private void ListView_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }

    public class GenericServiceLocator
    {
        public GenericServiceLocator()
        {
        }


        public static IContainerProvider Container { get; set; }
    }

}
