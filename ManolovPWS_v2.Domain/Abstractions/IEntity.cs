using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Abstractions
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey? Id { get; }
    }
}
