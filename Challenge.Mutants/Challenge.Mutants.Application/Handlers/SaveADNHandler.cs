using Challenge.Mutants.Application.Mappers;
using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Application.Models.Request;
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
        private readonly ADNPostMapper aDNPostMapper;

        public SaveADNHandler(ICustomLogger logger,
            IRepositoryCommand<ChallengeDbContext, ADNEntity> repositoryCommandADN,
            IUnitOfWork<ChallengeDbContext> unitOfWork,
            ADNPostMapper aDNPostMapper)
        {
            this.logger = logger;
            this.repositoryCommandADN = repositoryCommandADN;
            this.unitOfWork = unitOfWork;
            this.aDNPostMapper = aDNPostMapper;
        }

        public async Task<Unit> Handle(SaveADNRequest request, CancellationToken cancellationToken)
        {
            logger.Information($"Iniciado: SaveADNHandler.Hanlder(adn: {request.Model.Dna}");

            AdnModel model = aDNPostMapper.MapEntityToModel(request.Model);

            repositoryCommandADN.Create(new ADNEntity
            {
                Adn = model.Adn,
                Mutant = model.Mutant
            });

            await unitOfWork.SaveChangesAsync();

            if (!model.Mutant)
            {
                throw new ForbiddenProjectException();
            }

            return Unit.Value;
        }
    }
}
