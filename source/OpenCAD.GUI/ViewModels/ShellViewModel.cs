using System;
using System.Collections.Specialized;
using System.Reactive.Linq;
using AvalonDock;
using Caliburn.Micro;
using OpenCAD.GUI.Infrastructure;
using OpenCAD.GUI.Messages;

namespace OpenCAD.GUI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IHandle<AddTabViewCommand>, IHandle<AddToolViewCommand>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ProjectManager _projectManager;
        private PropertyChangedBase _activeDocument;

        public PropertyChangedBase ActiveDocument {
            get { return _activeDocument; }
            set {
                if (Equals(value, _activeDocument)) return;
                _activeDocument = value;
                NotifyOfPropertyChange(() => ActiveDocument);
            }
        }

        public BindableCollection<PropertyChangedBase> Tabs { get; set; }
        public BindableCollection<PropertyChangedBase> Tools { get; set; }
        public MenuViewModel Menu { get; set; }


        public ShellViewModel(IEventAggregator eventAggregator,
                              MenuViewModel menu,
                              Func<ProjectExplorerViewModel> projectExplorerViewModelBuilder,
                              ProjectManager projectManager,
                              Func<EventAggregatorDebugViewModel> eventsDebugBuilder) {
            _eventAggregator = eventAggregator;
            _projectManager = projectManager;
            Tabs = new BindableCollection<PropertyChangedBase>();
            Tools = new BindableCollection<PropertyChangedBase> {
                projectExplorerViewModelBuilder(),
                eventsDebugBuilder()
            };
            Menu = menu;

            InitializeEvents();
        }

        public void Handle(AddTabViewCommand message) {
            Tabs.Add(message.Model);
            ActiveDocument = message.Model;
        }

        public void Handle(AddToolViewCommand message) {
            Tools.Add(message.Model);
        }

        private void InitializeEvents() {
            var tabsChanged = Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Tabs, "CollectionChanged");
            var toolsChanged = Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Tools, "CollectionChanged");

            tabsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add).Subscribe(a => _eventAggregator.Publish(new TabAddedEvent {Args = a.EventArgs}));
            tabsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove).Subscribe(a => _eventAggregator.Publish(new TabRemovedEvent {Args = a.EventArgs}));

            toolsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add).Subscribe(a => _eventAggregator.Publish(new ToolAddedEvent {Args = a.EventArgs}));
            toolsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove).Subscribe(a => _eventAggregator.Publish(new ToolRemovedEvent {Args = a.EventArgs}));
        }

        public void DocumentClosed(DocumentClosedEventArgs e) {
            Tabs.Remove(e.Document.Content as Screen);
            var disposable = e.Document.Content as IDisposable;
            if (disposable != null) disposable.Dispose();
        }

        public void DocumentClosing(DocumentClosingEventArgs e) {}
    }
}