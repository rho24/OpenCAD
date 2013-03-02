using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Modeling.Sections
{
    public interface IEdge : IEntity
    {
        Vect2 Start { get; }
        Vect2 End { get; }
        Vect2 Interpolate(double t);
    }
}