using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        readonly DispatcherTimer _timer;
        private Stopwatch _sw = new Stopwatch();
        public double FPS;

        protected CADControl()
        {
            InitializeComponent();
            GL = new OpenGL();

            SizeChanged += CADControlSizeChanged;

            _timer = new DispatcherTimer();
            _timer.Tick += Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
        }

        public abstract void OnLoad(OpenGL gl);
        public abstract void OnUpdate(OpenGL gl);
        public abstract void OnRender(OpenGL gl);
        public abstract void OnResize(OpenGL gl, int width, int height);

        void CADControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            lock (GL)
            {
                GL.MakeCurrent();
                var width = (int)e.NewSize.Width;
                var height = (int)e.NewSize.Height;
                GL.SetDimensions(width, height);
                GL.Viewport(0, 0, width, height);
                OnResize(GL,(int) e.NewSize.Width,(int) e.NewSize.Height);
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
            _timer.Start();
        }

        private void Tick(object sender, EventArgs eventArgs)
        {
            lock (GL)
            {
                _sw.Restart();
                GL.MakeCurrent();

                OnUpdate(GL);
                OnRender(GL);
                
                GL.Blit(IntPtr.Zero);

                var provider = GL.RenderContextProvider as FBORenderContextProvider;
                if (provider == null) return;
                var newFormatedBitmapSource = new FormatConvertedBitmap();
                newFormatedBitmapSource.BeginInit();

                newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.InternalDIBSection.HBitmap);
                newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
                newFormatedBitmapSource.EndInit();

                image.Source = newFormatedBitmapSource;

                _sw.Stop();
                FPS = 1000.0 / _sw.Elapsed.TotalMilliseconds;    
            }
        }
    }
}
