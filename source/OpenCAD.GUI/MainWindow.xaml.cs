using System.Diagnostics;
using System.Windows;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling;
using OpenCAD.Core.Modeling.Datums;

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

            Features.Add(new DatumPlane("X Plane", Vect3.UnitX, 0.0));
            Features.Add(new DatumPlane("Y Plane", Vect3.UnitY, 0.0));
            Features.Add(new DatumPlane("Z Plane", Vect3.UnitZ, 0.0));

            Debug.WriteLine(new DatumPlane("X Plane", Vect3.UnitX, 0.0).Transform);
            Debug.WriteLine(new DatumPlane("Y Plane", Vect3.UnitY, 0.0).Transform);
            Debug.WriteLine(new DatumPlane("Z Plane", Vect3.UnitZ, 0.0).Transform);

        }
    }
}