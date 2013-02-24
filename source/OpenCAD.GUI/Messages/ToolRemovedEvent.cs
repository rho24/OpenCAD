using System.Collections.Specialized;
using System.Linq;

namespace OpenCAD.GUI.Messages
{
    internal class ToolRemovedEvent
    {
        public NotifyCollectionChangedEventArgs Args { get; set; }

        public override string ToString() {
            return string.Format("Removed {0} Tool(s)", Args.OldItems.Cast<object>().Count());
        }
    }
}