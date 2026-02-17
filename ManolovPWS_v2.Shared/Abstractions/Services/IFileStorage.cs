namespace ManolovPWS_v2.Shared.Abstractions.Services
{
    public interface IFileStorage
    {
        public Task<string> SaveFileAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default
            );
    }
}
