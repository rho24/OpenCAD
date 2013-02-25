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
        public Mat4 Transform
        {
            get
            {
                // Find a vector in the plane
                var tangent0 = Normal.CrossProduct(Vect3.UnitX);
                if(tangent0.DotProduct(tangent0) < 0.001)
                    tangent0 = Normal.CrossProduct(Vect3.UnitY);
                tangent0 = tangent0.Normalize();
                var tangent1 = Normal.CrossProduct(tangent0).Normalize();
                return new Mat4(new[,]
                            {
                                {tangent0.X, tangent0.Y, tangent0.Z, 0.0f},
                                {tangent1.X, tangent1.Y, tangent1.Z, 0.0f},
                                {Normal.X, Normal.Y, Normal.Z, 0.0f},
                                {0.0, 0.0, 0.0, 1.0}
                            });

            }
        }
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
