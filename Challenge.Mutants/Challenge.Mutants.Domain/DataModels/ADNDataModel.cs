using Challenge.Mutants.Infrastructure.Data;
using Newtonsoft.Json;

namespace Challenge.Mutants.Domain.DataModels
{
    public class ADNDataModel : DataModelBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("adn")]
        public string Adn { get; set; }

        [JsonProperty("mutant")]
        public bool Mutant { get; set; }
    }
}
