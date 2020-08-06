using Autofac;
using MediatR;

namespace Challenge.Mutants.Api.Configuration
{
    public class MediatrAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ServiceFactory>(ctx =>
            {
                IComponentContext c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
