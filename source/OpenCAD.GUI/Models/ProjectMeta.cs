using System.Collections.ObjectModel;
using Caliburn.Micro;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI.Models
{
    public class ProjectMeta : PropertyChangedBase, IProjectMeta
    {
        private ObservableCollection<JsonPartMeta> _parts;
        private ReadOnlyObservableCollection<JsonPartMeta> _readOnlyParts;

        public ObservableCollection<JsonPartMeta> Parts {
            get { return _parts; }
            set {
                if (Equals(value, _parts)) return;
                _parts = value;
                _readOnlyParts = new ReadOnlyObservableCollection<JsonPartMeta>(value); //TODO: Need covariant/contravariant version
                NotifyOfPropertyChange(() => Parts);
            }
        }

        public string Name { get; set; }

        ReadOnlyObservableCollection<JsonPartMeta> IProjectMeta.Parts {
            get { return _readOnlyParts; }
        }
    }
}