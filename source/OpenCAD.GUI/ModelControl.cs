using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OpenCAD.Core;
using OpenCAD.Core.Graphics;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling;
using OpenCAD.Core.Modeling.Datums;
using OpenCAD.GUI.Buffers;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using Point = System.Windows.Point;

namespace OpenCAD.GUI
{
    public class ModelControl:CADControl
    {
        private readonly IModel _model;

        private int _modelUniform;
        private int _viewUniform;
        private int _projectionUniform;



        private IShaderProgram _shader;
        private OrthographicCamera _camera;

        public ModelControl(IModel model)
        {
            _model = model;
            MouseWheel += (s, e) =>
                {
                    using (new Bind(_shader))
                    {

                        _camera.Scale += e.Delta/(120.0 * 2);
                    }
                    //_camera.View *= Mat4.Translate(0, 0, e.Delta / 120.0)
                };
            MouseDown += context_MouseDown;
            MouseMove += ModelControl_MouseMove;
        }

        void ModelControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var current = e.GetPosition(this) - new Point(ActualWidth / 2.0, ActualHeight / 2.0);
           // _camera.View *= Mat4.RotateZ(Angle.FromDegrees(1));
        }

        void context_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

           // _camera.View *= Mat4.Translate(0, 0, 10 / 120.0);
        }

        private CoordFBO coordFBO;
        private PlaneFBO planeFBO;
        public override void OnLoad(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(BlendingSourceFactor.SourceAlpha, BlendingDestinationFactor.OneMinusSourceAlpha);

            _camera = new OrthographicCamera();
            _camera.View *= Mat4.RotateY(Angle.FromDegrees(15));

            _shader = new BasicShader(gl);

            _modelUniform = gl.GetUniformLocation(_shader.Handle, "Model");
            _viewUniform = gl.GetUniformLocation(_shader.Handle, "View");
            _projectionUniform = gl.GetUniformLocation(_shader.Handle, "Projection");

            coordFBO = new CoordFBO(gl,_shader);
            planeFBO = new PlaneFBO(gl, _shader);
        }

        public override void OnUpdate(OpenGL gl)
        {
            _camera.View *= Mat4.RotateX(Angle.FromDegrees(0.6));


            using (new Bind(_shader))
            {
                _camera.Resize((int)ActualWidth, (int)ActualHeight);
                gl.UniformMatrix4(_projectionUniform, 1, false, _camera.Projection.ToColumnMajorArray());
                gl.UniformMatrix4(_viewUniform, 1, false, _camera.View.ToColumnMajorArray());
            }
        }

        public override void OnRender(OpenGL gl)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(0.137f, 0.121f, 0.125f, 0f);
            
            foreach (var csys in _model.Features.OfType<CoordinateSystem>())
            {
                _camera.Model = csys.Transform;
                using (new Bind(_shader))
                {
                    gl.UniformMatrix4(_modelUniform, 1, false, _camera.Model.ToColumnMajorArray());
                }
                coordFBO.Render();
            }

            foreach (var plane in _model.Features.OfType<DatumPlane>())
            {
                _camera.Model = plane.Transform;
                using (new Bind(_shader))
                {
                    gl.UniformMatrix4(_modelUniform, 1, false, _camera.Model.ToColumnMajorArray());
                }
                planeFBO.Render();
            }
        }

        public override void OnResize(OpenGL gl, int width, int height)
        {
            if (width == -1 || height == -1) return;
            using (new Bind(_shader))
            {
                _camera.Resize(width, height);
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


    public class CoordFBO
    {
        private readonly IShaderProgram _shader;

        private VBO _vbo;
        private VAO _vao;

        public CoordFBO(OpenGL gl,IShaderProgram shader)
        {
            _shader = shader;
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

        public void Render()
        {
            _vao.Render();
        }
    }


    public class PlaneFBO
    {
        private readonly OpenGL _gl;
        private readonly IShaderProgram _shader;
        private VBO _vbo;
        private VAO _vao;
        public PlaneFBO(OpenGL gl, IShaderProgram shader)
        {
            _gl = gl;
            _shader = shader;
            var data = new List<Vert>();
            const float size = 20f;
            var basecolour = Color.FromArgb(50, 0, 112, 204).ToVector4();
            data.Add(new Vert(new Vect3(-size / 2.0, -size / 2.0, 0), Vect3.Zero, basecolour));
            data.Add(new Vert(new Vect3(size / 2.0, -size / 2.0, 0), Vect3.Zero, basecolour));
            data.Add(new Vert(new Vect3(size / 2.0, size / 2.0, 0), Vect3.Zero, basecolour));
            data.Add(new Vert(new Vect3(-size / 2.0, size / 2.0, 0), Vect3.Zero, basecolour));



            data.Add(new Vert(new Vect3(-size / 2.0, size / 2.0, 0), Vect3.Zero, basecolour));
            data.Add(new Vert(new Vect3(size / 2.0, size / 2.0, 0), Vect3.Zero, basecolour));
            data.Add(new Vert(new Vect3(size / 2.0, -size / 2.0, 0), Vect3.Zero, basecolour));
            data.Add(new Vert(new Vect3(-size / 2.0, -size / 2.0, 0), Vect3.Zero, basecolour));





            _vbo = new VBO(gl, BeginMode.Quads, data);
            _vao = new VAO(gl, _shader, _vbo);
        }

        public void Render()
        {
            _vao.Render();
        }
    }
}
