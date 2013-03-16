using System;
using Caliburn.Micro;
using OpenCAD.GUI.Messages;
using OpenCAD.GUI.Models;

namespace OpenCAD.GUI.ViewModels
{
    public class MenuViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public BindableCollection<MenuItemViewModel> Items { get; set; }

        public MenuViewModel(IEventAggregator eventAggregator,
                             Func<ProjectExplorerViewModel> projectExplorerViewModelBuilder,
                             Func<EventAggregatorDebugViewModel> eventsDebugBuilder,
                             Func<TeapotViewModel> teapotBuilder) {
            _eventAggregator = eventAggregator;
            Items = new BindableCollection<MenuItemViewModel> {
                new MenuItemViewModel {
                    Header = "_FILE",
                    Items = new BindableCollection<MenuItemViewModel> {
                        new MenuItemViewModel {
                            Header = "Open Teapot",
                            Action = () =>
                                     _eventAggregator.Publish(new AddTabViewCommand {Model = teapotBuilder()})
                        },
                        new MenuItemViewModel {
                            Header = "Open Events Tool",
                            Action = () =>
                                     _eventAggregator.Publish(new AddToolViewCommand {Model = eventsDebugBuilder()})
                        },
                        new MenuItemViewModel {
                            Header = "Open Project Explorer",
                            Action = () =>
                                     _eventAggregator.Publish(new AddToolViewCommand {Model = projectExplorerViewModelBuilder()})
                        },
                        new MenuItemViewModel {
                            Header = "Add part",
                            Action = () => _eventAggregator.Publish(new AddPartCommand {Part = new JsonPartMeta {FilePath = @"C:\temp\OpenCad\temp.cadpart"}})
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