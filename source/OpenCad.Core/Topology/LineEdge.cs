using OpenCAD.Core.Maths;
namespace OpenCAD.Core.Topology
{
    public class LineEdge : BaseEdge
    {
        public LineEdge(Vect3 start, Vect3 end) : base(start,end)
        {

        }

        public override Vect3 Interpolate(double t)
        {
            return Vect3.Lerp(Start, End, t);
        }
    }
}