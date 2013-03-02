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
}