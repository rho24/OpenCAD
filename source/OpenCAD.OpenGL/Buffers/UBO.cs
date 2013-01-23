using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.OpenGL.Camera;
using OpenCAD.OpenGL.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenCAD.OpenGL.Buffers
{
    public interface IUBO : IBuffer
    {
        int Handle { get; }
        //void Update();
        void BindToShaderProgram(IShaderProgram program);
    }

    public abstract class BaseUBO<T> : IUBO where T : struct
    {
        private readonly string _blockName;
        private readonly int _location;
        private readonly int _size;
        private int _handle;

        protected T Data;

        public int Handle
        {
            get { return _handle; }
            private set { _handle = value; }
        }

        protected BaseUBO(string blockName, int location, int size)
        {
            _blockName = blockName;
            _location = location;
            _size = size;
            GL.GenBuffers(1, out _handle);
            using (new Bind(this))
            {
                GL.BufferData(BufferTarget.UniformBuffer, (IntPtr)(_size), (IntPtr)(null), BufferUsageHint.StreamDraw);
                GL.BindBufferRange(BufferTarget.UniformBuffer, _location, _handle, (IntPtr)0, (IntPtr)_size);
            }
        }
        protected BaseUBO(string blockName, int location)
            : this(blockName, location, Marshal.SizeOf(default(T)))
        {

        }

        public void BindToShaderProgram(IShaderProgram program)
        {
            GL.UniformBlockBinding(program.Handle, GL.GetUniformBlockIndex(program.Handle, _blockName), _location);
        }

        protected void Update()
        {
            using (new Bind(this))
            {
                GL.BufferSubData(BufferTarget.UniformBuffer, (IntPtr)0, (IntPtr)_size, ref Data);
            }
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, _handle);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);
        }
    }
    public class CameraUBO : BaseUBO<CameraUBO.CameraData>
    {

        public struct CameraData
        {
            public Matrix4 MVP;
            public Matrix4 Model;
            public Matrix4 View;
            public Matrix4 Projection;
            public Matrix4 NormalMatrix;
        }

        public CameraUBO()
            : base("Camera", 0)
        {

        }

        public void Update(ICamera camera)
        {
            var normal = (camera.Model * camera.View).ToMatrix4();
            normal.Invert();
            normal.Transpose();
            Data = new CameraData
            {
                MVP = camera.MVP.ToMatrix4(),
                Model = camera.Model.ToMatrix4(),
                View = camera.View.ToMatrix4(),
                Projection = camera.Projection.ToMatrix4(),
                NormalMatrix = normal
            };
            Update();
        }
    }
}
