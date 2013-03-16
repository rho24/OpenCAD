using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace OpenCAD.GUI.Misc
{
    public class MyReadOnlyObservableCollection<Tout, Tin> : IReadOnlyObservableCollection<Tout> where Tin : class
    {
        private readonly ObservableCollection<Tin> _list;

        public MyReadOnlyObservableCollection(ObservableCollection<Tin> list) {
            _list = list;
            _list.CollectionChanged += (sender, args) => OnCollectionChanged(args);
        }

        public IEnumerator<Tout> GetEnumerator() {
            return _list.Cast<Tout>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(Tout item) {
            throw new InvalidOperationException("Cannot change ReadOnly collection");
        }

        public void Clear() {
            throw new InvalidOperationException("Cannot change ReadOnly collection");
        }

        public bool Contains(Tout item) {
            return (item is Tin) && _list.Contains(item as Tin);
        }

        public void CopyTo(Tout[] array, int arrayIndex) {
            throw new InvalidOperationException("Cannot copy ReadOnly collection");
        }

        public bool Remove(Tout item) {
            throw new InvalidOperationException("Cannot change ReadOnly collection");
        }

        public int Count {
            get { return _list.Count; }
        }

        public bool IsReadOnly {
            get { return true; }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args) {
            var handlers = CollectionChanged;
            if (handlers != null)
                handlers(this, args);
        }
    }
}