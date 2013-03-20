using System;
using System.ComponentModel;
using OpenCAD.GUI.Misc;

namespace OpenCAD.GUI.ViewModels
{
    public interface IProjectMeta : INotifyPropertyChanged
    {
        string Name { get; }
        DateTime? CreatedDate { get; }
        DateTime? ModifiedDate { get; }
        bool Exists { get; }

        IReadOnlyObservableCollection<IPartMeta> Parts { get; }
    }

    public interface IPartMeta : INotifyPropertyChanged
    {
        string Name { get; }
        DateTime? CreatedDate { get; }
        DateTime? ModifiedDate { get; }
        bool Exists { get; }
    }
}