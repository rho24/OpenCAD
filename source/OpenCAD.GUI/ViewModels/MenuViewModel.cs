using Caliburn.Micro;
using OpenCAD.GUI.Messages;

namespace OpenCAD.GUI.ViewModels
{
    public class MenuViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public MenuViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
        }

        public void AddTeapot() {
            _eventAggregator.Publish(new AddTabViewCommand {Model = new TeapotViewModel {Title = "Teapot Demo"}});
        }

        public void AddTool() {
            _eventAggregator.Publish(new AddToolViewCommand {Model = new TempToolViewModel {Title = "Temp tool"}});
        }

        public void AddEventsDebug() {
            _eventAggregator.Publish(new AddToolViewCommand {Model = new EventAggregatorDebugViewModel(_eventAggregator) {Title = "Events"}});
        }
    }
}