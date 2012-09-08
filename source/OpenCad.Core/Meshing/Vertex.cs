using OpenCad.Core.Maths;

namespace OpenCad.Core.Meshing
{
    public class Vertex : Vector3
    {
        public HalfEdge Edge { get; set; }
        public Vertex(Vector3 position)
            : base(position)
        {

        }
    }
}