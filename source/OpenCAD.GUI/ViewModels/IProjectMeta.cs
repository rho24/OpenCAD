using System.Collections.ObjectModel;
using System.ComponentModel;
using Caliburn.Micro;

namespace OpenCAD.GUI.ViewModels
{
    public interface IProjectMeta : INotifyPropertyChanged
    {
        string Name { get; }

        ReadOnlyObservableCollection<IPartMeta> Parts { get; }
    }

    public interface IPartMeta : INotifyPropertyChanged
    {
        string Name { get; }
    }

    public class ProjectMeta : PropertyChangedBase, IProjectMeta
    {
        public string Name {
            get { return "temp proj"; }
        }

        public ReadOnlyObservableCollection<IPartMeta> Parts {
            get { return new ReadOnlyObservableCollection<IPartMeta>(new ObservableCollection<IPartMeta>(new[] {new PartMeta()})); }
        }
    }

    public class PartMeta : PropertyChangedBase, IPartMeta
    {
        public string Name {
            get { return "temp part"; }
        }
    }
}