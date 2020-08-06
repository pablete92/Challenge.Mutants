using Newtonsoft.Json;

namespace Challenge.Mutants.Infrastructure.Bootstrapers
{
    public class ValidationError
    {
        public ValidationError(string field, string message)
        {
            Field = string.IsNullOrWhiteSpace(field) ? null : field;

            Message = message;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }
    }
}
