using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Meshing
{
    public class MeshVertex : Vect3
    {
        public HalfEdge Edge { get; set; }
        public MeshVertex(Vect3 position)
            : base(position)
        {

        }
    }
}