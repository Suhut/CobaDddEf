using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DddEf.Domain.Common.Models;

public abstract class AuditableEntity
{
    private DateTimeOffset? CreatedDateOffset { get; set; }
    private DateTimeOffset? ModifiedDateOffset { get; set; }

    [ConcurrencyCheck]
    [Column("VersionId")]
#pragma warning disable IDE1006 // Naming Styles
    public int _versionId { get; private set; }
#pragma warning restore IDE1006 // Naming Styles
    public void IncreaseVersion()
    {
        _versionId++;
    }
    public void SetCreatedDateOffset(DateTimeOffset dt)
    {
        CreatedDateOffset = dt;
    }

    public void SetModifiedDateOffset(DateTimeOffset dt)
    {
        ModifiedDateOffset = dt;
    }


}