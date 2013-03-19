using System.Collections.Generic;
using System.Collections.Specialized;

namespace OpenCAD.GUI.Misc
{
    public interface IReadOnlyObservableCollection<out T>: IEnumerable<T>, INotifyCollectionChanged
    {}
}