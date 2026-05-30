namespace ManolovPWS_v2.Api.Contracts.Identity
{
    public sealed record SignInRequest(string UserNameOrEmail, string Password);
}
