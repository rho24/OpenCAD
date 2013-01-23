using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenCAD.Core;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Meshing;
using OpenCAD.Core.Shapes;

namespace OpenCAD.Test.Meshing
{
    [TestFixture]
    public class MeshTests
    {
        [Test]
        public void ConstructorTest()
        {
            var m = new Cube(Vect3.Zero, 1).ToMesh();
            foreach (var face in m.Faces)
            {
                foreach (var halfEdge in face.HalfEdges)
                {
                    Debug.WriteLine(halfEdge);
                }
            }

        }
    }
}
