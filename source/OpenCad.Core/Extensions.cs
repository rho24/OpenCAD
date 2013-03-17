using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Meshing;
using OpenCAD.Core.Shapes;

namespace OpenCAD.Core
{
    public static class Extensions
    {
        public static string fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool NearlyEquals(this double x, double y, double epsilon = 0.00000001)
        {
            return MathsHelper.NearlyEquals(x, y, epsilon);
        }
    }
}
