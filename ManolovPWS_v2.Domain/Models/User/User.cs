using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.User.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User
{
    public sealed class User : IEntity<UserId<Guid>>
    {
        public UserId<Guid>? Id { get; private set; }
        public UserName UserName { get; }
        public Name Name { get; }
        public Email Email { get; }
        public ProfilePicture? ProfilePicture { get; }
        public BirthDate BirthDate { get; }


        private User(
            UserName userName,
            Name name,
            Email email,
            BirthDate birthDate,
            ProfilePicture? profilePicture = default,
            UserId<Guid>? id = default)
        {
            Id = id;
            UserName = userName;
            Name = name;
            Email = email;
            ProfilePicture = profilePicture;
            BirthDate = birthDate;
        }

        public static User Create(
            UserName userName,
            Name name,
            Email email,
            BirthDate birthDate,
            ProfilePicture? profilePicture = default
            ) 
            => new(userName, name, email, birthDate, profilePicture);


        // Property-like methods for immutability
        internal User WithId(UserId<Guid> id) 
            => new(UserName, Name, Email, BirthDate, ProfilePicture, id);

        public User UpdateUserName(UserName userName)
            => new(userName, Name, Email, BirthDate, ProfilePicture, Id);

        public User UpdateName(Name name) 
            => new(UserName, name, Email, BirthDate, ProfilePicture, Id);

        public User UpdateEmail(Email email)
            => new(UserName, Name, email, BirthDate, ProfilePicture, Id);


    }
}
