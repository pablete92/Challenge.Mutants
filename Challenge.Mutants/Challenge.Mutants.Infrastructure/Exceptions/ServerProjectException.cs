namespace Challenge.Mutants.Infrastructure.Exceptions
{
    public class ServerProjectException : ProjectException
    {
        public ServerProjectException() { }

        public ServerProjectException(int internalCode)
            : base(internalCode) { }

        public ServerProjectException(int internalCode, string message)
            : base(internalCode, message) { }

    }
}
