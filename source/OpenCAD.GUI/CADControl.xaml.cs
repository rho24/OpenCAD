using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using OpenCAD.Core.Modeling;
using SharpGL;
using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.WPF;

namespace OpenCAD.GUI
{
    /// <summary>
    /// Interaction logic for CADControl.xaml
    /// </summary>
    public abstract partial class CADControl:UserControl
    {
        public OpenGL GL;
        DispatcherTimer timer;
        private readonly IObservable<Int64> _timer;

        public CADControl()
        {
            InitializeComponent();
            GL = new OpenGL();

            SizeChanged += CADControlSizeChanged;

            timer = new DispatcherTimer();
            timer.Tick += Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        public abstract void OnLoad(OpenGL gl);
        public abstract void OnUpdate(OpenGL gl);
        public abstract void OnRender(OpenGL gl);
        public abstract void OnResize(OpenGL gl);

        void CADControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            lock (GL)
            {
                var width = (int)e.NewSize.Width;
                var height = (int)e.NewSize.Height;
                GL.SetDimensions(width, height);
                GL.Viewport(0, 0, width, height);
                OnResize(GL);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            lock (GL)
            {
                GL.Create(RenderContextType.FBO, 1, 1, 32, null);
            }
            OnLoad(GL);
            timer.Start();
        }

        private void Tick(object sender, EventArgs eventArgs)
        {
            lock (GL)
            {
                GL.MakeCurrent();

                OnUpdate(GL);

                OnRender(GL);
                
                GL.Blit(IntPtr.Zero);

                var provider = GL.RenderContextProvider as SharpGL.RenderContextProviders.FBORenderContextProvider;
                if (provider != null)
                {
                    var newFormatedBitmapSource = new FormatConvertedBitmap();
                    newFormatedBitmapSource.BeginInit();

                    newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.InternalDIBSection.HBitmap);
                    newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
                    newFormatedBitmapSource.EndInit();

                    image.Source = newFormatedBitmapSource;
                }
            }
        }
    }
}
