using Prism.Ioc;
using Prism.Unity;
using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.ViewModel;
using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using RealtimeDrawingApplication.Views;
using System.Windows;

namespace RealtimeDrawingApplication.Views2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            GenericServiceLocator.Container = Container;
            return Container.Resolve<LogInPageWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

    }
}
