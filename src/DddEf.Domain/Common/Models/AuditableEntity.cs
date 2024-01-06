using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DddEf.Domain.Common.Models;

public class AuditableEntity
{ 
    public DateTime? CreatedDate { get; set; } 
    public DateTime? ModifiedDate { get; set; }
    public DateTimeOffset? CreatedDateOffset { get; set; }
    public DateTimeOffset? ModifiedDateOffset { get; set; }

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