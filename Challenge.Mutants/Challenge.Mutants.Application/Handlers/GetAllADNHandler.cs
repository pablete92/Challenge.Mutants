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
    public class GetAllADNRequest : IRequest<IEnumerable<AdnModel>>
    {
        public GetAllADNRequest() { }
    }


    public class GetAllADNHandler : IRequestHandler<GetAllADNRequest, IEnumerable<AdnModel>>
    {
        private readonly ICustomLogger logger;
        private readonly IRepositoryQuery<ChallengeDbContext, ADNEntity> repositoryQueryADN;
        private readonly ADNMapper adnMapper;

        public GetAllADNHandler(ICustomLogger logger, IRepositoryQuery<ChallengeDbContext, 
            ADNEntity> repositoryQueryADN,
            ADNMapper adnMapper)
        {
            this.logger = logger;
            this.repositoryQueryADN = repositoryQueryADN;
            this.adnMapper = adnMapper;
        }

        public async Task<IEnumerable<AdnModel>> Handle(GetAllADNRequest request, CancellationToken cancellationToken)
        {
            logger.Information("Iniciado: GetAllADNHandler.Hanlder");

            IEnumerable<ADNEntity> result = await repositoryQueryADN.GetAllAsync();

            if (!HayMuestras(result))
            {
                logger.Information("Terminado: Sin resultados.");
                throw new NotFoundProjectException("Terminado: Sin resultados. GetAllADNHandler");
            }

            return adnMapper.MapEntityToModelCollection(result);
        }
        private bool HayMuestras(IEnumerable<ADNEntity> models)
        => models.Count() > 0;
    }
}
