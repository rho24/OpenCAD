using System.Windows;

namespace OpenCAD.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();

            addDemo.Click += (sender, args) => mainDock.Children.Add(new TeaPotDemo());
        }
    }
}