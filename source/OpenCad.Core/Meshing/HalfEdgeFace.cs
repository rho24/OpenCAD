using System.Collections.Generic;

namespace OpenCAD.Core.Meshing
{
    public class HalfEdgeFace
    {
        public HalfEdge HalfEdge { get; set; }
        public IEnumerable<HalfEdge> HalfEdges {get
        {
            var e = HalfEdge;
            do
            {
                yield return e; 
                e = e.Next;
            } while (e != HalfEdge);
        }}
    }
}