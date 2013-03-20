using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.GUI.Misc
{
    public static class Extensions
    {
        public static IEnumerable<dynamic> DynamicSelect(this object source, Func<dynamic, dynamic> map) {
            foreach (dynamic item in source as dynamic)
                yield return map(item);
        }

        public static IEnumerable<dynamic> DynamicWhere(this object source, Func<dynamic, dynamic> predicate) {
            foreach (dynamic item in source as dynamic) {
                if (predicate(item))
                    yield return item;
            }
        }

        public static IReadOnlyObservableCollection<TResult> WrapReadOnly<TSource, TResult>(this ObservableCollection<TSource> list) where TSource : TResult {
            return new MyReadOnlyObservableCollection<TSource, TResult>(list);
        }
    }
}