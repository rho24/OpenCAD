using System;
using System.Windows.Controls;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;

namespace OpenCAD.GUI.Views
{
    public partial class TeapotView : UserControl, IDisposable
    {
        private readonly Teapot _teapot = new Teapot();

        public TeapotView() {
            InitializeComponent();
            context.OpenGLDraw += OpenGlControlOpenGlDraw;
            context.OpenGLInitialized += OpenGLControl_OpenGLInitialized;
        }

        private void OpenGlControlOpenGlDraw(object sender, OpenGLEventArgs args) {
            args.OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            args.OpenGL.LoadIdentity();
            args.OpenGL.Translate(0.0, -1.0, -6.0);

            _teapot.Draw(args.OpenGL, 14, 1.0, OpenGL.GL_FILL);
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args) {
            OpenGL gl = args.OpenGL;

            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] {0.5f, 0.5f, 0.5f, 1.0f};
            float[] light0pos = new float[] {0.0f, 5.0f, 10.0f, 1.0f};
            float[] light0ambient = new float[] {0.2f, 0.2f, 0.2f, 1.0f};
            float[] light0diffuse = new float[] {0.3f, 0.3f, 0.3f, 1.0f};
            float[] light0specular = new float[] {0.8f, 0.8f, 0.8f, 1.0f};

            float[] lmodel_ambient = new float[] {0.2f, 0.2f, 0.2f, 1.0f};
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }

        public void Dispose() {
            context.OpenGLDraw -= OpenGlControlOpenGlDraw;
            context.OpenGLInitialized -= OpenGLControl_OpenGLInitialized;
        }
    }
}