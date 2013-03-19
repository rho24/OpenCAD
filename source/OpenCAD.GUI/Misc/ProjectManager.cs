﻿using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Newtonsoft.Json;
using OpenCAD.GUI.Messages;
using OpenCAD.GUI.Models;

namespace OpenCAD.GUI.Misc
{
    public class ProjectManager : IHandle<OpenProjectCommand>, IHandle<AddPartCommand>, IHandle<CreateNewProjectCommand>
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
            var project = new ProjectMeta {
                Name = message.Name,
                Parts = new ObservableCollection<JsonPartMeta>()
            };

            Project = project;

            _eventAggregator.Publish(new ProjectOpenedEvent {Project = project});
        }

        public void Handle(OpenProjectCommand message) {
            var projFilePath = @"C:\temp\OpenCad\temp.cadproj";
            var projDirectory = Path.GetDirectoryName(projFilePath);

            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(projFilePath));

            var project = new ProjectMeta {
                Name = json.Name,
                Parts = new ObservableCollection<JsonPartMeta>(((object) json.Parts)
                                                                   .Select(p => new JsonPartMeta {FilePath = Path.GetFullPath(Path.Combine(projDirectory, p.FilePath.ToString()))}).Cast<JsonPartMeta>())
            };

            Project = project;

            _eventAggregator.Publish(new ProjectOpenedEvent {Project = project});
        }
    }
}