using Autofac;
using Challenge.Mutants.Application;
using Challenge.Mutants.Infrastructure;
using Challenge.Mutants.Infrastructure.Models;
using MediatR;
using System.Linq;
using System.Reflection;

namespace Challenge.Mutants.Api.Configuration
{
    public class MappersAutofacModule : Autofac.Module
    {
        private static Assembly[] ApplicationAssemblies
            => new[] { typeof(DummyApplication), typeof(DummyInfrastructure) }.Select(x => x.Assembly).ToArray();

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(ApplicationAssemblies)
                .AsClosedTypesOf(typeof(MapperBase<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
