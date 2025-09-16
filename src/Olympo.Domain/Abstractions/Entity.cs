using System.Diagnostics.CodeAnalysis;

namespace Olympo.Domain.Abstractions;

[ExcludeFromCodeCoverage]
public abstract class Entity
{
    public Guid Id { get; protected init; }

    protected Entity(Guid id)
    {
        Id = id;
    }
    
    protected Entity()
    {
    }
}