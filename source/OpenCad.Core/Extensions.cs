using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCad.Core.Meshing;
using OpenCad.Core.Shapes;

namespace OpenCad.Core
{
    public static class Extensions
    {
        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static Mesh ToMesh(this Cube c)
        {
            return new Mesh();
        }
    }
}
