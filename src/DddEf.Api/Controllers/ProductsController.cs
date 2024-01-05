using DddEf.Application.UseCases.Products.Commands;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DddEf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ProductId>> Create(CreateProductCommand request)
        {  
            return await _sender.Send(request);
        }
    }
}
