using System;
using System.Runtime.InteropServices;
using OpenCAD.Core.Maths;
using SharpGL;

namespace OpenCAD.GUI.Buffers
{
    public class VAO : IBuffer
    {
        private readonly OpenGL _gl;
        private readonly IShaderProgram _program;
        public uint Handle { get; private set; }

        public VBO VBO { get; private set; }

        public VAO(OpenGL gl, IShaderProgram program, VBO vbo)
        {
            _gl = gl;
            _program = program;
            VBO = vbo;

            var buffers = new uint[1];
            gl.GenVertexArrays(1, buffers);
            Handle = buffers[0];

            using (new Bind(program))
            using (new Bind(this))
            using (new Bind(vbo))
            {
                var stride = Vect3f.SizeInBytes * 2 + Vect4f.SizeInBytes;

                gl.EnableVertexAttribArray(0);
                gl.VertexAttribPointer(0, 3, OpenGL.GL_FLOAT, true, stride, IntPtr.Zero);
                gl.BindAttribLocation(program.Handle, 0, "vert_position");

                gl.EnableVertexAttribArray(1);
                gl.VertexAttribPointer(1, 3, OpenGL.GL_FLOAT, true, stride, new IntPtr(Vect3f.SizeInBytes));
                gl.BindAttribLocation(program.Handle, 1, "vert_normal");

                gl.EnableVertexAttribArray(2);
                gl.VertexAttribPointer(2, 4, OpenGL.GL_FLOAT, false, stride, new IntPtr(Vect3f.SizeInBytes * 2));
                gl.BindAttribLocation(program.Handle, 2, "vert_colour");
            }
        }

        public void Bind()
        {
            _program.Bind();
            _gl.BindVertexArray(Handle);
        }

        public void UnBind()
        {
            _gl.BindVertexArray(0);
            _program.Bind();
        }

        public void Render()
        {
            using (new Bind(_program))
            using (new Bind(this))
            {
                _gl.DrawArrays((uint)VBO.Mode, 0, VBO.Count);
            }
        }
    }
}