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
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(cancellationToken);
            if (customer == null)
            {
                throw new KeyNotFoundException($"No Customer record exists in the database of ID : {request.CustomerId}");
            }

            var hasOrder = await _context.Orders.AnyAsync(o => o.CustomerId == customer.CustomerId, cancellationToken);
            if (hasOrder)
            {
                throw new KeyNotFoundException($"CustomerID : {customer.CustomerId} has {customer.Orders.Count} orders, hence cannot be deleted");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
