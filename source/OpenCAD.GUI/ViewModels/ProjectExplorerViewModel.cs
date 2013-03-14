using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;

namespace OpenCAD.GUI.ViewModels
{
    public class ProjectExplorerViewModel : PropertyChangedBase
    {
        private BindableCollection<PartViewModel> _parts;
        private ProjectManager _project;

        public string Title {
            get { return "Project Explorer"; }
        }

        public ProjectManager Project {
            get { return _project; }
            set {
                if (Equals(value, _project)) return;
                _project = value;
                InitializeProject(value);
                NotifyOfPropertyChange(() => Project);
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string Name {
            get { return Project.Name; }
        }

        public BindableCollection<PartViewModel> Parts {
            get { return _parts; }
            set {
                if (Equals(value, _parts)) return;
                _parts = value;
                NotifyOfPropertyChange(() => Parts);
            }
        }

        private void InitializeProject(ProjectManager project) {
            Parts = new BindableCollection<PartViewModel>(project.Parts.Select(p => new PartViewModel(p)));
        }

        #region Nested type: PartViewModel

        public class PartViewModel
        {
            private readonly ProjectManager.Part _part;

            public string Name {
                get { return _part.Name; }
            }

            public PartViewModel(ProjectManager.Part part) {
                _part = part;
            }
        }

        #endregion
    }

    public class ProjectManager
    {
        public string Name { get; set; }
        public IEnumerable<Part> Parts { get; set; }

        #region Nested type: Part

        public class Part
        {
            public string Name { get; set; }
        }

        #endregion
    }
}