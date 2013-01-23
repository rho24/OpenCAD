using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Drawing;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Contexts
{
    public class Context2D
    {
        public Plane Parent { get; private set; }
        public List<IDrawingElement> Elements { get; private set; } 
    }
}
