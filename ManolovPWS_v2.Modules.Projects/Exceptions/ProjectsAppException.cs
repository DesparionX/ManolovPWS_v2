namespace ManolovPWS_v2.Modules.Projects.Exceptions
{
    public sealed class ProjectsAppException(string message, string code) : Exception($"{message}, {code}")
    {
    }
}
