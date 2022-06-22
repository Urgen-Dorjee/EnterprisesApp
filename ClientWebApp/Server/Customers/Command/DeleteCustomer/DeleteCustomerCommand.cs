using ClientWebApp.Server.Services.NorthwindContextService;
using MediatR;
using Microsoft.EntityFrameworkCore;
//ReSharper disable all

namespace ClientWebApp.Server.Customers.Command.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public string? CustomerId { get; set; }
    }


    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly NorthwindDbContext _context;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;

        public DeleteCustomerCommandHandler(NorthwindDbContext context, ILogger<DeleteCustomerCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.CustomerId == request.CustomerId)
                .FirstOrDefaultAsync(cancellationToken);
            if (customer == null)
            {
                _logger.LogError("No records available in the database of CustomerID : {0}",request.CustomerId);
                throw new KeyNotFoundException("No records exists");
            }

            var hasOrder = await _context.Orders.AnyAsync(o => o.CustomerId == customer.CustomerId, cancellationToken);
            if (hasOrder)
            {
                _logger.LogError("Records cannot be deleted has there has orders under the CustomerID : {0}", customer.CustomerId);
                throw new ArgumentException("Records cannot be deleted");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
