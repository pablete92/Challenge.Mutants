using Challenge.Mutants.Infrastructure.Data;
using Newtonsoft.Json;

namespace Challenge.Mutants.Domain.Entities
{
    public class ADNEntity : EntityBase
    {
        [JsonProperty("adn")]
        public string adn { get; set; }

        [JsonProperty("mutant")]
        public bool mutant { get; set; }
    }
}
