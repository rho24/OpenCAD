using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCad.Core.Maths
{
    public class Vector3
    {
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared
        {
            get { return X*X + Y*Y + Z*Z; }
        }


        public Vector3 Normalize()
        {
            var num = 1f / Length;
            return new Vector3(X * num, Y * num, Z * num);
        }


        public static Vector3 Zero
        {
            get { return new Vector3(0.0, 0.0, 0.0); }
        }

        public static Vector3 UnitX
        {
            get { return new Vector3(1.0, 0.0, 0.0); }
        }

        public static Vector3 UnitY
        {
            get { return new Vector3(0.0, 1.0, 0.0); }
        }

        public static Vector3 UnitZ
        {
            get { return new Vector3(0.0, 0.0, 1.0); }
        }

        public Vector3 CrossProduct(Vector3 v)
        {
            return new Vector3(Y*v.Z - Z*v.Y, Z*v.X - X*v.Z, X*v.Y - Y*v.X);
        }

        public double DotProduct(Vector3 v)
        {
            return X*v.X + Y*v.Y + Z*v.Z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator *(Vector3 v, double d)
        {
            return new Vector3(v.X*d, v.Y*d, v.Z*d);
        }

        public static Vector3 operator *(double d, Vector3 v)
        {
            return v*d;
        }

        public static Vector3 operator /(Vector3 v, double d)
        {
            return new Vector3(v.X/d, v.Y/d, v.Z/d);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Z.Equals(b.Z);
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var v = obj as Vector3;
            if (v == null)
            {
                return false;
            }
            return this == v;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        public override string ToString()
        {
            return "Vector3({0},{1},{2})".Format(X, Y, Z);
        }
    }
}
