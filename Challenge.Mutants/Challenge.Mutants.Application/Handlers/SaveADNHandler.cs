using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Application.Models.Request;
using Challenge.Mutants.Application.Services;
using Challenge.Mutants.Domain.Contexts;
using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Data.Contracts;
using Challenge.Mutants.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Mutants.Application.Handlers
{
    public class SaveADNRequest : IRequest<Unit>
    {
        public SaveADNRequest(SaveADNModel model)
        {
            this.Model = model;
        }

        public SaveADNModel Model { get; set; }
    }

    public class SaveADNHandler : IRequestHandler<SaveADNRequest, Unit>
    {
        private readonly ICustomLogger logger;
        private readonly IRepositoryCommand<ChallengeDbContext, ADNEntity> repositoryCommandADN;
        private readonly IUnitOfWork<ChallengeDbContext> unitOfWork;
        private readonly IAdnService adnService;

        public SaveADNHandler(ICustomLogger logger,
            IRepositoryCommand<ChallengeDbContext, ADNEntity> repositoryCommandADN,
            IUnitOfWork<ChallengeDbContext> unitOfWork,
            IAdnService adnService)
        {
            this.logger = logger;
            this.repositoryCommandADN = repositoryCommandADN;
            this.unitOfWork = unitOfWork;
            this.adnService = adnService;
        }

        public async Task<Unit> Handle(SaveADNRequest request, CancellationToken cancellationToken)
        {
            logger.Information($"Iniciado: SaveADNHandler.Hanlder(adn: {request.Model.Dna}");

            AdnModel model = adnService.IsMutant(request.Model);

            repositoryCommandADN.Create(new ADNEntity
            {
                adn = model.Adn,
                mutant = model.Mutant
            });

            await unitOfWork.SaveChangesAsync();

            if (!model.Mutant)
            {
                throw new ForbiddenProjectException("Es humano");
            }

            return Unit.Value;
        }
    }
}
