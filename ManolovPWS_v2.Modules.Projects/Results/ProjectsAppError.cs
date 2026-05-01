using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Projects.Results
{
    public sealed record ProjectsAppError(string Message, string Code) : IError;
}
