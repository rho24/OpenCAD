using System.Windows;
using Caliburn.Micro;
using OpenCAD.GUI.Messages;

namespace OpenCAD.GUI.ViewModels
{
    public class NewProjectDialogViewModel : ViewAware
    {
        private readonly IEventAggregator _eventAggregator;
        private string _name;

        public string Name {
            get { return _name; }
            set {
                if (value == _name) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public NewProjectDialogViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
        }

        public void Cancel() {
            CloseView();
        }

        public void Ok() {
            _eventAggregator.Publish(new CreateNewProjectCommand{Name = Name});
            CloseView();
        }

        private void CloseView() {
            var window = GetView() as Window;
            if (window != null) window.Close();
        }
    }
}