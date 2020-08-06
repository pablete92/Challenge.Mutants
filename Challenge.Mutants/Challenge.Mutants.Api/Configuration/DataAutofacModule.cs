using Autofac;
using Challenge.Mutants.Domain.Contexts;
using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Data;
using Challenge.Mutants.Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Mutants.Api.Configuration
{
    public class DataAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChallengeDbContext>()
                .As(typeof(DbContext))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(UnitOfWork<>))
                .As(typeof(IUnitOfWork<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<StoredProcedureRepository<ChallengeDbContext>>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<QueryManager>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomLogger>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<,>))
                .As(typeof(IRepositoryQuery<,>))
                .WithParameter("traking", false);

            builder.RegisterGeneric(typeof(Repository<,>))
                .As(typeof(IRepositoryCommand<,>))
                .WithParameter("traking", false);
        }
    }
}
