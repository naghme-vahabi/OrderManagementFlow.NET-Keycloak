using CustomerService.Application.Commands;
using CustomerService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { customerId = id });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _mediator.Send(new GetCustomersQuery());
            return Ok(customers);
        }
    }
}
