using Challenge.Mutants.Infrastructure.Exceptions;
using Challenge.Mutants.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Extensions
{
    public static class ResolveResponseExtension
    {
        public static async Task<string> GetContentWithStatusCodeValidated(this HttpResponseMessage httpResponseMessage, bool throwable = true)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return content;
            }

            ErrorModel error = JsonConvert.DeserializeObject<ErrorModel>(content);

            if (!throwable)
            {
                return JsonConvert.SerializeObject(error);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new TimeoutProjectException(error.Message);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundProjectException(error.Message);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ForbiddenProjectException(error.Message);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ConflictProjectException(error.Message);
            }

            throw new BadRequestProjectException(content);
        }
    }
}
