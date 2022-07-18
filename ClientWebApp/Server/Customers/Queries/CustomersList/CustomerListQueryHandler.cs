namespace ClientWebApp.Server.Customers.Queries.CustomersList
{
    public class CustomerListQueryHandler : IRequestHandler<CustomerListQuery, List<Customer>>
    {
        private readonly NorthwindDbContext _context;

        public CustomerListQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> Handle(CustomerListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync(cancellationToken);
        }
    }
}
