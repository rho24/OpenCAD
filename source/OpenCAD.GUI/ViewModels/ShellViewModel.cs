using System;
using System.Collections.Specialized;
using System.Reactive.Linq;
using AvalonDock;
using Caliburn.Micro;
using OpenCAD.GUI.Messages;

namespace OpenCAD.GUI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        private readonly EventAggregator _eventAggregator;
        private Screen _activeDocument;

        public Screen ActiveDocument {
            get { return _activeDocument; }
            set {
                if (Equals(value, _activeDocument)) return;
                _activeDocument = value;
                NotifyOfPropertyChange(() => ActiveDocument);
            }
        }

        public BindableCollection<Screen> Tabs { get; set; }
        public BindableCollection<Screen> Tools { get; set; }

        public ShellViewModel(EventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            Tabs = new BindableCollection<Screen>();
            Tools = new BindableCollection<Screen>() {new EventAggregatorDebugViewModel(eventAggregator) {Title = "Event Debugger"}};

            InitializeEvents();
        }

        private void InitializeEvents() {
            var tabsChanged = Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Tabs, "CollectionChanged");
            var toolsChanged = Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Tools, "CollectionChanged");

            tabsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add).Subscribe(a => _eventAggregator.Publish(new TabAddedEvent {Args = a.EventArgs}));
            tabsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove).Subscribe(a => _eventAggregator.Publish(new TabRemovedEvent {Args = a.EventArgs}));

            toolsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add).Subscribe(a => _eventAggregator.Publish(new ToolAddedEvent {Args = a.EventArgs}));
            toolsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove).Subscribe(a => _eventAggregator.Publish(new ToolRemovedEvent {Args = a.EventArgs}));
        }

        public void AddTeapot() {
            var model = new TeapotViewModel {Title = "Teapot Demo"};
            Tabs.Add(model);
            ActiveDocument = model;
        }

        public void AddTool() {
            var model = new TempToolViewModel {Title = "Temp tool"};
            Tools.Add(model);
        }

        public void AddEventsDebug() {
            var model = new EventAggregatorDebugViewModel(_eventAggregator) {Title = "Events"};
            Tools.Add(model);
        }

        public void DocumentClosed(DocumentClosedEventArgs e) {
            Tabs.Remove(e.Document.Content as Screen);
            var disposable = e.Document.Content as IDisposable;
            if (disposable != null) disposable.Dispose();
        }

        public void DocumentClosing(DocumentClosingEventArgs e) {}
    }
}