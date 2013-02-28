using OpenCAD.Core.Maths;

namespace OpenCAD.Core.Graphics
{
    public class OrthographicCamera:BaseCamera
    {
        public double Scale { get;  set; }

        public OrthographicCamera()
        {
            Near = -1000f;
            Far = 1000f;
            Target = Vect3.Zero;
            Up = Vect3.UnitY;
            Eye = new Vect3(0, 0, 20);
            Model = Mat4.Identity;
            View = Mat4.LookAt(Eye, Target, Up);
            Projection = Mat4.Identity;
            Scale = 15;
        }
        public override void Update(double delta)
        {
            
        }

        public override void Resize(int width, int height)
        {
            Projection = Mat4.CreateOrthographic(-width / 2.0, width / 2.0, -height / 2.0, height / 2.0, Near, Far) * Mat4.Scale(Scale);
        }
    }
}