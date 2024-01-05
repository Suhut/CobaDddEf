using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Application.UseCases.SalesOrders.Commands.Cancel;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DddEf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public SalesOrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SalesOrderId>> Add(AddSalesOrderCommand request)
        {  
            return await _sender.Send(request);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Cancel(CancelSalesOrderCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

    }
}
