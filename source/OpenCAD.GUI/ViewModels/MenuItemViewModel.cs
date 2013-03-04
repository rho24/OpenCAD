using Caliburn.Micro;
using Action = System.Action;

namespace OpenCAD.GUI.ViewModels
{
    public class MenuItemViewModel : PropertyChangedBase
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public BindableCollection<MenuItemViewModel> Items { get; set; }

        public Action Action { get; set; }

        public void Execute() {
            if (Action != null)
                Action();
        }
    }
}