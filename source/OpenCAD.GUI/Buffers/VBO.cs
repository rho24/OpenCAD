using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Graphics;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;

namespace OpenCAD.GUI.Buffers
{
    public class VBO : IBuffer
    {
        private readonly OpenGL _gl;

        public uint Handle { get; private set; }

        public int Count { get; private set; }

        public BeginMode Mode { get; set; }

        public readonly IEnumerable<Vert> Data;

        public VBO(OpenGL gl, BeginMode beginMode)
        {
            Mode = beginMode;
            _gl = gl;


            var buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            Handle = buffers[0];

            Count = 0;

        }

        public VBO(OpenGL gl, BeginMode beginMode, IEnumerable<Vert> data)
            : this(gl,beginMode)
        {
            Data = data;
            Buffer(Data);
        }


        private void Buffer(IEnumerable<Vert> data)
        {
            using (new Bind(this))
            {





                var gldata = data.ToOpenGLVertices().ToArray();
                Count = gldata.Count();
                var vertsHandle = GCHandle.Alloc(gldata, GCHandleType.Pinned);
                _gl.BufferData(OpenGL.GL_ARRAY_BUFFER, Count * OpenGLVertex.SizeInBytes, vertsHandle.AddrOfPinnedObject(), OpenGL.GL_STATIC_DRAW);
                vertsHandle.Free();
            }
        }

        public void Bind()
        {
            _gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, Handle);
        }
        public void UnBind()
        {
            _gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
