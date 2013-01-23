using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Core.CSG
{
    public interface ICSGSolid
    {
        IList<Polygon> Polygons { get; }
        ICSGSolid Union(ICSGSolid csg);
        ICSGSolid Subtract(ICSGSolid csg);
        ICSGSolid Intersect(ICSGSolid csg);
        ICSGSolid Inverse(ICSGSolid csg);
    }

    public abstract class BaseCSGSolid:ICSGSolid
    {

        public IList<Polygon> Polygons { get; protected set; }

        public ICSGSolid Union(ICSGSolid csg)
        {
            throw new NotImplementedException();
        }

        public ICSGSolid Subtract(ICSGSolid csg)
        {
            throw new NotImplementedException();
        }

        public ICSGSolid Intersect(ICSGSolid csg)
        {
            throw new NotImplementedException();
        }

        public ICSGSolid Inverse(ICSGSolid csg)
        {
            throw new NotImplementedException();
        }
    }


    public class Cube : BaseCSGSolid
    {
        public Cube()
        {
            Polygons = new List<Polygon>();

        }
    }
}

