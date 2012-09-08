using System.Collections.Generic;
using OpenCad.Core.Maths;



namespace OpenCad.Core.Meshing
{
    public class Mesh
    {
        public readonly List<HalfEdge> Edges;
        public List<Face> Faces;
        public List<Vertex> Vertices;



        public Mesh()
        {
            Edges = new List<HalfEdge>();
            Faces = new List<Face>();
            Vertices = new List<Vertex>();

            var v0 = new Vertex( new Vector3(0, 0, 0));
            var v1 = new Vertex( new Vector3(0, 0, 0));
            var v2 = new Vertex( new Vector3(0, 0, 0));
            var v3 = new Vertex( new Vector3(0, 0, 0));
            var v4 = new Vertex( new Vector3(0, 0, 0));
            var v5 = new Vertex( new Vector3(0, 0, 0));
            var v6 = new Vertex( new Vector3(0, 0, 0));
            var v7 = new Vertex( new Vector3(0, 0, 0));

            var face1 = new Face();
            var he1 = new HalfEdge() { ToVertex = v1, Face = face1 };
            var he2 = new HalfEdge() { ToVertex = v2, Face = face1 };
            var he3 = new HalfEdge() { ToVertex = v3, Face = face1 };
            var he4 = new HalfEdge() { ToVertex = v0, Face = face1 };
            he1.Next = he2;
            he2.Next = he3;
            he3.Next = he4;
            he4.Next = he1;
            face1.Edge = v0.Edge = he1;

            var face2 = new Face();
            var he5 = new HalfEdge() { ToVertex = v1, Face = face2 };
            var he6 = new HalfEdge() { ToVertex = v5, Face = face2 };
            var he7 = new HalfEdge() { ToVertex = v6, Face = face2 };
            var he8 = new HalfEdge() { ToVertex = v2, Face = face2 };
            he5.Next = he6;
            he6.Next = he7;
            he7.Next = he8;
            he8.Next = he5;
            face2.Edge = v2.Edge = he5;

            var face3 = new Face();
            var he9 = new HalfEdge() { ToVertex = v5, Face = face3 };
            var he10 = new HalfEdge() { ToVertex = v4, Face = face3 };
            var he11 = new HalfEdge() { ToVertex = v7, Face = face3 };
            var he12 = new HalfEdge() { ToVertex = v6, Face = face3 };
            he9.Next = he10;
            he10.Next = he11;
            he11.Next = he12;
            he12.Next = he9;
            face3.Edge = v6.Edge = he9;






        }

        public void AddFace(Vector3 v1, Vector3 v2, Vector3 v3)
        {

            var he = new HalfEdge() {};
            var face = new Face() {Edge = new HalfEdge()};

        }
    }
}