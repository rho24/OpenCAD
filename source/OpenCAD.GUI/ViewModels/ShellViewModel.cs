using AvalonDock.Layout;
using Caliburn.Micro;

namespace OpenCAD.GUI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        private Screen _activeDocument;
        public BindableCollection<Screen> Tabs { get; set; }
        public Screen ActiveDocument {
            get { return _activeDocument; }
            set {
                if (Equals(value, _activeDocument)) return;
                _activeDocument = value;
                NotifyOfPropertyChange(() => ActiveDocument);
            }
        }

        public ShellViewModel() {
            Tabs = new BindableCollection<Screen>();
        }

        public void AddTeapot() {
            var model = new TeapotViewModel {Title = "Teapot Demo"};
            Tabs.Add(model);
            ActiveDocument = model;
        }
    }
}