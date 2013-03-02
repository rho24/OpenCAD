using System;

namespace OpenCAD.Core.Maths
{
    public class Vect2
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Vect2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vect2(Vect2 v)
        {
            X = v.X;
            Y = v.Y;
        }



        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared
        {
            get { return X*X + Y*Y; }
        }


        public Vect2 Normalize()
        {
            var num = 1f / Length;
            return new Vect2(X * num, Y * num);
        }


        public static Vect2 Zero
        {
            get { return new Vect2(0.0, 0.0); }
        }

        public static Vect2 UnitX
        {
            get { return new Vect2(1.0, 0.0); }
        }

        public static Vect2 UnitY
        {
            get { return new Vect2(0.0, 1.0); }
        }


        public static Vect2 operator +(Vect2 v1, Vect2 v2)
        {
            return new Vect2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vect2 operator -(Vect2 v1, Vect2 v2)
        {
            return new Vect2(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vect2 operator -(Vect2 v)
        {
            return new Vect2(-v.X, -v.Y);
        }

        public static Vect2 operator *(Vect2 v, double d)
        {
            return new Vect2(v.X * d, v.Y * d);
        }

        public static Vect2 operator *(double d, Vect2 v)
        {
            return v * d;
        }

        public static Vect2 operator /(Vect2 v, double d)
        {
            return new Vect2(v.X / d, v.Y / d);
        }

        public static bool operator ==(Vect2 a, Vect2 b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }

        public static bool operator !=(Vect2 a, Vect2 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var v = obj as Vect2;
            if (v == null)
            {
                return false;
            }
            return this == v;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return "Vect2({0},{1})".fmt(X, Y);
        }

        public static Vect2 Lerp(Vect2 start, Vect2 end, double percent)
        {
            return (start + percent * (end - start));
        }

        public Vect3 ToVect3()
        {
            return new Vect3(X,Y,0);
        }
    }
}