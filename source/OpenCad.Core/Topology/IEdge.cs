using OpenCAD.Core.Maths;
namespace OpenCAD.Core.Topology
{
    public interface IEdge
    {
        Vect3 Start { get; }
        Vect3 End { get; }
        Vect3 Interpolate(double t);
    }
}