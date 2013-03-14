using System.Collections.Generic;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Topology;

namespace OpenCAD.Core.Modeling.Sections
{
    public class Section:ISection
    {
        public string Name { get; private set; }
        public Plane Location { get; private set; }
        public IList<Vertex> Points { get; private set; }

        public Section(string name, Plane location)
        {
            Name = name;
            Location = location;
            Points = new List<Vertex> { new Vertex(5, 0, 0), new Vertex(6, 0, 0), new Vertex(7, 5, 0) };
        }
    }
}
