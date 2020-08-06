using Challenge.Mutants.Infrastructure.Bootstrapers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Mutants.Infrastructure.Extensions
{
    public static class ModelStateExtension
    {
        public static IList<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            var result = new List<ValidationError>();

            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;

                var fieldErrors = erroneousField.Errors
                                   .Select(error => new ValidationError(fieldKey, error.ErrorMessage));

                result.AddRange(fieldErrors);
            }

            return result;
        }
    }
}
