using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenCad.Core.Maths;

namespace OpenCAD.Test.Maths
{
    [TestFixture]
    public class Vector3Test
    {
        readonly Vector3 _a = new Vector3(5, 4, 3);
        readonly Vector3 _b = new Vector3(5, 4, 3);
        readonly Vector3 _c = new Vector3(1, 2, 4);
        [Test]
        public void StaticHelpers()
        {
            Assert.AreEqual(Vector3.Zero.X, 0);
            Assert.AreEqual(Vector3.Zero.Y, 0);
            Assert.AreEqual(Vector3.Zero.Z, 0);

            Assert.AreEqual(Vector3.UnitX.X, 1);
            Assert.AreEqual(Vector3.UnitX.Y, 0);
            Assert.AreEqual(Vector3.UnitX.Z, 0);

            Assert.AreEqual(Vector3.UnitY.X, 0);
            Assert.AreEqual(Vector3.UnitY.Y, 1);
            Assert.AreEqual(Vector3.UnitY.Z, 0);

            Assert.AreEqual(Vector3.UnitZ.X, 0);
            Assert.AreEqual(Vector3.UnitZ.Y, 0);
            Assert.AreEqual(Vector3.UnitZ.Z, 1);
        }

        [Test]
        public void Overrides()
        {
            Assert.AreEqual(_a, _b);
            Assert.AreNotEqual(_a, _c);
            Assert.AreEqual(_a == _b, true);
            Assert.AreEqual(_a == _c, false);
            Assert.AreEqual(_a != _b, false);
            Assert.AreEqual(_a != _c, true);
        }
        [Test]
        public void MathematicalOperators()
        {
            Assert.AreEqual(_a + _c, new Vector3(6, 6, 7));

        }

    }
}
