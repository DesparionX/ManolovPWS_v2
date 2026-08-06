namespace ManolovPWS_v2.Api.Extensions
{
    public static class UrlsConfigurations
    {
        public static WebApplicationBuilder ConfigUrls(this WebApplicationBuilder builder)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

            return builder;
        }
    }
}
