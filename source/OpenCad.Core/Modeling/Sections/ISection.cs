using System.Collections.Generic;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling.Features;
using OpenCAD.Core.Topology;

namespace OpenCAD.Core.Modeling.Sections
{
    public interface ISection:IFeature 
    {
        Plane Location { get; }
        //IList<Vertex> Points { get; } 
        //IList<IEntity> Entities { get; } 
    }
}