using DddEf.Application.UseCases.Items.Commands;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DddEf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ISender _sender;

        public ItemsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ItemId>> Add(AddItemCommand request)
        {  
            return await _sender.Send(request);
        }
    }
}
