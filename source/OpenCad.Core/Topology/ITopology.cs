using System;
using System.Collections.Generic;
using OpenCAD.Core.Maths;
namespace OpenCAD.Core.Topology
{

    public interface IFace
    {
        string Name { get; }
        IList<IEdgeLoop> Loops { get; }
    }

    public interface IHalfEdge
    {
        string Name { get; }

        Vect3 Start { get; }
        Vect3 End { get; }
        Lazy<IHalfEdge> Opposite { get; }
        
        IFace Face { get; }
        IHalfEdge Next { get; set; }

        Func<double, Vect3> Equation { get; }
        void Connect(IHalfEdge next);
    }

    public class Vertex : Vect3
    {
        public string Name { get; private set; }
        public Vertex(string name, double x, double y, double z)
            : base(x, y, z)
        {
            Name = name;
        }

        public Vertex(string name, Vect3 v)
            : base(v)
        {
            Name = name;
        }

        public Vertex(string name)
        {
            Name = name;
        }
    }


    public class Face : IFace
    {
        public string Name { get; private set; }
        public IList<IEdgeLoop> Loops { get; private set; }
        public Face(string name)
        {
            Name = name;
            Loops = new List<IEdgeLoop>();
        }


    }

    public interface IEdgeLoop
    {
        IHalfEdge Start { get; }
        IEnumerable<IHalfEdge> Edges { get; }
    }

    public class EdgeLoop:IEdgeLoop
    {
        public IHalfEdge Start { get; private set; }
        public IEnumerable<IHalfEdge> Edges
        {
            get
            {
                var e = Start;
                do
                {
                    yield return e;
                    e = e.Next;
                } while (e != Start);
            }
        }

        public EdgeLoop(params IHalfEdge[] edges)
        {
            Start = edges[0];
            for (var i = 0; i < edges.Length - 1; i++)
            {
                edges[i].Connect(edges[i + 1]);
            }
            edges[edges.Length - 1].Connect(Start);
        }
    }
  

    public abstract class BaseHalfEdge : IHalfEdge
    {
        public string Name { get; private set; }

        public Vect3 Start { get { return Opposite.Value.End; } }
        public Vect3 End { get; protected set; }

        public  Lazy<IHalfEdge> Opposite { get; private set; }

        public IFace Face { get; private set; }
        public IHalfEdge Next { get; set; }

        public Func<double, Vect3> Equation { get; protected set; }

        protected BaseHalfEdge(string name, Vect3 end, Lazy<IHalfEdge> opposite, IFace face)
        {
            Name = name;
            End = end;
            Opposite = opposite;
            Face = face;
            
        }

        public void Connect(IHalfEdge next)
        {
            Next = next;
        }

    }

    public class LineHalfEdge : BaseHalfEdge
    {
        public LineHalfEdge(string name, Vect3 end, Lazy<IHalfEdge> opposite, IFace face)
            : base(name, end, opposite, face)
        {
            Equation = t => Vect3.Lerp(Start, End, t);
        }
    }


    public interface IEdge<T>
    {
        T Edge1 { get; }
        T Edge2 { get; }

    }



    //public class LineEdge:IEdge<LineHalfEdge>
    //{
    //    public LineHalfEdge Edge1 { get; private set; }
    //    public LineHalfEdge Edge2 { get; private set; }

    //    public LineEdge(string name, Vect3 start, Vect3 end,IFace face1,IFace face2)
    //    {
    //        Edge1 = new LineHalfEdge(name + "-1", end, Edge2, face1);
    //        Edge2 = new LineHalfEdge(name + "-2", start, Edge1, face2);
    //    }
    //    //public static Tuple<Line> 
    //}

    public interface IShell
    {
        IList<IFace> Faces { get; }
    }

    public class Shell : IShell
    {
        public IList<IFace> Faces { get; private set; }
        public Shell(params IFace[] edges)
        {
            Faces = edges;
        }
    }


    public interface ITopology
    {

        IEnumerable<IFace> Faces { get; }
        IEnumerable<Vertex> Vertices { get; }
        IEnumerable<IHalfEdge> Edges { get; }

    }





}