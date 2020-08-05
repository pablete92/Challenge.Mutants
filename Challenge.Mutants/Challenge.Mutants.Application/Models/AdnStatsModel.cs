using Newtonsoft.Json;

namespace Challenge.Mutants.Application.Models
{
    public class AdnStatsModel
    {
        [JsonProperty("count_mutant_dna")]
        public int CountMutantDna { get; set; }

        [JsonProperty("count_human_dna")]
        public int CountHumanDna { get; set; }

        [JsonProperty("ratio")]
        public double Ratio { get; set; }
    }
}
