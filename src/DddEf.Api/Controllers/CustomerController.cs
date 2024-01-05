﻿using DddEf.Application.UseCases.Customers.Commands;
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

        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerId>> Create(CreateCustomerCommand request)
        {  
            return await _sender.Send(request);
        }
    }
}