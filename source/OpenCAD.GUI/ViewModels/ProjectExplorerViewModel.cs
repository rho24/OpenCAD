namespace OpenCAD.GUI.ViewModels
{
    public class ProjectExplorerViewModel : AvalonViewModelBaseBase
    {
        private IProjectMeta _projectMeta;

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

        public ProjectExplorerViewModel() {
            Title = "Project Explorer";
        }

        public void LoadProject() {
            ProjectMeta = new ProjectMeta();
        }
    }
}