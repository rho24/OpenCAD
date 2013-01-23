using System.Runtime.InteropServices;
using OpenTK;

namespace OpenCAD.OpenGL
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OpenGLVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector4 Colour;
    }
}