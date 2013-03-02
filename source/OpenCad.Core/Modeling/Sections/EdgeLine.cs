using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Modeling.Sections
{
    public class EdgeLine : BaseEdge
    {
        public override Vect2 Interpolate(double t)
        {
            return Vect2.Lerp(Start, End, t);
        }
    }
}