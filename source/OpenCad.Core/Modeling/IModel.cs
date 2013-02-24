using System.Collections.Generic;
using OpenCAD.Core.Modeling.Features;

namespace OpenCAD.Core.Modeling
{
    public interface IModel
    {
        string Name { get; }
        IList<IFeature> Features { get; }
    }
}