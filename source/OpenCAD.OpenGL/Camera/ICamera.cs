using OpenCAD.Core.Maths;

namespace OpenCAD.OpenGL.Camera
{
    public interface ICamera
    {
        // Vect3 Postion { get; }
        double Near { get; }
        double Far { get; }

        Mat4 Model { get; set; }
        Mat4 View { get; }
        Mat4 Projection { get; set; }
        Mat4 MVP { get; }

        Vect3 Eye { get; set; }
        Vect3 Target { get; set; }
        Vect3 Up { get; set; }

        void Update(double delta);
        void Resize(int width, int height);
    }
}