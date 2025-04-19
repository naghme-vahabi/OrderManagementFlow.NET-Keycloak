using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Commands;

namespace PaymentService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Pay([FromBody] ProcessPaymentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new { Status = result ? "Success" : "Failed" });
        }
    }
}
