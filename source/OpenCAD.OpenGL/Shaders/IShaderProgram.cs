using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.OpenGL.Buffers;

namespace OpenCAD.OpenGL.Shaders
{
    public interface IShaderProgram : IBuffer
    {
        int Handle { get; }
    }
}
