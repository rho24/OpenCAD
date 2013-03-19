using OpenCAD.GUI.Models;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI.Messages
{
    public class ProjectOpenedEvent
    {
        public ProjectMeta Project { get; set; }
    }
}