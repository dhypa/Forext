using NodaTime;

namespace Forext.CcyProvider.Domain;

public interface IAuditableEntity
{
    public Instant CreatedAt { get; set; } 
    public Instant UpdatedAt { get; set; }
}
