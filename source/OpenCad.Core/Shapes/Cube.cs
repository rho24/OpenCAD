using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCad.Core.Maths;

namespace OpenCad.Core.Shapes
{
    public class Cube
    {
        public Vector3 Position { get; private set; }
        public double Size { get; private set; }
        public Cube(Vector3 position, double size)
        {
            Position = position;
            Size = size;
        }
    }
}
