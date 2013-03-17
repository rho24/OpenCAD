using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Graphics
{
    public abstract class BaseCamera : ICamera
    {


        public Mat4 Model { get; set; }
        public Mat4 View { get; set; }
        public Mat4 Projection { get; set; }

        public Mat4 MVP
        {
            get { return Projection * View * Model; }
        }

        public Vect3 Eye { get; set; }
        public Vect3 Target { get; set; }
        public Vect3 Up { get; set; }

        public double Near { get; protected set; }
        public double Far { get; protected set; }

        public abstract void Update(double delta);
        public abstract void Resize(int width, int height);

    }
}