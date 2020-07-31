using Challenge.Mutants.Application.Models.Request;
using Challenge.Mutants.Domain.Contexts;
using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Data.Contracts;
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

        public SaveADNHandler(ICustomLogger logger,
            IRepositoryCommand<ChallengeDbContext, ADNEntity> repositoryCommandADN,
            IUnitOfWork<ChallengeDbContext> unitOfWork)
        {
            this.logger = logger;
            this.repositoryCommandADN = repositoryCommandADN;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(SaveADNRequest request, CancellationToken cancellationToken)
        {
            //logger.Information($"Iniciado: SaveADNHandler.Hanlder(adn: {request.Model.Adn}, mutant: {request.Model.Mutant}");

            repositoryCommandADN.Create(new ADNEntity
            {
                Adn = request.Model.Adn,
                Mutant = request.Model.Mutant
            });

            await unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
