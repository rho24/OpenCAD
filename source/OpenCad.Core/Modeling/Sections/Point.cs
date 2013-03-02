using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Modeling.Sections
{
    public class Point:Vect2
    {
        public Point(double x, double y) : base(x, y)
        {
        }

        public Point(Vect2 v) : base(v)
        {
        }
    }
}