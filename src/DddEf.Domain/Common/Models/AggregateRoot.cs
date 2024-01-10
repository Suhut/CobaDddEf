using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DddEf.Domain.Common.Models;
public abstract class AggregateRoot
{
#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning disable CS8618
    
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