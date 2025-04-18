using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.Queries;
using OrderService.Domain.Enums;

namespace OrderService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { orderId = id, status = "Pending" });
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid customerId)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerQuery(customerId));
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatus newStatus)
        {
            var command = new UpdateOrderStatusCommand { OrderId = id, NewStatus = newStatus };
            var result = await _mediator.Send(command);

            if (result)
                return Ok(new { status = "Success" });

            return BadRequest();
        }
    }
}
