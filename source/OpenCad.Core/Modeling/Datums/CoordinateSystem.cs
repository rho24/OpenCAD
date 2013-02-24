using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling.Features;

namespace OpenCAD.Core.Modeling.Datums
{
    public class CoordinateSystem:IDatum
    {
        public string Name { get; private set; }
        public Mat4 Transform { get;  set; }

        public CoordinateSystem(string name, Mat4 transform)
        {
            Name = name;
            Transform = transform;
        }
    }
}
