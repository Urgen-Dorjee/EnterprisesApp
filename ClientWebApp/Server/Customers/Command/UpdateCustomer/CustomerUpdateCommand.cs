using ClientWebApp.Server.Services.NorthwindContextService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientWebApp.Server.Customers.Command.UpdateCustomer
{
    public class CustomerUpdateCommand : IRequest
    {
        public string? Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Country { get; set; }
        public string? Fax { get; set; }
        public string? Phone { get; set; }
        public string? PostalCode { get; set; }
        public string? Region { get; set; }
    }

    public class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommand>
    {
        private readonly NorthwindDbContext _context;
        private readonly ILogger<CustomerUpdateCommand> _logger;

        public CustomerUpdateCommandHandler(NorthwindDbContext context, ILogger<CustomerUpdateCommand> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Unit> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.CustomerId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (customer is null)
            {
                _logger.LogError("No such record exits of CustomerID {0}", request.Id);
                throw new FileNotFoundException("No Customers in the database");
            }
            customer.CustomerId = request.Id;
            customer.ContactName = request.ContactName;
            customer.Country = request.Country;
            customer.Phone = request.Phone;
            customer.PostalCode = request.PostalCode;
            customer.Region = request.Region;
            customer.Fax = request.Fax;
            customer.ContactTitle = request.ContactTitle;
            customer.City = request.City;
            customer.Address = request.Address;
            customer.CompanyName = request.CompanyName;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
