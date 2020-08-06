using Challenge.Mutants.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Challenge.Mutants.Infrastructure.Exceptions
{
    public class NoContentProjectException : ProjectException
    {
        public NoContentProjectException()
        {
        }

        public NoContentProjectException(string message)
            : base(message)
        {
        }

        public NoContentProjectException(ModelStateDictionary modelState)
            => ValidationError = modelState.AllErrors();

        public NoContentProjectException(string message, ModelStateDictionary modelState)
            : base(message) => ValidationError = modelState.AllErrors();

        public NoContentProjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
