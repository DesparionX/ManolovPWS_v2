namespace ManolovPWS_v2.Shared.Abstractions.Identity
{
    public interface ICurrentUser<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        public string UserName { get; }
        public string Email { get; }
        public bool IsAuthenticated { get; }
        public bool IsInRole(string role);
        public bool HasPermission(string permission);
        public IReadOnlyCollection<string> GetRoles();
        public IReadOnlyCollection<string> GetPermissions();
        
    }
}
