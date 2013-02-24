using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI
{
    public class MvvmBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterType<EventAggregator>().AsSelf().SingleInstance();
        }
    }
}