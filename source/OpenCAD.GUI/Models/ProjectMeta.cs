using System.Collections.ObjectModel;
using Caliburn.Micro;
using OpenCAD.GUI.Misc;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI.Models
{
    public class ProjectMeta : PropertyChangedBase, IProjectMeta
    {
        private ObservableCollection<JsonPartMeta> _parts;
        private IReadOnlyObservableCollection<IPartMeta> _readOnlyParts;

        public ObservableCollection<JsonPartMeta> Parts {
            get { return _parts; }
            set {
                if (Equals(value, _parts)) return;
                _parts = value;
                _readOnlyParts = value.WrapReadOnly<JsonPartMeta, IPartMeta>();
                NotifyOfPropertyChange(() => Parts);
            }
        }

        public string Name { get; set; }

        IReadOnlyObservableCollection<IPartMeta> IProjectMeta.Parts {
            get { return _readOnlyParts; }
        }
    }
}