using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Modules.Identity.User.Features.FindById
{
    public sealed record FindUserByIdQuery(UserId id) : IQuery<UserDto>;
}
