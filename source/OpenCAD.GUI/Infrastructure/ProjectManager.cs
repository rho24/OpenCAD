using Caliburn.Micro;
using OpenCAD.GUI.Messages;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI.Infrastructure
{
    public class ProjectManager: IHandle<OpenProjectCommand>
    {
        private readonly IEventAggregator _eventAggregator;
        public ProjectManager(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
        }

        public void Handle(OpenProjectCommand message) {
            var project = new ProjectMeta();
            _eventAggregator.Publish(new ProjectOpenedEvent{Project = project});
        }
    }
}