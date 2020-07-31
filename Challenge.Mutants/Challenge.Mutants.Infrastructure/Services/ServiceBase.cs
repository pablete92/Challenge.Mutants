using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Data;
using Challenge.Mutants.Infrastructure.Data.Contracts;
using Challenge.Mutants.Infrastructure.Extensions;
using Challenge.Mutants.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Services
{
    public abstract class ServiceBase
    {
        public ICustomLogger Logger;

        protected ServiceBase(ICustomLogger logger)
        {
            Logger = logger;
        }
    }

    public abstract class ServiceBase<TOptions>
        where TOptions : HttpOptionsBase, new()
    {
        public ICustomLogger Logger;
        public IHttpClientFactory ClientFactory { get; }
        public TOptions Options { get; }

        protected ServiceBase(ICustomLogger logger, IHttpClientFactory clientFactory, IOptions<TOptions> options)
        {
            Logger = logger;
            ClientFactory = clientFactory;
            Options = options.Value;
        }

        public async Task<TResponse> Get<TResponse>(string url)
        {
            using HttpClient client = ClientFactory.CreateClient(Options.HttpClienteName);

            using HttpResponseMessage responseMessage = await client.GetAsync(url);

            var response = await responseMessage.GetContentWithStatusCodeValidated();

            return JsonConvert.DeserializeObject<TResponse>(response);
        }

        public async Task Post(string url, DataModelBase body = null)
        {
            using HttpClient client = ClientFactory.CreateClient(Options.HttpClienteName);

            var jsonBody = body?.Serialize();

            using HttpResponseMessage responseMessage = await client.PostAsync(url, jsonBody);

            await responseMessage.GetContentWithStatusCodeValidated();
        }

        public async Task<TResponse> Post<TResponse>(string url, DataModelBase body = null)
        {
            using HttpClient client = ClientFactory.CreateClient(Options.HttpClienteName);

            var jsonBody = body?.Serialize();

            using HttpResponseMessage responseMessage = await client.PostAsync(url, jsonBody);

            var response = await responseMessage.GetContentWithStatusCodeValidated();

            return JsonConvert.DeserializeObject<TResponse>(response);
        }
    }

    public abstract class ServiceBase<TContext, TEntity>
        where TContext : DbContext
        where TEntity : EntityBase
    {
        public ICustomLogger Logger { get; }
        public IRepositoryQuery<TContext, TEntity> RepositoryQuery { get; }
        public IRepositoryCommand<TContext, TEntity> RepositoryCommand { get; }
        public IUnitOfWork<TContext> UnitOfWork { get; }

        protected ServiceBase(ICustomLogger logger, IRepositoryQuery<TContext, TEntity> repositoryQuery, IUnitOfWork<TContext> unitOfWork)
        {
            Logger = logger;
            RepositoryQuery = repositoryQuery;
            UnitOfWork = unitOfWork;
        }

        protected ServiceBase(ICustomLogger logger, IRepositoryCommand<TContext, TEntity> repositoryCommand, IUnitOfWork<TContext> unitOfWork)
        {
            Logger = logger;
            RepositoryCommand = repositoryCommand;
            UnitOfWork = unitOfWork;
        }
    }
}