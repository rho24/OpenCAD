using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using OpenCAD.Core;
using OpenCAD.Core.Graphics;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling;
using OpenCAD.Core.Modeling.Datums;
using OpenCAD.Core.Modeling.Sections;
using OpenCAD.Core.Topology;
using OpenCAD.GUI.Buffers;
using OpenCAD.GUI.LeafNodes;
using SharpGL;
using SharpGL.Enumerations;
using Point = System.Windows.Point;

namespace OpenCAD.GUI
{
    public class ModelControl:CADControl
    {
        private readonly IModel _model;

        private int _modelUniform;
        private int _viewUniform;
        private int _projectionUniform;


        private SceneGraph _graph;
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
                };
           // MouseDown += ContextMouseDown;
           // MouseMove += ModelControlMouseMove;
        }

        private PointRenderer _points;
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

            _graph = new SceneGraph();

            _graph.Nodes.Add(new CoordinateSystemLeaf(gl, _shader, _model.Features.OfType<CoordinateSystem>()));
            _graph.Nodes.Add(new DatumPlaneLeaf(gl, _shader, _model.Features.OfType<DatumPlane>()));






















            const double s = 5.0;
            var v0 = new Vertex("v0", new Vect3(0, 0, 0));
            var v1 = new Vertex("v1", new Vect3(0, 0, s));
            var v2 = new Vertex("v2", new Vect3(s, 0, s));
            var v3 = new Vertex("v3", new Vect3(s, 0, 0));
            var v4 = new Vertex("v4", new Vect3(0, s, 0));
            var v5 = new Vertex("v5", new Vect3(0, s, s));
            var v6 = new Vertex("v6", new Vect3(s, s, s));
            var v7 = new Vertex("v7", new Vect3(s, s, 0));


            var f0 = new Face("f0");
            var f1 = new Face("f1");
            var f2 = new Face("f2");
            var f3 = new Face("f3");
            var f4 = new Face("f4");
            var f5 = new Face("f5");
            LineHalfEdge e0a = null;
            LineHalfEdge e0b = null;
            LineHalfEdge e1a = null;
            LineHalfEdge e1b = null;
            LineHalfEdge e2a = null;
            LineHalfEdge e2b = null;
            LineHalfEdge e3a = null;
            LineHalfEdge e3b = null;
            LineHalfEdge e4a = null;
            LineHalfEdge e4b = null;
            LineHalfEdge e5a = null;
            LineHalfEdge e5b = null;
            LineHalfEdge e6a = null;
            LineHalfEdge e6b = null;
            LineHalfEdge e7a = null;
            LineHalfEdge e7b = null;
            LineHalfEdge e8a = null;
            LineHalfEdge e8b = null;
            LineHalfEdge e9a = null;
            LineHalfEdge e9b = null;
            LineHalfEdge e10a = null;
            LineHalfEdge e10b = null;
            LineHalfEdge e11a = null;
            LineHalfEdge e11b = null;

            e0a = new LineHalfEdge("e0a", v1, new Lazy<IHalfEdge>(() => e0b), f0);
            e1a = new LineHalfEdge("e1a", v2, new Lazy<IHalfEdge>(() => e1b), f0);
            e2a = new LineHalfEdge("e2a", v3, new Lazy<IHalfEdge>(() => e2b), f0);
            e3a = new LineHalfEdge("e3a", v0, new Lazy<IHalfEdge>(() => e3b), f0);
            f0.Loops.Add(new EdgeLoop(e0a, e1a, e2a, e3a));

            e0b = new LineHalfEdge("e0b", v0, new Lazy<IHalfEdge>(() => e0a), f1);
            e7a = new LineHalfEdge("e7a", v4, new Lazy<IHalfEdge>(() => e7b), f1);
            e8a = new LineHalfEdge("e8a", v5, new Lazy<IHalfEdge>(() => e8b), f1);
            e4a = new LineHalfEdge("e4a", v1, new Lazy<IHalfEdge>(() => e4b), f1);
            f1.Loops.Add(new EdgeLoop(e0b, e7a, e8a, e4a));

            e1b = new LineHalfEdge("e1b", v1, new Lazy<IHalfEdge>(() => e1a), f2);
            e4b = new LineHalfEdge("e4b", v5, new Lazy<IHalfEdge>(() => e4a), f2);
            e9a = new LineHalfEdge("e9a", v6, new Lazy<IHalfEdge>(() => e9b), f2);
            e5a = new LineHalfEdge("e5a", v2, new Lazy<IHalfEdge>(() => e5b), f2);
            f2.Loops.Add(new EdgeLoop(e1b, e4b, e9a, e5a));


            e2b = new LineHalfEdge("e2b", v2, new Lazy<IHalfEdge>(() => e2a), f3);
            e5b = new LineHalfEdge("e5b", v6, new Lazy<IHalfEdge>(() => e5a), f3);
            e10a = new LineHalfEdge("e10a", v7, new Lazy<IHalfEdge>(() => e10b), f3);
            e6a = new LineHalfEdge("e6a", v3, new Lazy<IHalfEdge>(() => e6b), f3);
            f3.Loops.Add(new EdgeLoop(e2b, e5b, e10a, e6a));


            e3b = new LineHalfEdge("e3b", v3, new Lazy<IHalfEdge>(() => e3a), f4);
            e6b = new LineHalfEdge("e6b", v7, new Lazy<IHalfEdge>(() => e6a), f4);
            e11a = new LineHalfEdge("e11a", v4, new Lazy<IHalfEdge>(() => e11b), f4);
            e7b = new LineHalfEdge("e7b", v0, new Lazy<IHalfEdge>(() => e7a), f4);
            f4.Loops.Add(new EdgeLoop(e3b, e6b, e11a, e7b));


            e8b = new LineHalfEdge("e8b", v4, new Lazy<IHalfEdge>(() => e8a), f5);
            e11b = new LineHalfEdge("e11b", v7, new Lazy<IHalfEdge>(() => e11a), f5);
            e10b = new LineHalfEdge("e10b", v6, new Lazy<IHalfEdge>(() => e10a), f5);
            e9b = new LineHalfEdge("e9b", v5, new Lazy<IHalfEdge>(() => e9b), f5);
            f5.Loops.Add(new EdgeLoop(e8b, e11b, e10b, e9b));

            var shell = new Shell(f0,f1,f2,f3,f4,f5);

            var data = new List<Vert>();
            foreach (var face in shell.Faces)
            {
                foreach (var loop in face.Loops)
                {
                    foreach (var edge in loop.Edges)
                    {
                        data.Add(new Vert(edge.Start, Vect3.Zero, Color.Gold.ToVector4()));
                        data.Add(new Vert(edge.End, Vect3.Zero, Color.Gold.ToVector4()));
                 
                    }
                }
            }
            _points = new PointRenderer(gl,_shader,data);
            // gl.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);
        }

        public override void OnUpdate(OpenGL gl)
        {
           _camera.View *= Mat4.RotateX(Angle.FromDegrees(0.6));
            using (new Bind(_shader))
            {
                _camera.Resize((int)ActualWidth, (int)ActualHeight);
                gl.UniformMatrix4(_modelUniform, 1, false, Mat4.Identity.ToColumnMajorArray());
                gl.UniformMatrix4(_viewUniform, 1, false, _camera.View.ToColumnMajorArray());
                gl.UniformMatrix4(_projectionUniform, 1, false, _camera.Projection.ToColumnMajorArray());
            }
        }

        public override void OnRender(OpenGL gl)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(0.137f, 0.121f, 0.125f, 0f);
            
            _graph.Render();


            gl.Disable(OpenGL.GL_DEPTH_TEST);
            using (new Bind(_shader))
            {
                gl.UniformMatrix4(_modelUniform, 1, false, Mat4.Identity.ToColumnMajorArray());

                gl.PointSize(10f);
                gl.Begin(BeginMode.Points);
                gl.Vertex(6, 0);
                gl.End();
            }


            gl.PointSize(5f);
            
            _points.Render();
            gl.Enable(OpenGL.GL_DEPTH_TEST);
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

    public class PointRenderer
    {
        private readonly IShaderProgram _shader;

        private readonly VBO _vbo;
        private readonly VAO _vao;

        public PointRenderer(OpenGL gl, IShaderProgram shader, IEnumerable<Vert> data)
        {
            _shader = shader;
            _vbo = new VBO(gl, BeginMode.Lines, data);
            _vao = new VAO(gl, _shader, _vbo);
        }

        public void Render()
        {
            _vao.Render();
        }
    }
}
