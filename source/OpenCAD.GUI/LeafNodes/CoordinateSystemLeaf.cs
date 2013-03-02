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
    public class CoordinateSystemLeaf:ILeafNode
    {
        private readonly IShaderProgram _shader;
        private readonly VAO _vao;
        private const float Size = 10f;
        public CoordinateSystemLeaf(OpenGL gl, IShaderProgram shader, IEnumerable<CoordinateSystem> coordinateSystems)
        {
            _shader = shader;
            var data = new List<Vert>();

            foreach (var csys in coordinateSystems)
            {
                var origin = csys.Transform.ToVect3();
                data.Add(new Vert(origin, Vect3.Zero, Color.Blue.ToVector4()));
                data.Add(new Vert((csys.Transform * Mat4.Translate(new Vect3(Size, 0, 0))).ToVect3(), Vect3.Zero, Color.Blue.ToVector4()));
                data.Add(new Vert(origin, Vect3.Zero, Color.Red.ToVector4()));
                data.Add(new Vert((csys.Transform * Mat4.Translate(new Vect3(0, Size, 0))).ToVect3(), Vect3.Zero, Color.Red.ToVector4()));
                data.Add(new Vert(origin, Vect3.Zero, Color.Green.ToVector4()));
                data.Add(new Vert((csys.Transform * Mat4.Translate(new Vect3(0, 0, Size))).ToVect3(), Vect3.Zero, Color.Green.ToVector4()));
            }
            _vao = new VAO(gl, _shader, new VBO(gl, BeginMode.Lines, data));
        }

        public void Render()
        {
            _vao.Render();
        }
    }

    public class DatumPlaneLeaf : ILeafNode
    {
        private readonly IShaderProgram _shader;
        private readonly VAO _lineVAO;
        private readonly VAO _quadVAO;
        private Vect4 _edgecolour = Color.FromArgb(255, 0, 112, 204).ToVector4();
        private Vect4 _basecolour = Color.FromArgb(50, 0, 112, 204).ToVector4();
        private const float Size = 10f;

        public DatumPlaneLeaf(OpenGL gl, IShaderProgram shader, IEnumerable<DatumPlane> planes)
        {
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
            _lineVAO.Render();
        }
    }
}