using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//clockwise half edge
namespace OpenCad.Core.Meshing
{
    public class HalfEdge
    {
        public Vertex ToVertex { get; set; }
        public HalfEdge Opposite { get; set; }
        public Face Face { get; set; }
        public HalfEdge Next { get; set; }


        public Vertex FromVertex { get { return Opposite.ToVertex; } }

        public HalfEdge()
        {
            
        }
    }
}
