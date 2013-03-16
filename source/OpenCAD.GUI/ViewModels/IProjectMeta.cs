using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OpenCAD.GUI.Models;

namespace OpenCAD.GUI.ViewModels
{
    public interface IProjectMeta : INotifyPropertyChanged
    {
        string Name { get; }

        ReadOnlyObservableCollection<JsonPartMeta> Parts { get; }
    }

    public interface IPartMeta : INotifyPropertyChanged
    {
        string Name { get; }
        DateTime? CreatedDate { get; }
        DateTime? ModifiedDate { get; }
        bool Exists { get; }
    }
}