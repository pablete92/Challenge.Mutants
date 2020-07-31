using Challenge.Mutants.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Challenge.Mutants.Infrastructure.Exceptions
{
    public class ConflictProjectException : ProjectException
    {
        public ConflictProjectException()
        {
        }

        public ConflictProjectException(string message)
            : base(message)
        {
        }

        public ConflictProjectException(ModelStateDictionary modelState)
            => ValidationError = modelState.AllErrors();

        public ConflictProjectException(string message, ModelStateDictionary modelState)
            : base(message) => ValidationError = modelState.AllErrors();

        public ConflictProjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
