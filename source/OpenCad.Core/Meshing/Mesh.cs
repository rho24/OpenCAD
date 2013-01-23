using System.Collections.Generic;
using OpenCAD.Core.Maths;



namespace OpenCAD.Core.Meshing
{

    public class Mesh
    {
        public List<HalfEdge> Edges { get; private set; }
        public List<HalfEdgeFace> Faces { get; private set; }
        public List<Vertex> Vertices { get; private set; }

        public Mesh()
        {
            Edges = new List<HalfEdge>();
            Faces = new List<HalfEdgeFace>();
            Vertices = new List<Vertex>();
        }

        public Mesh(List<HalfEdge> edges, List<HalfEdgeFace> faces, List<Vertex> vertices)
        {
            Edges = edges;
            Faces = faces;
            Vertices = vertices;
        }


        private void MapPair(HalfEdge he1, HalfEdge he2)
        {

        }

    }



    /*

    public interface IMesh<TEdge,TFace,TVert>
    {
        List<TEdge> Edges { get; }
        List<TFace> Faces { get; }
        List<TVert> Vertices { get; }
    }

    public class FaceVertex:IMesh<Vertex,>

    */



    public class Meshold
    {
        public readonly List<HalfEdge> Edges;
        public List<HalfEdgeFace> Faces;
        public List<Vertex> Vertices;


        public Meshold()
        {
            Edges = new List<HalfEdge>();
            Faces = new List<HalfEdgeFace>();
            Vertices = new List<Vertex>();


            double size = 1.0;
            var v0 = new Vertex(new Vect3(size / 2.0, -size / 2.0, size / 2.0));
            var v1 = new Vertex(new Vect3(-size / 2.0, -size / 2.0, size / 2.0));
            var v2 = new Vertex(new Vect3(-size / 2.0, -size / 2.0, -size / 2.0));
            var v3 = new Vertex(new Vect3(size / 2.0, -size / 2.0, -size / 2.0));
            var v4 = new Vertex(new Vect3(size / 2.0, size / 2.0, size / 2.0));
            var v5 = new Vertex(new Vect3(-size / 2.0, size / 2.0, size / 2.0));
            var v6 = new Vertex(new Vect3(-size / 2.0, size / 2.0, -size / 2.0));
            var v7 = new Vertex( new Vect3(0, 0, 0));

            var face1 = new HalfEdgeFace();
            var face2 = new HalfEdgeFace();
            var face3 = new HalfEdgeFace();
            var face4 = new HalfEdgeFace();
            var face5 = new HalfEdgeFace();
            var face6 = new HalfEdgeFace();
            Faces.AddRange(new[]{face1,face2,face3,face4,face5,face6});

            var he1 = new HalfEdge { ToVertex = v1, HalfEdgeFace = face1 };
            var he2 = new HalfEdge { ToVertex = v2, HalfEdgeFace = face1 };
            var he3 = new HalfEdge { ToVertex = v3, HalfEdgeFace = face1 };
            var he4 = new HalfEdge { ToVertex = v0, HalfEdgeFace = face1 };
            he1.Next = he2;
            he2.Next = he3;
            he3.Next = he4;
            he4.Next = he1;
            face1.HalfEdge = v0.Edge = he1;

            
            var he5 = new HalfEdge { ToVertex = v1, HalfEdgeFace = face2 };
            var he6 = new HalfEdge { ToVertex = v5, HalfEdgeFace = face2 };
            var he7 = new HalfEdge { ToVertex = v6, HalfEdgeFace = face2 };
            var he8 = new HalfEdge { ToVertex = v2, HalfEdgeFace = face2 };
            he5.Next = he6;
            he6.Next = he7;
            he7.Next = he8;
            he8.Next = he5;
            face2.HalfEdge = v2.Edge = he5;

            
            var he9 = new HalfEdge { ToVertex = v5, HalfEdgeFace = face3 };
            var he10 = new HalfEdge { ToVertex = v4, HalfEdgeFace = face3 };
            var he11 = new HalfEdge { ToVertex = v7, HalfEdgeFace = face3 };
            var he12 = new HalfEdge { ToVertex = v6, HalfEdgeFace = face3 };
            he9.Next = he10;
            he10.Next = he11;
            he11.Next = he12;
            he12.Next = he9;
            face3.HalfEdge = v6.Edge = he9;

           
            var he13 = new HalfEdge { ToVertex = v4, HalfEdgeFace = face4 };
            var he14 = new HalfEdge { ToVertex = v0, HalfEdgeFace = face4 };
            var he15 = new HalfEdge { ToVertex = v3, HalfEdgeFace = face4 };
            var he16 = new HalfEdge { ToVertex = v7, HalfEdgeFace = face4 };
            he13.Next = he14;
            he14.Next = he15;
            he15.Next = he16;
            he16.Next = he13;
            face4.HalfEdge = v7.Edge = he13;

            
            var he17 = new HalfEdge { ToVertex = v2, HalfEdgeFace = face5 };
            var he18 = new HalfEdge { ToVertex = v6, HalfEdgeFace = face5 };
            var he19 = new HalfEdge { ToVertex = v7, HalfEdgeFace = face5 };
            var he20 = new HalfEdge { ToVertex = v3, HalfEdgeFace = face5 };
            he17.Next = he18;
            he18.Next = he19;
            he19.Next = he20;
            he20.Next = he17;
            face5.HalfEdge = v3.Edge = he17;

            
            var he21 = new HalfEdge { ToVertex = v1, HalfEdgeFace = face6 };
            var he22 = new HalfEdge { ToVertex = v5, HalfEdgeFace = face6 };
            var he23 = new HalfEdge { ToVertex = v4, HalfEdgeFace = face6 };
            var he24 = new HalfEdge { ToVertex = v0, HalfEdgeFace = face6 };

            he21.Next = he22;
            he22.Next = he23;
            he23.Next = he24;
            he24.Next = he21;
            face6.HalfEdge = v0.Edge = he21;


            MapPair(he1, he21);
            MapPair(he2, he5);
            MapPair(he3, he17);
            MapPair(he4, he15);

            //MapPair(he5, he2);

            MapPair(he6, he24);
            MapPair(he7, he9);
            MapPair(he8, he18);
            //MapPair(he9, he7);
            MapPair(he10, he23);
            MapPair(he11, he13);
            MapPair(he12, he19);
            //MapPair(he13, he11);
            MapPair(he14, he22);
            //MapPair(he15, he4);
            MapPair(he16, he20);
            //MapPair(he17, he3);
            //MapPair(he18, he8);
            //MapPair(he19, he12);
            //MapPair(he20, he16);
            //MapPair(he21, he1);
            //MapPair(he22, he14);
            //MapPair(he23, he10);
            //MapPair(he24, he6);

        }

        private void MapPair(HalfEdge he1,HalfEdge he2)
        {
            he1.Opposite = he2;
            he2.Opposite = he1;
        }
    }
}