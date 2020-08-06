using System;

namespace Challenge.Mutants.Infrastructure.Exceptions
{
    public class UnauthorizedAccessProyectException : ProjectException
    {
        public UnauthorizedAccessProyectException()
        {
        }

        public UnauthorizedAccessProyectException(string message) : base(message)
        {
        }

        public UnauthorizedAccessProyectException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
