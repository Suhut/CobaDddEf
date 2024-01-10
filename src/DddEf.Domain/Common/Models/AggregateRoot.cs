using System.ComponentModel.DataAnnotations;

namespace DddEf.Domain.Common.Models;
public abstract class AggregateRoot
{  
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
    private int VersionId { get; set; } 
    public void IncreaseVersion()
    {
        VersionId++;
    }

}