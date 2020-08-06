namespace Challenge.Mutants.Infrastructure.Models
{
    public abstract class HttpOptionsBase
    {
        public virtual string HttpClienteName { get; set; }
        public virtual string UrlBase { get; set; }

        public HttpOptionsBase() { }
    }
}
