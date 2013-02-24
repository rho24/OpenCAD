using System;

namespace OpenCAD.GUI.Buffers
{
    public class Bind : IDisposable
    {
        private readonly IBuffer _buffer;
        public Bind(IBuffer buffer)
        {
            _buffer = buffer;
            _buffer.Bind();
        }
        public void Dispose()
        {
            _buffer.UnBind();
        }
        public static Bind Asset(IBuffer buffer)
        {
            return new Bind(buffer);
        }
    }
}