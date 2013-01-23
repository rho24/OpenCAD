using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Meshing
{
    public class Vertex : Vect3
    {
        public HalfEdge Edge { get; set; }
        public Vertex(Vect3 position)
            : base(position)
        {

        }
    }
}