using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using AvalonDock.Layout;
using OpenCAD.Core.Graphics;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling;
using OpenCAD.Core.Modeling.Datums;
using OpenCAD.GUI.Buffers;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using System.Collections.Generic;
using System.Reactive;

namespace OpenCAD.GUI
{
    public partial class TeaPotDemo
    {
        private readonly IModel _model;
        //private readonly Teapot _teapot = new Teapot();

        public TeaPotDemo(IModel model) {
            InitializeComponent();
            _model = model;

            Title = string.Format("Part: {0}",model.Name);
            context.OpenGLDraw += OnDraw;
            context.OpenGLInitialized += OnLoad;
            context.Resized += context_Resized;
            context.MouseWheel += (s, e) => _camera.View *= Mat4.Translate(0, 0, e.Delta / 120.0);
            context.MouseDown += context_MouseDown;
            context.Focus();
            
        }

        void context_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _camera.View *= Mat4.Translate(0, 0, 10 / 120.0);
        }

        void context_Resized(object sender, OpenGLEventArgs args)
        {
            var gl = args.OpenGL;
            using (new Bind(_shader))
            {
                _camera.Resize((int)context.ActualWidth, (int)context.ActualHeight);
                gl.UniformMatrix4(_projectionUniform, 1, false, _camera.Projection.ToColumnMajorArray());
            }
        }

   

        private int _modelUniform;
        private int _viewUniform;
        private int _projectionUniform;


        private VBO _vbo;
        private VAO _vao;

        private IShaderProgram _shader;
        private ICamera _camera;

        private void OnLoad(object sender, OpenGLEventArgs args)
        {
            _camera = new BaseCamera();

            OpenGL gl = args.OpenGL;
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
  
            _shader = new TestShader(gl);

            _modelUniform = gl.GetUniformLocation(_shader.Handle, "Model");
            _viewUniform = gl.GetUniformLocation(_shader.Handle, "View");
            _projectionUniform = gl.GetUniformLocation(_shader.Handle, "Projection");

            var data = new List<Vert>();
            const float size = 10f;
            data.Add(new Vert(new Vect3(0, 0, 0), Vect3.Zero, Color.Blue.ToVector4()));
            data.Add(new Vert(new Vect3(size, 0, 0), Vect3.Zero, Color.Blue.ToVector4()));

            data.Add(new Vert(new Vect3(0, 0, 0), Vect3.Zero, Color.Red.ToVector4()));
            data.Add(new Vert(new Vect3(0, size, 0), Vect3.Zero, Color.Red.ToVector4()));

            data.Add(new Vert(new Vect3(0, 0, 0), Vect3.Zero, Color.Green.ToVector4()));
            data.Add(new Vert(new Vect3(0, 0, size), Vect3.Zero, Color.Green.ToVector4()));

            _vbo = new VBO(gl, BeginMode.Lines, data);
            _vao = new VAO(gl,_shader,_vbo);
        }


        private void OnDraw(object sender, OpenGLEventArgs args) {
            var gl = args.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(0.137f, 0.121f, 0.125f,0f);

            using (new Bind(_shader))
            {
                gl.UniformMatrix4(_viewUniform, 1, false, _camera.View.ToColumnMajorArray());
            }

            foreach (var csys in _model.Features.OfType<CoordinateSystem>())
            {
                using (new Bind(_shader))
                {
                    _camera.Model = csys.Transform;
                    gl.UniformMatrix4(_modelUniform, 1, false, _camera.Model.ToColumnMajorArray());
                }
                _vao.Render();
            }
        }
    }

    public class TestShader:BaseShader
    {
        public TestShader(OpenGL gl) : base(gl, "Basic.vert","Basic.frag")
        {
        }
    }
}