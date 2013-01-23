using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Shapes;

namespace OpenCAD.Core.Meshing
{
    public static class MeshExtensions
    {
        public static Mesh ToMesh(this Cube c)
        {
            return new Mesh();
        }
    }
}
