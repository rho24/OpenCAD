using SharpGL;

namespace OpenCAD.GUI.Buffers
{
    public class BasicShader : BaseShader
    {
        public BasicShader(OpenGL gl)
            : base(gl, "Basic.vert", "Basic.frag")
        {
        }
    }
}