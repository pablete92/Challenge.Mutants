using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Challenge.Mutants.Infrastructure.Models
{
    public class ErrorModel : DataModelBase
    {
        public int Code { get; set; }

        public string Message { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Detail { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Module { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<ValidationError> ValidationError { get; set; }
    }
}
