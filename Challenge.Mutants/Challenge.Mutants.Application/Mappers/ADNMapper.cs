using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Application.Models.Request;
using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Mutants.Application.Mappers
{
    public class ADNMapper : MapperBase<AdnModel, SaveADNModel>
    {
        public AdnModel MapEntityToModel(ADNEntity q)
            => new AdnModel
            {
                Id = q.id,
                Adn = q.adn,
                Mutant = q.mutant
            };

        public IEnumerable<AdnModel> MapEntityToModelCollection(IEnumerable<ADNEntity> entities) =>
           entities.Select(x => MapEntityToModel(x));
    
    }
}
