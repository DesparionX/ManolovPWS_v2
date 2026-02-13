using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Contacts
{
    public sealed class Contacts : IEquatable<Contacts>
    {
        private readonly List<Contact> _contacts;

        public IReadOnlyList<Contact> ContactList => _contacts;

        [JsonConstructor]
        private Contacts(IEnumerable<Contact> contacts)
        {
            _contacts = [.. contacts];
        }

        public static Contacts Empty() => new([]);

        public static Contacts Create(IEnumerable<Contact> contacts)
            => new(contacts);

        public static Contacts? From(IEnumerable<Contact>? contacts)
            => contacts is null || !contacts.Any() ? null : new(contacts);

        // Contacts manipulations
        internal Contacts Clear() => Empty();

        internal Contacts Add(Contact contact)
            => new(_contacts.Append(contact));

        internal Contacts Update(Contact oldContact, Contact newContact)
            => new(_contacts.Select(c => c.Equals(oldContact) ? newContact : c));

        internal Contacts Remove(Contact contact)
            => new(_contacts.Where(c => !c.Equals(contact)));

        // Equality
        public bool Equals(Contacts? other) =>
            other is not null
            && _contacts.Count == other._contacts.Count
            && _contacts.OrderBy(c => c.Network).SequenceEqual(other._contacts.OrderBy(c => c.Network));

        public override bool Equals(object? obj) => Equals(obj as Contacts);

        public override int GetHashCode() =>
            HashCode.Combine(_contacts.Select(c => c.GetHashCode()));
    }
}
