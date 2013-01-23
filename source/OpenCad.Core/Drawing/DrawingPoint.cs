using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Drawing.Constraining;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Drawing
{
    public class DrawingPoint : IDrawingElement, IHasConstraints
    {

        public Vect3 Position { get; private set; }

    }
}
