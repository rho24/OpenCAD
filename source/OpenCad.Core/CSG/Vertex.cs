using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.CSG
{
    public class Vertex
    {
        public Vect3 Position { get; private set; }
        public Vect3 Normal { get; private set; }

        public Vertex(Vect3 position,Vect3 normal)
        {
            Position = position;
            Normal = normal;
        }

        public void Flip()
        {
            Normal = -Normal;
        }
    }
}
