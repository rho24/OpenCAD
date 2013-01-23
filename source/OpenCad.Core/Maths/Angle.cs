using System;

namespace OpenCAD.Core.Maths
{
    public struct Angle
    {


        private const double RadToDeg = 180.0 / Math.PI;
        private const double DegToRad = Math.PI / 180.0;

        public double Radians { get; private set; }

        public double Degrees
        {
            get { return Radians * RadToDeg; }
        }

        private Angle(double radians)
            : this()
        {
            Radians = radians;
        }

        public static Angle FromRadians(double radians)
        {
            return new Angle(radians);
        }

        public static Angle FromDegrees(double degrees)
        {
            return new Angle(degrees * DegToRad);
        }

        public override string ToString()
        {
            return "Angle<" + Degrees + "\x00B0>";
        }

        public static Angle Zero
        {
            get { return new Angle(); }
        }

        public static Angle TwoPI
        {
            get { return FromRadians(Math.PI * 2.0); }
        }

        public static Angle operator +(Angle a1, Angle a2)
        {
            return new Angle(a1.Radians + a2.Radians);
        }

        public static Angle operator -(Angle a1, Angle a2)
        {
            return new Angle(a1.Radians - a2.Radians);
        }

        public static Angle operator *(Angle a, double d)
        {
            return new Angle(a.Radians * d);
        }

        public static Angle operator /(Angle a, double d)
        {
            return new Angle(a.Radians / d);
        }
        /*
        public static implicit operator Angle(double radians)
        {
            return new Angle(radians);
        }
         */
        public static implicit operator double(Angle angle)
        {
            return angle.Radians;
        }



        public override bool Equals(object obj)
        {
            if (!(obj is Angle))
                return false;
            return Radians.NearlyEquals(((Angle)obj).Radians);
        }
        public bool Equals(Angle other)
        {
            return Radians.Equals(other.Radians);
        }

        public override int GetHashCode()
        {
            return Radians.GetHashCode();
        }

    }
}