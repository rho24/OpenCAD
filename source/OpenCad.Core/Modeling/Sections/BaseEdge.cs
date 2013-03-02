using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Modeling.Sections
{
    public abstract class BaseEdge:IEdge
    {

        public Vect2 Start { get; private set; }
        public Vect2 End { get; private set; }

        protected BaseEdge()
        {

        }

        public abstract Vect2 Interpolate(double t);
    }
}