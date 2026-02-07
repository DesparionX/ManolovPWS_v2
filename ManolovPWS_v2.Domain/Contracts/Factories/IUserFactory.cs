using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Contracts.Factories
{
    public interface IUserFactory : IFactory<User, UserId>
    {

    }
}
