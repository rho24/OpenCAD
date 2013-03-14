using Caliburn.Micro;
using OpenCAD.GUI.Messages;

namespace OpenCAD.GUI.ViewModels
{
    public class MenuViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public BindableCollection<MenuItemViewModel> Items { get; set; }

        public MenuViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            Items = new BindableCollection<MenuItemViewModel> {
                new MenuItemViewModel {
                    Header = "_FILE",
                    Items = new BindableCollection<MenuItemViewModel> {
                        new MenuItemViewModel {
                            Header = "Open Teapot",
                            Action = () =>
                                     _eventAggregator.Publish(new AddTabViewCommand {Model = new TeapotViewModel {Title = "Teapot Demo"}})
                        },
                        new MenuItemViewModel {
                            Header = "Open Temp Tool",
                            Action = () =>
                                     _eventAggregator.Publish(new AddToolViewCommand {Model = new TempToolViewModel {Title = "Temp tool"}})
                        },
                        new MenuItemViewModel {
                            Header = "Open Events Tool",
                            Action = () =>
                                     _eventAggregator.Publish(new AddToolViewCommand {Model = new EventAggregatorDebugViewModel(_eventAggregator) {Title = "Events"}})
                        },
                        new MenuItemViewModel {
                            Header = "Open Project Explorer",
                            Action = () =>
                                     _eventAggregator.Publish(new AddProjectExplorerViewCommand {
                                         Model =
                                             new ProjectExplorerViewModel {Project = new ProjectManager {Name = "Temp project", Parts = new[] {new ProjectManager.Part {Name = "Temp part"}}}}
                                     })
                        }
                    }
                },
                new MenuItemViewModel {
                    Header = "_EDIT"
                },
                new MenuItemViewModel {
                    Header = "_VIEW"
                },
                new MenuItemViewModel {
                    Header = "_WINDOW"
                },
                new MenuItemViewModel {
                    Header = "_HELP"
                }
            };
        }
    }
}