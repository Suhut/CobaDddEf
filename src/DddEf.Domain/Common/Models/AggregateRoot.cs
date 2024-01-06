using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DddEf.Domain.Common.Models;
public abstract class AggregateRoot<TID> : Entity<TID> where TID : notnull
{
#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning disable CS8618
    public AggregateRoot(TID id) : base(id)
    {
    } 

    private DateTimeOffset? CreatedDateOffset { get; set; }
    private DateTimeOffset? ModifiedDateOffset { get; set; }

    public void SetCreatedDateOffset(DateTimeOffset dt)
    {
        CreatedDateOffset = dt;
    }

    public void SetModifiedDateOffset(DateTimeOffset dt)
    {
        ModifiedDateOffset = dt;

    }

    [ConcurrencyCheck]
    [Column("VersionId")]
#pragma warning disable IDE1006 // Naming Styles
    public int _versionId { get; private set; }
#pragma warning restore IDE1006 // Naming Styles
    public void IncreaseVersion()
    {
        _versionId++;
    }

}