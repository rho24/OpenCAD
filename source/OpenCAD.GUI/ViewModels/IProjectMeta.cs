using System.Collections.ObjectModel;
using System.ComponentModel;
using Caliburn.Micro;

namespace OpenCAD.GUI.ViewModels
{
    public interface IProjectMeta : INotifyPropertyChanged
    {
        string Name { get; }

        ReadOnlyObservableCollection<PartMeta> Parts { get; }
    }

    public interface IPartMeta : INotifyPropertyChanged
    {
        string Name { get; }
    }

    public class ProjectMeta : PropertyChangedBase, IProjectMeta
    {
        private ObservableCollection<PartMeta> _parts;
        private ReadOnlyObservableCollection<PartMeta> _readOnlyParts;

        public ObservableCollection<PartMeta> Parts {
            get { return _parts; }
            set {
                if (Equals(value, _parts)) return;
                _parts = value;
                _readOnlyParts = new ReadOnlyObservableCollection<PartMeta>(value); //TODO: Need covariant/contravariant version
                NotifyOfPropertyChange(() => Parts);
            }
        }

        public string Name { get; set; }

        ReadOnlyObservableCollection<PartMeta> IProjectMeta.Parts {
            get { return _readOnlyParts; }
        }
    }

    public class PartMeta : PropertyChangedBase, IPartMeta
    {
        public string Name { get; set; }
    }
}