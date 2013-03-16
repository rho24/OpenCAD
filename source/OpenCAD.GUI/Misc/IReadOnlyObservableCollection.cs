using System.Collections.Generic;
using System.Collections.Specialized;

namespace OpenCAD.GUI.Misc
{
    public interface IReadOnlyObservableCollection<T>: ICollection<T>, INotifyCollectionChanged
    {}
}