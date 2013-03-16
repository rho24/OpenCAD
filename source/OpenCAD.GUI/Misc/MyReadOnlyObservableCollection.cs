using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace OpenCAD.GUI.Misc
{
    public class MyReadOnlyObservableCollection<TSource, TResult> : IReadOnlyObservableCollection<TResult>
    {
        private readonly ObservableCollection<TSource> _list;

        public MyReadOnlyObservableCollection(ObservableCollection<TSource> list) {
            _list = list;
            _list.CollectionChanged += (sender, args) => OnCollectionChanged(args);
        }

        public IEnumerator<TResult> GetEnumerator() {
            return _list.Cast<TResult>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args) {
            var handlers = CollectionChanged;
            if (handlers != null)
                handlers(this, args);
        }
    }
}