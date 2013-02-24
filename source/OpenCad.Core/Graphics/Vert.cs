using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vert
    {
        public Vect3 Position;
        public Vect3 Normal;
        public Vect4 Colour;
        public Vert(Vect3 position, Vect3 normal, Vect4 colour)
        {
            Position = position;
            Normal = normal;
            Colour = colour;
        }
        //public Vect2 TexCoord;
    }
}
