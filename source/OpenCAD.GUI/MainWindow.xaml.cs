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

        private readonly IModel _model;
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
        }
    }
}