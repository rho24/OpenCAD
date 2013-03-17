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
}