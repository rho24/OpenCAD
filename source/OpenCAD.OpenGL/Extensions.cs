using System.Drawing;
using OpenCAD.Core.Maths;
using OpenTK;
using OpenTK.Graphics;

namespace OpenCAD.OpenGL
{
    public static class Extensions
    {
        public static Vector3 ToVector3(this Vect3 v)
        {
            return new Vector3((float)v.X, (float)v.Y, (float)v.Z);
        }


        public static Vector4 ToVector4(this Color col)
        {
            return new Vector4(col.R/255f, col.G/255f, col.B/255f, col.A/255f);
        }

        public static Vector4 ToVector4(this Color4 col)
        {
            return new Vector4(col.R, col.G , col.B , col.A);
        }

        public static Matrix4 ToMatrix4(this Mat4 m)
        {
            var md = m.Transpose();//http://www.opentk.com/node/2771
            return new Matrix4(
                (float)md[1, 1], (float)md[1, 2], (float)md[1, 3], (float)md[1, 4],
                (float)md[2, 1], (float)md[2, 2], (float)md[2, 3], (float)md[2, 4],
                (float)md[3, 1], (float)md[3, 2], (float)md[3, 3], (float)md[3, 4],
                (float)md[4, 1], (float)md[4, 2], (float)md[4, 3], (float)md[4, 4]);
        }
    }
}