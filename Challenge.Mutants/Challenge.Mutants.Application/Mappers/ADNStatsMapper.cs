using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Mutants.Application.Mappers
{
    public class ADNStatsMapper : MapperBase<AdnStatsModel, IEnumerable<ADNEntity>>
    {
        public AdnStatsModel MapEntityToModel(IEnumerable<ADNEntity> models)
        {
            var newModel = models.GroupBy(q => q.mutant).Select(x => new { value = x.Key, dnaMutant = x.Count() });

            int mutant= newModel.Where(x => x.value).Select(q => q.dnaMutant).FirstOrDefault();

            int human = newModel.Where(x => !x.value).Select(q => q.dnaMutant).FirstOrDefault();

            return new AdnStatsModel
            {
                count_human_dna= human,
                count_mutant_dna = mutant,
                ratio = (double)mutant / (double)human
            };
        }
    }
}
