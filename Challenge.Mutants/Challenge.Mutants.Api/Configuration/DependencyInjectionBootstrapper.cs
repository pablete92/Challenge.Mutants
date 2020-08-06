using Autofac;
using Autofac.Features.Variance;
using System;

namespace Challenge.Mutants.Api.Configuration
{
    public class DependencyInjectionBootstrapper
    {
        private static IContainer container;

        public static IContainer GetContainer()
        {
            if (container == null)
            {
                throw new Exception("InitializeContainer was not called");
            }

            return container;
        }

        public static void InitializeContainer(Action<ContainerBuilder> configurator = null)
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterModule(new DataAutofacModule());
            builder.RegisterModule(new MediatrAutofacModule());
            builder.RegisterModule(new ServicesAutofacModule());
            builder.RegisterModule(new MappersAutofacModule());

            configurator?.Invoke(builder);

            container = builder.Build();
        }
    }
}
