using System.Collections.Generic;

namespace Sama.Core.Domain
{
    public interface IAggregateRoot : IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}