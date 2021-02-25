using RealtimeDrawingApplication.ViewModel.DrawingViewModel;
using System;

namespace RealtimeDrawingApplication.ViewModel
{
    public class PropertyWindowEventModel
    {
        public Guid PropertyId { get; set; }
        public ComponentEnum ComponentType { get; set; }
        public PropertyType PropertyType { get; set; }
        public object Value { get; set; }
    }
}
