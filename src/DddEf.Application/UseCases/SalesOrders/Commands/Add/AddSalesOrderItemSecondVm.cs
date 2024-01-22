using DddEf.Domain.Aggregates.Item.ValueObjects;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;

public record AddSalesOrderItemSecondVm(
    ItemId ItemId,
    double Qty,
    double Price,
    List<AddSalesOrderItemSecondBinVm> Bins
);
