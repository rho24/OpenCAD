using System;
using OpenCAD.Core.Maths;

namespace OpenCAD.OpenGL.Camera
{
    public class MainCamera : ICamera
    {
        private int _width;
        private int _height;
        public MainCamera()
        {

            Near = 1f;
            Far = 512.0f;
            Model = Mat4.Identity;
            Eye = Vect3.Zero;
            Target = Vect3.Zero;
            Up = Vect3.UnitY;
            Eye = new Vect3(0.0f, 0.0f, 5.0f);
        }

        public double Near { get; private set; }
        public double Far { get; private set; }
        public Mat4 Model { get; set; }


        public Mat4 View
        {
            get { return Mat4.LookAt(Eye, Target, Up); }
        }

        public Mat4 Projection { get; set; }

        public Mat4 MVP
        {
            get { return Projection * View * Model; }
        }

        public Vect3 Eye { get; set; }

        public Vect3 Target { get; set; }

        public Vect3 Up { get; set; }

        public void Update(double delta)
        {

        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
            Projection = Mat4.CreatePerspective(Math.PI / 4, _width / (float)_height, Near, Far);
        }
    }
}