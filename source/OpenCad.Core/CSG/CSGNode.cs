using System.Collections.Generic;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.CSG
{
    public class CSGNode
    {
        public IList<Polygon> Polygons { get; private set; }
        public Plane Plane;

        public IList<CSGNode> Front { get; private set; }
        public IList<CSGNode> Back { get; private set; } 

        public CSGNode(IList<Polygon> polygons)
        {
            Polygons = polygons;
            Plane = polygons[0].Plane;

            Front = new List<CSGNode>();
            Back = new List<CSGNode>();

            foreach (var polygon in polygons)
            {
                //Plane.SplitPolygon(polygon,polygons,polygons,Front,Back);
            }



        }
    }
}