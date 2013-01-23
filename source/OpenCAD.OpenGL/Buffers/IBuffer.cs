using System;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenGL.Buffers
{
    public interface IBuffer
    {
        void Bind();
        void UnBind();
    }

    public class Bind : IDisposable
    {
        private readonly IBuffer _asset;
        public Bind(IBuffer asset)
        {
            _asset = asset;
            _asset.Bind();
        }

        public void Dispose()
        {
            _asset.UnBind();
        }
        public static Bind Asset(IBuffer asset)
        {
            return new Bind(asset);
        }
    }
}
