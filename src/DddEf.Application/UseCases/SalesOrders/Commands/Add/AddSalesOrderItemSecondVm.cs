namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;

public record AddSalesOrderItemSecondVm(
    Guid ItemId,
    double Qty,
    double Price,
    List<AddSalesOrderItemSecondBinVm> Bins
);
