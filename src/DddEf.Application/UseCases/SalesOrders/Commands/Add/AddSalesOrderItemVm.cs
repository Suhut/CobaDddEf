using DddEf.Domain.Aggregates.Item.ValueObjects;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;

public record AddSalesOrderItemVm(
    ItemId ItemId,
    double Qty,
    double Price,
    List<AddSalesOrderItemBinVm> Bins
);
