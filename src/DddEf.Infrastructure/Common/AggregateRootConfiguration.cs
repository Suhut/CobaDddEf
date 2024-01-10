using DddEf.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Common;

public class AggregateRootConfiguration<T> : IEntityTypeConfiguration<T> where T : AggregateRoot
{ 
    public virtual void Configure(EntityTypeBuilder<T> builder)
    { 
        builder.Property("CreatedDateOffset");
        builder.Property("ModifiedDateOffset");
        builder.Property("VersionId");

    } 
}

