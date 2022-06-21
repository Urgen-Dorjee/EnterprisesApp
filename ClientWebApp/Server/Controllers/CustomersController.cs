using ClientWebApp.Server.Customers.Command;
using ClientWebApp.Server.Customers.Queries.CustomerDetail;
using ClientWebApp.Server.Customers.Queries.CustomersList;
using ClientWebApp.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// { Display List of Customers Records }
        /// </summary>
        /// <param name="customers"></param>
        /// <returns> Returns All the Customer Records From the Database</returns>
        [HttpGet("/customers", Name = nameof(GetAllCustomers))]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers([FromQuery] CustomerListQuery customers)
        {
            return await _mediator.Send(customers);
        }

        /// <summary>
        /// { Populates Customer Record by CustomerID }
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>This method returns only Customer Record with CustomerID</returns>
        [HttpGet("/customers/{customerId}", Name = nameof(GetCustomer))]
        public async Task<ActionResult<Customer?>> GetCustomer(string? customerId)
        {
            if (customerId is null) return BadRequest();
            return await _mediator.Send(new CustomerDetailQuery { CustomerId = customerId });
        }

        /// <summary>
        /// { Add a new Customer to the database }
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns newly created customer data</returns>
        [HttpPost("/customers", Name = nameof(CreateCustomer))]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody]CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _mediator.Send(command, cancellationToken);
            return Ok(customer);
        }
    }
}
