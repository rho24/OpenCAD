using Caliburn.Micro;

namespace OpenCAD.GUI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        private Screen _activeDocument;

        public Screen ActiveDocument {
            get { return _activeDocument; }
            set {
                if (Equals(value, _activeDocument)) return;
                _activeDocument = value;
                NotifyOfPropertyChange(() => ActiveDocument);
            }
        }

        public BindableCollection<Screen> Tabs { get; set; }
        public BindableCollection<Screen> Tools { get; set; }

        public ShellViewModel() {
            Tabs = new BindableCollection<Screen>();
            Tools = new BindableCollection<Screen>();
        }

        public void AddTeapot() {
            var model = new TeapotViewModel {Title = "Teapot Demo"};
            Tabs.Add(model);
            ActiveDocument = model;
        }

        public void AddTool() {
            var model = new TempToolViewModel {Title = "Temp tool"};
            Tools.Add(model);
        }
    }
}