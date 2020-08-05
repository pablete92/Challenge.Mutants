using Challenge.Mutants.Application.Mappers;
using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Domain.Contexts;
using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Data.Contracts;
using Challenge.Mutants.Infrastructure.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Mutants.Application.Handlers
{
    public class GetStatsADNRequest : IRequest<AdnStatsModel>
    {
        public GetStatsADNRequest() { }
    }

    public class GetStatsADNHandler : IRequestHandler<GetStatsADNRequest, AdnStatsModel>
    {
        private readonly ICustomLogger logger;
        private readonly IRepositoryQuery<ChallengeDbContext, ADNEntity> repositoryQueryADN;
        private readonly ADNStatsMapper aDNStatsMapper;

        public GetStatsADNHandler(ICustomLogger logger,
            IRepositoryQuery<ChallengeDbContext, ADNEntity> repositoryQueryADN,
            ADNStatsMapper aDNStatsMapper)
        {
            this.logger = logger;
            this.repositoryQueryADN = repositoryQueryADN;
            this.aDNStatsMapper = aDNStatsMapper;
        }

        public async Task<AdnStatsModel> Handle(GetStatsADNRequest request, CancellationToken cancellationToken)
        {
            logger.Information("Iniciado: GetStatsADNHandler.Hanlder");

            IEnumerable<ADNEntity> result = await repositoryQueryADN.GetAllAsync();

            if (!HayMuestras(result))
            {
                logger.Information("Terminado: Sin resultados.");
                throw new NotFoundProjectException("Terminado: Sin resultados. GetStatsADNHandler");
            }

            return aDNStatsMapper.MapEntityToModel(result);
        }

        private bool HayMuestras(IEnumerable<ADNEntity> models)
       => models.Count() > 0;
    }
}
