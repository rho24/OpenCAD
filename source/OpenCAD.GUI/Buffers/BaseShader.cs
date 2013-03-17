using System.Diagnostics;
using SharpGL;
using SharpGL.SceneGraph.Shaders;

namespace OpenCAD.GUI.Buffers
{
    public abstract class BaseShader:IShaderProgram
    {
        private readonly OpenGL _gl;
        public uint Handle { get; private set; }

        private readonly ShaderProgram _program = new ShaderProgram();
        private VertexShader _vert = new VertexShader();
        private FragmentShader _frag = new FragmentShader();


        public BaseShader(OpenGL gl,string vertFile, string fragFile)
        {
            _gl = gl;
            _program.CreateInContext(gl);
            
            Handle = _program.ProgramObject; 

            _vert.CreateInContext(gl);
            _vert.LoadSource(vertFile);
            _vert.Compile();
            Debug.Write(_vert.InfoLog);


            _frag.CreateInContext(gl);
            _frag.LoadSource(fragFile);
            _frag.Compile();
            Debug.Write(_frag.InfoLog);


            _program.AttachShader(_vert);
            _program.AttachShader(_frag);
            _program.Link();

            Debug.Write(_program.InfoLog);
        }

        public void Bind()
        {
            _gl.UseProgram(Handle);
        }

        public void UnBind()
        {
            _gl.UseProgram(0);
        }
    }
}