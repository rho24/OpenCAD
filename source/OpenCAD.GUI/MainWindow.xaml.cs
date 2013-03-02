using System.Diagnostics;
using System.Windows;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling;
using OpenCAD.Core.Modeling.Datums;
using OpenCAD.Core.Modeling.Sections;

namespace OpenCAD.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
            mainDock.Children.Add(new CADDocument(new TestPart("testing")));
            addDemo.Click += (sender, args) => mainDock.Children.Add(new CADDocument(new TestPart("second model")));
        }
    }

    public class TestPart:BasePart
    {
        public TestPart(string name)
        {
            Name = name;
            Features.Add(new CoordinateSystem("Origin", Mat4.Identity));
            Features.Add(new CoordinateSystem("Translated", Mat4.Translate(5,5,5)*Mat4.RotateY(Angle.FromDegrees(27))));

            var x = new DatumPlane("X Plane", Vect3.UnitX, 0.0);
            var y = new DatumPlane("Y Plane", Vect3.UnitY, 0.0);
            var z = new DatumPlane("Z Plane", Vect3.UnitZ, 0.0);
            Features.Add(x);
            Features.Add(y);
            Features.Add(z);

            Features.Add(new Section("Test Section", z));
        }
    }
}