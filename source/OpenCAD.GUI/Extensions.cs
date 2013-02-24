using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Graphics;
using OpenCAD.Core.Maths;
using OpenCAD.GUI.Buffers;
using SharpGL;

namespace OpenCAD.GUI
{
    public static class Extensions
    {
        public static Vect3f ToVect3f(this Vect3 v)
        {
            return new Vect3f { X = (float)v.X, Y = (float)v.Y, Z = (float)v.Z };
        }
        public static Vect4f ToVect4f(this Vect4 v)
        {
            return new Vect4f { X = (float)v.X, Y = (float)v.Y, Z = (float)v.Z, W = (float)v.W };
        }
        public static OpenGLVertex ToOpenGLVertex(this Vert v)
        {
            return new OpenGLVertex{Position = v.Position.ToVect3f(),Normal = v.Normal.ToVect3f(),Colour = v.Colour.ToVect4f()};
        }
        public static IEnumerable<OpenGLVertex> ToOpenGLVertices(this IEnumerable<Vert> verticies)
        {
            return verticies.Select(v => v.ToOpenGLVertex());
        }

        public static float[] ToColumnMajorArray(this Mat4 v)
        {
            return new []
                {
                    (float)v[1, 1],(float)v[2, 1],(float)v[3, 1],(float)v[4, 1],
                    (float)v[1, 2],(float)v[2, 2],(float)v[3, 2],(float)v[4, 2],
                    (float)v[1, 3],(float)v[2, 3],(float)v[3, 3],(float)v[4, 3],
                    (float)v[1, 4],(float)v[2, 4],(float)v[3, 4],(float)v[4, 4]
                };
        }

        public static Vect4 ToVector4(this Color col)
        {
            return new Vect4(col.R / 255f, col.G / 255f, col.B / 255f, col.A / 255f);
        }

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OpenGLVertex
    {
        public Vect3f Position;
        public Vect3f Normal;
        public Vect4f Colour;

        public static readonly int SizeInBytes = Vect3f.SizeInBytes * 2 + Vect4f.SizeInBytes;
    }

    public struct Vect3f
    {
        public float X;
        public float Y;
        public float Z;

        public static readonly int SizeInBytes = Marshal.SizeOf(new Vect3f());
    }

    public struct Vect4f
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public static readonly int SizeInBytes = Marshal.SizeOf(new Vect4f());
    }
}
