using System;

namespace OpenCAD.Core.Maths
{
    public static class MathsHelper
    {
        public const double PI = Math.PI;
        public const double TwoPi = Math.PI * 2.0;
        public const double PiOverTwo = Math.PI / 2.0;

        public static readonly double E = 2.71828182845904523536F;

        public static Double Sqrt(Double d)
        {
            return Math.Pow(d, 0.5);
        }
        public static Double Pow(Double x, Double y)
        {
            return Math.Pow(x, y);
        }
        public static Double Sin(Angle angle)
        {
            return Math.Sin(angle.Radians);
        }
        public static Double Cos(Angle angle)
        {
            return Math.Cos(angle.Radians);
        }
        public static Double Tan(Angle angle)
        {
            return Math.Tan(angle.Radians);
        }

        public static Angle Acos(Double d)
        {
            return Angle.FromRadians(Math.Acos(d));
        }
        public static Angle Asin(Double d)
        {
            return Angle.FromRadians(Math.Asin(d));
        }
        public static Angle Atan(Double d)
        {
            return Angle.FromRadians(Math.Asin(d));
        }

        public static Angle Atan2(Double y, Double x)
        {
            return Angle.FromRadians(Math.Atan2(y, x));
        }

        public static Double Abs(Double d)
        {
            if (d >= 0.0)
                return d;
            return -d;
        }

        public static bool NearlyEquals(Double x, Double y, Double epsilon = 2E-24)
        {
            return 2.0 * Math.Abs(x - y) <= epsilon * (Math.Abs(x) + Math.Abs(y));
        }

        public static Double Round(Double d)
        {
            return Math.Round(d);
        }
        public static double Map(double x, double inMin, double inMax, double outMin, double outMax)
        {
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        public static double Clamp(double x, double min, double max)
        {

            return Math.Max(Math.Min(x, max), min);
        }
    }
}