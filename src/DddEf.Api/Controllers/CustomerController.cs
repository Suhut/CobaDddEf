using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Customers.Queries;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DddEf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ISender _sender;

        public CustomersController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRes>> GetById(CustomerId id)
        {
            var cmd = new GetCustomerByIdQuery { Id = id };
            return await _sender.Send(cmd);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerId>> Add(AddCustomerCommand request)
        {  
            return await _sender.Send(request);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerId>> Update(UpdateCustomerCommand request)
        {
            return await _sender.Send(request);
        }
    }
}
