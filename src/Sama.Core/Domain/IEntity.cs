using System;

namespace Sama.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}