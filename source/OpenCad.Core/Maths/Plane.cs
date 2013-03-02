using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Core.Maths
{
    public class Plane
    {
        
        public Vect3 Normal { get; private set; }
        public double Distance { get; private set; }

        public double A { get { return Normal.X; } }
        public double B { get { return Normal.Y; } }
        public double C { get { return Normal.Z; } }

        public Mat4 Transform
        {
            get
            {
                var tangent0 = Normal.CrossProduct(Vect3.UnitX);
                if (tangent0.DotProduct(tangent0) < 0.001)
                    tangent0 = Normal.CrossProduct(Vect3.UnitY);
                tangent0 = tangent0.Normalize();
                var tangent1 = Normal.CrossProduct(tangent0).Normalize();
                return new Mat4(new[,]
                {
                    {tangent0.X, tangent1.X, Normal.X, 0.0f},
                    {tangent0.Y, tangent1.Y, Normal.Y, 0.0f},
                    {tangent0.Z, tangent1.Z, Normal.Z, 0.0f},
                    {0.0, 0.0, 0.0, 1.0}
                });

            }
        }
        public Plane(Vect3 normal, double distance)
        {
            Normal = normal;
            Distance = distance;
        }

        public Plane(Vect3 normal, Vect3 point)
        {
            Normal = normal;
            Distance = -normal.DotProduct(point);
        }

        public Plane (Vect3 point1,Vect3 point2,Vect3 point3)
        {
            Normal = (point2 - point1).CrossProduct(point3 - point1).Normalize();
            Distance = -Normal.DotProduct(point1);
        }

        public Axis Intersect(Plane plane)
        {
            throw new NotImplementedException();
        }

        public double DistanceTo(Vect3 point)
        {
            return (A*point.X + B*point.Y + C*point.Y + Distance)/Normal.Length;
        }

        public Plane Offset(double offset)
        {
            return new Plane(Normal,offset + Distance);
        }

        public static Plane FromPoints(Vect3 p1, Vect3 p2, Vect3 p3)
        {
            var normal = (p2 - p1).CrossProduct(p3 - p1).Normalize();
            return new Plane(normal,normal.DotProduct(p1));
        }
    }
}
