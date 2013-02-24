using System.Collections.Generic;
using OpenCAD.Core.Modeling.Features;

namespace OpenCAD.Core.Modeling
{
    public abstract class BasePart:IModel
    {
        public string Name { get; protected set; }
        public IList<IFeature> Features { get; protected set; }
        public BasePart()
        {
            Features = new List<IFeature>();
        }
    }
}