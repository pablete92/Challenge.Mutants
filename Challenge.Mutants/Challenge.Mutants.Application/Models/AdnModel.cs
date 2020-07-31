using Newtonsoft.Json;

namespace Challenge.Mutants.Application.Models
{
    public class AdnModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("adn")]
        public string Adn { get; set; }

        [JsonProperty("mutant")]
        public bool Mutant { get; set; }
    }
}
