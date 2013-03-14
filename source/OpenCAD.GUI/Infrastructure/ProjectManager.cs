using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Newtonsoft.Json;
using OpenCAD.GUI.Messages;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI.Infrastructure
{
    public class ProjectManager : IHandle<OpenProjectCommand>
    {
        private readonly IEventAggregator _eventAggregator;

        public ProjectManager(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
        }

        public void Handle(OpenProjectCommand message) {
            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(@"C:\temp\OpenCad\temp.cadproj"));

            var project = new ProjectMeta {Name = json.Name, Parts = new ObservableCollection<PartMeta>(((object) json.Parts).Select(p => new PartMeta {Name = p.Name}).Cast<PartMeta>())};

            _eventAggregator.Publish(new ProjectOpenedEvent {Project = project});
        }
    }
}