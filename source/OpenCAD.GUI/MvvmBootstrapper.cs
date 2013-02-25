using System.Reflection;
using Autofac;
using Caliburn.Micro.Autofac;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI
{
    public class MvvmBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        protected override void ConfigureBootstrapper() {
            base.ConfigureBootstrapper();
            AutoSubscribeEventAggegatorHandlers = true;
        }

        protected override void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            base.OnUnhandledException(sender, e);
        }
    }
}