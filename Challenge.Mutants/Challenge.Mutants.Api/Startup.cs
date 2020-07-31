using Autofac;
using Autofac.Features.Variance;
using Challenge.Mutants.Api.Configuration;
using Challenge.Mutants.Application;
using Challenge.Mutants.Domain;
using Challenge.Mutants.Domain.Contexts;
using Challenge.Mutants.Infrastructure;
using Challenge.Mutants.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;

namespace Challenge.Mutants.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChallengeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ChallengeDbContext")));

            services.AddMvc(q => q.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();

            MediatorRegister(services);

            ConfigureSwagger(services);
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterModule(new DataAutofacModule());
            builder.RegisterModule(new MediatrAutofacModule());
            builder.RegisterModule(new ServicesAutofacModule());
            builder.RegisterModule(new MappersAutofacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseErrorHandlingMiddleware();

            app.UseSwagger();

            app.UseSwaggerUI(q => q.SwaggerEndpoint(Configuration.GetSection("SwaggerConfiguration:EndpointUrl").Value, Configuration.GetSection("SwaggerConfiguration:EndpointDescription").Value));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void MediatorRegister(IServiceCollection services)
        {
            Assembly[] applicationAssembliesMediator = new[] { typeof(DummyApplication), typeof(DummyInfrastructure) }.Select(x => x.Assembly).ToArray();
            Assembly[] domainAssembliesMediator = new[] { typeof(DummyInfrastructure), typeof(DummyDomain) }.Select(x => x.Assembly).ToArray();

            services.AddMediatR(applicationAssembliesMediator);
            services.AddMediatR(domainAssembliesMediator);
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = Configuration.GetSection("SwaggerConfiguration:ContactName").Value,
                Email = Configuration.GetSection("SwaggerConfiguration:ContactMail").Value
            };

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(Configuration.GetSection("SwaggerConfiguration:DocNameV1").Value,
                    new OpenApiInfo
                    {
                        Title = Configuration.GetSection("SwaggerConfiguration:DocInfoTitle").Value,
                        Version = Configuration.GetSection("SwaggerConfiguration:DocInfoVersion").Value,
                        Description = Configuration.GetSection("SwaggerConfiguration:DocInfoDescription").Value,
                        Contact = contact
                    }
                );
            });
        }
    }
}
