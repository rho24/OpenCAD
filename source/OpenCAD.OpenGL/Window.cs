using System;
using System.Collections.Generic;
using OpenCAD.Core.CSG;
using OpenCAD.OpenGL.Buffers;
using OpenCAD.OpenGL.Camera;
using OpenCAD.OpenGL.Shaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenCAD.OpenGL
{
    public class Window : GameWindow
    {

        private readonly ICamera _camera;



        public Window()
            : base(1280, 720, new GraphicsMode(32, 0, 0, 4), "OpenCAD")
        {
            VSync = VSyncMode.On;

            _camera = new MainCamera();

        }


        private ICSGSolid solid;


        private IShaderProgram _shader;

        private IShaderProgram _basicShader;
        private VBO _vbo;
        private VAO _vao;
        private CameraUBO _ubo;

        private VAO _dotsvao;
        private VBO _dotsvbo;

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //solid = new Cube();
            var data = new List<OpenGLVertex>
            {
                new OpenGLVertex {Position = new Vector3(-1, -1, 0), Colour = new Color4(0.137f, 0.121f, 0.125f, 0f).ToVector4()},
                new OpenGLVertex {Position = new Vector3(1, -1, 0), Colour = Color4.Blue.ToVector4()},
                new OpenGLVertex {Position = new Vector3(1, 1, 0), Colour = Color4.Blue.ToVector4()},
                new OpenGLVertex {Position = new Vector3(-1, 1, 0), Colour = new Color4(0.137f, 0.121f, 0.125f, 0f).ToVector4()},
            };

            _ubo = new CameraUBO();

            _shader = new FlatShaderProgram();
            _basicShader = new BasicShaderProgram(_ubo);

            _vbo = new VBO(data) { BeginMode = BeginMode.Quads };
            _vao = new VAO(_shader, _vbo);

            var dotdata = new List<OpenGLVertex>
            {
                new OpenGLVertex {Position = new Vector3(0, 0, 0)},
                new OpenGLVertex {Position = new Vector3(10, 10, 0)},
            };

            _dotsvbo = new VBO(dotdata) { BeginMode = BeginMode.Lines };
            _dotsvao = new VAO(_basicShader, _dotsvbo);


            var err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at OnLoad: " + err);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _ubo.Update(_camera);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(new Color4(0.137f, 0.121f, 0.125f, 0f));


            using (new Bind(_vao))
            {
                //GL.DrawArrays(_vao.VBO.BeginMode, 0, _vao.VBO.Count);
            }

            using (new Bind(_dotsvao))
            {
                GL.DrawArrays(_dotsvao.VBO.BeginMode, 0, _dotsvao.VBO.Count);
            }

            SwapBuffers();
            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Swapbuffers: " + err.ToString());
            Title = String.Format(" FPS:{0} Mouse<{1},{2}>",1.0 / e.Time ,Mouse.X, Height - Mouse.Y);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            _camera.Resize(Width, Height);
        }
    }
}