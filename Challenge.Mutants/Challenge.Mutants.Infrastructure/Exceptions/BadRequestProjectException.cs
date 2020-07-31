using Challenge.Mutants.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Challenge.Mutants.Infrastructure.Exceptions
{
    public class BadRequestProjectException : ProjectException
    {
        public BadRequestProjectException()
        {
        }

        public BadRequestProjectException(string message)
            : base(message)
        {
        }

        public BadRequestProjectException(ModelStateDictionary modelState)
            => ValidationError = modelState.AllErrors();

        public BadRequestProjectException(string message, ModelStateDictionary modelState)
            : base(message) => ValidationError = modelState.AllErrors();

        public BadRequestProjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
