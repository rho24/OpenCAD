using OpenCAD.OpenGL.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenCAD.OpenGL.Buffers
{
    public class VAO : IBuffer
    {
        private readonly IShaderProgram _program;
        private int _handle;
        public int Handle
        {
            get { return _handle; }
            private set { _handle = value; }
        }

        public VBO VBO { get; private set; }

        public VAO(IShaderProgram program, VBO vbo)
        {
            _program = program;
            VBO = vbo;
            GL.GenVertexArrays(1, out _handle);
            using (new Bind(program))
            using (new Bind(this))
            using (new Bind(vbo))
            {
                var stride = Vector3.SizeInBytes * 2 + Vector4.SizeInBytes;

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, stride, 0);
                GL.BindAttribLocation(program.Handle, 0, "vert_position");

                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, stride, Vector3.SizeInBytes);
                GL.BindAttribLocation(program.Handle, 1, "vert_normal");

                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, stride, Vector3.SizeInBytes * 2);
                GL.BindAttribLocation(program.Handle, 2, "vert_colour");
            }
        }

        public void Bind()
        {
            _program.Bind();
            GL.BindVertexArray(Handle);
        }

        public void UnBind()
        {
            GL.BindVertexArray(0);
            _program.Bind();
        }

        public void Draw()
        {
            using (new Bind(_program))
            using (new Bind(this))
            {
                GL.DrawArrays(VBO.BeginMode, 0, VBO.Count);
            }
        }
    }
}