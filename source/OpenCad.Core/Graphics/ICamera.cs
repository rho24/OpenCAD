using System;
using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Graphics
{
    public interface ICamera
    {
        Mat4 Model { get; set; }
        Mat4 View { get; set; }
        Mat4 Projection { get; set; }
        Mat4 MVP { get; }

        Vect3 Eye { get; set; }
        Vect3 Target { get; set; }
        Vect3 Up { get; set; }

        void Update(double delta);
        void Resize(int width, int height);
    }

    public class BaseCamera : ICamera
    {


        public Mat4 Model { get; set; }
        public Mat4 View { get; set; }
        public Mat4 Projection { get; set; }

        public Mat4 MVP
        {
            get { return Projection*View*Model; }
        }

        public Vect3 Eye { get; set; }
        public Vect3 Target { get; set; }
        public Vect3 Up { get; set; }
        private int _width;
        private int _height;

        public double Near { get; private set; }
        public double Far { get; private set; }

        public  BaseCamera()
        {

            Near = 1f;
            Far = 500f;

            Target = Vect3.Zero;
            Up = Vect3.UnitY;

            Eye = new Vect3(0, 0, 20);

            Model = Mat4.Identity;
            View = Mat4.LookAt(Eye, Target, Up);
            Projection = Mat4.Identity;


        }

        public void Update(double delta)
        {

        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
            Projection = Mat4.CreatePerspectiveFieldOfView(Math.PI / 2, _width / (float)_height, Near, Far);

        }
    }
}