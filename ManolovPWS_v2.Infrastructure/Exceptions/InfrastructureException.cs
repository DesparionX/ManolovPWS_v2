namespace ManolovPWS_v2.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        public string Code { get; }

        public InfrastructureException(string message, string code) : base(message)
        {
            Code = code;
        }

        public InfrastructureException(string message, string code, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}
