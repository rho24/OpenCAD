using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Core.Graphics;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling;
using OpenCAD.Core.Modeling.Datums;
using OpenCAD.GUI.Buffers;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;

namespace OpenCAD.GUI
{
    public class ModelControl:CADControl
    {
        private readonly IModel _model;

        private int _modelUniform;
        private int _viewUniform;
        private int _projectionUniform;


        private VBO _vbo;
        private VAO _vao;

        private IShaderProgram _shader;
        private ICamera _camera;

        public ModelControl(IModel model)
        {

            _model = model;

            MouseWheel += (s, e) => _camera.View *= Mat4.Translate(0, 0, e.Delta / 120.0);
            MouseDown += context_MouseDown;

        }
        void context_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _camera.View *= Mat4.Translate(0, 0, 10 / 120.0);
        }


        public override void OnLoad(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            _camera = new BaseCamera();
            _shader = new BasicShader(gl);

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
            _vao = new VAO(gl, _shader, _vbo);


        }

        public override void OnUpdate(OpenGL gl)
        {
            using (new Bind(_shader))
            {
                gl.UniformMatrix4(_viewUniform, 1, false, _camera.View.ToColumnMajorArray());
            }
        }

        public override void OnRender(OpenGL gl)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(0.137f, 0.121f, 0.125f, 0f);

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

        public override void OnResize(OpenGL gl)
        {
            using (new Bind(_shader))
            {
                _camera.Resize((int)ActualWidth, (int)ActualHeight);
                gl.UniformMatrix4(_projectionUniform, 1, false, _camera.Projection.ToColumnMajorArray());
            }
        }


    }

    public class BasicShader : BaseShader
    {
        public BasicShader(OpenGL gl)
            : base(gl, "Basic.vert", "Basic.frag")
        {
        }
    }
}
