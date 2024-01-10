//using DddEf.Application.Common.Interfaces;
//using DddEf.Domain.Aggregates.SalesOrder;
//using MediatR;

//namespace DddEf.Application.UseCases.SalesOrders.Queries;

//public class GetSalesOrderByIdQuery: IRequest<SalesOrder>
//{
//    public Guid Id { get; set; }
//}
//public class GetSalesOrderByIdQueryHandler(IDddEfContext applicationDbContext) : IRequestHandler<GetSalesOrderByIdQuery, SalesOrder>
//{  
//    public async Task<SalesOrder> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
//    {

//        var result = await applicationDbContext.SalesOrders.FindAsync(new object[] { request.Id }, cancellationToken);

//        return result;
//    }
//}
