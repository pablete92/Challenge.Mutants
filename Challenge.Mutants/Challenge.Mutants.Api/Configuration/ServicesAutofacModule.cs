using Autofac;
using Challenge.Mutants.Application.Mappers;
using Challenge.Mutants.Infrastructure.Services;

namespace Challenge.Mutants.Api.Configuration
{
    public class ServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Mappers

            builder.RegisterType<ADNMapper>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            #endregion

            builder.RegisterType<AppUserService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
