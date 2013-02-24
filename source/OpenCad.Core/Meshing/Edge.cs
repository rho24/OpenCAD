using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Core.Meshing
{
    public class Edge
    {
        public MeshVertex Vertex1 { get; set; }
        public MeshVertex Vertex2 { get; set; }
    }
}
