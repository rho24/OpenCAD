using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Modeling.Datums
{
    public class DatumPlane:Plane,IDatum
    {
        public string Name { get; private set; }
        public DatumPlane(string name, Vect3 normal, double distance) : base(normal, distance)
        {
            Name = name;
        }

        public DatumPlane(string name, Vect3 normal, Vect3 point) : base(normal, point)
        {
            Name = name;
        }

        public DatumPlane(string name, Vect3 point1, Vect3 point2, Vect3 point3) : base(point1, point2, point3)
        {
            Name = name;
        }
    }
}
