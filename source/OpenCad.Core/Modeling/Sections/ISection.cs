using System.Collections.Generic;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling.Features;

namespace OpenCAD.Core.Modeling.Sections
{
    public interface ISection:IFeature 
    {
        Plane Location { get; }
        IList<Point> Points { get; } 
        //IList<IEntity> Entities { get; } 
    }
}