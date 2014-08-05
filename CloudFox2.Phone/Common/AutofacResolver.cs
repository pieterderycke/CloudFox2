using Autofac;
using CloudFox2.Phone.ViewModels;
using FxSyncNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFox2.Phone.Common
{
    public static class AutofacResolver
    {
        private static IContainer container;

        static AutofacResolver()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<ChooseLoginViewModel>();
            builder.RegisterType<LoginViewModel>();

            builder.RegisterType<SettingManager>().InstancePerLifetimeScope();
            builder.RegisterType<NavigationService>().As<INavigationService>();

            builder.RegisterType<SyncClient>().InstancePerLifetimeScope();

            container = builder.Build();
        }

        public static TService Resolve<TService>()
        {
            return container.Resolve<TService>();
        }
    }
}
