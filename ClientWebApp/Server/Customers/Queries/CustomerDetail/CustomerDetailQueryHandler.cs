using ClientWebApp.Server.Services.NorthwindContextService;
using ClientWebApp.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientWebApp.Server.Customers.Queries.CustomerDetail
{
    public class CustomerDetailQueryHandler : IRequestHandler<CustomerDetailQuery, Customer?>
    {
        private readonly NorthwindDbContext _context;

        public CustomerDetailQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }
        public async Task<Customer?> Handle(CustomerDetailQuery request, CancellationToken cancellationToken)
        {
            return  await _context.Customers.Where(c=>c.CustomerId == request.CustomerId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }
    }
}
