using System.Collections.Generic;
using System.Drawing;
using OpenCAD.Core.Graphics;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling.Datums;
using OpenCAD.GUI.Buffers;
using SharpGL;
using SharpGL.Enumerations;

namespace OpenCAD.GUI.LeafNodes
{
    public class DatumPlaneLeaf : ILeafNode
    {
        private readonly OpenGL _gl;
        private readonly IShaderProgram _shader;
        private readonly VAO _lineVAO;
        private readonly VAO _quadVAO;
        private readonly Vect4 _edgecolour = Color.FromArgb(255, 0, 112, 204).ToVector4();
        private readonly Vect4 _basecolour = Color.FromArgb(50, 0, 112, 204).ToVector4();
        private const float Size = 10f;

        public DatumPlaneLeaf(OpenGL gl, IShaderProgram shader, IEnumerable<DatumPlane> planes)
        {
            _gl = gl;
            _shader = shader;
            var linedata = new List<Vert>();
            var quaddata = new List<Vert>();

            const float size = 20f;
            foreach (var plane in planes)
            {
          
                var v1 = (plane.Transform * Mat4.Translate(new Vect3(-size / 2.0, -size / 2.0, 0))).ToVect3();
                var v2 = (plane.Transform * Mat4.Translate(new Vect3(size / 2.0, -size / 2.0, 0))).ToVect3();
                var v3 = (plane.Transform * Mat4.Translate(new Vect3(size / 2.0, size / 2.0, 0))).ToVect3();
                var v4 = (plane.Transform * Mat4.Translate(new Vect3(-size / 2.0, size / 2.0, 0))).ToVect3();

                linedata.Add(new Vert(v1, Vect3.Zero, _edgecolour));
                linedata.Add(new Vert(v2, Vect3.Zero, _edgecolour));
                linedata.Add(new Vert(v2, Vect3.Zero, _edgecolour));
                linedata.Add(new Vert(v3, Vect3.Zero, _edgecolour));
                linedata.Add(new Vert(v3, Vect3.Zero, _edgecolour));
                linedata.Add(new Vert(v4, Vect3.Zero, _edgecolour));
                linedata.Add(new Vert(v4, Vect3.Zero, _edgecolour));
                linedata.Add(new Vert(v1, Vect3.Zero, _edgecolour));

                quaddata.AddRange(new[] { new Vert(v1, Vect3.Zero, _basecolour), new Vert(v2, Vect3.Zero, _basecolour), new Vert(v3, Vect3.Zero, _basecolour), new Vert(v4, Vect3.Zero, _basecolour) });
                quaddata.AddRange(new[] { new Vert(v4, Vect3.Zero, _basecolour), new Vert(v3, Vect3.Zero, _basecolour), new Vert(v2, Vect3.Zero, _basecolour), new Vert(v1, Vect3.Zero, _basecolour) });

            }
            _lineVAO = new VAO(gl, _shader, new VBO(gl, BeginMode.Lines, linedata));
            _quadVAO = new VAO(gl, _shader, new VBO(gl, BeginMode.Quads, quaddata));
        }

        public void Render()
        {
            _quadVAO.Render();
            _gl.Disable(OpenGL.GL_DEPTH_TEST);
            _lineVAO.Render();
            _gl.Enable(OpenGL.GL_DEPTH_TEST);
        }
    }
}