using System;
using System.Collections.Generic;
using System.Drawing;

using OpenCAD.Core.Meshing.Loaders;
using OpenCAD.OpenGL.Buffers;
using OpenCAD.OpenGL.Camera;
using OpenCAD.OpenGL.Shaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

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


        private IShaderProgram _shader;

        private IShaderProgram _basicShader;
        private VBO _vbo;
        private VAO _vao;
        private CameraUBO _ubo;

        private VAO _dotsvao;
        private VBO _dotsvbo;

        private VAO _geovao;
        private VBO _geovbo;


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
                new OpenGLVertex {Position = new Vector3(-1, -1, 0), Colour  =new Color4(0.118f, 0.118f, 0.118f, 1f).ToVector4()},//bottom left
                new OpenGLVertex {Position = new Vector3(1, -1, 0), Colour= new Color4(0.118f, 0.118f, 0.118f, 1f).ToVector4() }, //bottom right
                new OpenGLVertex {Position = new Vector3(1, 1, 0), Colour = new Color4(0.176f, 0.176f, 0.188f, 1f).ToVector4() },//top right
                new OpenGLVertex {Position = new Vector3(-1, 1, 0), Colour =  new Color4(0.176f, 0.176f, 0.188f, 1f).ToVector4()},//top left
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

            var stl = new STL("rounded_cube.stl", Color.GreenYellow);

            var cubedata = new List<OpenGLVertex>();
            var col = stl.Color.ToVector4();

            foreach (var ele in stl.Elements)
            {
                cubedata.Add(new OpenGLVertex { Position = ele.P1.ToVector3(), Normal = ele.Normal.ToVector3(), Colour = col });
                cubedata.Add(new OpenGLVertex { Position = ele.P2.ToVector3(), Normal = ele.Normal.ToVector3(), Colour = col });
                cubedata.Add(new OpenGLVertex { Position = ele.P3.ToVector3(), Normal = ele.Normal.ToVector3(), Colour = col });
            }

            _geovbo = new VBO(cubedata) { BeginMode = BeginMode.Triangles };
            _geovao = new VAO(_basicShader, _geovbo);


            var err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at OnLoad: " + err);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.P])
            {
                ToggleWireFrame();
            }

            _ubo.Update(_camera);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(new Color4(0.137f, 0.121f, 0.125f, 0f));


            using (new Bind(_vao))
            {
                GL.DrawArrays(_vao.VBO.BeginMode, 0, _vao.VBO.Count);
            }

            using (new Bind(_dotsvao))
            {
              //  GL.DrawArrays(_dotsvao.VBO.BeginMode, 0, _dotsvao.VBO.Count);
            }

            using (new Bind(_geovao))
            {
                GL.DrawArrays(_geovao.VBO.BeginMode, 0, _geovao.VBO.Count);
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

        private bool _wireframe;
        private void ToggleWireFrame()
        {
            _wireframe = !_wireframe;
            GL.PolygonMode(MaterialFace.FrontAndBack, _wireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }
}