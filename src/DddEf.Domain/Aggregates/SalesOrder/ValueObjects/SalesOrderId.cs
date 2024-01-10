using DddEf.Domain.Common.Models;
using StronglyTypedIds;

namespace DddEf.Domain.Aggregates.SalesOrder.ValueObjects; 

[StronglyTypedId(Template.Guid)]
public partial struct SalesOrderId { }