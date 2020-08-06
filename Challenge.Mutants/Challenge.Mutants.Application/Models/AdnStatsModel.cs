using Newtonsoft.Json;

namespace Challenge.Mutants.Application.Models
{
    public class AdnStatsModel
    {
        [JsonProperty("count_mutant_dna")]
        public int count_mutant_dna { get; set; }

        [JsonProperty("count_human_dna")]
        public int count_human_dna { get; set; }

        [JsonProperty("ratio")]
        public double ratio { get; set; }
    }
}
