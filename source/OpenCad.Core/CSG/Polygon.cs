using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.CSG
{
    public class Polygon
    {
        public Vertex V1 { get; private set; }
        public Vertex V2 { get; private set; }
        public Vertex V3 { get; private set; }

        public Plane Plane { get { return Plane.FromPoints(V1.Position, V2.Position, V3.Position); } }
    }
}
