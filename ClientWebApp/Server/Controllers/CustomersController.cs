namespace ClientWebApp.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CustomersController : BaseController
    {

        public CustomersController(IMediator mediator) : base(mediator)
        {}

        /// <summary>
        /// { Display List of Customers Records }
        /// </summary>
        /// <param name="customers"></param>
        /// <returns> Returns All the Customer Records From the Database</returns>
        [HttpGet("/customers", Name = nameof(GetAllCustomers))]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers([FromQuery] CustomerListQuery customers)
        {
            return await _Mediator.Send(customers);
        }

        /// <summary>
        /// { Populates Customer Record by CustomerID }
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>This method returns only Customer Record with CustomerID</returns>
        [HttpGet("/customers/{customerId}", Name = nameof(GetCustomer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer?>> GetCustomer(string? customerId)
        {
            if (customerId is null) return BadRequest();
            return await _Mediator.Send(new CustomerDetailQuery { CustomerId = customerId });
        }

        /// <summary>
        /// { Add a new Customer to the database }
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns newly created customer data</returns>
        [HttpPost("/customers", Name = nameof(CreateCustomer))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _Mediator.Send(command, cancellationToken);
            return Ok(customer);
        }
        /// <summary>
        /// { Update Customer existing record in the database }
        /// </summary>
        /// <param name="customerId">Requires Customer ID to update the record</param>
        /// <param name="command">It presents all the customer data to be updated</param>
        /// <returns>Returns newly updated customer's data</returns>
        [HttpPut("/customers/{customerId}", Name = nameof(UpdateCustomerRecord))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> UpdateCustomerRecord(string customerId,
            CustomerUpdateCommand command)
        {
            if (customerId != command.Id) return BadRequest();
            var customer = await _Mediator.Send(command);
            return Ok(customer);
        }
        /// <summary>
        /// {Deleting Existing customer's record from the database }
        /// </summary>
        /// <param name="customerId">Requires customer ID to remove the data</param>
        /// <returns>Returns No Content</returns>
        [HttpDelete("/customers/{customerId}", Name = nameof(DeleteCustomerRecord))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCustomerRecord(string customerId)
        {
            var delete = await _Mediator.Send(new DeleteCustomerCommand { CustomerId = customerId });
            return Ok(delete);
        }
    }
}
