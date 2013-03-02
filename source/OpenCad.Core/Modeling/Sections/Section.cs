using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Modeling.Sections
{
    public class Section:ISection
    {
        public string Name { get; private set; }
        public Plane Location { get; private set; }
        public IList<Point> Points { get; private set; }

        public Section(string name, Plane location)
        {
            Name = name;
            Location = location;
            Points = new List<Point> { new Point(5, 0), new Point(6, 0), new Point(7, 5) };
        }
    }
}
