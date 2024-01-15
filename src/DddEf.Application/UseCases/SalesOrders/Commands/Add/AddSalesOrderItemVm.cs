namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;

public record AddSalesOrderItemVm(
    Guid ItemId,
    double Qty,
    double Price
);
