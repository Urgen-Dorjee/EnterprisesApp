using ClientWebApp.Server.Services.ContextService;
using ClientWebApp.Shared.Models;
using MediatR;

namespace ClientWebApp.Server.Customers.Command
{
    public record CreateCustomerCommand : IRequest
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

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly NorthwindDbContext _context;

        public CreateCustomerCommandHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                CustomerId = request.Id,
                Address = request.Address,
                City = request.City,
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                ContactTitle = request.ContactTitle,
                Country = request.Country,
                Phone = request.Phone,
                PostalCode = request.PostalCode,
                Region = request.Region,
                Fax = request.Fax
            };
            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
