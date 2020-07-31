using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Challenge.Mutants.Infrastructure.Exceptions
{
    public class ProjectException : ApplicationException
    {
        public ProjectException()
        {
        }

        public ProjectException(int internalCode)
        {
            InternalCode = internalCode;
        }

        public ProjectException(int internalCode, string message) : base(message)
        {
            InternalCode = internalCode;
        }

        public ProjectException(string message) : base(message)
        {
        }

        public ProjectException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ErrorModel ErrorModel { get; set; }

        public bool WithLogError { get; set; } = false;

        public IList<ValidationError> ValidationError { get; protected set; }

        public int InternalCode { get; }

        public string Module { get; set; }

        public string Detail { get; set; }
    }
}
