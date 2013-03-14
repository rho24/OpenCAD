using Caliburn.Micro;
using OpenCAD.GUI.Messages;

namespace OpenCAD.GUI.ViewModels
{
    public class ProjectExplorerViewModel : AvalonViewModelBaseBase, IHandle<ProjectOpenedEvent>
    {
        private IProjectMeta _projectMeta;
        private readonly IEventAggregator _eventAggregator;

        public IProjectMeta ProjectMeta {
            get { return _projectMeta; }
            set {
                if (Equals(value, _projectMeta)) return;
                _projectMeta = value;
                NotifyOfPropertyChange(() => ProjectMeta);
                NotifyOfPropertyChange(() => ProjectIsVisible);
                NotifyOfPropertyChange(() => ButtonsIsVisible);
            }
        }
        
        public bool ProjectIsVisible {
            get { return ProjectMeta != null; }
        }

        public bool ButtonsIsVisible {
            get { return ProjectMeta == null; }
        }

        public ProjectExplorerViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            Title = "Project Explorer";
        }

        public void LoadProject() {
            _eventAggregator.Publish(new OpenProjectCommand());
        }

        public void Handle(ProjectOpenedEvent message) {
            ProjectMeta = message.Project;
        }
    }
}