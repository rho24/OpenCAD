using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Topology
{
    public abstract class BaseEdge : IEdge
    {
        public Vect3 Start { get; protected set; }
        public Vect3 End { get; protected set; }
        public abstract Vect3 Interpolate(double t);

        protected BaseEdge(Vect3 start, Vect3 end)
        {
            Start = start;
            End = end;
        }
    }
}