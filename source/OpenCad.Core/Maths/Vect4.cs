namespace OpenCAD.Core.Maths
{
    public class Vect4
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double W { get; private set; }

        public Vect4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Vect4 Zero
        {
            get { return new Vect4(0.0, 0.0, 0.0,0.0); }
        }
    }
}