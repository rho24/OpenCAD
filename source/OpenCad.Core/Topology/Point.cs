using OpenCAD.Core.Maths;
namespace OpenCAD.Core.Topology
{
    public class Vertex:Vect3
    {
        public Vertex(double x, double y, double z) : base(x, y, z)
        {

        }

        public Vertex(Vect3 v)
            : base(v)
        {

        }
    }
}