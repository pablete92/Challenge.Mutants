using Challenge.Mutants.Infrastructure.Data;
using Newtonsoft.Json;

namespace Challenge.Mutants.Domain.Entities
{
    public class ADNEntity : EntityBase
    {
        [JsonProperty("adn")]
        public string Adn { get; set; }

        [JsonProperty("mutant")]
        public bool Mutant { get; set; }
    }
}
