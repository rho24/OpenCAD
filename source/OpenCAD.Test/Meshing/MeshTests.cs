using System;
using NUnit.Framework;
using OpenCad.Core;
using OpenCad.Core.Maths;
using OpenCad.Core.Meshing;
using OpenCad.Core.Shapes;

namespace OpenCAD.Test.Meshing
{
    [TestFixture]
    public class MeshTests
    {
        public void ConstructorTest()
        {
            var m = new Cube(Vector3.Zero, 1).ToMesh();
        }
    }
}
