namespace ManolovPWS_v2.Shared.Abstractions.Errors
{
    public interface IError
    {
        string Message { get; }
        string Code { get; }
    }
}
