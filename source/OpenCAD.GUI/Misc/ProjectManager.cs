using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Newtonsoft.Json;
using OpenCAD.GUI.Messages;
using OpenCAD.GUI.Models;

namespace OpenCAD.GUI.Misc
{
    public class ProjectManager : IHandle<OpenProjectCommand>, IHandle<AddPartCommand>, IHandle<CreateNewProjectCommand>, IHandle<SaveProjectCommand>
    {
        private readonly IEventAggregator _eventAggregator;

        protected ProjectMeta Project { get; private set; }

        public ProjectManager(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
        }

        public void Handle(AddPartCommand message) {
            if (Project == null)
                return;

            Project.Parts.Add(message.Part);
        }

        public void Handle(CreateNewProjectCommand message) {
            var systemTempProjDir = @"C:\temp\OpenCAD\TempProjects";
            if (!Directory.Exists(systemTempProjDir))
                Directory.CreateDirectory(systemTempProjDir);

            var tempProjDir = Path.Combine(systemTempProjDir, Path.GetRandomFileName());
            Directory.CreateDirectory(tempProjDir);

            var tempProjFile = Path.Combine(tempProjDir, "TempProject.cadproj");
            File.WriteAllText(tempProjFile, JsonConvert.SerializeObject(new {Parts = new object[0]}));

            OpenProject(tempProjFile);
        }

        public void Handle(OpenProjectCommand message) {
            var projFilePath = @"C:\temp\OpenCAD\Projects\Temp\temp.cadproj";
            
            OpenProject(projFilePath);
        }

        public void Handle(SaveProjectCommand message) {
            SaveProject(Project);
        }

        private void OpenProject(string projectFileName) {
            var projDirectory = Path.GetDirectoryName(projectFileName);

            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(projectFileName));

            var project = new ProjectMeta {
                FilePath = projectFileName,
                Parts = new ObservableCollection<JsonPartMeta>(((object) json.Parts)
                                                                   .Select(p => new JsonPartMeta {FilePath = Path.GetFullPath(Path.Combine(projDirectory, p.FilePath.ToString()))}).Cast<JsonPartMeta>())
            };
            Project = project;

            _eventAggregator.Publish(new ProjectOpenedEvent {Project = project});
        }

        private void SaveProject(ProjectMeta project) {
            if (!project.Exists)
                throw new InvalidOperationException("Project file does not exist");
        }
    }
}